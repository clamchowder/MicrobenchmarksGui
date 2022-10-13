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
        private Random randomThings;

        private bool avxSupported;
        private bool avx512Supported;

        public MicrobenchmarkForm()
        {
            plottedSeries = new Dictionary<string, Series>();
            InitializeComponent();
            ResultChart.Series.Clear();

            Size groupBoxRadioButtonSize = new Size(220, 17);
            FourByteNops = new RadioButton();
            FourByteNops.Text = "4B NOPs (0F 1F 40 00)";
            FourByteNops.Location = new Point(7, 20);
            FourByteNops.Size = groupBoxRadioButtonSize;

            EightByteNops = new RadioButton();
            EightByteNops.Text = "8B NOPs (0F 1F 84 00 00 00 00 00)";
            EightByteNops.Location = new Point(7, 44);
            EightByteNops.Size = groupBoxRadioButtonSize;

            BranchPer16B = new RadioButton();
            BranchPer16B.Text = "Taken Branch Per 16B";
            BranchPer16B.Location = new Point(7, 68);
            BranchPer16B.Size = groupBoxRadioButtonSize;

            K8FourByteNops = new RadioButton();
            K8FourByteNops.Text = "4B NOPs (66 66 66 90)";
            K8FourByteNops.Location = new Point(7, 92);
            K8FourByteNops.Size = groupBoxRadioButtonSize;

            RepMovsbRadioButton = new RadioButton();
            RepMovsbRadioButton.Text = "REP MOVSB (Copy)";
            RepMovsbRadioButton.Location = new Point(7, 20);
            RepMovsbRadioButton.Size = groupBoxRadioButtonSize;

            RepStosbRadioButton = new RadioButton();
            RepStosbRadioButton.Text = "REP STOSB (Write)";
            RepStosbRadioButton.Location = new Point(7, 44);
            RepStosbRadioButton.Size = groupBoxRadioButtonSize;

            RepMovsdRadioButton = new RadioButton();
            RepMovsdRadioButton.Text = "REP MOVSD (Copy)";
            RepMovsdRadioButton.Location = new Point(7, 68);
            RepMovsdRadioButton.Size = groupBoxRadioButtonSize;

            RepStosdRadioButton = new RadioButton();
            RepStosdRadioButton.Text = "REP STOSD (Write)";
            RepStosdRadioButton.Location = new Point(7, 92);
            RepStosdRadioButton.Size = groupBoxRadioButtonSize;

            AsmRadioButton = new RadioButton();
            AsmRadioButton.Text = "Simple Addressing (ASM)";
            AsmRadioButton.Location = new Point(7, 20);
            AsmRadioButton.Size = groupBoxRadioButtonSize;
            AsmRadioButton.Checked = true;

            CRadioButton = new RadioButton();
            CRadioButton.Text = "Indexed Addressing (C)";
            CRadioButton.Location = new Point(7, 44);
            CRadioButton.Size = groupBoxRadioButtonSize;

            DefaultPagesRadioButton = new RadioButton();
            DefaultPagesRadioButton.Text = "Default (4 KB Pages)";
            DefaultPagesRadioButton.Location = new Point(7, 20);
            DefaultPagesRadioButton.Size = groupBoxRadioButtonSize;
            DefaultPagesRadioButton.Checked = true;

            LargePagesRadioButton = new RadioButton();
            LargePagesRadioButton.Text = "Large Pages (2 MB Pages)";
            LargePagesRadioButton.Location = new Point(7, 44);
            LargePagesRadioButton.Size = groupBoxRadioButtonSize;

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
                    parseSucceeded &= (red < 255 && red > 0);
                    parseSucceeded &= (green < 255 && green > 0);
                    parseSucceeded &= (blue < 255 && blue > 0);
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

            // Add latency test results
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

        bool latencyTestSet = false;
        private void LatencyTestRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MemoryLatencyRadioButton.Checked && latencyTestSet == false)
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

                AccessModeGroupBox.Controls.Clear();
                AccessModeGroupBox.Controls.Add(CRadioButton);
                AccessModeGroupBox.Controls.Add(AsmRadioButton);
                AccessModeGroupBox.PerformLayout();
                TestDurationLabel.Text = "Base Iterations:";
                dataToTransferTextBox.Text = "400000000";
                gbLabel.Text = "times";

                ResultChart.ChartAreas[0].AxisY.IsLogarithmic = true;
                ResultChart.ChartAreas[0].AxisY.LogarithmBase = 2;
                latencyTestSet = true;
            }
            else if (MemoryBandwidthRadioButton.Checked && latencyTestSet == true)
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

                TestDurationLabel.Text = "Base Data to Transfer:";
                dataToTransferTextBox.Text = "512";
                gbLabel.Text = "GB";

                ResultChart.ChartAreas[0].AxisY.IsLogarithmic = false;
                latencyTestSet = false;
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
