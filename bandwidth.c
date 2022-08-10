#include "pch.h"
#include "BenchmarkDllCommon.h"

extern float sse_asm_read(void* arr, uint64_t arr_length, uint64_t iterations);
extern float sse_asm_write(void* arr, uint64_t arr_length, uint64_t iterations);
extern float sse_asm_copy(void* arr, uint64_t arr_length, uint64_t iterations);
extern float sse_asm_add(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx_asm_read(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx_asm_write(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx_asm_copy(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx_asm_cflip(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx_asm_add(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx512_asm_read(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx512_asm_write(void* arr, uint64_t arr_length, uint64_t iterations);
extern float avx512_asm_add(void* arr, uint64_t arr_length, uint64_t iterations);
float (*bw_func)(void*, uint64_t, uint64_t) = sse_asm_read;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

// Does thing work?
__declspec(dllexport) float __stdcall test(int size);
float __stdcall test(int size)
{
    return (float)size + 0.1f;
}

enum TestType { 
    None = 0,
    SseRead = 1,
    SseWrite = 2,
    SseCopy = 3,
    SseAdd = 4,
    AvxRead = 5,
    AvxWrite = 6,
    AvxCopy = 7,
    AvxCflip = 8,
    AvxAdd = 9,
    Avx512Read = 10,
    Avx512Write = 11,
    Avx512Add = 12,
    Instr4 = 13, 
    Instr8 = 14, 
    K8Instr4 = 15, 
    Branch16 = 16 
};

typedef struct BandwidthTestThreadData {
    uint32_t iterations;
    uint32_t arr_length;
    float* arr;
    float bw; // written to by the thread
} BandwidthTestThreadData;

DWORD WINAPI ReadBandwidthTestThread(LPVOID param) {
    BandwidthTestThreadData* bwTestData = (BandwidthTestThreadData*)param;
    float sum = bw_func(bwTestData->arr, bwTestData->arr_length, bwTestData->iterations);
    if (sum == 0) return 1;
    return 0;
}

void FillInstructionArray(uint64_t* arr, uint64_t sizeKb, enum TestType nopSize)
{
    char nop8b[8] = { 0x0F, 0x1F, 0x84, 0x00, 0x00, 0x00, 0x00, 0x00 };

    // zen/piledriver optimization manual uses this pattern
    char nop4b[8] = { 0x0F, 0x1F, 0x40, 0x00, 0x0F, 0x1F, 0x40, 0x00 };

    // athlon64 (K8) optimization manual pattern
    char k8_nop4b[8] = { 0x66, 0x66, 0x66, 0x90, 0x66, 0x66, 0x66, 0x90 };

    uint64_t elements = (sizeKb * 1024 / 8) - 1; // leave room for ret
    unsigned char* functionEnd = (unsigned char*)(arr + elements);

    if (nopSize != Branch16) {
        uint64_t* nopPtr;
        if (nopSize == Instr8) nopPtr = (uint64_t*)(nop8b);
        else if (nopSize == Instr4) nopPtr = (uint64_t*)(nop4b);
        else if (nopSize == K8Instr4) nopPtr = (uint64_t*)(k8_nop4b);
        else {
            return;
        }

        for (uint64_t nopIdx = 0; nopIdx < elements; nopIdx++) {
            arr[nopIdx] = *nopPtr;
        }

        functionEnd[0] = 0xC3;
    }
    else {
        // jump forward 14 bytes
        char branch16b[8] = { 0xEB, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        char ret8b[8] = { 0xC3, 0, 0, 0, 0, 0, 0, 0 };
        uint64_t* branchPtr = (uint64_t*)(branch16b);
        uint64_t* nopPtr = (uint64_t*)(nop8b); // doesn't really matter, we should never hit this

        // last iteration must have nopIdx % 2 == 1, so the jump will go to the return statement
        // i.e. branchElements for loop must be even, so the last iteration is odd
        uint64_t branchElements = elements % 2 == 0 ? elements : elements - 1;
        uint64_t nopIdx;
        for (nopIdx = 0; nopIdx < branchElements; nopIdx++) {
            arr[nopIdx] = nopIdx % 2 == 0 ? *branchPtr : *nopPtr;
        }

        arr[nopIdx] = *(uint64_t*)ret8b;
    }
}
__declspec(dllexport) float __stdcall MeasureBw(uint32_t sizeKb, uint32_t iterations, uint32_t threads, int shared, enum TestType mode);

float __stdcall MeasureBw(uint32_t sizeKb, uint32_t iterations, uint32_t threads, int shared, enum TestType mode) {
    struct timeb start, end;
    float bw = 0;
    uint32_t elements = sizeKb * 1024 / sizeof(float);
    uint32_t private_elements = (uint32_t)ceil((double)sizeKb / (double)threads) * 256;
    DWORD protection_flags = PAGE_EXECUTE_READWRITE;

    //if (instr != None) protection_flags = PAGE_EXECUTE_READWRITE;
    if (!shared) elements = private_elements;

    //fprintf(stderr, "%llu elements per thread\n", elements);

    if (!shared && sizeKb < threads) {
        //fprintf(stderr, "Too many threads for this size, continuing\n");
        return 0;
    }

    if (mode == None)
    {
        // need to auto detect later
        bw_func = sse_asm_read; // guaranteed to work
    }
    else if (mode == AvxRead) { bw_func = avx_asm_read; }
    else if (mode == AvxWrite) { bw_func = avx_asm_write; }
    else if (mode == AvxAdd) { bw_func = avx_asm_add; }
    else if (mode == AvxCflip) { bw_func = avx_asm_cflip; }
    else if (mode == AvxCopy) { bw_func = avx_asm_copy; }
    else if (mode == SseRead) { bw_func = sse_asm_read; }
    else if (mode == SseWrite) { bw_func = sse_asm_write; }
    else if (mode == SseAdd) { bw_func = sse_asm_add; }
    else if (mode == SseCopy) { bw_func = sse_asm_copy; }
    else if (mode == Avx512Read) { bw_func = avx512_asm_read; }
    else if (mode == Avx512Write) { bw_func = avx512_asm_write; }
    else if (mode == Avx512Add) { bw_func = avx512_asm_add; }
    else if (mode != Instr4 && mode != Instr8 && mode != K8Instr4 && mode != Branch16)
    {
        return 3;
    }

    // make array and fill it with something
    float* testArr = NULL;
    if (shared) {
        testArr = (float*)VirtualAlloc(NULL, elements * sizeof(float), MEM_COMMIT | MEM_RESERVE, protection_flags);
        if (testArr == NULL) {
            return 15;
        }

        if (mode != None)
        {
            FillInstructionArray((uint64_t*)testArr, sizeKb, mode);
        }
        else {
            for (uint32_t i = 0; i < elements; i++) {
                testArr[i] = i + 0.5f;
            }
        }
    }

    HANDLE* testThreads = (HANDLE*)malloc(threads * sizeof(HANDLE));
    DWORD* tids = (DWORD*)malloc(threads * sizeof(DWORD));
    struct BandwidthTestThreadData* threadData = (struct BandwidthTestThreadData*)malloc(threads * sizeof(struct BandwidthTestThreadData));

    for (uint64_t i = 0; i < threads; i++) {
        if (shared) {
            threadData[i].arr = testArr;
            threadData[i].iterations = iterations;
        }
        else {
            threadData[i].arr = (float*)VirtualAlloc(NULL, elements * sizeof(float), MEM_COMMIT | MEM_RESERVE, protection_flags);
            if (threadData[i].arr == NULL) {
                return 0;
            }

            if (mode != None)
            {
                FillInstructionArray((uint64_t*)threadData[i].arr, (elements * 4) / 1024, mode);
            }
            else
            {
                for (uint64_t arr_idx = 0; arr_idx < elements; arr_idx++) {
                    threadData[i].arr[arr_idx] = arr_idx + i + 0.5f;
                }
            }

            threadData[i].iterations = iterations * threads;
        }

        threadData[i].arr_length = elements;
        threadData[i].bw = 0;
        testThreads[i] = CreateThread(NULL, 0, ReadBandwidthTestThread, threadData + i, CREATE_SUSPENDED, tids + i);

        // turns out setting affinity makes no difference, and it's easier to set affinity via start /affinity <mask> anyway
        //SetThreadAffinityMask(testThreads[i], 1UL << i);
    }

    ftime(&start);
    for (uint32_t i = 0; i < threads; i++) ResumeThread(testThreads[i]);
    WaitForMultipleObjects((DWORD)threads, testThreads, TRUE, INFINITE);
    ftime(&end);

    int64_t time_diff_ms = 1000 * (end.time - start.time) + (end.millitm - start.millitm);
    double gbTransferred = (uint64_t)iterations * sizeof(float) * elements * threads / (double)1e9;
    bw = (float)(1000 * gbTransferred / (double)time_diff_ms);
    if (!shared) bw = bw * threads;

    free(testThreads);
    if (shared) VirtualFree(testArr, elements * sizeof(float), MEM_RELEASE);
    free(tids);

    if (!shared) {
        for (uint32_t i = 0; i < threads; i++) {
            VirtualFreeEx(GetCurrentProcess(), threadData[i].arr, 0, MEM_RELEASE);
        }
    }

    free(threadData);
    return bw;
}