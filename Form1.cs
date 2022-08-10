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
        public MicrobenchmarkForm()
        {
            plottedSeries = new Dictionary<string, Series>();
            InitializeComponent();
            ResultChart.Series.Clear();
        }

        public delegate void SafeSetResultListViewColumns(string[] cols);
        public delegate void SafeSetResultListView(string[][] items);
        public delegate void SafeSetResultsChart(string seriesNames, float[] testPoints, float[] testResults);
        public delegate void SafeSetCancelButtonState(bool enabled);

        public void SetCancelButtonState(bool enabled) { CancelButton.Enabled = enabled; }

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

            foreach(ColumnHeader column in resultListView.Columns)
            {
                column.Width = 75;
                column.TextAlign = HorizontalAlignment.Right;
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

        Dictionary<string, Series> plottedSeries;

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
            ResultChart.ChartAreas[0].AxisX.Maximum = max;

            for (uint i = 0; i < testPoints.Length && i < testResults.Length; i++)
            {
                series.Points.AddXY((double)testPoints[i], (double)testResults[i]);
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
            if (bwRunner == null) bwRunner = new BandwidthRunner(SetResultListView, SetResultListViewColumns, SetResultChart, resultListView, ResultChart, this);
            uint threadCount = uint.Parse(ThreadCountTextBox.Text);
            bool sharedMode = SharedRadioButton.Checked;
            if (runCancel == null)

            if (runCancel != null && bwTask != null)
            {
                runCancel.Cancel();
                bwTask.Wait();
            }

            runCancel = new CancellationTokenSource();
            bwTask = Task.Run(() => bwRunner.StartFullTest(threadCount, sharedMode, BenchmarkFunctions.TestType.AvxRead, runCancel.Token));
            CancelButton.Enabled = true;
            Task.Run(() => HandleBwThreadCompletion(bwTask, SetCancelButtonState));
        }

        private async Task HandleBwThreadCompletion(Task task, SafeSetCancelButtonState setCancelButtonDelegate)
        {
            await task;
            CancelButton.Invoke(setCancelButtonDelegate, new object[] { false });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (runCancel != null && bwTask != null)
            {
                runCancel.Cancel();
            }
        }

        private void PlotButton_Click(object sender, EventArgs e)
        {
            if (bwRunner != null && bwRunner.floatTestPoints != null && bwRunner.testResultsList != null)
            {
                SetResultChart("test", bwRunner.floatTestPoints.ToArray(), bwRunner.testResultsList.ToArray());
            }
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
    }
}
