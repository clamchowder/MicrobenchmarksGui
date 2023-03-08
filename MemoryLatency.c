#include "pch.h"
#include "BenchmarkDllCommon.h"

// If set, memory latency tests will use this as the test array
// If not set, test runs will use malloc()
void* mem = NULL;

// mem latency functions
__declspec(dllexport) float __stdcall RunAsmLatencyTest(uint32_t size_kb, uint64_t iterations);
__declspec(dllexport) float __stdcall RunLatencyTest(uint32_t size_kb, uint64_t iterations);
__declspec(dllexport) int __stdcall SetLargePages(uint32_t enable);

int GetPrivilege();

/// <summary>
/// Sets large pages state. Will allocate array if large pages are enabled
/// </summary>
/// <param name="enable">If greater than 0, enable large pages, with array set to specified size in bytes. If 0, disable large pages and free any allocated arr</param>
/// <returns>0 on success, something else otherwise</returns>
int SetLargePages(uint32_t enable)
{
    if (enable == 0)
    {
        if (mem != NULL)
        {
            VirtualFree(mem, 0, MEM_RELEASE);
            mem = NULL;
        }

        return 0;
    }
    else
    {
        if (mem != NULL)
        {
            VirtualFree(mem, 0, MEM_RELEASE);
            mem = NULL;
        }

        if (GetPrivilege() != 0)
        {
            return -1;
        }

        mem = VirtualAlloc(NULL, enable, MEM_RESERVE | MEM_COMMIT | MEM_LARGE_PAGES, PAGE_READWRITE);
        if (mem == NULL)
        {
            return -2;
        }

        return 0;
    }
}

/// <summary>
/// Fills pattern array with 32-bit integers
/// </summary>
/// <param name="pattern_arr">array to fill</param>
/// <param name="list_size">number of 32-bit elements</param>
/// <param name="byte_increment">how far apart elements should be spaced</param>
void FillPatternArr(uint32_t* pattern_arr, uint32_t list_size, uint32_t byte_increment) {
    uint32_t increment = byte_increment / sizeof(uint32_t);
    uint32_t element_count = list_size / increment;
    for (int i = 0; i < element_count; i++) {
        pattern_arr[i * increment] = i * increment;
    }

    int iter = element_count;
    while (iter > 1) {
        iter -= 1;
        int j = iter - 1 == 0 ? 0 : rand() % (iter - 1);
        uint32_t tmp = pattern_arr[iter * increment];
        pattern_arr[iter * increment] = pattern_arr[j * increment];
        pattern_arr[j * increment] = tmp;
    }
}

/// <summary>
/// Fills pattern array with 64-bit integers
/// </summary>
/// <param name="pattern_arr">array to fill</param>
/// <param name="list_size">number of 64-bit elements in array</param>
/// <param name="byte_increment">how far apart elements should be spaced</param>
void FillPatternArr64(uint64_t* pattern_arr, uint64_t list_size, uint64_t byte_increment) {
    uint32_t increment = byte_increment / sizeof(uint64_t);
    uint32_t element_count = list_size / increment;
    for (int i = 0; i < element_count; i++) {
        pattern_arr[i * increment] = i * increment;
    }

    int iter = element_count;
    while (iter > 1) {
        iter -= 1;
        int j = iter - 1 == 0 ? 0 : rand() % (iter - 1);
        uint64_t tmp = pattern_arr[iter * increment];
        pattern_arr[iter * increment] = pattern_arr[j * increment];
        pattern_arr[j * increment] = tmp;
    }
}

float RunAsmLatencyTest(uint32_t size_kb, uint64_t iterations) {
    struct timeb start, end;
    uint32_t list_size = size_kb * 1024 / sizeof(void*);

    uint64_t* A;
    if (mem == NULL) {
        A = (uint64_t*)malloc(size_kb * 1024);
    }
    else {
        A = (uint64_t*)mem;
    }

    memset(A, 0, 1024 * size_kb);
    FillPatternArr64(A, size_kb * 1024 / sizeof(uint64_t), 64);
    preplatencyarr(A, size_kb * 1024 / sizeof(uint64_t));

    ftime(&start);
    uint64_t sum = latencytest(iterations, A);
    ftime(&end);
    int64_t time_diff_ms = 1000 * (end.time - start.time) + (end.millitm - start.millitm);
    float latency = 1e6 * (float)time_diff_ms / (float)iterations;
    if (mem == NULL) free(A);
    return latency;
}

float RunLatencyTest(uint32_t size_kb, uint64_t iterations) {
    struct timeb start, end;
    uint32_t list_size = size_kb * 1024 / 4;
    uint32_t current;

    // Fill list to create random access pattern
    int* A;
    if (mem == NULL) {
        A = (int*)malloc(sizeof(int) * list_size);
    }
    else {
        A = (int*)mem;
    }

    for (int i = 0; i < list_size; i++) {
        A[i] = i;
    }

    FillPatternArr(A, list_size, 64);

    // Run test
    ftime(&start);
    current = A[0];
    for (int i = 0; i < iterations; i++) {
        current = A[current];
    }
    ftime(&end);
    int64_t time_diff_ms = 1000 * (end.time - start.time) + (end.millitm - start.millitm);
    float latency = 1e6 * (float)time_diff_ms / (float)iterations;
    if (mem == NULL) free(A);
    if (current == A[current]) return 0;
    return latency;
}

int GetPrivilege()
{
    HANDLE           hToken;
    TOKEN_PRIVILEGES tp;
    BOOL             status;
    DWORD            error;

    // open process token
    if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken))
    {
        return -1;
    }

    // get the luid
    if (!LookupPrivilegeValue(NULL, TEXT("SeLockMemoryPrivilege"), &tp.Privileges[0].Luid))
    {
        return -1;
    }

    // enable privilege
    tp.PrivilegeCount = 1;
    tp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
    status = AdjustTokenPrivileges(hToken, FALSE, &tp, 0, (PTOKEN_PRIVILEGES)NULL, 0);

    // It is possible for AdjustTokenPrivileges to return TRUE and still not succeed.
    // So always check for the last error value.
    error = GetLastError();
    if (!status || (error != ERROR_SUCCESS))
    {
        return -1;
    }

    // close the handle
    if (!CloseHandle(hToken))
    {
        return -1;
    }

    return 0;
}
