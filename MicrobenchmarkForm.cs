using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicrobenchmarkGui
{
    public partial class MicrobenchmarkForm : Form
    {
        private RadioButton FourByteNops;
        private RadioButton EightByteNops;
        private RadioButton BranchPer16B;
        private RadioButton K8FourByteNops;
        private RadioButton RepMovsbRadioButton;
        private RadioButton RepStosbRadioButton;
        private RadioButton RepMovsdRadioButton;
        private RadioButton RepStosdRadioButton;

        private Random randomThings;

        private bool avxSupported;
        private bool avx512Supported;

        public MicrobenchmarkForm()
        {
            plottedSeries = new Dictionary<ResultChartType, Dictionary<string, Series>>();
            InitializeComponent();
            ResultsChart.Series.Clear();

            #region manual control initialization
            ToolTip tooltips = new ToolTip();
            Size groupBoxRadioButtonSize = new Size(220, 17);
            FourByteNops = new RadioButton();
            FourByteNops.Text = "4B NOPs (0F 1F 40 00)";
            FourByteNops.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(FourByteNops, "Corresponds to average instruction lengths found in typical integer code");

            EightByteNops = new RadioButton();
            EightByteNops.Text = "8B NOPs (0F 1F 84 00 00 00 00 00)";
            EightByteNops.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(EightByteNops, "Relatively long 8-byte instructions. More representative of AVX/AVX2/AVX-512 code, or code with lots of large immediates");

            BranchPer16B = new RadioButton();
            BranchPer16B.Text = "Taken Branch Per 16B";
            BranchPer16B.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(BranchPer16B, "Test with a chain of taken branches, spaced 16B apart. More of a BTB/branch predictor speed test.");

            K8FourByteNops = new RadioButton();
            K8FourByteNops.Text = "4B NOPs (66 66 66 90)";
            K8FourByteNops.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(K8FourByteNops, "Use a NOP encoding recommended by the AMD Athlon/K8 optimization manual. Curiously avoids decoding bottlenecks on certain Intel CPUs, but doesn't matter for AMD (?)");

            RepMovsbRadioButton = new RadioButton();
            RepMovsbRadioButton.Text = "REP MOVSB (Copy)";
            RepMovsbRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepMovsbRadioButton, "Tells CPU to copy a block of memory, with size known upfront");

            RepStosbRadioButton = new RadioButton();
            RepStosbRadioButton.Text = "REP STOSB (Write)";
            RepStosbRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepStosbRadioButton, "Tells CPU to write to a block of memory, with size known upfront");

            RepMovsdRadioButton = new RadioButton();
            RepMovsdRadioButton.Text = "REP MOVSD (Copy)";
            RepMovsdRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepMovsdRadioButton, "Tells CPU to copy a block of memory with DWORD (4 byte) granularity. Potentially faster than REP MOVSB on some CPUs");

            RepStosdRadioButton = new RadioButton();
            RepStosdRadioButton.Text = "REP STOSD (Write)";
            RepStosdRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepStosdRadioButton, "Tells CPU to write to a block of memory with DWORD (4 byte) granularity. Potentially faster than REP STOSB on some CPUs");

            tooltips.SetToolTip(MemoryLatencyAsmRadioButton, "Uses direct addressing, i.e. mov r15, [r15]. Should be the lowest latency way to hit the memory hierarchy");
            tooltips.SetToolTip(MemoryLatencyIndexedAddressingRadioButton, "Tests a = A[a] latency. The CPU has to add the offset to the array base address, which can incur additional latency");
            tooltips.SetToolTip(MemoryLatencyDefaultPagesRadioButton, "Should be representative of memory latency seen by most user applications");
            tooltips.SetToolTip(MemoryLatencyLargePagesRadioButton, "Asks the OS to handle address translation at a larger granularity, giving a better view of raw cache latency with TLB miss penalties minimized. Requires 'Lock Pages in Memory' permission");

            tooltips.SetToolTip(GpuMemoryLatencyScalarRadioButton, "Similar to CPU latency test. Simple pointer chasing pattern in GPU global memory. AMD GPUs often have scalar caches.");
            tooltips.SetToolTip(GpuMemoryLatencyVectorRadioButton, "Uses two threads, set up so that the compiler can't determine if a loaded value will be constant across a wave/warp. AMD has separate vector and scalar caches.");
            tooltips.SetToolTip(GpuMemoryLatencyConstantScalarRadioButton, "Uses constant, read-only memory. Nvidia GPUs have separate constant caches."); ;

            tooltips.SetToolTip(GpuMemoryLatencyTextureRadioButton, "Test latency through the texture pipeline (TMUs), using a 1D image buffer. Some Nvidia GPUs have separate texture caches.");
            tooltips.SetToolTip(GpuMemoryLatencyLocalRadioButton, "Tests local memory (scratchpad) latency. Nvidia calls this Shared Memory, AMD calls it the LDS, and Intel calls it the SLM");

            tooltips.SetToolTip(ExportExcelButton, "Get results in an Excel (or custom JS) parse-able format");
            tooltips.SetToolTip(ClearChartButton, "Embarassingly bad results? Click here");
            tooltips.SetToolTip(DataReadRadioButton, "Tests memory read bandwidth. Most memory accesses are reads");
            tooltips.SetToolTip(DataWriteRadioButton, "Tests memory write bandwidth. Usually 1/4-1/2 of memory accesses are writes, though that can vary a lot");
            tooltips.SetToolTip(DataNtWriteRadioButton, "Tests memory write bandwidth with non-temporal accesses, which bypass caches. Potentially allows better use of DRAM write bandwidth by avoiding RFOs");
            tooltips.SetToolTip(DataMicrocodedRadioButton, "Tests memory bandwidth using microcoded string instructions, which tell the CPU upfront how much data it has to move. Potentially allows RFO avoidance while using caches.");
            tooltips.SetToolTip(InstructionFetchRadioButton, "Fills an array with valid instructions and jumps to it, to test how fast the CPU can bring instructions into the core");
            tooltips.SetToolTip(DataAddRadioButton, "Tests bandwidth using a 1:1 read-to-write ratio, by adding a constant to every element of an array. Can show if there's an advantage to mixing reads and writes");
            tooltips.SetToolTip(PrivateRadioButton, "Gives each thread its own data. Shows the sum of private cache capacity");
            tooltips.SetToolTip(SharedRadioButton, "All threads read from one shared array. Shared data is duplicated across private caches, so you won't see the sum of cache capacity");

            tooltips.SetToolTip(AvxRadioButton, "Uses 256-bit accesses with YMM registers");
            tooltips.SetToolTip(Avx512RadioButton, "Uses 512-bit accesses with ZMM registers");
            tooltips.SetToolTip(SseRadioButton, "Uses 128-bit accesses with XMM registers");
            tooltips.SetToolTip(MmxRadioButton, "Uses 64-bit accesess with MM registers");

            string gpuPtrStrideTooltip = "Sets the pointer chasing stride. If matched to cacheline size, this could produce cleaner results. If set higher than cacheline cacheline size, the test will overestimate cache capacity";
            tooltips.SetToolTip(GpuPointerChasingStrideLabel, gpuPtrStrideTooltip);
            tooltips.SetToolTip(GpuPointerChasingStrideTextBox, gpuPtrStrideTooltip);

            string gpuPageSizeEstimateTooltip = "Sets the pointer chasing stride for TLB measurements.";
            tooltips.SetToolTip(GpuEstimatedPageSizeLabel, gpuPageSizeEstimateTooltip);
            tooltips.SetToolTip(GpuEstimatedPageSizeTextBox, gpuPageSizeEstimateTooltip);
            #endregion

            ThreadCountTrackbar.Maximum = Environment.ProcessorCount;
            ResultsChart.Titles.Add("Result Plot");

            if (BenchmarkInteropFunctions.CheckAvxSupport() != 1)
            {
                avxSupported = false;
                tooltips.SetToolTip(AvxRadioButton, "Your CPU does not support AVX");
            }
            else
            {
                avxSupported = true;
            }

            if (BenchmarkInteropFunctions.CheckAvx512Support() != 1)
            {
                avx512Supported = false;
                tooltips.SetToolTip(AvxRadioButton, "Your CPU does not support AVX-512");
            }
            else
            {
                avx512Supported = true;
            }

            randomThings = new Random();
            SetDefaultMethodState();

            OpCode.Open();
            this.Text = "Clam Cache/Mem Benchmark: " + OpCode.GetProcessorName();

            bwRunner = new BandwidthRunner(SetResultListView,
                SetResultListViewColumns, SetResultChart, SetProgressLabel, resultListView, ResultsChart, progressLabel);
            latencyRunner = new LatencyRunner(SetResultListView,
                SetResultListViewColumns, SetResultChart, SetProgressLabel, resultListView, ResultsChart, progressLabel);
        }

        public void SetDefaultMethodState()
        {
            SseRadioButton.Checked = false;
            AvxRadioButton.Checked = false;
            Avx512RadioButton.Checked = false;

            if (!avx512Supported) Avx512RadioButton.Enabled = false;
            if (!avxSupported) AvxRadioButton.Enabled = false;

            if (avx512Supported)
            {
                Avx512RadioButton.Checked = true;
            }
            else if (avxSupported)
            {
                AvxRadioButton.Checked = true;
            }
            else
            {
                SseRadioButton.Checked = true;
            }
        }

        public delegate void SafeSetResultListViewColumns(string[] cols);
        public delegate void SafeSetResultListView(string[][] items);
        public delegate void SafeSetResultsChart(string seriesNames, float[] testPoints, float[] testResults, ResultChartType chartType);
        public delegate void SafeSetCancelButtonState(bool enabled);
        public delegate void SafeSetProgressLabel(string message);

        public void SetProgressLabel(string message) { progressLabel.Text = message; }

        public void SetCancelButtonState(bool enabled) { CancelRunButton.Enabled = enabled; }

        /// <summary>
        /// Sets result list view items
        /// </summary>
        /// <param name="cols">Columns</param>
        public void SetResultListViewColumns(string[] cols)
        {
            resultListView.Columns.Clear();
            resultListView.Items.Clear();
            foreach (string col in cols)
            {
                resultListView.Columns.Add(col);
            }

            int i = 0;
            foreach (ColumnHeader column in resultListView.Columns)
            {
                if (i == 0) column.Width = 70;
                if (i == 1) column.Width = 100;
                column.TextAlign = HorizontalAlignment.Right;
                i++;
            }
        }

        /// <summary>
        /// Set result list view items
        /// </summary>
        /// <param name="items">items</param>
        public void SetResultListView(string[][] items)
        {
            resultListView.Items.Clear();
            for (int unitIdx = 0; unitIdx < items.Length; unitIdx++)
            {
                resultListView.Items.Add(new ListViewItem(items[unitIdx]));
            }
        }

        private Dictionary<ResultChartType, Dictionary<string, Series>> plottedSeries;

        // Make sure we don't reduce the max when a new test is run
        private double chartMax = 0, chartMin = 0;

        // Somehow can't retrieve radio button value?
        private bool specifyColor = false;

        public enum ResultChartType
        {
            CpuMemoryBandwidth,
            CpuMemoryLatency,
            GpuMemoryLatency,
            GpuLinkBandwidth
        }

        public void SetResultChart(string seriesName, float[] testPoints, float[] testResults, ResultChartType chartType)
        {
            Series series;
            Dictionary<string, Series> chartTypeSeries;
            if (!plottedSeries.TryGetValue(chartType, out chartTypeSeries))
            {
                chartTypeSeries = new Dictionary<string, Series>();
                plottedSeries.Add(chartType, chartTypeSeries);
            }

            if (!chartTypeSeries.TryGetValue(seriesName, out series))
            {
                series = new Series(seriesName);
                series.ChartType = SeriesChartType.Line;
                if (RandomizeNextColorRadioButton.Checked)
                {
                    byte[] randomBytes = new byte[3];
                    randomThings.NextBytes(randomBytes);
                    series.Color = Color.FromArgb(randomBytes[0], randomBytes[1], randomBytes[2]);
                }

                if (specifyColor)
                {
                    int red, green, blue;
                    bool parseSucceeded = true;
                    parseSucceeded &= int.TryParse(ColorRBox.Text, out red);
                    parseSucceeded &= int.TryParse(ColorGBox.Text, out green);
                    parseSucceeded &= int.TryParse(ColorBBox.Text, out blue);
                    parseSucceeded &= (red <= 255 && red >= 0);
                    parseSucceeded &= (green <= 255 && green >= 0);
                    parseSucceeded &= (blue <= 255 && blue >= 0);
                    if (!parseSucceeded)
                    {
                        SetProgressLabel("Red/Green/Blue values must be numbers between 0-255");
                    }
                    else
                    {
                        series.Color = Color.FromArgb(red, green, blue);
                    }
                }

                chartTypeSeries.Add(seriesName, series);
                ResultsChart.ChartAreas[0].AxisX.IsLogarithmic = true;
                ResultsChart.ChartAreas[0].AxisX.LogarithmBase = 2;
                ResultsChart.ChartAreas[0].AxisX.LabelStyle.Format = "#";
                ResultsChart.ChartAreas[0].AxisX.Title = "Data (KB)";
                ResultsChart.Series.Add(series);
                SetChartYAxis(chartType);
            }

            series.Points.Clear();
            double min = testPoints[0], max = testPoints[0];
            // we expect this to be sorted but just in case
            for (uint i = 0; i < testPoints.Length && i < testResults.Length; i++)
            {
                if (testPoints[i] > max) max = testPoints[i];
                if (testPoints[i] < min) min = testPoints[i];
            }

            if (chartMin == 0 || min < chartMin)
            {
                ResultsChart.ChartAreas[0].AxisX.Minimum = min;
                chartMin = min;
            }

            if (max > chartMax)
            {
                ResultsChart.ChartAreas[0].AxisX.Maximum = max;
                chartMax = max;
            }

            for (uint i = 0; i < testPoints.Length && i < testResults.Length; i++)
            {
                series.Points.AddXY((double)testPoints[i], (double)testResults[i]);
            }

            foreach (Legend a in ResultsChart.Legends)
            {
                a.Docking = Docking.Bottom;
            }

            return;
        }

        private void SetChartType(ResultChartType chartType)
        {
            ResultsChart.ChartAreas[0].AxisX.IsLogarithmic = false;
            ResultsChart.Series.Clear();
            Dictionary<string, Series> chartSeries;
            if (!plottedSeries.TryGetValue(chartType, out chartSeries)) return;
            foreach (KeyValuePair<string, Series> a in chartSeries)
            {
                ResultsChart.Series.Add(a.Value);
            }

            if (chartSeries.Count() > 0)
            {
                SetChartYAxis(chartType);
                ResultsChart.ChartAreas[0].AxisX.IsLogarithmic = true;
            }
        }

        /// <summary>
        /// Set up chart's Y axis. Can't be called if nothing has been plotted yet
        /// because setting a log axis can error out
        /// </summary>
        /// <param name="chartType">Chart type</param>
        private void SetChartYAxis(ResultChartType chartType)
        {
            if (chartType == ResultChartType.CpuMemoryLatency)
            {
                ResultsChart.ChartAreas[0].AxisY.IsLogarithmic = true;
                ResultsChart.ChartAreas[0].AxisY.LogarithmBase = 2;
                ResultsChart.ChartAreas[0].AxisY.Title = "Latency (ns)";
            }
            else if (chartType == ResultChartType.CpuMemoryBandwidth)
            {
                ResultsChart.ChartAreas[0].AxisY.IsLogarithmic = false;
                ResultsChart.ChartAreas[0].AxisY.Title = "Bandwidth (GB/s)";
            }
            else if (chartType == ResultChartType.GpuMemoryLatency)
            {
                ResultsChart.ChartAreas[0].AxisY.IsLogarithmic = false;
                ResultsChart.ChartAreas[0].AxisY.Title = "Latency (ns)";
            }
            else if (chartType == ResultChartType.GpuLinkBandwidth)
            {
                ResultsChart.ChartAreas[0].AxisY.IsLogarithmic = false;
                ResultsChart.ChartAreas[0].AxisY.Title = "Bandwidth (GB/s)";
            }
        }

        private BandwidthRunner bwRunner;
        private LatencyRunner latencyRunner;
        private CancellationTokenSource runCancel;
        private Task testTask;

        /// <summary>
        /// Kicks off bandwidth test, automated run through all sizes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunBandwidthTestButton_Click(object sender, EventArgs e)
        {
            // Set common settings
            GlobalTestSettings.MinTestSizeKb = 0;
            if (!string.IsNullOrEmpty(MinTestSizeTextBox.Text))
            {
                uint minTestSizeKb;
                if (uint.TryParse(MinTestSizeTextBox.Text, out minTestSizeKb))
                {
                    StartSizeLabel.ForeColor = Color.Blue;
                    GlobalTestSettings.MinTestSizeKb = minTestSizeKb;
                } 
                else
                {
                    StartSizeLabel.ForeColor = Color.Red;
                }
            }
            else
            {
                StartSizeLabel.ForeColor = Color.Black;
            }

            // Launch selected test
            if (TestSelectTabControl.SelectedTab == MemoryBandwidthTab)
            {
                RunBandwidthTest();
            }
            else if (TestSelectTabControl.SelectedTab == MemoryLatencyTab)
            {
                RunLatencyTest();
            }
            else if (TestSelectTabControl.SelectedTab == GpuMemLatencyTab)
            {
                RunClLatencyTest();
            }
            else if (TestSelectTabControl.SelectedTab == GpuLinkBandwidthTab)
            {
                RunCLLinkTest();
            }
        }

        private void RunCLLinkTest()
        {
            CancelRunningTest(true);
            runCancel = new CancellationTokenSource();
            bool cpuToGpu = GpuLinkCpuToGpuRadioButton.Checked;
            testTask = Task.Run(() => OpenCLTest.RunLinkBandwidthTest(SetResultListView,
                    SetResultListViewColumns,
                    SetResultChart,
                    SetProgressLabel,
                    resultListView,
                    ResultsChart,
                    progressLabel,
                    cpuToGpu,
                    runCancel.Token));

            CancelRunButton.Enabled = true;
            Task.Run(() => HandleTestRunCompletion(testTask, SetCancelButtonState));
        }

        private void RunClLatencyTest()
        {
            CancelRunningTest(true);
            runCancel = new CancellationTokenSource();
            BenchmarkInteropFunctions.CLTestType clLatencyTestMode = BenchmarkInteropFunctions.CLTestType.GlobalScalar;
            if (GpuMemoryLatencyScalarRadioButton.Checked) clLatencyTestMode = BenchmarkInteropFunctions.CLTestType.GlobalScalar;
            else if (GpuMemoryLatencyVectorRadioButton.Checked) clLatencyTestMode = BenchmarkInteropFunctions.CLTestType.GlobalVector;
            else if (GpuMemoryLatencyConstantScalarRadioButton.Checked) clLatencyTestMode = BenchmarkInteropFunctions.CLTestType.ConstantScalar;
            else if (GpuMemoryLatencyTextureRadioButton.Checked) clLatencyTestMode = BenchmarkInteropFunctions.CLTestType.Texture;
            else if (GpuMemoryLatencyLocalRadioButton.Checked) clLatencyTestMode = BenchmarkInteropFunctions.CLTestType.Local;

            // Handle stride option
            uint gpuPointerChasingStride;
            if (!uint.TryParse(GpuPointerChasingStrideTextBox.Text, out gpuPointerChasingStride))
            {
                gpuPointerChasingStride = OpenCLTest.DefaultGpuPointerChasingStride;
                GpuPointerChasingStrideLabel.ForeColor = Color.Red;
                progressLabel.Text = "Could not parse pointer chasing stride";
                return;
            }

            if (gpuPointerChasingStride != OpenCLTest.DefaultGpuPointerChasingStride)
            {
                GpuPointerChasingStrideLabel.ForeColor = Color.Blue;
            }
            else
            {
                GpuPointerChasingStrideLabel.ForeColor = Color.Black;
            }

            // Handle TLB option
            bool testTlb = false;
            uint gpuEstimatedPageSizeBytes = 0;
            if (GpuTlbTestCheckbox.Checked)
            {
                if (!uint.TryParse(GpuEstimatedPageSizeTextBox.Text, out gpuEstimatedPageSizeBytes))
                {
                    GpuEstimatedPageSizeLabel.ForeColor = Color.Red;
                    progressLabel.Text = "Could not parse estimated page size (must be an integer)";
                    return;
                }

                GpuEstimatedPageSizeLabel.ForeColor = Color.Blue;
                testTlb = true;
                gpuEstimatedPageSizeBytes *= 1024;
            }

            if (!testTlb)
            {
                testTask = Task.Run(() => OpenCLTest.RunLatencyTest(SetResultListView,
                    SetResultListViewColumns,
                    SetResultChart,
                    SetProgressLabel,
                    resultListView,
                    ResultsChart,
                    progressLabel,
                    clLatencyTestMode,
                    gpuPointerChasingStride,
                    runCancel.Token));
            }
            else
            {
                testTask = Task.Run(() => OpenCLTest.RunTlbTest(SetResultListView,
                    SetResultListViewColumns,
                    SetResultChart,
                    SetProgressLabel,
                    resultListView,
                    ResultsChart,
                    progressLabel,
                    clLatencyTestMode,
                    gpuPointerChasingStride,
                    gpuEstimatedPageSizeBytes,
                    runCancel.Token));
            }
            CancelRunButton.Enabled = true;
            Task.Run(() => HandleTestRunCompletion(testTask, SetCancelButtonState));
        }

        private void RunLatencyTest()
        {
            bool largePages = MemoryLatencyLargePagesRadioButton.Checked;
            bool asm = MemoryLatencyAsmRadioButton.Checked;

            CancelRunningTest(true);
            runCancel = new CancellationTokenSource();
            testTask = Task.Run(() => latencyRunner.StartFullTest(asm, largePages, runCancel.Token));
            CancelRunButton.Enabled = true;
            Task.Run(() => HandleTestRunCompletion(testTask, SetCancelButtonState));
        }

        private void RunBandwidthTest()
        {
            // Read test parameters from interface
            uint threadCount = (uint)ThreadCountTrackbar.Value;
            bool sharedMode = SharedRadioButton.Checked;

            BenchmarkInteropFunctions.TestType testType = BenchmarkInteropFunctions.TestType.AvxRead;
            if (DataReadRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.SseRead;
                else if (AvxRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.AvxRead;
                else if (Avx512RadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.Avx512Read;
                else if (MmxRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.MmxRead;
            }
            else if (DataWriteRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.SseWrite;
                else if (AvxRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.AvxWrite;
                else if (Avx512RadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.Avx512Write;
                else if (MmxRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.MmxWrite;
            }
            else if (DataNtWriteRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.SseNtWrite;
                else if (AvxRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.AvxNtWrite;
                else if (Avx512RadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.Avx512NtWrite;
                else if (MmxRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.MmxNtWrite;
            }
            else if (DataMicrocodedRadioButton.Checked)
            {
                testType = BenchmarkInteropFunctions.TestType.SseNtRead;
            }
            else if (DataAddRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.SseAdd;
                else if (AvxRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.AvxAdd;
                else if (Avx512RadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.Avx512Add;
                else if (MmxRadioButton.Checked)
                {
                    SetProgressLabel("Sorry no FP support in MMX. And x87 is scary so nope.");
                    return;
                }
            }

            if (InstructionFetchRadioButton.Checked)
            {
                if (FourByteNops.Checked) testType = BenchmarkInteropFunctions.TestType.Instr4;
                else if (EightByteNops.Checked) testType = BenchmarkInteropFunctions.TestType.Instr8;
                else if (K8FourByteNops.Checked) testType = BenchmarkInteropFunctions.TestType.K8Instr4;
                else if (BranchPer16B.Checked) testType = BenchmarkInteropFunctions.TestType.Branch16;
            }

            if (DataMicrocodedRadioButton.Checked)
            {
                if (RepMovsbRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.RepMovsb;
                else if (RepStosbRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.RepStosb;
                else if (RepMovsdRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.RepMovsd;
                else if (RepStosdRadioButton.Checked) testType = BenchmarkInteropFunctions.TestType.RepStosd;
            }

            CancelRunningTest(true);
            runCancel = new CancellationTokenSource();
            testTask = Task.Run(() => bwRunner.StartFullTest(threadCount, sharedMode, testType, runCancel.Token));
            CancelRunButton.Enabled = true;
            Task.Run(() => HandleTestRunCompletion(testTask, SetCancelButtonState));
        }

        private void CancelRunningTest(bool wait)
        {
            if (runCancel != null && testTask != null)
            {
                runCancel.Cancel();
                SetProgressLabel("Cancel requested, waiting for current test to finish");
                if (wait) testTask.Wait();
            }
        }

        public delegate void SafeSetExportListBox();
        public void SetExportListBox() 
        {
            // Update ExportListBox
            ExportListBox.Items.Clear();

            // Add test results
            if (bwRunner != null)
            {
                foreach (KeyValuePair<string, List<Tuple<float, float>>> kvp in bwRunner.RunResults)
                {
                    ExportListBox.Items.Add(kvp.Key);
                }
            }

            if (latencyRunner != null)
            {
                foreach (KeyValuePair<string, List<Tuple<float, float>>> kvp in latencyRunner.RunResults)
                {
                    ExportListBox.Items.Add(kvp.Key);
                }
            }

            if (OpenCLTest.RunResults != null)
            {
                foreach (KeyValuePair<string, List<Tuple<float, float>>> kvp in OpenCLTest.RunResults)
                {
                    ExportListBox.Items.Add(kvp.Key);
                }
            }
        }

        private async Task HandleTestRunCompletion(Task task, SafeSetCancelButtonState setCancelButtonDelegate)
        {
            await task;
            CancelRunButton.Invoke(setCancelButtonDelegate, new object[] { false });
            SafeSetExportListBox safeSetExportListBox = SetExportListBox;
            ExportListBox.Invoke(safeSetExportListBox, new object[] { });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelRunningTest(false);
        }

        private void CheckWriteModeChange(object sender, EventArgs e)
        {
            if (DataWriteRadioButton.Checked || DataAddRadioButton.Checked || DataNtWriteRadioButton.Checked || DataMicrocodedRadioButton.Checked)
            {
                PrivateRadioButton.Checked = true;
                SharedRadioButton.Checked = false;
                SharedRadioButton.Enabled = false;
            }
            else if (!SharedRadioButton.Enabled)
            {
                SharedRadioButton.Enabled = true;
            }
        }

        private void SetInstructionFetchTestMethods()
        {
            this.BandwidthTestMethodFlowLayoutPanel.SuspendLayout();
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Clear();
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.FourByteNops);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.EightByteNops);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.K8FourByteNops);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.BranchPer16B);
            this.BandwidthTestMethodFlowLayoutPanel.ResumeLayout();
            this.BandwidthTestMethodFlowLayoutPanel.PerformLayout();
            this.EightByteNops.Checked = true;
            this.FourByteNops.Checked = false;
            this.K8FourByteNops.Checked = false;
            this.BranchPer16B.Checked = false;
        }

        private void SetDataTestMethods()
        {
            this.BandwidthTestMethodFlowLayoutPanel.SuspendLayout();
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Clear();
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.Avx512RadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.AvxRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.SseRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.MmxRadioButton);
            SetDefaultMethodState();
            this.BandwidthTestMethodFlowLayoutPanel.ResumeLayout();
            this.BandwidthTestMethodFlowLayoutPanel.PerformLayout();
        }

        private void SetMicrocodedTestMethods()
        {
            this.BandwidthTestMethodFlowLayoutPanel.SuspendLayout();
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Clear();
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.RepMovsbRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.RepStosbRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.RepMovsdRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.RepStosdRadioButton);
            this.RepMovsbRadioButton.Checked = true;
            this.RepStosbRadioButton.Checked = false;
            this.RepMovsdRadioButton.Checked = false;
            this.RepStosdRadioButton.Checked = false;
            this.BandwidthTestMethodFlowLayoutPanel.ResumeLayout();
            this.BandwidthTestMethodFlowLayoutPanel.PerformLayout();
        }

        private void SetTestMethodState()
        {
            if (InstructionFetchRadioButton.Checked)
            {
                SetInstructionFetchTestMethods();
            }
            else if (DataMicrocodedRadioButton.Checked)
            {
                SetMicrocodedTestMethods();
            }
            else
            {
                SetDataTestMethods();
            }
        }

        private void InstructionFetchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetTestMethodState();
            CheckWriteModeChange(sender, e);
        }

        private void ThreadCountTrackbar_Scroll(object sender, EventArgs e)
        {
            int threadCount = ThreadCountTrackbar.Value;
            ThreadCountLabel.Text = "Threads: " + threadCount;
        }

        private void ExportExcelButton_Click(object sender, EventArgs e)
        {
            string output = "";
            bool jsFormat = JsFormatRadioButton.Checked;

            if (ExportListBox.SelectedItem == null)
            {
                ExportTextBox.Text = "No run selected";
                return;
            }

            string selectedRun = ExportListBox.SelectedItem.ToString();
            List<Tuple<float, float>> runResults;
            if (bwRunner != null && bwRunner.RunResults.ContainsKey(selectedRun))
            {
                runResults = bwRunner.RunResults[selectedRun];
                if (!jsFormat) output = "Region (KB),Bandwidth (GB/s)";
            }
            else if (latencyRunner != null && latencyRunner.RunResults.ContainsKey(selectedRun))
            {
                runResults = latencyRunner.RunResults[selectedRun];
                if (!jsFormat) output = "Test Size (KB), Latency (ns)";
            }
            else if (OpenCLTest.RunResults != null && OpenCLTest.RunResults.ContainsKey(selectedRun))
            {
                runResults = OpenCLTest.RunResults[selectedRun];
                if (!jsFormat) output = "Test Size (KB), Latency (ns)";
            }
            else
            {
                // should not happen
                return;
            }

            foreach (Tuple<float, float> pt in runResults)
            {
                if (!jsFormat)
                {
                    output += "\r\n" + pt.Item1 + "," + pt.Item2;
                }
                else
                {
                    output += pt.Item1 + "," + pt.Item2 + " ";
                }
            }

            ExportTextBox.Text = output;
        }

        private void ClearChartButton_Click(object sender, EventArgs e)
        {
            ResultsChart.DataSource = null;
            ResultsChart.Series.Clear();
            ResultsChart.ChartAreas[0].AxisX.IsLogarithmic = false;
            plottedSeries.Clear();
            ExportListBox.Items.Clear();
            if (bwRunner != null) bwRunner.RunResults.Clear();
            if (latencyRunner != null) latencyRunner.RunResults.Clear();
            if (OpenCLTest.RunResults != null) OpenCLTest.RunResults.Clear();
        }

        private void specifyNextColorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SpecifyNextColorRadioButton.Checked)
            {
                ColorRBox.Enabled = true;
                ColorGBox.Enabled = true;
                ColorBBox.Enabled = true;
                specifyColor = true;
            }
            else
            {
                ColorRBox.Enabled = false;
                ColorGBox.Enabled = false;
                ColorBBox.Enabled = false;
                specifyColor = false;
            }
        }

        private void MicrobenchmarkForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OpCode.Close();
        }

        private void GpuTlbTestCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (GpuTlbTestCheckbox.Checked)
            {
                GpuEstimatedPageSizeKbLabel.Enabled = true;
                GpuEstimatedPageSizeTextBox.Enabled = true;
                GpuEstimatedPageSizeLabel.Enabled = true;
            }
            else
            {
                GpuEstimatedPageSizeKbLabel.Enabled = false;
                GpuEstimatedPageSizeTextBox.Enabled = false;
                GpuEstimatedPageSizeLabel.Enabled = false;
            }
        }

        private string[][] convertUintToStringArr(uint[] arr)
        {
            string[][] dest = new string[arr.Length][]; 
            for (int i = 0; i < arr.Length; i++)
            {
                dest[i] = new string[] { arr[i].ToString() };
            }
            return dest;
        }

        private uint[] removeIndicesFromArr(ListView.SelectedIndexCollection indices, uint[] arr)
        {
            uint[] retval = new uint[arr.Length - indices.Count];
            for (int i = 0, t = 0; t < retval.Length; i++, t++)
            {
                if (indices.Contains(i)) i++;
                retval[t] = arr[i];
            }

            return retval;
        }

        private uint[] addSizeToArr(uint[] arr, uint size)
        {
            if (arr.Contains(size)) return arr;
            List<uint> tmpList = new List<uint>(arr);
            tmpList.Add(size);
            tmpList.Sort();
            return tmpList.ToArray();
        }

        private void EditTestSizeButton_Click(object sender, EventArgs e)
        {
            SetResultListViewColumns(new string[] { "Test Size (KB)" });
            if (TestSelectTabControl.SelectedTab == GpuMemLatencyTab)
            {
                SetResultListView(convertUintToStringArr(OpenCLTest.latencyTestSizes));
            }
            else if (TestSelectTabControl.SelectedTab == MemoryLatencyTab)
            {
                SetResultListView(convertUintToStringArr(latencyRunner.testSizes));
            }
            else if (TestSelectTabControl.SelectedTab == MemoryBandwidthTab)
            {
                SetResultListView(convertUintToStringArr(bwRunner.testSizes));
            }
            else if (TestSelectTabControl.SelectedTab == GpuLinkBandwidthTab)
            {
                SetResultListView(convertUintToStringArr(OpenCLTest.linkTestSizes));
            }

            RemoveTestSizeButton.Enabled = true;
            AddTestSizeButton.Enabled = true;
        }

        private void RemoveTestSizeButton_Click(object sender, EventArgs e)
        {
            if (resultListView.SelectedIndices.Count != 1)
            {
                progressLabel.Text = "Select a test size to remove";
                return;
            }

            if (TestSelectTabControl.SelectedTab == GpuMemLatencyTab)
            {
                OpenCLTest.latencyTestSizes = removeIndicesFromArr(resultListView.SelectedIndices, OpenCLTest.latencyTestSizes);
                SetResultListView(convertUintToStringArr(OpenCLTest.latencyTestSizes));
            }
            else if (TestSelectTabControl.SelectedTab == MemoryLatencyTab)
            {
                latencyRunner.testSizes = removeIndicesFromArr(resultListView.SelectedIndices, latencyRunner.testSizes);
                SetResultListView(convertUintToStringArr(latencyRunner.testSizes));
            }
            else if (TestSelectTabControl.SelectedTab == MemoryBandwidthTab)
            {
                bwRunner.testSizes = removeIndicesFromArr(resultListView.SelectedIndices, bwRunner.testSizes);
                SetResultListView(convertUintToStringArr(bwRunner.testSizes));
            }
            else if (TestSelectTabControl.SelectedTab == GpuLinkBandwidthTab)
            {
                OpenCLTest.linkTestSizes = removeIndicesFromArr(resultListView.SelectedIndices, OpenCLTest.linkTestSizes);
                SetResultListView(convertUintToStringArr(OpenCLTest.linkTestSizes));
            }

            progressLabel.Text = "Removed size";
        }

        private void AddTestSizeButton_Click(object sender, EventArgs e)
        {
            string inputText = AddSizeTextBox.Text;
            if (!uint.TryParse(inputText, out uint sizeToAdd))
            {
                progressLabel.Text = "Size has to be a positive integer";
            }

            if (TestSelectTabControl.SelectedTab == GpuMemLatencyTab)
            {
                OpenCLTest.latencyTestSizes = addSizeToArr(OpenCLTest.latencyTestSizes, sizeToAdd);
                SetResultListView(convertUintToStringArr(OpenCLTest.latencyTestSizes));
            }
            else if (TestSelectTabControl.SelectedTab == MemoryLatencyTab)
            {
                latencyRunner.testSizes = addSizeToArr(latencyRunner.testSizes, sizeToAdd);
                SetResultListView(convertUintToStringArr(latencyRunner.testSizes));
            }
            else if (TestSelectTabControl.SelectedTab == MemoryBandwidthTab)
            {
                bwRunner.testSizes = addSizeToArr(bwRunner.testSizes, sizeToAdd);
                SetResultListView(convertUintToStringArr(bwRunner.testSizes));
            }
            else if (TestSelectTabControl.SelectedTab == GpuLinkBandwidthTab)
            {
                OpenCLTest.linkTestSizes = addSizeToArr(OpenCLTest.linkTestSizes, sizeToAdd);
                SetResultListView(convertUintToStringArr(OpenCLTest.linkTestSizes));
            }
        }

        private void TestSelectTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (TestSelectTabControl.SelectedTab == GpuMemLatencyTab)
            {
                progressLabel.Text = OpenCLTest.InitializeDeviceControls(GpuMemoryLatencyDeviceFlowLayoutPanel);
                SetChartType(ResultChartType.GpuMemoryLatency);
            }
            else if (TestSelectTabControl.SelectedTab == GpuLinkBandwidthTab)
            {
                progressLabel.Text = OpenCLTest.InitializeDeviceControls(GpuLinkBandwidthDeviceFlowLayoutPanel);
                SetChartType(ResultChartType.GpuLinkBandwidth);
            }
            else if (TestSelectTabControl.SelectedTab == MemoryLatencyTab)
            {
                SetChartType(ResultChartType.CpuMemoryLatency);
            }
            else if (TestSelectTabControl.SelectedTab == MemoryBandwidthTab)
            {
                SetChartType(ResultChartType.CpuMemoryBandwidth);
            }

            resultListView.Items.Clear();
            resultListView.Columns.Clear();
            RemoveTestSizeButton.Enabled = false;
            AddTestSizeButton.Enabled = false;
        }
    }
}
