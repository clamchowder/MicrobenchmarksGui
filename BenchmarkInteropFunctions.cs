using System;
using System.Runtime.InteropServices;

namespace MicrobenchmarkGui
{
    public static class BenchmarkInteropFunctions
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
            Avx512NtWrite = 22,
            SseNtRead = 23,
            RepMovsb = 24,
            RepStosb = 25,
            RepMovsd = 26,
            RepStosd = 27
        };

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern float MeasureBw(uint sizeKb, ulong iterations, uint threads, int shared, TestType testType);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckAvxSupport();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckAvx512Support();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetLargePages(uint enable);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern float RunLatencyTest(uint sizeKb, ulong iterations);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern float RunAsmLatencyTest(uint sizeKb, ulong iterations);

        // OpenCL related functions
        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetOpenCLContext(int platformIndex, int deviceIndex);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPlatformCount();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDeviceCount(int platformIndex);

        /// <summary>
        /// Gets an OpenCL device's name
        /// </summary>
        /// <param name="platformIndex">Platform index</param>
        /// <param name="deviceIndex">Device index</param>
        /// <param name="deviceNamePtr">Pointer to block of memory to put the device name into</param>
        /// <param name="maxDeviceNameLen">Max length of device (size of memory block above). Includes terminating null</param>
        /// <returns>0 on success, opencl error code on failure</returns>
        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDeviceName(int platformIndex, int deviceIndex, IntPtr deviceNamePtr, int maxDeviceNameLen);

        /// <summary>
        /// Gets an OpenCL platform's name
        /// </summary>
        /// <param name="platformIndex">Platform index</param>
        /// <param name="platformNamePtr">Pointer to block of memory to put the name into</param>
        /// <param name="maxPlatformNameLen">Max name length, includes terminating null</param>
        /// <returns>0 on success, error code on fail</returns>
        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPlatformName(int platformIndex, IntPtr platformNamePtr, int maxPlatformNameLen);

        // keep in sync with the one in OpenCLFunctions.c
        public enum CLTestType
        {
            None = 0,
            GlobalScalar = 1,
            GlobalVector = 2,
            ConstantScalar = 3,
            Texture = 4,
            Local = 5,
            LinkBw = 6
        };

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern float RunCLLatencyTest(uint sizeKb, uint iterations, CLTestType testType, int tlb);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern float RunCLLinkBwTest(uint sizeKb, uint iterations, int cpuToGpu);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int InitializeLatencyTest(CLTestType testType);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeinitializeLatencyTest();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern ulong GetDeviceMaxConstantBufferSize();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern ulong GetDeviceMaxBufferSize();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern ulong GetDeviceMaxTextureSize();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetGpuPtrChasingStride(uint stride);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetGpuPtrChasingStride();

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetGpuEstimatedPageSize(uint pageSize);

        [DllImport(@"BenchmarkDll.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void GetGpuEstimatedPageSize();
    }
}
