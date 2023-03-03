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
        private RadioButton AsmRadioButton;
        private RadioButton CRadioButton;
        private RadioButton DefaultPagesRadioButton;
        private RadioButton LargePagesRadioButton;
        private RadioButton RepMovsbRadioButton;
        private RadioButton RepStosbRadioButton;
        private RadioButton RepMovsdRadioButton;
        private RadioButton RepStosdRadioButton;

        private RadioButton GlobalScalarRadioButton;
        private RadioButton GlobalVectorRadioButton;
        private RadioButton ConstantScalarRadioButton;
        private RadioButton TextureRadioButton;

        private Random randomThings;

        private bool avxSupported;
        private bool avx512Supported;

        public MicrobenchmarkForm()
        {
            plottedSeries = new Dictionary<string, Series>();
            InitializeComponent();
            ResultChart.Series.Clear();

            #region manual control initialization
            ToolTip tooltips = new ToolTip();
            Size groupBoxRadioButtonSize = new Size(220, 17);
            FourByteNops = new RadioButton();
            FourByteNops.Text = "4B NOPs (0F 1F 40 00)";
            FourByteNops.Location = new Point(7, 20);
            FourByteNops.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(FourByteNops, "Corresponds to average instruction lengths found in typical integer code");

            EightByteNops = new RadioButton();
            EightByteNops.Text = "8B NOPs (0F 1F 84 00 00 00 00 00)";
            EightByteNops.Location = new Point(7, 44);
            EightByteNops.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(EightByteNops, "Relatively long 8-byte instructions. More representative of AVX/AVX2/AVX-512 code, or code with lots of large immediates");

            BranchPer16B = new RadioButton();
            BranchPer16B.Text = "Taken Branch Per 16B";
            BranchPer16B.Location = new Point(7, 68);
            BranchPer16B.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(BranchPer16B, "Test with a chain of taken branches, spaced 16B apart. More of a BTB/branch predictor speed test.");

            K8FourByteNops = new RadioButton();
            K8FourByteNops.Text = "4B NOPs (66 66 66 90)";
            K8FourByteNops.Location = new Point(7, 92);
            K8FourByteNops.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(K8FourByteNops, "Use a NOP encoding recommended by the AMD Athlon/K8 optimization manual. Curiously avoids decoding bottlenecks on certain Intel CPUs, but doesn't matter for AMD (?)");

            RepMovsbRadioButton = new RadioButton();
            RepMovsbRadioButton.Text = "REP MOVSB (Copy)";
            RepMovsbRadioButton.Location = new Point(7, 20);
            RepMovsbRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepMovsbRadioButton, "Tells CPU to copy a block of memory, with size known upfront");

            RepStosbRadioButton = new RadioButton();
            RepStosbRadioButton.Text = "REP STOSB (Write)";
            RepStosbRadioButton.Location = new Point(7, 44);
            RepStosbRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepStosbRadioButton, "Tells CPU to write to a block of memory, with size known upfront");

            RepMovsdRadioButton = new RadioButton();
            RepMovsdRadioButton.Text = "REP MOVSD (Copy)";
            RepMovsdRadioButton.Location = new Point(7, 68);
            RepMovsdRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepMovsdRadioButton, "Tells CPU to copy a block of memory with DWORD (4 byte) granularity. Potentially faster than REP MOVSB on some CPUs");

            RepStosdRadioButton = new RadioButton();
            RepStosdRadioButton.Text = "REP STOSD (Write)";
            RepStosdRadioButton.Location = new Point(7, 92);
            RepStosdRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(RepStosdRadioButton, "Tells CPU to write to a block of memory with DWORD (4 byte) granularity. Potentially faster than REP STOSB on some CPUs");

            AsmRadioButton = new RadioButton();
            AsmRadioButton.Text = "Simple Addressing (ASM)";
            AsmRadioButton.Location = new Point(7, 20);
            AsmRadioButton.Size = groupBoxRadioButtonSize;
            AsmRadioButton.Checked = true;
            tooltips.SetToolTip(AsmRadioButton, "Uses direct addressing, i.e. mov r15, [r15]. Should be the lowest latency way to hit the memory hierarchy");

            CRadioButton = new RadioButton();
            CRadioButton.Text = "Indexed Addressing (C)";
            CRadioButton.Location = new Point(7, 44);
            CRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(CRadioButton, "Tests a = A[a] latency. The CPU has to add the offset to the array base address, which can incur additional latency");

            DefaultPagesRadioButton = new RadioButton();
            DefaultPagesRadioButton.Text = "Default (4 KB Pages)";
            DefaultPagesRadioButton.Location = new Point(7, 20);
            DefaultPagesRadioButton.Size = groupBoxRadioButtonSize;
            DefaultPagesRadioButton.Checked = true;
            tooltips.SetToolTip(DefaultPagesRadioButton, "Should be representative of memory latency seen by most user applications");

            LargePagesRadioButton = new RadioButton();
            LargePagesRadioButton.Text = "Large Pages (2 MB Pages)";
            LargePagesRadioButton.Location = new Point(7, 44);
            LargePagesRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(LargePagesRadioButton, "Asks the OS to handle address translation at a larger granularity, giving a better view of raw cache latency with TLB miss penalties minimized. Requires 'Lock Pages in Memory' permission");

            GlobalScalarRadioButton = new RadioButton();
            GlobalScalarRadioButton.Text = "Global Memory, Scalar";
            GlobalScalarRadioButton.Location = new Point(7, 20);
            GlobalScalarRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(GlobalScalarRadioButton, "Similar to CPU latency test. Simple pointer chasing pattern in GPU global memory. AMD GPUs often have scalar caches.");

            GlobalVectorRadioButton = new RadioButton();
            GlobalVectorRadioButton.Text = "Global Memory, Vector";
            GlobalVectorRadioButton.Location = new Point(7, 44);
            GlobalVectorRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(GlobalVectorRadioButton, "Uses two threads, set up so that the compiler can't determine if a loaded value will be constant across a wave/warp. AMD has separate vector and scalar caches.");

            ConstantScalarRadioButton = new RadioButton();
            ConstantScalarRadioButton.Text = "Constant Memory, Scalar";
            ConstantScalarRadioButton.Location = new Point(7, 68);
            ConstantScalarRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(ConstantScalarRadioButton, "Uses constant, read-only memory. Nvidia GPUs have separate constant caches."); ;

            TextureRadioButton = new RadioButton();
            TextureRadioButton.Text = "Texture";
            TextureRadioButton.Location = new Point(7, 92);
            TextureRadioButton.Size = groupBoxRadioButtonSize;
            tooltips.SetToolTip(TextureRadioButton, "Test latency through the texture pipeline (TMUs), using a 1D image buffer. Some Nvidia GPUs have separate texture caches.");

            tooltips.SetToolTip(ExportExcelButton, "Get results in an Excel (or custom JS) parse-able format");
            tooltips.SetToolTip(ClearChartButton, "Embarassingly bad results? Click here");
            tooltips.SetToolTip(OpenCLLatencyRadioButton, "Test GPU memory latency using the OpenCL API");
            tooltips.SetToolTip(DataReadRadioButton, "Tests memory read bandwidth. Most memory accesses are reads");
            tooltips.SetToolTip(DataWriteRadioButton, "Tests memory write bandwidth. Usually 1/4-1/2 of memory accesses are writes, though that can vary a lot");
            tooltips.SetToolTip(DataNtWriteRadioButton, "Tests memory write bandwidth with non-temporal accesses, which bypass caches. Potentially allows better use of DRAM write bandwidth by avoiding RFOs");
            tooltips.SetToolTip(DataMicrocodedRadioButton, "Tests memory bandwidth using microcoded string instructions, which tell the CPU upfront how much data it has to move. Potentially allows RFO avoidance while using caches.");
            tooltips.SetToolTip(InstructionFetchRadioButton, "Fills an array with valid instructions and jumps to it, to test how fast the CPU can bring instructions into the core");
            tooltips.SetToolTip(DataAddRadioButton, "Tests bandwidth using a 1:1 read-to-write ratio, by adding a constant to every element of an array. Can show if there's an advantage to mixing reads and writes");
            tooltips.SetToolTip(PrivateRadioButton, "Gives each thread its own data. Shows the sum of private cache capacity");
            tooltips.SetToolTip(SharedRadioButton, "All threads read from one shared array. Shared data is duplicated across private caches, so you won't see the sum of cache capacity");
            #endregion

            ThreadCountTrackbar.Maximum = Environment.ProcessorCount;
            ResultChart.Titles.Add("Result Plot");

            if (BenchmarkFunctions.CheckAvxSupport() != 1)
            {
                avxSupported = false;
            }
            else
            {
                avxSupported = true;
            }

            if (BenchmarkFunctions.CheckAvx512Support() != 1)
            {
                avx512Supported = false;
            }
            else
            {
                avx512Supported = true;
            }

            randomThings = new Random();
            SetDefaultMethodState();

            OpCode.Open();
            this.Text = "Clam Cache/Mem Benchmark: " + OpCode.GetProcessorName();
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
        public delegate void SafeSetResultsChart(string seriesNames, float[] testPoints, float[] testResults);
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

        private Dictionary<string, Series> plottedSeries;

        // Make sure we don't reduce the max when a new test is run
        private double chartMax = 0;

        // Somehow can't retrieve radio button value?
        private bool specifyColor = false;

        public void SetResultChart(string seriesName, float[] testPoints, float[] testResults)
        {
            Series series;
            if (!plottedSeries.TryGetValue(seriesName, out series))
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
                    parseSucceeded &= (red < 255 && red >= 0);
                    parseSucceeded &= (green < 255 && green >= 0);
                    parseSucceeded &= (blue < 255 && blue >= 0);
                    if (!parseSucceeded)
                    {
                        SetProgressLabel("Red/Green/Blue values must be numbers between 0-255");
                    }
                    else
                    {
                        series.Color = Color.FromArgb(red, green, blue);
                    }
                }

                plottedSeries.Add(seriesName, series);
                ResultChart.ChartAreas[0].AxisX.IsLogarithmic = true;
                ResultChart.ChartAreas[0].AxisX.LogarithmBase = 2;
                ResultChart.ChartAreas[0].AxisX.LabelStyle.Format = "#";
                ResultChart.ChartAreas[0].AxisX.Title = "Data (KB)";
                ResultChart.Series.Add(series);
            }

            series.Points.Clear();
            double min = testPoints[0], max = testPoints[0];
            // we expect this to be sorted but just in case
            for (uint i = 0; i < testPoints.Length && i < testResults.Length; i++)
            {
                if (testPoints[i] > max) max = testPoints[i];
                if (testPoints[i] < min) min = testPoints[i];
            }

            ResultChart.ChartAreas[0].AxisX.Minimum = min;
            if (max > chartMax)
            {
                ResultChart.ChartAreas[0].AxisX.Maximum = max;
                chartMax = max;
            }

            for (uint i = 0; i < testPoints.Length && i < testResults.Length; i++)
            {
                series.Points.AddXY((double)testPoints[i], (double)testResults[i]);
            }

            foreach (Legend a in ResultChart.Legends)
            {
                a.Docking = Docking.Bottom;
            }

            return;
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
            if (this.MemoryBandwidthRadioButton.Checked)
            {
                RunBandwidthTest();
            }
            else if (this.MemoryLatencyRadioButton.Checked)
            {
                RunLatencyTest();
            }
            else if (this.OpenCLLatencyRadioButton.Checked)
            {
                RunClLatencyTest();
            }
        }

        private void RunClLatencyTest()
        {
            CancelRunningTest(true);
            runCancel = new CancellationTokenSource();
            BenchmarkFunctions.CLTestType clLatencyTestMode = BenchmarkFunctions.CLTestType.GlobalScalar;
            if (GlobalScalarRadioButton.Checked) clLatencyTestMode = BenchmarkFunctions.CLTestType.GlobalScalar;
            else if (GlobalVectorRadioButton.Checked) clLatencyTestMode = BenchmarkFunctions.CLTestType.GlobalVector;
            else if (ConstantScalarRadioButton.Checked) clLatencyTestMode = BenchmarkFunctions.CLTestType.ConstantScalar;
            else if (TextureRadioButton.Checked) clLatencyTestMode = BenchmarkFunctions.CLTestType.Texture;
            testTask = Task.Run(() => OpenCLTest.RunLatencyTest(SetResultListView,
                SetResultListViewColumns,
                SetResultChart,
                SetProgressLabel,
                resultListView,
                ResultChart,
                progressLabel,
                clLatencyTestMode,
                runCancel.Token));
            CancelRunButton.Enabled = true;
            Task.Run(() => HandleTestRunCompletion(testTask, SetCancelButtonState));
        }

        private void RunLatencyTest()
        {
            if (latencyRunner == null) latencyRunner = new LatencyRunner(SetResultListView,
                SetResultListViewColumns, SetResultChart, SetProgressLabel, resultListView, ResultChart, progressLabel);

            bool largePages = LargePagesRadioButton.Checked;
            bool asm = AsmRadioButton.Checked;
            uint iterations;
            string iterationsStr = dataToTransferTextBox.Text;
            if (!uint.TryParse(iterationsStr, out iterations))
            {
                SetProgressLabel("Iteration count " + iterationsStr + " must be a whole number");
                return;
            }

            if (iterations < 1000)
            {
                SetProgressLabel("Iteration count is unreasonably low. Try a value over 1000");
                return;
            }

            CancelRunningTest(true);
            runCancel = new CancellationTokenSource();
            testTask = Task.Run(() => latencyRunner.StartFullTest(asm, largePages, iterations, runCancel.Token));
            CancelRunButton.Enabled = true;
            Task.Run(() => HandleTestRunCompletion(testTask, SetCancelButtonState));
        }

        private void RunBandwidthTest()
        {
            if (bwRunner == null) bwRunner = new BandwidthRunner(SetResultListView,
                SetResultListViewColumns, SetResultChart, SetProgressLabel, resultListView, ResultChart, progressLabel);

            // Read test parameters from interface
            uint threadCount = (uint)ThreadCountTrackbar.Value;
            bool sharedMode = SharedRadioButton.Checked;

            string dataGbStr = dataToTransferTextBox.Text;
            uint dataGb;
            if (!uint.TryParse(dataGbStr, out dataGb))
            {
                SetProgressLabel("Data to transfer or iter count (" + dataGbStr + ") has to be a whole number");
                return;
            }

            BenchmarkFunctions.TestType testType = BenchmarkFunctions.TestType.AvxRead;
            if (DataReadRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkFunctions.TestType.SseRead;
                else if (AvxRadioButton.Checked) testType = BenchmarkFunctions.TestType.AvxRead;
                else if (Avx512RadioButton.Checked) testType = BenchmarkFunctions.TestType.Avx512Read;
                else if (MmxRadioButton.Checked) testType = BenchmarkFunctions.TestType.MmxRead;
            }
            else if (DataWriteRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkFunctions.TestType.SseWrite;
                else if (AvxRadioButton.Checked) testType = BenchmarkFunctions.TestType.AvxWrite;
                else if (Avx512RadioButton.Checked) testType = BenchmarkFunctions.TestType.Avx512Write;
                else if (MmxRadioButton.Checked) testType = BenchmarkFunctions.TestType.MmxWrite;
            }
            else if (DataNtWriteRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkFunctions.TestType.SseNtWrite;
                else if (AvxRadioButton.Checked) testType = BenchmarkFunctions.TestType.AvxNtWrite;
                else if (Avx512RadioButton.Checked) testType = BenchmarkFunctions.TestType.Avx512NtWrite;
                else if (MmxRadioButton.Checked) testType = BenchmarkFunctions.TestType.MmxNtWrite;
            }
            else if (DataMicrocodedRadioButton.Checked)
            {
                testType = BenchmarkFunctions.TestType.SseNtRead;
            }
            else if (DataAddRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkFunctions.TestType.SseAdd;
                else if (AvxRadioButton.Checked) testType = BenchmarkFunctions.TestType.AvxAdd;
                else if (Avx512RadioButton.Checked) testType = BenchmarkFunctions.TestType.Avx512Add;
                else if (MmxRadioButton.Checked)
                {
                    SetProgressLabel("Sorry no FP support in MMX. And x87 is scary so nope.");
                    return;
                }
            }

            if (InstructionFetchRadioButton.Checked)
            {
                if (FourByteNops.Checked) testType = BenchmarkFunctions.TestType.Instr4;
                else if (EightByteNops.Checked) testType = BenchmarkFunctions.TestType.Instr8;
                else if (K8FourByteNops.Checked) testType = BenchmarkFunctions.TestType.K8Instr4;
                else if (BranchPer16B.Checked) testType = BenchmarkFunctions.TestType.Branch16;
            }

            if (DataMicrocodedRadioButton.Checked)
            {
                if (RepMovsbRadioButton.Checked) testType = BenchmarkFunctions.TestType.RepMovsb;
                else if (RepStosbRadioButton.Checked) testType = BenchmarkFunctions.TestType.RepStosb;
                else if (RepMovsdRadioButton.Checked) testType = BenchmarkFunctions.TestType.RepMovsd;
                else if (RepStosdRadioButton.Checked) testType = BenchmarkFunctions.TestType.RepStosd;
            }

            CancelRunningTest(true);
            runCancel = new CancellationTokenSource();
            testTask = Task.Run(() => bwRunner.StartFullTest(threadCount, sharedMode, testType, dataGb, runCancel.Token));
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
            this.TestMethodGroupBox.SuspendLayout();
            this.TestMethodGroupBox.Controls.Clear();
            this.TestMethodGroupBox.Controls.Add(this.FourByteNops);
            this.TestMethodGroupBox.Controls.Add(this.EightByteNops);
            this.TestMethodGroupBox.Controls.Add(this.K8FourByteNops);
            this.TestMethodGroupBox.Controls.Add(this.BranchPer16B);
            this.TestMethodGroupBox.PerformLayout();
            this.EightByteNops.Checked = true;
            this.FourByteNops.Checked = false;
            this.K8FourByteNops.Checked = false;
            this.BranchPer16B.Checked = false;
        }

        private void SetDataTestMethods()
        {
            this.TestMethodGroupBox.SuspendLayout();
            this.TestMethodGroupBox.Controls.Clear();
            this.TestMethodGroupBox.Controls.Add(this.Avx512RadioButton);
            this.TestMethodGroupBox.Controls.Add(this.AvxRadioButton);
            this.TestMethodGroupBox.Controls.Add(this.SseRadioButton);
            this.TestMethodGroupBox.Controls.Add(this.MmxRadioButton);
            SetDefaultMethodState();
            this.TestMethodGroupBox.PerformLayout();
        }

        private void SetMicrocodedTestMethods()
        {
            this.TestMethodGroupBox.SuspendLayout();
            this.TestMethodGroupBox.Controls.Clear();
            this.TestMethodGroupBox.Controls.Add(this.RepMovsbRadioButton);
            this.TestMethodGroupBox.Controls.Add(this.RepStosbRadioButton);
            this.TestMethodGroupBox.Controls.Add(this.RepMovsdRadioButton);
            this.TestMethodGroupBox.Controls.Add(this.RepStosdRadioButton);
            this.RepMovsbRadioButton.Checked = true;
            this.RepStosbRadioButton.Checked = false;
            this.RepMovsdRadioButton.Checked = false;
            this.RepStosdRadioButton.Checked = false;
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

        private void LatencyTestRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MemoryLatencyRadioButton.Checked)
            {
                // mem latency test is always ST, no exceptions
                ThreadingModeGroupBox.Enabled = false;
                ThreadCountTrackbar.Enabled = false;
                ThreadCountTrackbar.Value = 1;

                // switch other controls over
                TestMethodGroupBox.Text = "Paging Mode";
                TestMethodGroupBox.SuspendLayout();
                TestMethodGroupBox.Controls.Clear();
                TestMethodGroupBox.Controls.Add(DefaultPagesRadioButton);
                TestMethodGroupBox.Controls.Add(LargePagesRadioButton);
                TestMethodGroupBox.PerformLayout();

                AccessModeGroupBox.Text = "Access Mode";
                AccessModeGroupBox.Controls.Clear();
                AccessModeGroupBox.Controls.Add(CRadioButton);
                AccessModeGroupBox.Controls.Add(AsmRadioButton);
                AccessModeGroupBox.PerformLayout();

                TestDurationLabel.Enabled = true;
                dataToTransferTextBox.Enabled = true;
                TestDurationLabel.Text = "Base Iterations:";
                dataToTransferTextBox.Text = "400000000";
                gbLabel.Text = "times";
                gbLabel.Enabled = true;

                // CPU mem latencies are easier to read on a log axis
                ResultChart.ChartAreas[0].AxisY.IsLogarithmic = true;
                ResultChart.ChartAreas[0].AxisY.LogarithmBase = 2;
            }
            else if (MemoryBandwidthRadioButton.Checked)
            {
                ThreadingModeGroupBox.Enabled = true;
                ThreadCountTrackbar.Enabled = true;
                TestMethodGroupBox.Text = "Test Method";
                TestMethodGroupBox.Controls.Clear();
                InstructionFetchRadioButton_CheckedChanged(sender, e);

                AccessModeGroupBox.Text = "Access Mode";
                AccessModeGroupBox.Controls.Clear();
                AccessModeGroupBox.Controls.Add(DataReadRadioButton);
                AccessModeGroupBox.Controls.Add(DataMicrocodedRadioButton);
                AccessModeGroupBox.Controls.Add(DataWriteRadioButton);
                AccessModeGroupBox.Controls.Add(DataNtWriteRadioButton);
                AccessModeGroupBox.Controls.Add(DataAddRadioButton);
                AccessModeGroupBox.Controls.Add(InstructionFetchRadioButton);
                AccessModeGroupBox.PerformLayout();

                TestDurationLabel.Enabled = true;
                dataToTransferTextBox.Enabled = true;
                TestDurationLabel.Text = "Base Data to Transfer:";
                dataToTransferTextBox.Text = "512";
                gbLabel.Text = "GB";
                gbLabel.Enabled = true;

                ResultChart.ChartAreas[0].AxisY.IsLogarithmic = false;
            }
            else if (OpenCLLatencyRadioButton.Checked)
            {
                ThreadingModeGroupBox.Enabled = false;
                ThreadCountTrackbar.Enabled = false;
                ThreadCountTrackbar.Value = 1;
                ThreadingModeGroupBox.Enabled = false;

                TestMethodGroupBox.Text = "Access Mode";
                TestMethodGroupBox.SuspendLayout();
                TestMethodGroupBox.Controls.Clear();
                TestMethodGroupBox.Controls.Add(GlobalScalarRadioButton);
                TestMethodGroupBox.Controls.Add(GlobalVectorRadioButton);
                TestMethodGroupBox.Controls.Add(ConstantScalarRadioButton);
                TestMethodGroupBox.Controls.Add(TextureRadioButton);
                GlobalScalarRadioButton.Checked = true;
                GlobalVectorRadioButton.Checked = false;
                ConstantScalarRadioButton.Checked = false;
                TestMethodGroupBox.PerformLayout();

                // have to auto set duration to avoid TDR
                TestDurationLabel.Enabled = false;
                dataToTransferTextBox.Enabled = false;
                gbLabel.Enabled = false;

                // Add OpenCL devices via pinvoke call
                progressLabel.Text = OpenCLTest.InitializeDeviceControls(AccessModeGroupBox);

                // GPU mem latencies are easier to view on a linear axis
                ResultChart.ChartAreas[0].AxisY.IsLogarithmic = false;
            }
        }

        private void MicrobenchmarkForm_Load(object sender, EventArgs e)
        {

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

            if (ExportListBox.SelectedItem == null) return;
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
            ResultChart.DataSource = null;
            ResultChart.Series.Clear();
            ResultChart.ChartAreas[0].AxisX.IsLogarithmic = false;
            plottedSeries.Clear();
            ExportListBox.Items.Clear();
            if (bwRunner != null) bwRunner.RunResults.Clear();
            if (latencyRunner != null) latencyRunner.RunResults.Clear();
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
    }
}
