
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
            this.TestLabel = new System.Windows.Forms.Label();
            this.RunBandwidthTestButton = new System.Windows.Forms.Button();
            this.resultListView = new System.Windows.Forms.ListView();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.ResultChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ThreadCountTextBox = new System.Windows.Forms.TextBox();
            this.ThreadCountLabel = new System.Windows.Forms.Label();
            this.ThreadingModeGroupBox = new System.Windows.Forms.GroupBox();
            this.SharedRadioButton = new System.Windows.Forms.RadioButton();
            this.PrivateRadioButton = new System.Windows.Forms.RadioButton();
            this.CancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InstructionFetchRadioButton = new System.Windows.Forms.RadioButton();
            this.DataAddRadioButton = new System.Windows.Forms.RadioButton();
            this.DataWriteRadioButton = new System.Windows.Forms.RadioButton();
            this.DataReadRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ResultChart)).BeginInit();
            this.ThreadingModeGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestLabel
            // 
            this.TestLabel.AutoSize = true;
            this.TestLabel.Location = new System.Drawing.Point(12, 12);
            this.TestLabel.Name = "TestLabel";
            this.TestLabel.Size = new System.Drawing.Size(121, 13);
            this.TestLabel.TabIndex = 1;
            this.TestLabel.Text = "Memory Bandwidth Test";
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
            this.resultListView.Location = new System.Drawing.Point(234, 30);
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(244, 408);
            this.resultListView.TabIndex = 4;
            this.resultListView.UseCompatibleStateImageBehavior = false;
            this.resultListView.View = System.Windows.Forms.View.Details;
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(234, 12);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(45, 13);
            this.ResultLabel.TabIndex = 5;
            this.ResultLabel.Text = "Results:";
            // 
            // ResultChart
            // 
            chartArea1.Name = "ChartArea1";
            this.ResultChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ResultChart.Legends.Add(legend1);
            this.ResultChart.Location = new System.Drawing.Point(484, 30);
            this.ResultChart.Name = "ResultChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ResultChart.Series.Add(series1);
            this.ResultChart.Size = new System.Drawing.Size(625, 408);
            this.ResultChart.TabIndex = 6;
            this.ResultChart.Text = "chart1";
            // 
            // ThreadCountTextBox
            // 
            this.ThreadCountTextBox.Location = new System.Drawing.Point(68, 33);
            this.ThreadCountTextBox.Name = "ThreadCountTextBox";
            this.ThreadCountTextBox.Size = new System.Drawing.Size(36, 20);
            this.ThreadCountTextBox.TabIndex = 7;
            this.ThreadCountTextBox.Text = "1";
            // 
            // ThreadCountLabel
            // 
            this.ThreadCountLabel.AutoSize = true;
            this.ThreadCountLabel.Location = new System.Drawing.Point(13, 36);
            this.ThreadCountLabel.Name = "ThreadCountLabel";
            this.ThreadCountLabel.Size = new System.Drawing.Size(49, 13);
            this.ThreadCountLabel.TabIndex = 8;
            this.ThreadCountLabel.Text = "Threads:";
            // 
            // ThreadingModeGroupBox
            // 
            this.ThreadingModeGroupBox.Controls.Add(this.SharedRadioButton);
            this.ThreadingModeGroupBox.Controls.Add(this.PrivateRadioButton);
            this.ThreadingModeGroupBox.Location = new System.Drawing.Point(12, 59);
            this.ThreadingModeGroupBox.Name = "ThreadingModeGroupBox";
            this.ThreadingModeGroupBox.Size = new System.Drawing.Size(206, 69);
            this.ThreadingModeGroupBox.TabIndex = 9;
            this.ThreadingModeGroupBox.TabStop = false;
            this.ThreadingModeGroupBox.Text = "Threading Mode";
            // 
            // SharedRadioButton
            // 
            this.SharedRadioButton.AutoSize = true;
            this.SharedRadioButton.Location = new System.Drawing.Point(7, 44);
            this.SharedRadioButton.Name = "SharedRadioButton";
            this.SharedRadioButton.Size = new System.Drawing.Size(195, 17);
            this.SharedRadioButton.TabIndex = 1;
            this.SharedRadioButton.Text = "Single Array Shared Across Threads";
            this.SharedRadioButton.UseVisualStyleBackColor = true;
            // 
            // PrivateRadioButton
            // 
            this.PrivateRadioButton.AutoSize = true;
            this.PrivateRadioButton.Checked = true;
            this.PrivateRadioButton.Location = new System.Drawing.Point(7, 21);
            this.PrivateRadioButton.Name = "PrivateRadioButton";
            this.PrivateRadioButton.Size = new System.Drawing.Size(141, 17);
            this.PrivateRadioButton.TabIndex = 0;
            this.PrivateRadioButton.TabStop = true;
            this.PrivateRadioButton.Text = "Private Array Per Thread";
            this.PrivateRadioButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(96, 415);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Cancel Run";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InstructionFetchRadioButton);
            this.groupBox1.Controls.Add(this.DataAddRadioButton);
            this.groupBox1.Controls.Add(this.DataWriteRadioButton);
            this.groupBox1.Controls.Add(this.DataReadRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 116);
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
            // MicrobenchmarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ThreadingModeGroupBox);
            this.Controls.Add(this.ThreadCountLabel);
            this.Controls.Add(this.ThreadCountTextBox);
            this.Controls.Add(this.ResultChart);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.resultListView);
            this.Controls.Add(this.RunBandwidthTestButton);
            this.Controls.Add(this.TestLabel);
            this.Name = "MicrobenchmarkForm";
            this.Text = "Why";
            ((System.ComponentModel.ISupportInitialize)(this.ResultChart)).EndInit();
            this.ThreadingModeGroupBox.ResumeLayout(false);
            this.ThreadingModeGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TestLabel;
        private System.Windows.Forms.Button RunBandwidthTestButton;
        private System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart ResultChart;
        private System.Windows.Forms.TextBox ThreadCountTextBox;
        private System.Windows.Forms.Label ThreadCountLabel;
        private System.Windows.Forms.GroupBox ThreadingModeGroupBox;
        private System.Windows.Forms.RadioButton SharedRadioButton;
        private System.Windows.Forms.RadioButton PrivateRadioButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton InstructionFetchRadioButton;
        private System.Windows.Forms.RadioButton DataAddRadioButton;
        private System.Windows.Forms.RadioButton DataWriteRadioButton;
        private System.Windows.Forms.RadioButton DataReadRadioButton;
    }
}

