using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Management;
using Newtonsoft.Json;
using System.Net.Http;

namespace MicrobenchmarkGui
{
    public partial class BenchmarkSubmissionDialog : Form
    {
        private readonly string testName;
        private readonly List<(float size, float result)> results;
        private readonly BenchmarkSubmission submission;
        private const string SERVER_URL = "https://memrank.reali.es";

        public BenchmarkSubmissionDialog(string testName, List<(float size, float result)> results)
        {
            InitializeComponent();
            this.testName = testName;
            this.results = results;
            this.submission = new BenchmarkSubmission();

            // Set window properties
            this.MinimumSize = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Initialize summary text
            UpdateSummary();

            // Pre-populate system information
            PopulateSystemInfo();
        }

        private void PopulateSystemInfo()
        {
            try
            {
                // Get CPU info
                cpuNameTextBox.Text = OpCode.GetProcessorName() ?? "Unknown CPU";
                
                // Get motherboard info using WMI
                string motherboardInfo = "Unknown Motherboard";
                try 
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
                    {
                        foreach (ManagementObject board in searcher.Get())
                        {
                            string manufacturer = board["Manufacturer"]?.ToString() ?? "";
                            string product = board["Product"]?.ToString() ?? "";
                            motherboardInfo = $"{manufacturer} {product}".Trim();
                            if (string.IsNullOrWhiteSpace(motherboardInfo))
                            {
                                motherboardInfo = "Unknown Motherboard";
                            }
                            break;
                        }
                    }
                }
                catch 
                {
                    // Keep default "Unknown Motherboard" value
                }
                motherboardTextBox.Text = motherboardInfo;

                // Get memory configuration using WMI
                string memoryConfig = "Unknown Memory Configuration";
                try 
                {
                    // First try to get actual running speed from BIOS
                    int currentSpeed = 0;
                    try 
                    {
                        using (var searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSMemory_Performance"))
                        {
                            foreach (ManagementObject obj in searcher.Get())
                            {
                                if (obj["ConfiguredMemoryClockSpeed"] != null)
                                {
                                    currentSpeed = Convert.ToInt32(obj["ConfiguredMemoryClockSpeed"]);
                                    break;
                                }
                            }
                        }
                    }
                    catch 
                    {
                        // If we can't get the actual speed, we'll fall back to rated speed
                    }

                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
                    {
                        var memoryModules = new List<string>();
                        ulong totalCapacity = 0;
                        int moduleCount = 0;

                        foreach (ManagementObject memory in searcher.Get())
                        {
                            moduleCount++;
                            ulong capacity = Convert.ToUInt64(memory["Capacity"]);
                            totalCapacity += capacity;
                            
                            // Use actual speed if we got it, otherwise fall back to rated speed
                            int speed = currentSpeed > 0 ? currentSpeed : 
                                Convert.ToInt32(memory["ConfiguredClockSpeed"] ?? memory["Speed"] ?? 0);
                            
                            string memoryType = GetMemoryType(memory);
                            
                            memoryModules.Add($"{capacity / (1024 * 1024 * 1024)}GB {memoryType}" + 
                                (speed > 0 ? $" @ {speed}MHz" : ""));
                        }

                        if (totalCapacity > 0)
                        {
                            memoryConfig = $"{totalCapacity / (1024 * 1024 * 1024)}GB Total ({moduleCount} modules)";
                            if (memoryModules.Count > 0)
                            {
                                memoryConfig += $" - {string.Join(", ", memoryModules)}";
                            }
                        }
                    }
                }
                catch 
                {
                    // Keep default "Unknown Memory Configuration" value
                }
                memoryConfigTextBox.Text = memoryConfig;

                // Make the fields read-only without edit option
                cpuNameTextBox.ReadOnly = true;
                motherboardTextBox.ReadOnly = true;
                memoryConfigTextBox.ReadOnly = true;
            }
            catch (Exception ex)
            {
                // Set default values if overall detection fails
                cpuNameTextBox.Text = "Unknown CPU";
                motherboardTextBox.Text = "Unknown Motherboard";
                memoryConfigTextBox.Text = "Unknown Memory Configuration";
                
                MessageBox.Show($"Error retrieving system information: {ex.Message}\nDefault values have been set.", 
                    "System Info Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetMemoryType(ManagementObject memory)
        {
            // Try to get detailed memory type
            try
            {
                int memoryType = Convert.ToInt32(memory["SMBIOSMemoryType"]);
                switch (memoryType)
                {
                    case 26: return "DDR4";
                    case 30: return "LPDDR4";
                    case 34: return "DDR5";
                    case 35: return "LPDDR5";
                    default:
                        if (memory["MemoryType"] != null)
                        {
                            return $"DDR{memory["MemoryType"]}";
                        }
                        return "DDR";
                }
            }
            catch
            {
                return "DDR";
            }
        }

        private void UpdateSummary()
        {
            var summary = new StringBuilder();
            summary.AppendLine($"Test: {testName}");
            summary.AppendLine($"Number of data points: {results.Count}");
            
            if (results.Any())
            {
                summary.AppendLine($"Size range: {results.Min(r => r.size):F2} KB to {results.Max(r => r.size):F2} KB");
                summary.AppendLine($"Result range: {results.Min(r => r.result):F2} to {results.Max(r => r.result):F2}");
            }

            summaryTextBox.Text = summary.ToString();
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            if (await SubmitAsync())
            {
                DialogResult = DialogResult.OK;
            }
        }

        public async Task<bool> SubmitAsync()
        {
            // Get values from form controls
            submission.TestName = testName;
            submission.CpuName = cpuNameTextBox.Text;
            submission.MotherboardName = motherboardTextBox.Text;
            submission.MemoryConfig = memoryConfigTextBox.Text;
            submission.Notes = notesTextBox.Text;
            submission.Results = results.Select(r => new float[] { r.size, r.result }).ToArray();

            // Validate required fields
            if (string.IsNullOrWhiteSpace(submission.CpuName) ||
                string.IsNullOrWhiteSpace(submission.MotherboardName) ||
                string.IsNullOrWhiteSpace(submission.MemoryConfig))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                string jsonSubmission = JsonConvert.SerializeObject(submission, Formatting.Indented);

                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonSubmission, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{SERVER_URL}/submit", content);
                    
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Server returned status code: {response.StatusCode}");
                    }

                    var responseJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeAnonymousType(responseJson, new { success = false, url = "", id = 0 });

                    if (result?.success == true && !string.IsNullOrEmpty(result.url))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = $"{SERVER_URL}{result.url}",
                            UseShellExecute = true
                        });
                        return true;
                    }
                    
                    throw new Exception("Invalid response from server");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting results: {ex.Message}", 
                    "Submission Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
            base.OnFormClosing(e);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
