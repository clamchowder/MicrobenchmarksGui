
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.Avx512RadioButton = new System.Windows.Forms.RadioButton();
            this.AvxRadioButton = new System.Windows.Forms.RadioButton();
            this.SseRadioButton = new System.Windows.Forms.RadioButton();
            this.progressLabel = new System.Windows.Forms.Label();
            this.ThreadCountTrackbar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ResultChart)).BeginInit();
            this.ThreadingModeGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TestMethodGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadCountTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // RunBandwidthTestButton
            // 
            this.RunBandwidthTestButton.Location = new System.Drawing.Point(15, 415);
            this.RunBandwidthTestButton.Name = "RunBandwidthTestButton";
            this.RunBandwidthTestButton.Size = new System.Drawing.Size(75, 23);
            this.RunBandwidthTestButton.TabIndex = 3;
            this.RunBandwidthTestButton.Text = "Run";
            this.RunBandwidthTestButton.UseVisualStyleBackColor = true;
            this.RunBandwidthTestButton.Click += new System.EventHandler(this.RunBandwidthTestButton_Click);
            // 
            // resultListView
            // 
            this.resultListView.HideSelection = false;
            this.resultListView.Location = new System.Drawing.Point(247, 30);
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(195, 408);
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
            chartArea7.Name = "ChartArea1";
            this.ResultChart.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.ResultChart.Legends.Add(legend7);
            this.ResultChart.Location = new System.Drawing.Point(448, 30);
            this.ResultChart.Name = "ResultChart";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.ResultChart.Series.Add(series7);
            this.ResultChart.Size = new System.Drawing.Size(661, 408);
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
            // CancelButton
            // 
            this.CancelRunButton.Enabled = false;
            this.CancelRunButton.Location = new System.Drawing.Point(96, 415);
            this.CancelRunButton.Name = "CancelButton";
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
            // MicrobenchmarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 450);
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
    }
}

