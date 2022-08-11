
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.RunBandwidthTestButton = new System.Windows.Forms.Button();
            this.resultListView = new System.Windows.Forms.ListView();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.ResultChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ThreadCountLabel = new System.Windows.Forms.Label();
            this.ThreadingModeGroupBox = new System.Windows.Forms.GroupBox();
            this.SharedRadioButton = new System.Windows.Forms.RadioButton();
            this.PrivateRadioButton = new System.Windows.Forms.RadioButton();
            this.CancelRunButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InstructionFetchRadioButton = new System.Windows.Forms.RadioButton();
            this.DataAddRadioButton = new System.Windows.Forms.RadioButton();
            this.DataWriteRadioButton = new System.Windows.Forms.RadioButton();
            this.DataReadRadioButton = new System.Windows.Forms.RadioButton();
            this.TestMethodGroupBox = new System.Windows.Forms.GroupBox();
            this.MmxRadioButton = new System.Windows.Forms.RadioButton();
            this.Avx512RadioButton = new System.Windows.Forms.RadioButton();
            this.AvxRadioButton = new System.Windows.Forms.RadioButton();
            this.SseRadioButton = new System.Windows.Forms.RadioButton();
            this.progressLabel = new System.Windows.Forms.Label();
            this.ThreadCountTrackbar = new System.Windows.Forms.TrackBar();
            this.TestDurationLabel = new System.Windows.Forms.Label();
            this.dataToTransferTextBox = new System.Windows.Forms.TextBox();
            this.gbLabel = new System.Windows.Forms.Label();
            this.ExportExcelButton = new System.Windows.Forms.Button();
            this.ExportTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.JsFormatRadioButton = new System.Windows.Forms.RadioButton();
            this.CsvFormatRadioButton = new System.Windows.Forms.RadioButton();
            this.ClearChartButton = new System.Windows.Forms.Button();
            this.ChartControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.RandomizeNextColorRadioButton = new System.Windows.Forms.RadioButton();
            this.SpecifyNextColorRadioButton = new System.Windows.Forms.RadioButton();
            this.RLabel = new System.Windows.Forms.Label();
            this.ColorRBox = new System.Windows.Forms.TextBox();
            this.GLabel = new System.Windows.Forms.Label();
            this.ColorGBox = new System.Windows.Forms.TextBox();
            this.ColorBBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DefaultNextColorRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ResultChart)).BeginInit();
            this.ThreadingModeGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TestMethodGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadCountTrackbar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.ChartControlsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // RunBandwidthTestButton
            // 
            this.RunBandwidthTestButton.Location = new System.Drawing.Point(12, 486);
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
            this.resultListView.Location = new System.Drawing.Point(247, 30);
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(195, 479);
            this.resultListView.TabIndex = 4;
            this.resultListView.UseCompatibleStateImageBehavior = false;
            this.resultListView.View = System.Windows.Forms.View.Details;
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(244, 12);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(74, 13);
            this.ResultLabel.TabIndex = 5;
            this.ResultLabel.Text = "Run Progress:";
            // 
            // ResultChart
            // 
            this.ResultChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.ResultChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.ResultChart.Legends.Add(legend2);
            this.ResultChart.Location = new System.Drawing.Point(448, 141);
            this.ResultChart.Name = "ResultChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ResultChart.Series.Add(series2);
            this.ResultChart.Size = new System.Drawing.Size(661, 368);
            this.ResultChart.TabIndex = 6;
            this.ResultChart.Text = "chart1";
            // 
            // ThreadCountLabel
            // 
            this.ThreadCountLabel.AutoSize = true;
            this.ThreadCountLabel.Location = new System.Drawing.Point(12, 12);
            this.ThreadCountLabel.Name = "ThreadCountLabel";
            this.ThreadCountLabel.Size = new System.Drawing.Size(58, 13);
            this.ThreadCountLabel.TabIndex = 8;
            this.ThreadCountLabel.Text = "Threads: 1";
            // 
            // ThreadingModeGroupBox
            // 
            this.ThreadingModeGroupBox.Controls.Add(this.SharedRadioButton);
            this.ThreadingModeGroupBox.Controls.Add(this.PrivateRadioButton);
            this.ThreadingModeGroupBox.Location = new System.Drawing.Point(12, 81);
            this.ThreadingModeGroupBox.Name = "ThreadingModeGroupBox";
            this.ThreadingModeGroupBox.Size = new System.Drawing.Size(229, 69);
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
            this.CancelRunButton.Location = new System.Drawing.Point(93, 486);
            this.CancelRunButton.Name = "CancelRunButton";
            this.CancelRunButton.Size = new System.Drawing.Size(75, 23);
            this.CancelRunButton.TabIndex = 10;
            this.CancelRunButton.Text = "Cancel Run";
            this.CancelRunButton.UseVisualStyleBackColor = true;
            this.CancelRunButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InstructionFetchRadioButton);
            this.groupBox1.Controls.Add(this.DataAddRadioButton);
            this.groupBox1.Controls.Add(this.DataWriteRadioButton);
            this.groupBox1.Controls.Add(this.DataReadRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 116);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Access Mode";
            // 
            // InstructionFetchRadioButton
            // 
            this.InstructionFetchRadioButton.AutoSize = true;
            this.InstructionFetchRadioButton.Location = new System.Drawing.Point(7, 91);
            this.InstructionFetchRadioButton.Name = "InstructionFetchRadioButton";
            this.InstructionFetchRadioButton.Size = new System.Drawing.Size(104, 17);
            this.InstructionFetchRadioButton.TabIndex = 3;
            this.InstructionFetchRadioButton.TabStop = true;
            this.InstructionFetchRadioButton.Text = "Instruction Fetch";
            this.InstructionFetchRadioButton.UseVisualStyleBackColor = true;
            this.InstructionFetchRadioButton.CheckedChanged += new System.EventHandler(this.InstructionFetchRadioButton_CheckedChanged);
            // 
            // DataAddRadioButton
            // 
            this.DataAddRadioButton.AutoSize = true;
            this.DataAddRadioButton.Location = new System.Drawing.Point(7, 67);
            this.DataAddRadioButton.Name = "DataAddRadioButton";
            this.DataAddRadioButton.Size = new System.Drawing.Size(167, 17);
            this.DataAddRadioButton.TabIndex = 2;
            this.DataAddRadioButton.TabStop = true;
            this.DataAddRadioButton.Text = "Data Read-Modify-Write (Add)";
            this.DataAddRadioButton.UseVisualStyleBackColor = true;
            this.DataAddRadioButton.CheckedChanged += new System.EventHandler(this.CheckWriteModeChange);
            // 
            // DataWriteRadioButton
            // 
            this.DataWriteRadioButton.AutoSize = true;
            this.DataWriteRadioButton.Location = new System.Drawing.Point(7, 43);
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
            this.TestMethodGroupBox.Controls.Add(this.MmxRadioButton);
            this.TestMethodGroupBox.Controls.Add(this.Avx512RadioButton);
            this.TestMethodGroupBox.Controls.Add(this.AvxRadioButton);
            this.TestMethodGroupBox.Controls.Add(this.SseRadioButton);
            this.TestMethodGroupBox.Location = new System.Drawing.Point(12, 279);
            this.TestMethodGroupBox.Name = "TestMethodGroupBox";
            this.TestMethodGroupBox.Size = new System.Drawing.Size(229, 116);
            this.TestMethodGroupBox.TabIndex = 15;
            this.TestMethodGroupBox.TabStop = false;
            this.TestMethodGroupBox.Text = "Test Method";
            // 
            // MmxRadioButton
            // 
            this.MmxRadioButton.AutoSize = true;
            this.MmxRadioButton.Location = new System.Drawing.Point(7, 93);
            this.MmxRadioButton.Name = "MmxRadioButton";
            this.MmxRadioButton.Size = new System.Drawing.Size(85, 17);
            this.MmxRadioButton.TabIndex = 3;
            this.MmxRadioButton.TabStop = true;
            this.MmxRadioButton.Text = "MMX (64-bit)";
            this.MmxRadioButton.UseVisualStyleBackColor = true;
            // 
            // Avx512RadioButton
            // 
            this.Avx512RadioButton.AutoSize = true;
            this.Avx512RadioButton.Location = new System.Drawing.Point(7, 68);
            this.Avx512RadioButton.Name = "Avx512RadioButton";
            this.Avx512RadioButton.Size = new System.Drawing.Size(108, 17);
            this.Avx512RadioButton.TabIndex = 2;
            this.Avx512RadioButton.Text = "AVX-512 (512-bit)";
            this.Avx512RadioButton.UseVisualStyleBackColor = true;
            // 
            // AvxRadioButton
            // 
            this.AvxRadioButton.AutoSize = true;
            this.AvxRadioButton.Checked = true;
            this.AvxRadioButton.Location = new System.Drawing.Point(7, 44);
            this.AvxRadioButton.Name = "AvxRadioButton";
            this.AvxRadioButton.Size = new System.Drawing.Size(87, 17);
            this.AvxRadioButton.TabIndex = 1;
            this.AvxRadioButton.TabStop = true;
            this.AvxRadioButton.Text = "AVX (256-bit)";
            this.AvxRadioButton.UseVisualStyleBackColor = true;
            // 
            // SseRadioButton
            // 
            this.SseRadioButton.AutoSize = true;
            this.SseRadioButton.Location = new System.Drawing.Point(7, 20);
            this.SseRadioButton.Name = "SseRadioButton";
            this.SseRadioButton.Size = new System.Drawing.Size(87, 17);
            this.SseRadioButton.TabIndex = 0;
            this.SseRadioButton.Text = "SSE (128-bit)";
            this.SseRadioButton.UseVisualStyleBackColor = true;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(324, 12);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(61, 13);
            this.progressLabel.TabIndex = 16;
            this.progressLabel.Text = "Not Started";
            // 
            // ThreadCountTrackbar
            // 
            this.ThreadCountTrackbar.LargeChange = 2;
            this.ThreadCountTrackbar.Location = new System.Drawing.Point(12, 30);
            this.ThreadCountTrackbar.Maximum = 4;
            this.ThreadCountTrackbar.Minimum = 1;
            this.ThreadCountTrackbar.Name = "ThreadCountTrackbar";
            this.ThreadCountTrackbar.Size = new System.Drawing.Size(229, 45);
            this.ThreadCountTrackbar.TabIndex = 18;
            this.ThreadCountTrackbar.Value = 1;
            this.ThreadCountTrackbar.Scroll += new System.EventHandler(this.ThreadCountTrackbar_Scroll);
            // 
            // TestDurationLabel
            // 
            this.TestDurationLabel.AutoSize = true;
            this.TestDurationLabel.Location = new System.Drawing.Point(12, 461);
            this.TestDurationLabel.Name = "TestDurationLabel";
            this.TestDurationLabel.Size = new System.Drawing.Size(87, 13);
            this.TestDurationLabel.TabIndex = 19;
            this.TestDurationLabel.Text = "Data to Transfer:";
            // 
            // dataToTransferTextBox
            // 
            this.dataToTransferTextBox.Location = new System.Drawing.Point(105, 458);
            this.dataToTransferTextBox.MaxLength = 7;
            this.dataToTransferTextBox.Name = "dataToTransferTextBox";
            this.dataToTransferTextBox.Size = new System.Drawing.Size(34, 20);
            this.dataToTransferTextBox.TabIndex = 20;
            this.dataToTransferTextBox.Text = "512";
            this.dataToTransferTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbLabel
            // 
            this.gbLabel.AutoSize = true;
            this.gbLabel.Location = new System.Drawing.Point(146, 461);
            this.gbLabel.Name = "gbLabel";
            this.gbLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gbLabel.Size = new System.Drawing.Size(22, 13);
            this.gbLabel.TabIndex = 21;
            this.gbLabel.Text = "GB";
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
            this.ExportTextBox.Location = new System.Drawing.Point(836, 12);
            this.ExportTextBox.Multiline = true;
            this.ExportTextBox.Name = "ExportTextBox";
            this.ExportTextBox.Size = new System.Drawing.Size(273, 123);
            this.ExportTextBox.TabIndex = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.JsFormatRadioButton);
            this.groupBox2.Controls.Add(this.CsvFormatRadioButton);
            this.groupBox2.Controls.Add(this.ExportExcelButton);
            this.groupBox2.Location = new System.Drawing.Point(720, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(110, 123);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Last Run";
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
            this.ClearChartButton.Size = new System.Drawing.Size(75, 23);
            this.ClearChartButton.TabIndex = 27;
            this.ClearChartButton.Text = "Clear Chart";
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
            this.ChartControlsGroupBox.Location = new System.Drawing.Point(449, 12);
            this.ChartControlsGroupBox.Name = "ChartControlsGroupBox";
            this.ChartControlsGroupBox.Size = new System.Drawing.Size(265, 123);
            this.ChartControlsGroupBox.TabIndex = 28;
            this.ChartControlsGroupBox.TabStop = false;
            this.ChartControlsGroupBox.Text = "Chart Controls";
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
            // specifyNextColorRadioButton
            // 
            this.SpecifyNextColorRadioButton.AutoSize = true;
            this.SpecifyNextColorRadioButton.Location = new System.Drawing.Point(7, 44);
            this.SpecifyNextColorRadioButton.Name = "specifyNextColorRadioButton";
            this.SpecifyNextColorRadioButton.Size = new System.Drawing.Size(112, 17);
            this.SpecifyNextColorRadioButton.TabIndex = 29;
            this.SpecifyNextColorRadioButton.Text = "Specify Next Color";
            this.SpecifyNextColorRadioButton.UseVisualStyleBackColor = true;
            this.SpecifyNextColorRadioButton.CheckedChanged += new System.EventHandler(this.specifyNextColorRadioButton_CheckedChanged);
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
            // ColorRBox
            // 
            this.ColorRBox.Enabled = false;
            this.ColorRBox.Location = new System.Drawing.Point(195, 13);
            this.ColorRBox.Name = "ColorRBox";
            this.ColorRBox.Size = new System.Drawing.Size(24, 20);
            this.ColorRBox.TabIndex = 31;
            this.ColorRBox.Text = "50";
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
            // ColorGBox
            // 
            this.ColorGBox.Enabled = false;
            this.ColorGBox.Location = new System.Drawing.Point(195, 39);
            this.ColorGBox.Name = "ColorGBox";
            this.ColorGBox.Size = new System.Drawing.Size(24, 20);
            this.ColorGBox.TabIndex = 33;
            this.ColorGBox.Text = "50";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Blue";
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
            // MicrobenchmarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 521);
            this.Controls.Add(this.ChartControlsGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ExportTextBox);
            this.Controls.Add(this.gbLabel);
            this.Controls.Add(this.dataToTransferTextBox);
            this.Controls.Add(this.TestDurationLabel);
            this.Controls.Add(this.ThreadCountTrackbar);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.TestMethodGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelRunButton);
            this.Controls.Add(this.ThreadingModeGroupBox);
            this.Controls.Add(this.ThreadCountLabel);
            this.Controls.Add(this.ResultChart);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.resultListView);
            this.Controls.Add(this.RunBandwidthTestButton);
            this.Name = "MicrobenchmarkForm";
            this.Text = "Memory Bandwidth Test";
            this.Load += new System.EventHandler(this.MicrobenchmarkForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ResultChart)).EndInit();
            this.ThreadingModeGroupBox.ResumeLayout(false);
            this.ThreadingModeGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TestMethodGroupBox.ResumeLayout(false);
            this.TestMethodGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadCountTrackbar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ChartControlsGroupBox.ResumeLayout(false);
            this.ChartControlsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunBandwidthTestButton;
        private System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart ResultChart;
        private System.Windows.Forms.Label ThreadCountLabel;
        private System.Windows.Forms.GroupBox ThreadingModeGroupBox;
        private System.Windows.Forms.RadioButton SharedRadioButton;
        private System.Windows.Forms.RadioButton PrivateRadioButton;
        private System.Windows.Forms.Button CancelRunButton;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Label TestDurationLabel;
        private System.Windows.Forms.TextBox dataToTransferTextBox;
        private System.Windows.Forms.Label gbLabel;
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
    }
}

