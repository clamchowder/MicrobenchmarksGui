
namespace MicrobenchmarkGui
{
    partial class MicrobenchmarkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.RunBandwidthTestButton = new System.Windows.Forms.Button();
            this.resultListView = new System.Windows.Forms.ListView();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.ResultsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ThreadCountLabel = new System.Windows.Forms.Label();
            this.ThreadingModeGroupBox = new System.Windows.Forms.GroupBox();
            this.SharedRadioButton = new System.Windows.Forms.RadioButton();
            this.PrivateRadioButton = new System.Windows.Forms.RadioButton();
            this.CancelRunButton = new System.Windows.Forms.Button();
            this.AccessModeGroupBox = new System.Windows.Forms.GroupBox();
            this.DataMicrocodedRadioButton = new System.Windows.Forms.RadioButton();
            this.DataNtWriteRadioButton = new System.Windows.Forms.RadioButton();
            this.InstructionFetchRadioButton = new System.Windows.Forms.RadioButton();
            this.DataAddRadioButton = new System.Windows.Forms.RadioButton();
            this.DataWriteRadioButton = new System.Windows.Forms.RadioButton();
            this.DataReadRadioButton = new System.Windows.Forms.RadioButton();
            this.TestMethodGroupBox = new System.Windows.Forms.GroupBox();
            this.BandwidthTestMethodFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SseRadioButton = new System.Windows.Forms.RadioButton();
            this.AvxRadioButton = new System.Windows.Forms.RadioButton();
            this.Avx512RadioButton = new System.Windows.Forms.RadioButton();
            this.MmxRadioButton = new System.Windows.Forms.RadioButton();
            this.progressLabel = new System.Windows.Forms.Label();
            this.ThreadCountTrackbar = new System.Windows.Forms.TrackBar();
            this.ExportExcelButton = new System.Windows.Forms.Button();
            this.ExportTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TestRunLabel = new System.Windows.Forms.Label();
            this.ExportListBox = new System.Windows.Forms.ListBox();
            this.JsFormatRadioButton = new System.Windows.Forms.RadioButton();
            this.CsvFormatRadioButton = new System.Windows.Forms.RadioButton();
            this.ClearChartButton = new System.Windows.Forms.Button();
            this.ChartControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.DefaultNextColorRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ColorBBox = new System.Windows.Forms.TextBox();
            this.ColorGBox = new System.Windows.Forms.TextBox();
            this.GLabel = new System.Windows.Forms.Label();
            this.ColorRBox = new System.Windows.Forms.TextBox();
            this.RLabel = new System.Windows.Forms.Label();
            this.SpecifyNextColorRadioButton = new System.Windows.Forms.RadioButton();
            this.RandomizeNextColorRadioButton = new System.Windows.Forms.RadioButton();
            this.TestSelectTabControl = new System.Windows.Forms.TabControl();
            this.MemoryBandwidthTab = new System.Windows.Forms.TabPage();
            this.CpuMemoryBandwidthLabel = new System.Windows.Forms.Label();
            this.MemoryLatencyTab = new System.Windows.Forms.TabPage();
            this.CpuMemoryLatencyLabel = new System.Windows.Forms.Label();
            this.MemoryLatencyPagingModeGroupBox = new System.Windows.Forms.GroupBox();
            this.MemoryLatencyLargePagesRadioButton = new System.Windows.Forms.RadioButton();
            this.MemoryLatencyDefaultPagesRadioButton = new System.Windows.Forms.RadioButton();
            this.MemoryLatencyAddressingModeGroupBox = new System.Windows.Forms.GroupBox();
            this.MemoryLatencyAsmRadioButton = new System.Windows.Forms.RadioButton();
            this.MemoryLatencyIndexedAddressingRadioButton = new System.Windows.Forms.RadioButton();
            this.GpuMemLatencyTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.GpuMemoryLatencyDeviceGroupBox = new System.Windows.Forms.GroupBox();
            this.GpuMemoryLatencyDeviceFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.GpuMemoryLatencyMemoryPathGroupBox = new System.Windows.Forms.GroupBox();
            this.GpuMemoryLatencyLocalRadioButton = new System.Windows.Forms.RadioButton();
            this.GpuMemoryLatencyTextureRadioButton = new System.Windows.Forms.RadioButton();
            this.GpuMemoryLatencyConstantScalarRadioButton = new System.Windows.Forms.RadioButton();
            this.GpuMemoryLatencyVectorRadioButton = new System.Windows.Forms.RadioButton();
            this.GpuMemoryLatencyScalarRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GpuPointerChasingStrideLabel = new System.Windows.Forms.Label();
            this.GpuPointerChasingStrideTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsChart)).BeginInit();
            this.ThreadingModeGroupBox.SuspendLayout();
            this.AccessModeGroupBox.SuspendLayout();
            this.TestMethodGroupBox.SuspendLayout();
            this.BandwidthTestMethodFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadCountTrackbar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.ChartControlsGroupBox.SuspendLayout();
            this.TestSelectTabControl.SuspendLayout();
            this.MemoryBandwidthTab.SuspendLayout();
            this.MemoryLatencyTab.SuspendLayout();
            this.MemoryLatencyPagingModeGroupBox.SuspendLayout();
            this.MemoryLatencyAddressingModeGroupBox.SuspendLayout();
            this.GpuMemLatencyTab.SuspendLayout();
            this.GpuMemoryLatencyDeviceGroupBox.SuspendLayout();
            this.GpuMemoryLatencyMemoryPathGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RunBandwidthTestButton
            // 
            this.RunBandwidthTestButton.Location = new System.Drawing.Point(7, 695);
            this.RunBandwidthTestButton.Name = "RunBandwidthTestButton";
            this.RunBandwidthTestButton.Size = new System.Drawing.Size(75, 23);
            this.RunBandwidthTestButton.TabIndex = 3;
            this.RunBandwidthTestButton.Text = "Run";
            this.RunBandwidthTestButton.UseVisualStyleBackColor = true;
            this.RunBandwidthTestButton.Click += new System.EventHandler(this.RunBandwidthTestButton_Click);
            // 
            // resultListView
            // 
            this.resultListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.resultListView.HideSelection = false;
            this.resultListView.Location = new System.Drawing.Point(298, 30);
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(195, 688);
            this.resultListView.TabIndex = 4;
            this.resultListView.UseCompatibleStateImageBehavior = false;
            this.resultListView.View = System.Windows.Forms.View.Details;
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(295, 12);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(74, 13);
            this.ResultLabel.TabIndex = 5;
            this.ResultLabel.Text = "Run Progress:";
            // 
            // ResultsChart
            // 
            this.ResultsChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.ResultsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ResultsChart.Legends.Add(legend1);
            this.ResultsChart.Location = new System.Drawing.Point(499, 159);
            this.ResultsChart.Name = "ResultsChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ResultsChart.Series.Add(series1);
            this.ResultsChart.Size = new System.Drawing.Size(764, 559);
            this.ResultsChart.TabIndex = 6;
            this.ResultsChart.Text = "chart1";
            // 
            // ThreadCountLabel
            // 
            this.ThreadCountLabel.AutoSize = true;
            this.ThreadCountLabel.Location = new System.Drawing.Point(3, 37);
            this.ThreadCountLabel.Name = "ThreadCountLabel";
            this.ThreadCountLabel.Size = new System.Drawing.Size(58, 13);
            this.ThreadCountLabel.TabIndex = 8;
            this.ThreadCountLabel.Text = "Threads: 1";
            // 
            // ThreadingModeGroupBox
            // 
            this.ThreadingModeGroupBox.Controls.Add(this.SharedRadioButton);
            this.ThreadingModeGroupBox.Controls.Add(this.PrivateRadioButton);
            this.ThreadingModeGroupBox.Location = new System.Drawing.Point(9, 107);
            this.ThreadingModeGroupBox.Name = "ThreadingModeGroupBox";
            this.ThreadingModeGroupBox.Size = new System.Drawing.Size(227, 70);
            this.ThreadingModeGroupBox.TabIndex = 9;
            this.ThreadingModeGroupBox.TabStop = false;
            this.ThreadingModeGroupBox.Text = "Threading Mode";
            // 
            // SharedRadioButton
            // 
            this.SharedRadioButton.AutoSize = true;
            this.SharedRadioButton.Location = new System.Drawing.Point(7, 44);
            this.SharedRadioButton.Name = "SharedRadioButton";
            this.SharedRadioButton.Size = new System.Drawing.Size(171, 17);
            this.SharedRadioButton.TabIndex = 1;
            this.SharedRadioButton.Text = "One array shared by all threads";
            this.SharedRadioButton.UseVisualStyleBackColor = true;
            // 
            // PrivateRadioButton
            // 
            this.PrivateRadioButton.AutoSize = true;
            this.PrivateRadioButton.Checked = true;
            this.PrivateRadioButton.Location = new System.Drawing.Point(7, 21);
            this.PrivateRadioButton.Name = "PrivateRadioButton";
            this.PrivateRadioButton.Size = new System.Drawing.Size(135, 17);
            this.PrivateRadioButton.TabIndex = 0;
            this.PrivateRadioButton.TabStop = true;
            this.PrivateRadioButton.Text = "Private array per thread";
            this.PrivateRadioButton.UseVisualStyleBackColor = true;
            // 
            // CancelRunButton
            // 
            this.CancelRunButton.Enabled = false;
            this.CancelRunButton.Location = new System.Drawing.Point(88, 695);
            this.CancelRunButton.Name = "CancelRunButton";
            this.CancelRunButton.Size = new System.Drawing.Size(75, 23);
            this.CancelRunButton.TabIndex = 10;
            this.CancelRunButton.Text = "Cancel Run";
            this.CancelRunButton.UseVisualStyleBackColor = true;
            this.CancelRunButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AccessModeGroupBox
            // 
            this.AccessModeGroupBox.Controls.Add(this.DataMicrocodedRadioButton);
            this.AccessModeGroupBox.Controls.Add(this.DataNtWriteRadioButton);
            this.AccessModeGroupBox.Controls.Add(this.InstructionFetchRadioButton);
            this.AccessModeGroupBox.Controls.Add(this.DataAddRadioButton);
            this.AccessModeGroupBox.Controls.Add(this.DataWriteRadioButton);
            this.AccessModeGroupBox.Controls.Add(this.DataReadRadioButton);
            this.AccessModeGroupBox.Location = new System.Drawing.Point(9, 183);
            this.AccessModeGroupBox.Name = "AccessModeGroupBox";
            this.AccessModeGroupBox.Size = new System.Drawing.Size(227, 160);
            this.AccessModeGroupBox.TabIndex = 14;
            this.AccessModeGroupBox.TabStop = false;
            this.AccessModeGroupBox.Text = "Access Mode";
            // 
            // DataMicrocodedRadioButton
            // 
            this.DataMicrocodedRadioButton.AutoSize = true;
            this.DataMicrocodedRadioButton.Location = new System.Drawing.Point(7, 42);
            this.DataMicrocodedRadioButton.Name = "DataMicrocodedRadioButton";
            this.DataMicrocodedRadioButton.Size = new System.Drawing.Size(162, 17);
            this.DataMicrocodedRadioButton.TabIndex = 5;
            this.DataMicrocodedRadioButton.Text = "Data, Microcoded String Ops";
            this.DataMicrocodedRadioButton.UseVisualStyleBackColor = true;
            this.DataMicrocodedRadioButton.CheckedChanged += new System.EventHandler(this.InstructionFetchRadioButton_CheckedChanged);
            // 
            // DataNtWriteRadioButton
            // 
            this.DataNtWriteRadioButton.AutoSize = true;
            this.DataNtWriteRadioButton.Location = new System.Drawing.Point(7, 88);
            this.DataNtWriteRadioButton.Name = "DataNtWriteRadioButton";
            this.DataNtWriteRadioButton.Size = new System.Drawing.Size(146, 17);
            this.DataNtWriteRadioButton.TabIndex = 4;
            this.DataNtWriteRadioButton.Text = "Data Non-Temporal Write";
            this.DataNtWriteRadioButton.UseVisualStyleBackColor = true;
            this.DataNtWriteRadioButton.CheckedChanged += new System.EventHandler(this.CheckWriteModeChange);
            // 
            // InstructionFetchRadioButton
            // 
            this.InstructionFetchRadioButton.AutoSize = true;
            this.InstructionFetchRadioButton.Location = new System.Drawing.Point(7, 134);
            this.InstructionFetchRadioButton.Name = "InstructionFetchRadioButton";
            this.InstructionFetchRadioButton.Size = new System.Drawing.Size(104, 17);
            this.InstructionFetchRadioButton.TabIndex = 3;
            this.InstructionFetchRadioButton.Text = "Instruction Fetch";
            this.InstructionFetchRadioButton.UseVisualStyleBackColor = true;
            this.InstructionFetchRadioButton.CheckedChanged += new System.EventHandler(this.InstructionFetchRadioButton_CheckedChanged);
            // 
            // DataAddRadioButton
            // 
            this.DataAddRadioButton.AutoSize = true;
            this.DataAddRadioButton.Location = new System.Drawing.Point(7, 111);
            this.DataAddRadioButton.Name = "DataAddRadioButton";
            this.DataAddRadioButton.Size = new System.Drawing.Size(167, 17);
            this.DataAddRadioButton.TabIndex = 2;
            this.DataAddRadioButton.Text = "Data Read-Modify-Write (Add)";
            this.DataAddRadioButton.UseVisualStyleBackColor = true;
            this.DataAddRadioButton.CheckedChanged += new System.EventHandler(this.CheckWriteModeChange);
            // 
            // DataWriteRadioButton
            // 
            this.DataWriteRadioButton.AutoSize = true;
            this.DataWriteRadioButton.Location = new System.Drawing.Point(7, 66);
            this.DataWriteRadioButton.Name = "DataWriteRadioButton";
            this.DataWriteRadioButton.Size = new System.Drawing.Size(76, 17);
            this.DataWriteRadioButton.TabIndex = 1;
            this.DataWriteRadioButton.Text = "Data Write";
            this.DataWriteRadioButton.UseVisualStyleBackColor = true;
            this.DataWriteRadioButton.CheckedChanged += new System.EventHandler(this.CheckWriteModeChange);
            // 
            // DataReadRadioButton
            // 
            this.DataReadRadioButton.AutoSize = true;
            this.DataReadRadioButton.Checked = true;
            this.DataReadRadioButton.Location = new System.Drawing.Point(7, 19);
            this.DataReadRadioButton.Name = "DataReadRadioButton";
            this.DataReadRadioButton.Size = new System.Drawing.Size(77, 17);
            this.DataReadRadioButton.TabIndex = 0;
            this.DataReadRadioButton.TabStop = true;
            this.DataReadRadioButton.Text = "Data Read";
            this.DataReadRadioButton.UseVisualStyleBackColor = true;
            // 
            // TestMethodGroupBox
            // 
            this.TestMethodGroupBox.Controls.Add(this.BandwidthTestMethodFlowLayoutPanel);
            this.TestMethodGroupBox.Location = new System.Drawing.Point(9, 349);
            this.TestMethodGroupBox.Name = "TestMethodGroupBox";
            this.TestMethodGroupBox.Size = new System.Drawing.Size(227, 174);
            this.TestMethodGroupBox.TabIndex = 15;
            this.TestMethodGroupBox.TabStop = false;
            this.TestMethodGroupBox.Text = "Test Method";
            // 
            // BandwidthTestMethodFlowLayoutPanel
            // 
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.SseRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.AvxRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.Avx512RadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.Controls.Add(this.MmxRadioButton);
            this.BandwidthTestMethodFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.BandwidthTestMethodFlowLayoutPanel.Location = new System.Drawing.Point(6, 19);
            this.BandwidthTestMethodFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.BandwidthTestMethodFlowLayoutPanel.Name = "BandwidthTestMethodFlowLayoutPanel";
            this.BandwidthTestMethodFlowLayoutPanel.Size = new System.Drawing.Size(215, 149);
            this.BandwidthTestMethodFlowLayoutPanel.TabIndex = 4;
            // 
            // SseRadioButton
            // 
            this.SseRadioButton.AutoSize = true;
            this.SseRadioButton.Location = new System.Drawing.Point(3, 3);
            this.SseRadioButton.Name = "SseRadioButton";
            this.SseRadioButton.Size = new System.Drawing.Size(87, 17);
            this.SseRadioButton.TabIndex = 0;
            this.SseRadioButton.Text = "SSE (128-bit)";
            this.SseRadioButton.UseVisualStyleBackColor = true;
            // 
            // AvxRadioButton
            // 
            this.AvxRadioButton.AutoSize = true;
            this.AvxRadioButton.Checked = true;
            this.AvxRadioButton.Location = new System.Drawing.Point(3, 26);
            this.AvxRadioButton.Name = "AvxRadioButton";
            this.AvxRadioButton.Size = new System.Drawing.Size(87, 17);
            this.AvxRadioButton.TabIndex = 1;
            this.AvxRadioButton.TabStop = true;
            this.AvxRadioButton.Text = "AVX (256-bit)";
            this.AvxRadioButton.UseVisualStyleBackColor = true;
            // 
            // Avx512RadioButton
            // 
            this.Avx512RadioButton.AutoSize = true;
            this.Avx512RadioButton.Location = new System.Drawing.Point(3, 49);
            this.Avx512RadioButton.Name = "Avx512RadioButton";
            this.Avx512RadioButton.Size = new System.Drawing.Size(108, 17);
            this.Avx512RadioButton.TabIndex = 2;
            this.Avx512RadioButton.Text = "AVX-512 (512-bit)";
            this.Avx512RadioButton.UseVisualStyleBackColor = true;
            // 
            // MmxRadioButton
            // 
            this.MmxRadioButton.AutoSize = true;
            this.MmxRadioButton.Location = new System.Drawing.Point(3, 72);
            this.MmxRadioButton.Name = "MmxRadioButton";
            this.MmxRadioButton.Size = new System.Drawing.Size(85, 17);
            this.MmxRadioButton.TabIndex = 3;
            this.MmxRadioButton.TabStop = true;
            this.MmxRadioButton.Text = "MMX (64-bit)";
            this.MmxRadioButton.UseVisualStyleBackColor = true;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(375, 12);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(61, 13);
            this.progressLabel.TabIndex = 16;
            this.progressLabel.Text = "Not Started";
            // 
            // ThreadCountTrackbar
            // 
            this.ThreadCountTrackbar.LargeChange = 2;
            this.ThreadCountTrackbar.Location = new System.Drawing.Point(6, 56);
            this.ThreadCountTrackbar.Maximum = 4;
            this.ThreadCountTrackbar.Minimum = 1;
            this.ThreadCountTrackbar.Name = "ThreadCountTrackbar";
            this.ThreadCountTrackbar.Size = new System.Drawing.Size(230, 45);
            this.ThreadCountTrackbar.TabIndex = 18;
            this.ThreadCountTrackbar.Value = 1;
            this.ThreadCountTrackbar.Scroll += new System.EventHandler(this.ThreadCountTrackbar_Scroll);
            // 
            // ExportExcelButton
            // 
            this.ExportExcelButton.Location = new System.Drawing.Point(7, 67);
            this.ExportExcelButton.Name = "ExportExcelButton";
            this.ExportExcelButton.Size = new System.Drawing.Size(71, 23);
            this.ExportExcelButton.TabIndex = 23;
            this.ExportExcelButton.Text = "Export";
            this.ExportExcelButton.UseVisualStyleBackColor = true;
            this.ExportExcelButton.Click += new System.EventHandler(this.ExportExcelButton_Click);
            // 
            // ExportTextBox
            // 
            this.ExportTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportTextBox.Location = new System.Drawing.Point(310, 10);
            this.ExportTextBox.Multiline = true;
            this.ExportTextBox.Name = "ExportTextBox";
            this.ExportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExportTextBox.Size = new System.Drawing.Size(212, 107);
            this.ExportTextBox.TabIndex = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TestRunLabel);
            this.groupBox2.Controls.Add(this.ExportListBox);
            this.groupBox2.Controls.Add(this.JsFormatRadioButton);
            this.groupBox2.Controls.Add(this.CsvFormatRadioButton);
            this.groupBox2.Controls.Add(this.ExportTextBox);
            this.groupBox2.Controls.Add(this.ExportExcelButton);
            this.groupBox2.Location = new System.Drawing.Point(735, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 123);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export";
            // 
            // TestRunLabel
            // 
            this.TestRunLabel.AutoSize = true;
            this.TestRunLabel.Location = new System.Drawing.Point(108, 17);
            this.TestRunLabel.Name = "TestRunLabel";
            this.TestRunLabel.Size = new System.Drawing.Size(87, 13);
            this.TestRunLabel.TabIndex = 27;
            this.TestRunLabel.Text = "Select Test Run:";
            // 
            // ExportListBox
            // 
            this.ExportListBox.FormattingEnabled = true;
            this.ExportListBox.Location = new System.Drawing.Point(108, 36);
            this.ExportListBox.Name = "ExportListBox";
            this.ExportListBox.Size = new System.Drawing.Size(196, 82);
            this.ExportListBox.TabIndex = 26;
            // 
            // JsFormatRadioButton
            // 
            this.JsFormatRadioButton.AutoSize = true;
            this.JsFormatRadioButton.Location = new System.Drawing.Point(7, 44);
            this.JsFormatRadioButton.Name = "JsFormatRadioButton";
            this.JsFormatRadioButton.Size = new System.Drawing.Size(95, 17);
            this.JsFormatRadioButton.TabIndex = 25;
            this.JsFormatRadioButton.TabStop = true;
            this.JsFormatRadioButton.Text = "CnC JS Format";
            this.JsFormatRadioButton.UseVisualStyleBackColor = true;
            // 
            // CsvFormatRadioButton
            // 
            this.CsvFormatRadioButton.AutoSize = true;
            this.CsvFormatRadioButton.Checked = true;
            this.CsvFormatRadioButton.Location = new System.Drawing.Point(7, 21);
            this.CsvFormatRadioButton.Name = "CsvFormatRadioButton";
            this.CsvFormatRadioButton.Size = new System.Drawing.Size(81, 17);
            this.CsvFormatRadioButton.TabIndex = 24;
            this.CsvFormatRadioButton.TabStop = true;
            this.CsvFormatRadioButton.Text = "CSV Format";
            this.CsvFormatRadioButton.UseVisualStyleBackColor = true;
            // 
            // ClearChartButton
            // 
            this.ClearChartButton.Location = new System.Drawing.Point(7, 94);
            this.ClearChartButton.Name = "ClearChartButton";
            this.ClearChartButton.Size = new System.Drawing.Size(86, 23);
            this.ClearChartButton.TabIndex = 27;
            this.ClearChartButton.Text = "Clear Results";
            this.ClearChartButton.UseVisualStyleBackColor = true;
            this.ClearChartButton.Click += new System.EventHandler(this.ClearChartButton_Click);
            // 
            // ChartControlsGroupBox
            // 
            this.ChartControlsGroupBox.Controls.Add(this.DefaultNextColorRadioButton);
            this.ChartControlsGroupBox.Controls.Add(this.label1);
            this.ChartControlsGroupBox.Controls.Add(this.ColorBBox);
            this.ChartControlsGroupBox.Controls.Add(this.ColorGBox);
            this.ChartControlsGroupBox.Controls.Add(this.GLabel);
            this.ChartControlsGroupBox.Controls.Add(this.ColorRBox);
            this.ChartControlsGroupBox.Controls.Add(this.RLabel);
            this.ChartControlsGroupBox.Controls.Add(this.SpecifyNextColorRadioButton);
            this.ChartControlsGroupBox.Controls.Add(this.RandomizeNextColorRadioButton);
            this.ChartControlsGroupBox.Controls.Add(this.ClearChartButton);
            this.ChartControlsGroupBox.Location = new System.Drawing.Point(499, 30);
            this.ChartControlsGroupBox.Name = "ChartControlsGroupBox";
            this.ChartControlsGroupBox.Size = new System.Drawing.Size(230, 123);
            this.ChartControlsGroupBox.TabIndex = 28;
            this.ChartControlsGroupBox.TabStop = false;
            this.ChartControlsGroupBox.Text = "Chart Controls";
            // 
            // DefaultNextColorRadioButton
            // 
            this.DefaultNextColorRadioButton.AutoSize = true;
            this.DefaultNextColorRadioButton.Checked = true;
            this.DefaultNextColorRadioButton.Location = new System.Drawing.Point(7, 68);
            this.DefaultNextColorRadioButton.Name = "DefaultNextColorRadioButton";
            this.DefaultNextColorRadioButton.Size = new System.Drawing.Size(111, 17);
            this.DefaultNextColorRadioButton.TabIndex = 36;
            this.DefaultNextColorRadioButton.TabStop = true;
            this.DefaultNextColorRadioButton.Text = "Default Next Color";
            this.DefaultNextColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Blue";
            // 
            // ColorBBox
            // 
            this.ColorBBox.Enabled = false;
            this.ColorBBox.Location = new System.Drawing.Point(195, 65);
            this.ColorBBox.Name = "ColorBBox";
            this.ColorBBox.Size = new System.Drawing.Size(24, 20);
            this.ColorBBox.TabIndex = 34;
            this.ColorBBox.Text = "50";
            // 
            // ColorGBox
            // 
            this.ColorGBox.Enabled = false;
            this.ColorGBox.Location = new System.Drawing.Point(195, 39);
            this.ColorGBox.Name = "ColorGBox";
            this.ColorGBox.Size = new System.Drawing.Size(24, 20);
            this.ColorGBox.TabIndex = 33;
            this.ColorGBox.Text = "50";
            // 
            // GLabel
            // 
            this.GLabel.AutoSize = true;
            this.GLabel.Location = new System.Drawing.Point(145, 42);
            this.GLabel.Name = "GLabel";
            this.GLabel.Size = new System.Drawing.Size(36, 13);
            this.GLabel.TabIndex = 32;
            this.GLabel.Text = "Green";
            // 
            // ColorRBox
            // 
            this.ColorRBox.Enabled = false;
            this.ColorRBox.Location = new System.Drawing.Point(195, 13);
            this.ColorRBox.Name = "ColorRBox";
            this.ColorRBox.Size = new System.Drawing.Size(24, 20);
            this.ColorRBox.TabIndex = 31;
            this.ColorRBox.Text = "50";
            // 
            // RLabel
            // 
            this.RLabel.AutoSize = true;
            this.RLabel.Location = new System.Drawing.Point(145, 16);
            this.RLabel.Name = "RLabel";
            this.RLabel.Size = new System.Drawing.Size(27, 13);
            this.RLabel.TabIndex = 30;
            this.RLabel.Text = "Red";
            // 
            // SpecifyNextColorRadioButton
            // 
            this.SpecifyNextColorRadioButton.AutoSize = true;
            this.SpecifyNextColorRadioButton.Location = new System.Drawing.Point(7, 44);
            this.SpecifyNextColorRadioButton.Name = "SpecifyNextColorRadioButton";
            this.SpecifyNextColorRadioButton.Size = new System.Drawing.Size(112, 17);
            this.SpecifyNextColorRadioButton.TabIndex = 29;
            this.SpecifyNextColorRadioButton.Text = "Specify Next Color";
            this.SpecifyNextColorRadioButton.UseVisualStyleBackColor = true;
            this.SpecifyNextColorRadioButton.CheckedChanged += new System.EventHandler(this.specifyNextColorRadioButton_CheckedChanged);
            // 
            // RandomizeNextColorRadioButton
            // 
            this.RandomizeNextColorRadioButton.AutoSize = true;
            this.RandomizeNextColorRadioButton.Location = new System.Drawing.Point(7, 20);
            this.RandomizeNextColorRadioButton.Name = "RandomizeNextColorRadioButton";
            this.RandomizeNextColorRadioButton.Size = new System.Drawing.Size(117, 17);
            this.RandomizeNextColorRadioButton.TabIndex = 28;
            this.RandomizeNextColorRadioButton.Text = "Random Next Color";
            this.RandomizeNextColorRadioButton.UseVisualStyleBackColor = true;
            // 
            // TestSelectTabControl
            // 
            this.TestSelectTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TestSelectTabControl.Controls.Add(this.MemoryBandwidthTab);
            this.TestSelectTabControl.Controls.Add(this.MemoryLatencyTab);
            this.TestSelectTabControl.Controls.Add(this.GpuMemLatencyTab);
            this.TestSelectTabControl.Location = new System.Drawing.Point(10, 12);
            this.TestSelectTabControl.Multiline = true;
            this.TestSelectTabControl.Name = "TestSelectTabControl";
            this.TestSelectTabControl.SelectedIndex = 0;
            this.TestSelectTabControl.Size = new System.Drawing.Size(269, 632);
            this.TestSelectTabControl.TabIndex = 30;
            this.TestSelectTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.TestSelectTabControl_Selected);
            // 
            // MemoryBandwidthTab
            // 
            this.MemoryBandwidthTab.BackColor = System.Drawing.SystemColors.Control;
            this.MemoryBandwidthTab.Controls.Add(this.CpuMemoryBandwidthLabel);
            this.MemoryBandwidthTab.Controls.Add(this.ThreadCountLabel);
            this.MemoryBandwidthTab.Controls.Add(this.ThreadCountTrackbar);
            this.MemoryBandwidthTab.Controls.Add(this.ThreadingModeGroupBox);
            this.MemoryBandwidthTab.Controls.Add(this.AccessModeGroupBox);
            this.MemoryBandwidthTab.Controls.Add(this.TestMethodGroupBox);
            this.MemoryBandwidthTab.Location = new System.Drawing.Point(23, 4);
            this.MemoryBandwidthTab.Name = "MemoryBandwidthTab";
            this.MemoryBandwidthTab.Padding = new System.Windows.Forms.Padding(3);
            this.MemoryBandwidthTab.Size = new System.Drawing.Size(242, 584);
            this.MemoryBandwidthTab.TabIndex = 0;
            this.MemoryBandwidthTab.Text = "CPU Memory Bandwidth";
            // 
            // CpuMemoryBandwidthLabel
            // 
            this.CpuMemoryBandwidthLabel.Location = new System.Drawing.Point(3, 3);
            this.CpuMemoryBandwidthLabel.Name = "CpuMemoryBandwidthLabel";
            this.CpuMemoryBandwidthLabel.Size = new System.Drawing.Size(236, 34);
            this.CpuMemoryBandwidthLabel.TabIndex = 19;
            this.CpuMemoryBandwidthLabel.Text = "Tests CPU cache and memory bandwidth using a linear access pattern";
            // 
            // MemoryLatencyTab
            // 
            this.MemoryLatencyTab.BackColor = System.Drawing.SystemColors.Control;
            this.MemoryLatencyTab.Controls.Add(this.CpuMemoryLatencyLabel);
            this.MemoryLatencyTab.Controls.Add(this.MemoryLatencyPagingModeGroupBox);
            this.MemoryLatencyTab.Controls.Add(this.MemoryLatencyAddressingModeGroupBox);
            this.MemoryLatencyTab.Location = new System.Drawing.Point(23, 4);
            this.MemoryLatencyTab.Name = "MemoryLatencyTab";
            this.MemoryLatencyTab.Padding = new System.Windows.Forms.Padding(3);
            this.MemoryLatencyTab.Size = new System.Drawing.Size(242, 584);
            this.MemoryLatencyTab.TabIndex = 1;
            this.MemoryLatencyTab.Text = "CPU Memory Latency";
            // 
            // CpuMemoryLatencyLabel
            // 
            this.CpuMemoryLatencyLabel.Location = new System.Drawing.Point(3, 3);
            this.CpuMemoryLatencyLabel.Name = "CpuMemoryLatencyLabel";
            this.CpuMemoryLatencyLabel.Size = new System.Drawing.Size(232, 32);
            this.CpuMemoryLatencyLabel.TabIndex = 2;
            this.CpuMemoryLatencyLabel.Text = "Tests CPU memory latency using random, dependent accesses";
            // 
            // MemoryLatencyPagingModeGroupBox
            // 
            this.MemoryLatencyPagingModeGroupBox.Controls.Add(this.MemoryLatencyLargePagesRadioButton);
            this.MemoryLatencyPagingModeGroupBox.Controls.Add(this.MemoryLatencyDefaultPagesRadioButton);
            this.MemoryLatencyPagingModeGroupBox.Location = new System.Drawing.Point(6, 110);
            this.MemoryLatencyPagingModeGroupBox.Name = "MemoryLatencyPagingModeGroupBox";
            this.MemoryLatencyPagingModeGroupBox.Size = new System.Drawing.Size(229, 66);
            this.MemoryLatencyPagingModeGroupBox.TabIndex = 1;
            this.MemoryLatencyPagingModeGroupBox.TabStop = false;
            this.MemoryLatencyPagingModeGroupBox.Text = "Paging Mode";
            // 
            // MemoryLatencyLargePagesRadioButton
            // 
            this.MemoryLatencyLargePagesRadioButton.AutoSize = true;
            this.MemoryLatencyLargePagesRadioButton.Location = new System.Drawing.Point(6, 43);
            this.MemoryLatencyLargePagesRadioButton.Name = "MemoryLatencyLargePagesRadioButton";
            this.MemoryLatencyLargePagesRadioButton.Size = new System.Drawing.Size(152, 17);
            this.MemoryLatencyLargePagesRadioButton.TabIndex = 1;
            this.MemoryLatencyLargePagesRadioButton.TabStop = true;
            this.MemoryLatencyLargePagesRadioButton.Text = "Large Pages (2 MB Pages)";
            this.MemoryLatencyLargePagesRadioButton.UseVisualStyleBackColor = true;
            // 
            // MemoryLatencyDefaultPagesRadioButton
            // 
            this.MemoryLatencyDefaultPagesRadioButton.AutoSize = true;
            this.MemoryLatencyDefaultPagesRadioButton.Checked = true;
            this.MemoryLatencyDefaultPagesRadioButton.Location = new System.Drawing.Point(6, 19);
            this.MemoryLatencyDefaultPagesRadioButton.Name = "MemoryLatencyDefaultPagesRadioButton";
            this.MemoryLatencyDefaultPagesRadioButton.Size = new System.Drawing.Size(124, 17);
            this.MemoryLatencyDefaultPagesRadioButton.TabIndex = 0;
            this.MemoryLatencyDefaultPagesRadioButton.TabStop = true;
            this.MemoryLatencyDefaultPagesRadioButton.Text = "Default (4 KB Pages)";
            this.MemoryLatencyDefaultPagesRadioButton.UseVisualStyleBackColor = true;
            // 
            // MemoryLatencyAddressingModeGroupBox
            // 
            this.MemoryLatencyAddressingModeGroupBox.Controls.Add(this.MemoryLatencyAsmRadioButton);
            this.MemoryLatencyAddressingModeGroupBox.Controls.Add(this.MemoryLatencyIndexedAddressingRadioButton);
            this.MemoryLatencyAddressingModeGroupBox.Location = new System.Drawing.Point(6, 38);
            this.MemoryLatencyAddressingModeGroupBox.Name = "MemoryLatencyAddressingModeGroupBox";
            this.MemoryLatencyAddressingModeGroupBox.Size = new System.Drawing.Size(229, 66);
            this.MemoryLatencyAddressingModeGroupBox.TabIndex = 0;
            this.MemoryLatencyAddressingModeGroupBox.TabStop = false;
            this.MemoryLatencyAddressingModeGroupBox.Text = "Addressing Mode";
            // 
            // MemoryLatencyAsmRadioButton
            // 
            this.MemoryLatencyAsmRadioButton.AutoSize = true;
            this.MemoryLatencyAsmRadioButton.Checked = true;
            this.MemoryLatencyAsmRadioButton.Location = new System.Drawing.Point(6, 21);
            this.MemoryLatencyAsmRadioButton.Name = "MemoryLatencyAsmRadioButton";
            this.MemoryLatencyAsmRadioButton.Size = new System.Drawing.Size(143, 17);
            this.MemoryLatencyAsmRadioButton.TabIndex = 1;
            this.MemoryLatencyAsmRadioButton.TabStop = true;
            this.MemoryLatencyAsmRadioButton.Text = "Simple Addressing (ASM)";
            this.MemoryLatencyAsmRadioButton.UseVisualStyleBackColor = true;
            // 
            // MemoryLatencyIndexedAddressingRadioButton
            // 
            this.MemoryLatencyIndexedAddressingRadioButton.AutoSize = true;
            this.MemoryLatencyIndexedAddressingRadioButton.Location = new System.Drawing.Point(6, 43);
            this.MemoryLatencyIndexedAddressingRadioButton.Name = "MemoryLatencyIndexedAddressingRadioButton";
            this.MemoryLatencyIndexedAddressingRadioButton.Size = new System.Drawing.Size(161, 17);
            this.MemoryLatencyIndexedAddressingRadioButton.TabIndex = 0;
            this.MemoryLatencyIndexedAddressingRadioButton.TabStop = true;
            this.MemoryLatencyIndexedAddressingRadioButton.Text = "Indexed Addressing (C Array)";
            this.MemoryLatencyIndexedAddressingRadioButton.UseVisualStyleBackColor = true;
            // 
            // GpuMemLatencyTab
            // 
            this.GpuMemLatencyTab.BackColor = System.Drawing.SystemColors.Control;
            this.GpuMemLatencyTab.Controls.Add(this.groupBox1);
            this.GpuMemLatencyTab.Controls.Add(this.label2);
            this.GpuMemLatencyTab.Controls.Add(this.GpuMemoryLatencyDeviceGroupBox);
            this.GpuMemLatencyTab.Controls.Add(this.GpuMemoryLatencyMemoryPathGroupBox);
            this.GpuMemLatencyTab.Location = new System.Drawing.Point(23, 4);
            this.GpuMemLatencyTab.Name = "GpuMemLatencyTab";
            this.GpuMemLatencyTab.Size = new System.Drawing.Size(242, 624);
            this.GpuMemLatencyTab.TabIndex = 2;
            this.GpuMemLatencyTab.Text = "GPU Memory Latency";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 51);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tests GPU memory latency using random, dependent array accesses. Will include sig" +
    "nificant address generation latency.";
            // 
            // GpuMemoryLatencyDeviceGroupBox
            // 
            this.GpuMemoryLatencyDeviceGroupBox.Controls.Add(this.GpuMemoryLatencyDeviceFlowLayoutPanel);
            this.GpuMemoryLatencyDeviceGroupBox.Location = new System.Drawing.Point(7, 197);
            this.GpuMemoryLatencyDeviceGroupBox.Name = "GpuMemoryLatencyDeviceGroupBox";
            this.GpuMemoryLatencyDeviceGroupBox.Size = new System.Drawing.Size(226, 300);
            this.GpuMemoryLatencyDeviceGroupBox.TabIndex = 1;
            this.GpuMemoryLatencyDeviceGroupBox.TabStop = false;
            this.GpuMemoryLatencyDeviceGroupBox.Text = "OpenCL Device";
            // 
            // GpuMemoryLatencyDeviceFlowLayoutPanel
            // 
            this.GpuMemoryLatencyDeviceFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.GpuMemoryLatencyDeviceFlowLayoutPanel.Location = new System.Drawing.Point(6, 20);
            this.GpuMemoryLatencyDeviceFlowLayoutPanel.Name = "GpuMemoryLatencyDeviceFlowLayoutPanel";
            this.GpuMemoryLatencyDeviceFlowLayoutPanel.Size = new System.Drawing.Size(220, 274);
            this.GpuMemoryLatencyDeviceFlowLayoutPanel.TabIndex = 0;
            // 
            // GpuMemoryLatencyMemoryPathGroupBox
            // 
            this.GpuMemoryLatencyMemoryPathGroupBox.Controls.Add(this.GpuMemoryLatencyLocalRadioButton);
            this.GpuMemoryLatencyMemoryPathGroupBox.Controls.Add(this.GpuMemoryLatencyTextureRadioButton);
            this.GpuMemoryLatencyMemoryPathGroupBox.Controls.Add(this.GpuMemoryLatencyConstantScalarRadioButton);
            this.GpuMemoryLatencyMemoryPathGroupBox.Controls.Add(this.GpuMemoryLatencyVectorRadioButton);
            this.GpuMemoryLatencyMemoryPathGroupBox.Controls.Add(this.GpuMemoryLatencyScalarRadioButton);
            this.GpuMemoryLatencyMemoryPathGroupBox.Location = new System.Drawing.Point(7, 53);
            this.GpuMemoryLatencyMemoryPathGroupBox.Name = "GpuMemoryLatencyMemoryPathGroupBox";
            this.GpuMemoryLatencyMemoryPathGroupBox.Size = new System.Drawing.Size(226, 138);
            this.GpuMemoryLatencyMemoryPathGroupBox.TabIndex = 0;
            this.GpuMemoryLatencyMemoryPathGroupBox.TabStop = false;
            this.GpuMemoryLatencyMemoryPathGroupBox.Text = "Memory Access Method";
            // 
            // GpuMemoryLatencyLocalRadioButton
            // 
            this.GpuMemoryLatencyLocalRadioButton.AutoSize = true;
            this.GpuMemoryLatencyLocalRadioButton.Location = new System.Drawing.Point(6, 109);
            this.GpuMemoryLatencyLocalRadioButton.Name = "GpuMemoryLatencyLocalRadioButton";
            this.GpuMemoryLatencyLocalRadioButton.Size = new System.Drawing.Size(127, 17);
            this.GpuMemoryLatencyLocalRadioButton.TabIndex = 4;
            this.GpuMemoryLatencyLocalRadioButton.TabStop = true;
            this.GpuMemoryLatencyLocalRadioButton.Text = "Local Memory, Scalar";
            this.GpuMemoryLatencyLocalRadioButton.UseVisualStyleBackColor = true;
            // 
            // GpuMemoryLatencyTextureRadioButton
            // 
            this.GpuMemoryLatencyTextureRadioButton.AutoSize = true;
            this.GpuMemoryLatencyTextureRadioButton.Location = new System.Drawing.Point(6, 86);
            this.GpuMemoryLatencyTextureRadioButton.Name = "GpuMemoryLatencyTextureRadioButton";
            this.GpuMemoryLatencyTextureRadioButton.Size = new System.Drawing.Size(61, 17);
            this.GpuMemoryLatencyTextureRadioButton.TabIndex = 3;
            this.GpuMemoryLatencyTextureRadioButton.TabStop = true;
            this.GpuMemoryLatencyTextureRadioButton.Text = "Texture";
            this.GpuMemoryLatencyTextureRadioButton.UseVisualStyleBackColor = true;
            // 
            // GpuMemoryLatencyConstantScalarRadioButton
            // 
            this.GpuMemoryLatencyConstantScalarRadioButton.AutoSize = true;
            this.GpuMemoryLatencyConstantScalarRadioButton.Location = new System.Drawing.Point(6, 63);
            this.GpuMemoryLatencyConstantScalarRadioButton.Name = "GpuMemoryLatencyConstantScalarRadioButton";
            this.GpuMemoryLatencyConstantScalarRadioButton.Size = new System.Drawing.Size(143, 17);
            this.GpuMemoryLatencyConstantScalarRadioButton.TabIndex = 2;
            this.GpuMemoryLatencyConstantScalarRadioButton.TabStop = true;
            this.GpuMemoryLatencyConstantScalarRadioButton.Text = "Constant Memory, Scalar";
            this.GpuMemoryLatencyConstantScalarRadioButton.UseVisualStyleBackColor = true;
            // 
            // GpuMemoryLatencyVectorRadioButton
            // 
            this.GpuMemoryLatencyVectorRadioButton.AutoSize = true;
            this.GpuMemoryLatencyVectorRadioButton.Location = new System.Drawing.Point(6, 39);
            this.GpuMemoryLatencyVectorRadioButton.Name = "GpuMemoryLatencyVectorRadioButton";
            this.GpuMemoryLatencyVectorRadioButton.Size = new System.Drawing.Size(132, 17);
            this.GpuMemoryLatencyVectorRadioButton.TabIndex = 1;
            this.GpuMemoryLatencyVectorRadioButton.TabStop = true;
            this.GpuMemoryLatencyVectorRadioButton.Text = "Global Memory, Vector";
            this.GpuMemoryLatencyVectorRadioButton.UseVisualStyleBackColor = true;
            // 
            // GpuMemoryLatencyScalarRadioButton
            // 
            this.GpuMemoryLatencyScalarRadioButton.AutoSize = true;
            this.GpuMemoryLatencyScalarRadioButton.Checked = true;
            this.GpuMemoryLatencyScalarRadioButton.Location = new System.Drawing.Point(6, 16);
            this.GpuMemoryLatencyScalarRadioButton.Name = "GpuMemoryLatencyScalarRadioButton";
            this.GpuMemoryLatencyScalarRadioButton.Size = new System.Drawing.Size(131, 17);
            this.GpuMemoryLatencyScalarRadioButton.TabIndex = 0;
            this.GpuMemoryLatencyScalarRadioButton.TabStop = true;
            this.GpuMemoryLatencyScalarRadioButton.Text = "Global Memory, Scalar";
            this.GpuMemoryLatencyScalarRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GpuPointerChasingStrideTextBox);
            this.groupBox1.Controls.Add(this.GpuPointerChasingStrideLabel);
            this.groupBox1.Location = new System.Drawing.Point(7, 504);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced Options";
            // 
            // GpuPointerChasingStrideLabel
            // 
            this.GpuPointerChasingStrideLabel.AutoSize = true;
            this.GpuPointerChasingStrideLabel.Location = new System.Drawing.Point(7, 20);
            this.GpuPointerChasingStrideLabel.Name = "GpuPointerChasingStrideLabel";
            this.GpuPointerChasingStrideLabel.Size = new System.Drawing.Size(149, 13);
            this.GpuPointerChasingStrideLabel.TabIndex = 0;
            this.GpuPointerChasingStrideLabel.Text = "Pointer Chasing Stride (Bytes):";
            // 
            // GpuPointerChasingStrideTextBox
            // 
            this.GpuPointerChasingStrideTextBox.Location = new System.Drawing.Point(162, 17);
            this.GpuPointerChasingStrideTextBox.Name = "GpuPointerChasingStrideTextBox";
            this.GpuPointerChasingStrideTextBox.Size = new System.Drawing.Size(43, 20);
            this.GpuPointerChasingStrideTextBox.TabIndex = 1;
            this.GpuPointerChasingStrideTextBox.Text = "64";
            this.GpuPointerChasingStrideTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MicrobenchmarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 730);
            this.Controls.Add(this.TestSelectTabControl);
            this.Controls.Add(this.ChartControlsGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.CancelRunButton);
            this.Controls.Add(this.ResultsChart);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.resultListView);
            this.Controls.Add(this.RunBandwidthTestButton);
            this.Name = "MicrobenchmarkForm";
            this.Text = "Clam Cache and Mem Benchmark";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MicrobenchmarkForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ResultsChart)).EndInit();
            this.ThreadingModeGroupBox.ResumeLayout(false);
            this.ThreadingModeGroupBox.PerformLayout();
            this.AccessModeGroupBox.ResumeLayout(false);
            this.AccessModeGroupBox.PerformLayout();
            this.TestMethodGroupBox.ResumeLayout(false);
            this.BandwidthTestMethodFlowLayoutPanel.ResumeLayout(false);
            this.BandwidthTestMethodFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadCountTrackbar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ChartControlsGroupBox.ResumeLayout(false);
            this.ChartControlsGroupBox.PerformLayout();
            this.TestSelectTabControl.ResumeLayout(false);
            this.MemoryBandwidthTab.ResumeLayout(false);
            this.MemoryBandwidthTab.PerformLayout();
            this.MemoryLatencyTab.ResumeLayout(false);
            this.MemoryLatencyPagingModeGroupBox.ResumeLayout(false);
            this.MemoryLatencyPagingModeGroupBox.PerformLayout();
            this.MemoryLatencyAddressingModeGroupBox.ResumeLayout(false);
            this.MemoryLatencyAddressingModeGroupBox.PerformLayout();
            this.GpuMemLatencyTab.ResumeLayout(false);
            this.GpuMemoryLatencyDeviceGroupBox.ResumeLayout(false);
            this.GpuMemoryLatencyMemoryPathGroupBox.ResumeLayout(false);
            this.GpuMemoryLatencyMemoryPathGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunBandwidthTestButton;
        private System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart ResultsChart;
        private System.Windows.Forms.Label ThreadCountLabel;
        private System.Windows.Forms.GroupBox ThreadingModeGroupBox;
        private System.Windows.Forms.RadioButton SharedRadioButton;
        private System.Windows.Forms.RadioButton PrivateRadioButton;
        private System.Windows.Forms.Button CancelRunButton;
        private System.Windows.Forms.GroupBox AccessModeGroupBox;
        private System.Windows.Forms.RadioButton InstructionFetchRadioButton;
        private System.Windows.Forms.RadioButton DataAddRadioButton;
        private System.Windows.Forms.RadioButton DataWriteRadioButton;
        private System.Windows.Forms.RadioButton DataReadRadioButton;
        private System.Windows.Forms.GroupBox TestMethodGroupBox;
        private System.Windows.Forms.RadioButton Avx512RadioButton;
        private System.Windows.Forms.RadioButton AvxRadioButton;
        private System.Windows.Forms.RadioButton SseRadioButton;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.TrackBar ThreadCountTrackbar;
        private System.Windows.Forms.Button ExportExcelButton;
        private System.Windows.Forms.TextBox ExportTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton JsFormatRadioButton;
        private System.Windows.Forms.RadioButton CsvFormatRadioButton;
        private System.Windows.Forms.RadioButton MmxRadioButton;
        private System.Windows.Forms.Button ClearChartButton;
        private System.Windows.Forms.GroupBox ChartControlsGroupBox;
        private System.Windows.Forms.TextBox ColorRBox;
        private System.Windows.Forms.Label RLabel;
        private System.Windows.Forms.RadioButton SpecifyNextColorRadioButton;
        private System.Windows.Forms.RadioButton RandomizeNextColorRadioButton;
        private System.Windows.Forms.RadioButton DefaultNextColorRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ColorBBox;
        private System.Windows.Forms.TextBox ColorGBox;
        private System.Windows.Forms.Label GLabel;
        private System.Windows.Forms.RadioButton DataNtWriteRadioButton;
        private System.Windows.Forms.RadioButton DataMicrocodedRadioButton;
        private System.Windows.Forms.ListBox ExportListBox;
        private System.Windows.Forms.Label TestRunLabel;
        private System.Windows.Forms.FlowLayoutPanel BandwidthTestMethodFlowLayoutPanel;
        private System.Windows.Forms.TabControl TestSelectTabControl;
        private System.Windows.Forms.TabPage MemoryBandwidthTab;
        private System.Windows.Forms.TabPage MemoryLatencyTab;
        private System.Windows.Forms.GroupBox MemoryLatencyAddressingModeGroupBox;
        private System.Windows.Forms.GroupBox MemoryLatencyPagingModeGroupBox;
        private System.Windows.Forms.RadioButton MemoryLatencyIndexedAddressingRadioButton;
        private System.Windows.Forms.RadioButton MemoryLatencyAsmRadioButton;
        private System.Windows.Forms.RadioButton MemoryLatencyLargePagesRadioButton;
        private System.Windows.Forms.RadioButton MemoryLatencyDefaultPagesRadioButton;
        private System.Windows.Forms.TabPage GpuMemLatencyTab;
        private System.Windows.Forms.GroupBox GpuMemoryLatencyMemoryPathGroupBox;
        private System.Windows.Forms.RadioButton GpuMemoryLatencyScalarRadioButton;
        private System.Windows.Forms.RadioButton GpuMemoryLatencyVectorRadioButton;
        private System.Windows.Forms.RadioButton GpuMemoryLatencyTextureRadioButton;
        private System.Windows.Forms.RadioButton GpuMemoryLatencyConstantScalarRadioButton;
        private System.Windows.Forms.RadioButton GpuMemoryLatencyLocalRadioButton;
        private System.Windows.Forms.GroupBox GpuMemoryLatencyDeviceGroupBox;
        private System.Windows.Forms.Label CpuMemoryBandwidthLabel;
        private System.Windows.Forms.Label CpuMemoryLatencyLabel;
        private System.Windows.Forms.FlowLayoutPanel GpuMemoryLatencyDeviceFlowLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox GpuPointerChasingStrideTextBox;
        private System.Windows.Forms.Label GpuPointerChasingStrideLabel;
    }
}

