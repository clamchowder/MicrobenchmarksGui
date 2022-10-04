// From LibreHardwareMonitor
// Mozilla Public License 2.0
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) LibreHardwareMonitor and Contributors
// All Rights Reserved

using System;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MicrobenchmarkGui
{
    internal static class OpCode
    {
        public static CpuidDelegate Cpuid;
        public static RdtscDelegate Rdtsc;

        private static IntPtr _codeBuffer;
        private static ulong _size;

        // void __stdcall cpuidex(unsigned int index, unsigned int ecxValue,
        //   unsigned int* eax, unsigned int* ebx, unsigned int* ecx,
        //   unsigned int* edx)
        // {
        //   int info[4];
        //   __cpuidex(info, index, ecxValue);
        //   *eax = info[0];
        //   *ebx = info[1];
        //   *ecx = info[2];
        //   *edx = info[3];
        // }

        private static readonly byte[] CpuId32 =
        {
            0x55, // push ebp
            0x8B,
            0xEC, // mov ebp, esp
            0x83,
            0xEC,
            0x10, // sub esp, 10h
            0x8B,
            0x45,
            0x08, // mov eax, dword ptr [ebp+8]
            0x8B,
            0x4D,
            0x0C, // mov ecx, dword ptr [ebp+0Ch]
            0x53, // push ebx
            0x0F,
            0xA2, // cpuid
            0x56, // push esi
            0x8D,
            0x75,
            0xF0, // lea esi, [info]
            0x89,
            0x06, // mov dword ptr [esi],eax
            0x8B,
            0x45,
            0x10, // mov eax, dword ptr [eax]
            0x89,
            0x5E,
            0x04, // mov dword ptr [esi+4], ebx
            0x89,
            0x4E,
            0x08, // mov dword ptr [esi+8], ecx
            0x89,
            0x56,
            0x0C, // mov dword ptr [esi+0Ch], edx
            0x8B,
            0x4D,
            0xF0, // mov ecx, dword ptr [info]
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x8B,
            0x45,
            0x14, // mov eax, dword ptr [ebx]
            0x8B,
            0x4D,
            0xF4, // mov ecx, dword ptr [ebp-0Ch]
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x8B,
            0x45,
            0x18, // mov eax, dword ptr [ecx]
            0x8B,
            0x4D,
            0xF8, // mov ecx, dword ptr [ebp-8]
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x8B,
            0x45,
            0x1C, // mov eax, dword ptr [edx]
            0x8B,
            0x4D,
            0xFC, // mov ecx, dword ptr [ebp-4]
            0x5E, // pop esi
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x5B, // pop ebx
            0xC9, // leave
            0xC2,
            0x18,
            0x00 // ret 18h
        };

        private static readonly byte[] CpuId64Linux =
        {
            0x49,
            0x89,
            0xD2, // mov r10, rdx
            0x49,
            0x89,
            0xCB, // mov r11, rcx
            0x53, // push rbx
            0x89,
            0xF8, // mov eax, edi
            0x89,
            0xF1, // mov ecx, esi
            0x0F,
            0xA2, // cpuid
            0x41,
            0x89,
            0x02, // mov dword ptr [r10], eax
            0x41,
            0x89,
            0x1B, // mov dword ptr [r11], ebx
            0x41,
            0x89,
            0x08, // mov dword ptr [r8], ecx
            0x41,
            0x89,
            0x11, // mov dword ptr [r9], edx
            0x5B, // pop rbx
            0xC3 // ret
        };

        private static readonly byte[] CpuId64Windows =
        {
            0x48,
            0x89,
            0x5C,
            0x24,
            0x08, // mov qword ptr [rsp+8], rbx
            0x8B,
            0xC1, // mov eax, ecx
            0x8B,
            0xCA, // mov ecx, edx
            0x0F,
            0xA2, // cpuid
            0x41,
            0x89,
            0x00, // mov dword ptr [r8], eax
            0x48,
            0x8B,
            0x44,
            0x24,
            0x28, // mov rax, qword ptr [rsp+28h]
            0x41,
            0x89,
            0x19, // mov dword ptr [r9], ebx
            0x48,
            0x8B,
            0x5C,
            0x24,
            0x08, // mov rbx, qword ptr [rsp+8]
            0x89,
            0x08, // mov dword ptr [rax], ecx
            0x48,
            0x8B,
            0x44,
            0x24,
            0x30, // mov rax, qword ptr [rsp+30h]
            0x89,
            0x10, // mov dword ptr [rax], edx
            0xC3 // ret
        };

        // unsigned __int64 __stdcall rdtsc() {
        //   return __rdtsc();
        // }

        private static readonly byte[] Rdtsc32 =
        {
            0x0F,
            0x31, // rdtsc
            0xC3 // ret
        };

        private static readonly byte[] Rdtsc64 =
        {
            0x0F,
            0x31, // rdtsc
            0x48,
            0xC1,
            0xE2,
            0x20, // shl rdx, 20h
            0x48,
            0x0B,
            0xC2, // or rax, rdx
            0xC3 // ret
        };

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool CpuidDelegate(uint index, uint ecxValue, out uint eax, out uint ebx, out uint ecx, out uint edx);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ulong RdtscDelegate();

        [Flags]
        internal enum MEM : uint
        {
            MEM_COMMIT = 0x1000,
            MEM_RESERVE = 0x2000,
            MEM_DECOMMIT = 0x4000,
            MEM_RELEASE = 0x8000,
            MEM_RESET = 0x80000,
            MEM_LARGE_PAGES = 0x20000000,
            MEM_PHYSICAL = 0x400000,
            MEM_TOP_DOWN = 0x100000,
            MEM_WRITE_WATCH = 0x200000
        }

        [Flags]
        internal enum PAGE : uint
        {
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, MEM flAllocationType, PAGE flProtect);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        internal static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, MEM dwFreeType);

        public static void Open()
        {
            byte[] rdTscCode;
            byte[] cpuidCode;
            if (IntPtr.Size == 4)
            {
                rdTscCode = Rdtsc32;
                cpuidCode = CpuId32;
            }
            else
            {
                rdTscCode = Rdtsc64;

                cpuidCode = CpuId64Windows;
            }

            _size = (ulong)(rdTscCode.Length + cpuidCode.Length);

            _codeBuffer = VirtualAlloc(IntPtr.Zero,
                                        (UIntPtr)_size,
                                        MEM.MEM_COMMIT | MEM.MEM_RESERVE,
                                        PAGE.PAGE_EXECUTE_READWRITE);

            Marshal.Copy(rdTscCode, 0, _codeBuffer, rdTscCode.Length);
            Rdtsc = Marshal.GetDelegateForFunctionPointer(_codeBuffer, typeof(RdtscDelegate)) as RdtscDelegate;
            IntPtr cpuidAddress = (IntPtr)((long)_codeBuffer + rdTscCode.Length);
            Marshal.Copy(cpuidCode, 0, cpuidAddress, cpuidCode.Length);
            Cpuid = Marshal.GetDelegateForFunctionPointer(cpuidAddress, typeof(CpuidDelegate)) as CpuidDelegate;
        }

        public static void Close()
        {
            Rdtsc = null;
            Cpuid = null;
            VirtualFree(_codeBuffer, UIntPtr.Zero, MEM.MEM_RELEASE);
        }

        /// <summary>
        /// Gets the CPU manufacturer ID string, from cpuid with eax = 0
        /// </summary>
        /// <returns>Manufacturer ID string</returns>
        public static string GetManufacturerId()
        {
            uint eax, ecx, edx, ebx;
            byte[] cpuManufacturerBytes = new byte[12];
            Cpuid(0, 0, out eax, out ebx, out ecx, out edx);

            // when you use a managed language and can't play with types
            cpuManufacturerBytes[0] = (byte)ebx;
            cpuManufacturerBytes[1] = (byte)(ebx >> 8);
            cpuManufacturerBytes[2] = (byte)(ebx >> 16);
            cpuManufacturerBytes[3] = (byte)(ebx >> 24);
            cpuManufacturerBytes[4] = (byte)edx;
            cpuManufacturerBytes[5] = (byte)(edx >> 8);
            cpuManufacturerBytes[6] = (byte)(edx >> 16);
            cpuManufacturerBytes[7] = (byte)(edx >> 24);
            cpuManufacturerBytes[8] = (byte)ecx;
            cpuManufacturerBytes[9] = (byte)(ecx >> 8);
            cpuManufacturerBytes[10] = (byte)(ecx >> 16);
            cpuManufacturerBytes[11] = (byte)(ecx >> 24);
            return System.Text.Encoding.ASCII.GetString(cpuManufacturerBytes);
        }

        public static string GetProcessorName()
        {
            uint[] buffer = new uint[12];
            Cpuid(0x80000002, 0, out buffer[0], out buffer[1], out buffer[2], out buffer[3]);
            Cpuid(0x80000003, 0, out buffer[4], out buffer[5], out buffer[6], out buffer[7]);
            Cpuid(0x80000004, 0, out buffer[8], out buffer[9], out buffer[10], out buffer[11]);

            byte[] dst = new byte[buffer.Length * 4];
            Buffer.BlockCopy(buffer, 0, dst, 0, buffer.Length * 4);
            return System.Text.Encoding.ASCII.GetString(dst);
        }

        public static void GetProcessorVersion(out byte family, out byte model, out byte stepping)
        {
            uint eax, ecx, edx, ebx;
            Cpuid(1, 0, out eax, out ebx, out ecx, out edx);

            stepping = (byte)(eax & 0xF);
            family = (byte)((eax >> 8) & 0xF);
            model = (byte)((eax >> 4) & 0xF);

            // wikipedia says if family id is 6 or 15, model = model + extended model id shifted left by 4 bits
            // extended model id starts on bit 16
            if (family == 6 || family == 15)
            {
                model += (byte)((eax >> 12) & 0xF0);
            }

            // if family is 15, family = family + extended family
            if (family == 15)
            {
                family += (byte)(eax >> 20);
            }
        }
    }
}
