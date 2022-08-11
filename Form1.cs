using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            ThreadCountTrackbar.Maximum = Environment.ProcessorCount;
            ResultChart.Titles.Add("Result Plot");
        }

        public delegate void SafeSetResultListViewColumns(string[] cols);
        public delegate void SafeSetResultListView(string[][] items);
        public delegate void SafeSetResultsChart(string seriesNames, float[] testPoints, float[] testResults);
        public delegate void SafeSetCancelButtonState(bool enabled);
        public delegate void SafeSetProgressLabel(string message);

        public void SetProgressLabel(string message) { progressLabel.Text = message;  }

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
            foreach(ColumnHeader column in resultListView.Columns)
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

        public void SetResultChart(string seriesName, float[] testPoints, float[] testResults)
        {
            Series series;
            if (!plottedSeries.TryGetValue(seriesName, out series))
            {
                series = new Series(seriesName);
                series.ChartType = SeriesChartType.Line;
                plottedSeries.Add(seriesName, series);
                ResultChart.ChartAreas[0].AxisX.IsLogarithmic = true;
                ResultChart.ChartAreas[0].AxisX.LogarithmBase = 2;
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
        private CancellationTokenSource runCancel;
        private Task bwTask;

        /// <summary>
        /// Kicks off bandwidth test, automated run through all sizes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunBandwidthTestButton_Click(object sender, EventArgs e)
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
                SetProgressLabel("Data to transfer (" + dataGbStr + ") has to be a whole number");
                return;
            }

            BenchmarkFunctions.TestType testType = BenchmarkFunctions.TestType.AvxRead;
            if (DataReadRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkFunctions.TestType.SseRead;
                else if (AvxRadioButton.Checked) testType = BenchmarkFunctions.TestType.AvxRead;
                else if (Avx512RadioButton.Checked) testType = BenchmarkFunctions.TestType.Avx512Read;
            }
            else if (DataWriteRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkFunctions.TestType.SseWrite;
                else if (AvxRadioButton.Checked) testType = BenchmarkFunctions.TestType.AvxWrite;
                else if (Avx512RadioButton.Checked) testType = BenchmarkFunctions.TestType.Avx512Write;
            }
            else if (DataAddRadioButton.Checked)
            {
                if (SseRadioButton.Checked) testType = BenchmarkFunctions.TestType.SseAdd;
                else if (AvxRadioButton.Checked) testType = BenchmarkFunctions.TestType.AvxAdd;
                else if (Avx512RadioButton.Checked) testType = BenchmarkFunctions.TestType.Avx512Add;
            }

            if (InstructionFetchRadioButton.Checked)
            {
                if (FourByteNops.Checked) testType = BenchmarkFunctions.TestType.Instr4;
                else if (EightByteNops.Checked) testType = BenchmarkFunctions.TestType.Instr8;
                else if (K8FourByteNops.Checked) testType = BenchmarkFunctions.TestType.K8Instr4;
                else if (BranchPer16B.Checked) testType = BenchmarkFunctions.TestType.Branch16;
            }

            if (runCancel != null && bwTask != null)
            {
                runCancel.Cancel();
                bwTask.Wait();
            }

            runCancel = new CancellationTokenSource();
            bwTask = Task.Run(() => bwRunner.StartFullTest(threadCount, sharedMode, testType, dataGb, runCancel.Token));
            CancelRunButton.Enabled = true;
            Task.Run(() => HandleBwThreadCompletion(bwTask, SetCancelButtonState));
        }

        private async Task HandleBwThreadCompletion(Task task, SafeSetCancelButtonState setCancelButtonDelegate)
        {
            await task;
            CancelRunButton.Invoke(setCancelButtonDelegate, new object[] { false });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (runCancel != null && bwTask != null)
            {
                runCancel.Cancel();
            }

            SetProgressLabel("Cancel requested, waiting for current test to finish");
        }

        private void CheckWriteModeChange(object sender, EventArgs e)
        {
            if (DataWriteRadioButton.Checked || DataAddRadioButton.Checked)
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

        bool methodSetForInstr = false;

        private void InstructionFetchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (InstructionFetchRadioButton.Checked && !methodSetForInstr)
            {
                this.TestMethodGroupBox.SuspendLayout();
                this.TestMethodGroupBox.Controls.Remove(this.Avx512RadioButton);
                this.TestMethodGroupBox.Controls.Remove(this.AvxRadioButton);
                this.TestMethodGroupBox.Controls.Remove(this.SseRadioButton);
                this.TestMethodGroupBox.Controls.Add(this.FourByteNops);
                this.TestMethodGroupBox.Controls.Add(this.EightByteNops);
                this.TestMethodGroupBox.Controls.Add(this.K8FourByteNops);
                this.TestMethodGroupBox.Controls.Add(this.BranchPer16B);
                this.TestMethodGroupBox.PerformLayout();
                this.EightByteNops.Checked = true;
                this.FourByteNops.Checked = false;
                this.K8FourByteNops.Checked = false;
                this.BranchPer16B.Checked = false;
                methodSetForInstr = true;
            }
            else if (!InstructionFetchRadioButton.Checked && methodSetForInstr)
            {
                this.TestMethodGroupBox.SuspendLayout();
                this.TestMethodGroupBox.Controls.Remove(this.FourByteNops);
                this.TestMethodGroupBox.Controls.Remove(this.EightByteNops);
                this.TestMethodGroupBox.Controls.Remove(this.K8FourByteNops);
                this.TestMethodGroupBox.Controls.Remove(this.BranchPer16B);
                this.TestMethodGroupBox.Controls.Add(this.Avx512RadioButton);
                this.TestMethodGroupBox.Controls.Add(this.AvxRadioButton);
                this.TestMethodGroupBox.Controls.Add(this.SseRadioButton);
                this.TestMethodGroupBox.PerformLayout();
                this.AvxRadioButton.Checked = true;
                this.Avx512RadioButton.Checked = false;
                this.SseRadioButton.Checked = false;
                methodSetForInstr = false;
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
            bool jsFormat = JsFormatRadioButton.Checked;
            string output = "No run yet";
            if (bwRunner != null && bwRunner.testResultsList != null && bwRunner.floatTestPoints != null)
            {
                float[] testPoints = bwRunner.floatTestPoints.ToArray();
                float[] testResults = bwRunner.testResultsList.ToArray();
                if (!jsFormat)
                {
                    output = "Region (KB),Bandwidth (GB/s)";
                }
                else output = "";

                for (int i = 0; i < testPoints.Length && i < testResults.Length; i++)
                {
                    if (!jsFormat)
                    {
                        output += "\r\n" + testPoints[i] + "," + testResults[i];
                    }
                    else
                    {
                        output += testPoints[i] + "," + testResults[i] + " ";
                    } 
                }
            }

            ExportTextBox.Text = output;
        }
    }
}
