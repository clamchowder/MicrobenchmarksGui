using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MicrobenchmarkGui
{
    public class BandwidthRunner
    {
        public uint[] testSizes = { 2, 4, 8, 12, 16, 24, 32, 48, 64, 96, 128, 192, 256, 512, 600, 768, 1024, 1536, 2048,
                               3072, 4096, 5120, 6144, 8192, 10240, 12288, 16384, 24567, 32768, 65536, 98304,
                               131072, 262144, 393216, 524288, 1048576, 1572864, 2097152, 3145728 };

        public bool running = false;

        /// <summary>
        /// Test type to run, for automated test
        /// </summary>
        public BenchmarkInteropFunctions.TestType testType;

        // run results
        public Dictionary<string, List<Tuple<float, float>>> RunResults;

        // last run results
        public string[][] formattedResults;

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
        private string[] bwCols = { "Data Size", "Bandwidth" };

        public BandwidthRunner(MicrobenchmarkForm.SafeSetResultListView setListViewDelegate,
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

            this.RunResults = new Dictionary<string, List<Tuple<float, float>>>();
        }

        private uint GetIterationCount(uint testSize, uint dataGb)
        {
            uint gbToTransfer = dataGb;
            if (testSize > 64) gbToTransfer = dataGb / 2;
            if (testSize > 512) gbToTransfer = dataGb / 4;
            if (testSize > 8192) gbToTransfer = dataGb / 8;
            uint iterations = gbToTransfer * 1024 * 1024 / testSize;
            if (iterations % 2 != 0) iterations += 1;

            if (iterations < 4) return 4; // Set a minimum to reduce noise
            else return iterations;
        }

        // Run through test sizes, meant to be run in a background thread
        public void StartFullTest(uint threads, bool shared, BenchmarkInteropFunctions.TestType testType, CancellationToken runCancel)
        {
            running = true;
            string testLabel = threads + "T " + testType.ToString();
            List<Tuple<float, float>> currentRunResults = new List<Tuple<float, float>>();
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

            float lastTimeMs = 0;
            for (uint testIdx = 0; testIdx < testSizes.Length; testIdx++)
            {
                if (runCancel.IsCancellationRequested)
                {
                    break;
                }

                uint testSize = testSizes[testIdx];
                ulong currentIterations = GetIterationCount(testSize, 32);
                float targetTimeMs = 3000, minTimeMs = 1000, result;

                if (GlobalTestSettings.MinTestSizeKb != 0 && GlobalTestSettings.MinTestSizeKb > testSize) continue;

                Stopwatch debugStopwatch = new Stopwatch();

                do {
                    float dataTransferredGb = (float)((currentIterations * testSize * 1024.0 * threads) / 1e9);
                    string progressMessage = string.Format("Testing bandwidth over {0} KB, {1}K iterations = {2:F2} GB, last run = {3:F2} ms", testSize, currentIterations / 1000, dataTransferredGb, lastTimeMs);
                    progressLabel.Invoke(setProgressLabelDelegate, new object[] { progressMessage });

                    debugStopwatch.Restart();
                    result = BenchmarkInteropFunctions.MeasureBw(testSize, currentIterations, threads, shared ? 1 : 0, testType);
                    debugStopwatch.Stop();

                    lastTimeMs = 1000 * dataTransferredGb / result;
                    currentIterations = TestUtilities.ScaleIterations(currentIterations, targetTimeMs, lastTimeMs);
                    Console.WriteLine("Reported {0:F2} ms, sw {1} ms. Next Iteration Count: {2}", lastTimeMs, debugStopwatch.ElapsedMilliseconds, currentIterations);
                } while (lastTimeMs < minTimeMs);

                testResults[testIdx] = result;
                if (result != 0) formattedResults[testIdx][1] = string.Format("{0:F2} GB/s", result);
                else formattedResults[testIdx][1] = "N/A";
                resultListView.Invoke(setListViewDelegate, new object[] { formattedResults });

                if (result != 0)
                {
                    floatTestPoints.Add(testSize);
                    testResultsList.Add(result);
                    currentRunResults.Add(new Tuple<float, float>(testSize, result));
                    resultChart.Invoke(setChartDelegate, new object[] { testLabel, floatTestPoints.ToArray(), testResultsList.ToArray(), MicrobenchmarkForm.ResultChartType.CpuMemoryBandwidth });
                }
            }

            progressLabel.Invoke(setProgressLabelDelegate, new object[] { "Run finished" });
            running = false;
            RunResults.Add(testLabel, currentRunResults);
        }

        // Run a single test size, meant to be run in a background thread
        public void RunSingleTest(uint sizeKb, uint threads, bool shared, BenchmarkInteropFunctions.TestType testType)
        {
            running = true;
            float result = BenchmarkInteropFunctions.MeasureBw(sizeKb, GetIterationCount(sizeKb, 512), threads, shared ? 1 : 0, testType);
            resultListView.Invoke(setListViewColumnsDelegate, new object[] { bwCols });
            string[][] formattedResults = new string[1][];
            formattedResults[0] = new string[2];
            formattedResults[0][0] = sizeKb + " KB";
            formattedResults[0][1] = result + " GB/s";
            resultListView.Invoke(setListViewDelegate, new object[] { formattedResults });
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
