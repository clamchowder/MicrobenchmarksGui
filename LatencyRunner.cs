using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicrobenchmarkGui
{
    public class LatencyRunner
    {
        public uint[] testSizes = { 2, 4, 8, 12, 16, 24, 32, 48, 64, 96, 128, 192, 256, 512, 600, 768, 1024, 1536, 2048,
                               3072, 4096, 5120, 6144, 8192, 10240, 12288, 16384, 24567, 32768, 65536, 98304,
                               131072, 262144, 393216, 524288, 1048576 };

        public bool running = false;

        // last run results
        public string[][] formattedResults;
        public float[] testResults;

        /// <summary>
        /// List of test results from last run
        /// </summary>
        public List<float> testResultsList;

        /// <summary>
        /// List of tested points from last run
        /// </summary>
        public List<float> floatTestPoints;

        private ListView resultListView;
        private Chart resultChart;
        private MicrobenchmarkForm.SafeSetResultListView setListViewDelegate;
        private MicrobenchmarkForm.SafeSetResultListViewColumns setListViewColumnsDelegate;
        private MicrobenchmarkForm.SafeSetResultsChart setChartDelegate;
        private MicrobenchmarkForm.SafeSetProgressLabel setProgressLabelDelegate;
        private Label progressLabel;
        private string[] bwCols = { "Data Size", "Latency" };

        public LatencyRunner(MicrobenchmarkForm.SafeSetResultListView setListViewDelegate,
            MicrobenchmarkForm.SafeSetResultListViewColumns setListViewColsDelegate,
            MicrobenchmarkForm.SafeSetResultsChart setChartDelegate,
            MicrobenchmarkForm.SafeSetProgressLabel setLabelDelegate,
            ListView resultListView,
            Chart resultChart,
            Label progressLabel)
        {
            this.setListViewColumnsDelegate = setListViewColsDelegate;
            this.setListViewDelegate = setListViewDelegate;
            this.setChartDelegate = setChartDelegate;
            this.setProgressLabelDelegate = setLabelDelegate;
            this.resultListView = resultListView;
            this.resultChart = resultChart;
            this.progressLabel = progressLabel;
        }

        // Run through test sizes, meant to be run in a background thread
        public void StartFullTest(bool asm, bool largePages, uint iterations, CancellationToken runCancel)
        {
            testResultsList = new List<float>();
            floatTestPoints = new List<float>();
            resultListView.Invoke(setListViewColumnsDelegate, new object[] { bwCols });
            float[] testResults = new float[testSizes.Length];
            formattedResults = new string[testSizes.Length][];

            for (uint i = 0; i < testSizes.Length; i++)
            {
                testResults[i] = 0;
                formattedResults[i] = new string[2];
                formattedResults[i][0] = string.Format("{0} KB", testSizes[i]);
                formattedResults[i][1] = "Not Run";
            }

            resultListView.Invoke(setListViewDelegate, new object[] { formattedResults });

            if (!largePages)
            {
                BenchmarkFunctions.SetLargePages(0);
            }
            else
            {
                uint maxTestSize = testSizes[testSizes.Length - 1];
                int rc = BenchmarkFunctions.SetLargePages(maxTestSize);
                if (rc == -1)
                {
                    progressLabel.Invoke(setProgressLabelDelegate, 
                        new object[] { "Failed to get SeLockMemoryPrivilege for large lages. See README.md" });
                    return;
                }
                else if (rc == -2)
                {
                    progressLabel.Invoke(setProgressLabelDelegate, 
                        new object[] { "Could not allocate " + maxTestSize + " KB with large pages. If you have enough free memory, try rebooting" });
                    return;
                }
            }


            for (uint testIdx = 0; testIdx < testSizes.Length; testIdx++)
            {
                if (runCancel.IsCancellationRequested)
                {
                    break;
                }

                uint testSize = testSizes[testIdx];
                progressLabel.Invoke(setProgressLabelDelegate, new object[] { "Testing " + testSize + " KB" });
                float result;
                if (asm) result = BenchmarkFunctions.RunAsmLatencyTest(testSize, iterations);
                else result = BenchmarkFunctions.RunLatencyTest(testSize, iterations);
                testResults[testIdx] = result;

                if (result != 0) formattedResults[testIdx][1] = string.Format("{0:F2} ns", result);
                else formattedResults[testIdx][1] = "N/A";
                resultListView.Invoke(setListViewDelegate, new object[] { formattedResults });

                string testLabel = (asm ? "ASM" : "C") + ", " + (largePages ? "Large Pages" : "Default Pages");
                if (result != 0)
                {
                    floatTestPoints.Add(testSize);
                    testResultsList.Add(result);
                    resultChart.Invoke(setChartDelegate, new object[] { testLabel, floatTestPoints.ToArray(), testResultsList.ToArray() });
                }
            }

            progressLabel.Invoke(setProgressLabelDelegate, new object[] { "Run finished" });
        }

        public string GetTestSizesAsString()
        {
            return string.Join(",", testSizes);
        }

        // Shouldn't be called when test is running, but UI will take care of that
        public void SetTestSizes(string input)
        {
            string[] inputArr = input.Split(new char[] { ',' } , StringSplitOptions.RemoveEmptyEntries);
            uint[] newTestSizes = new uint[inputArr.Length];
            for (uint i = 0;i < inputArr.Length; i++)
            {
                newTestSizes[i] = uint.Parse(inputArr[i]);
            }

            testSizes = newTestSizes;
        }
    }
}
