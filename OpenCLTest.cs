using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicrobenchmarkGui
{
    public static class OpenCLTest
    {
        private static List<OpenCLDevice> openCLDevices;
        private static string[] openCLPlatforms;

        public static string InitializeDeviceControls(GroupBox deviceGroupBox)
        {
            // what can we fit in the GUI
            int maxNameLength = 128;

            // Enumerate platforms and devices
            int platformCount = BenchmarkFunctions.GetPlatformCount();
            openCLPlatforms = new string[platformCount];
            openCLDevices = new List<OpenCLDevice>();

            int totalDeviceCount = 0;
            IntPtr nameBuffer = Marshal.AllocHGlobal(maxNameLength);
            for (int platformIdx = 0; platformIdx < platformCount; platformIdx++)
            {
                int platformDeviceCount = BenchmarkFunctions.GetDeviceCount(platformIdx);
                totalDeviceCount += platformDeviceCount;
                BenchmarkFunctions.GetPlatformName(platformIdx, nameBuffer, maxNameLength);
                openCLPlatforms[platformIdx] = Marshal.PtrToStringAnsi(nameBuffer);
                for (int deviceIdx = 0; deviceIdx < platformDeviceCount; deviceIdx++)
                {
                    OpenCLDevice device = new OpenCLDevice();
                    device.PlatformIndex = platformIdx;
                    device.DeviceIndex = deviceIdx;
                    BenchmarkFunctions.GetDeviceName(platformIdx, deviceIdx, nameBuffer, maxNameLength);
                    device.DeviceName = Marshal.PtrToStringAnsi(nameBuffer);
                    openCLDevices.Add(device);
                }
            }

            Marshal.FreeHGlobal(nameBuffer);

            deviceGroupBox.Text = "OpenCL Device";
            deviceGroupBox.Controls.Clear();
            deviceGroupBox.SuspendLayout();

            // Assemble controls. We populate the list by platforms, sequentially
            int currentPlatform = -1, currentVerticalOffset = 20, controlSpacing = 24;
            Size groupBoxRadioButtonSize = new Size(220, 17);
            foreach (OpenCLDevice clDevice in openCLDevices)
            {
                if (clDevice.PlatformIndex != currentPlatform)
                {
                    currentPlatform = clDevice.PlatformIndex;
                    Label platformLabel = new Label();
                    platformLabel.Text = openCLPlatforms[clDevice.PlatformIndex] + ":";
                    platformLabel.Name = $"platform{clDevice.PlatformIndex}Label";
                    platformLabel.Location = new Point(3, currentVerticalOffset);
                    platformLabel.Size = groupBoxRadioButtonSize;
                    currentVerticalOffset += controlSpacing - 4; // labels seem to be a bit shorter?
                    deviceGroupBox.Controls.Add(platformLabel);
                }

                RadioButton deviceButton = new RadioButton();
                deviceButton.Text = clDevice.DeviceName;
                deviceButton.Name = $"platform{clDevice.PlatformIndex}dev{clDevice.DeviceIndex}RadioButton";
                deviceButton.Location = new Point(7, currentVerticalOffset);
                deviceButton.Size = groupBoxRadioButtonSize;
                currentVerticalOffset += controlSpacing;
                deviceGroupBox.Controls.Add(deviceButton);
                clDevice.DeviceButton = deviceButton;
            }

            deviceGroupBox.ResumeLayout();
            return $"System has {totalDeviceCount} OpenCL devices across {platformCount} platforms";
        }

        public static void RunLatencyTest(MicrobenchmarkForm.SafeSetResultListView setListViewDelegate,
            MicrobenchmarkForm.SafeSetResultListViewColumns setListViewColsDelegate,
            MicrobenchmarkForm.SafeSetResultsChart setChartDelegate,
            MicrobenchmarkForm.SafeSetProgressLabel setLabelDelegate,
            ListView resultListView,
            Chart resultChart,
            Label progressLabel,
            BenchmarkFunctions.CLTestType testMode)
        {
            // figure out which device is checked
            int platformIndex = -1, deviceIndex = -1;
            foreach (OpenCLDevice clDevice in openCLDevices)
            {
                if (clDevice.DeviceButton != null && clDevice.DeviceButton.Checked)
                {
                    platformIndex = clDevice.PlatformIndex;
                    deviceIndex = clDevice.DeviceIndex;
                }
            }

            if (platformIndex == -1 || deviceIndex == -1)
            {
                progressLabel.Invoke(setLabelDelegate, new object[] { "No OpenCL device selected" });
                return;
            }

            int rc = BenchmarkFunctions.SetOpenCLContext(platformIndex, deviceIndex);
            if (rc < 0)
            {
                progressLabel.Invoke(setLabelDelegate, new object[] { "Could not create OpenCL context and command queue for selected device" });
                return;
            }

            ExtractResourceFile("latencykernel.cl");
            rc = BenchmarkFunctions.InitializeLatencyTest(testMode);
            if (rc < 0)
            {
                progressLabel.Invoke(setLabelDelegate, new object[] { "Could not build OpenCL kernel for selected device" });
                return;
            }

            // Run test

            rc = BenchmarkFunctions.DeinitializeLatencyTest();
            if (rc < 0)
            {
                progressLabel.Invoke(setLabelDelegate, new object[] { "Could clean up OpenCL state for selected device" });
                return;
            }
        }

        private static void ExtractResourceFile(string filename)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (string resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.EndsWith(filename))
                {
                    Stream stream = assembly.GetManifestResourceStream(resourceName);
                    if (stream != null)
                    {
                        byte[]  buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        using (FileStream target = new FileStream(filename, FileMode.Create))
                        {
                            target.Write(buffer, 0, buffer.Length);
                            target.Flush();
                        }
                    }
                }
            }
        }

        public class OpenCLDevice
        {
            public int PlatformIndex;
            public int DeviceIndex;
            public string DeviceName;
            public RadioButton DeviceButton;
        }
    }
}
