using System.Runtime.InteropServices;

namespace MicrobenchmarkGui
{
    public static class BenchmarkFunctions
    {
        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern float test(int size);

        // must be kept in sync with the one in bandwidth.c
        public enum TestType
        {
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
            Branch16 = 16,
            MmxRead = 17,
            MmxWrite = 18,
            MmxNtWrite = 19,
            SseNtWrite = 20,
            AvxNtWrite = 21,
            Avx512NtWrite = 22
        };

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern float MeasureBw(uint sizeKb, uint iterations, uint threads, int shared, TestType testType);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckAvxSupport();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckAvx512Support();
    }
}
