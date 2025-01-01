using System.Drawing;
using System.Windows.Forms;

namespace MicrobenchmarkGui
{
    partial class BenchmarkSubmissionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox summaryTextBox;
        private System.Windows.Forms.TextBox cpuNameTextBox;
        private System.Windows.Forms.TextBox motherboardTextBox;
        private System.Windows.Forms.TextBox memoryConfigTextBox;
        private System.Windows.Forms.TextBox notesTextBox;
        private System.Windows.Forms.TableLayoutPanel mainLayout;

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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.summaryLabel = new System.Windows.Forms.Label();
            this.summaryTextBox = new System.Windows.Forms.TextBox();
            this.cpuLabel = new System.Windows.Forms.Label();
            this.cpuNameTextBox = new System.Windows.Forms.TextBox();
            this.mbLabel = new System.Windows.Forms.Label();
            this.motherboardTextBox = new System.Windows.Forms.TextBox();
            this.memLabel = new System.Windows.Forms.Label();
            this.memoryConfigTextBox = new System.Windows.Forms.TextBox();
            this.notesLabel = new System.Windows.Forms.Label();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 2;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.summaryLabel, 0, 0);
            this.mainLayout.Controls.Add(this.summaryTextBox, 1, 0);
            this.mainLayout.Controls.Add(this.cpuLabel, 0, 1);
            this.mainLayout.Controls.Add(this.cpuNameTextBox, 1, 1);
            this.mainLayout.Controls.Add(this.mbLabel, 0, 2);
            this.mainLayout.Controls.Add(this.motherboardTextBox, 1, 2);
            this.mainLayout.Controls.Add(this.memLabel, 0, 3);
            this.mainLayout.Controls.Add(this.memoryConfigTextBox, 1, 3);
            this.mainLayout.Controls.Add(this.notesLabel, 0, 4);
            this.mainLayout.Controls.Add(this.notesTextBox, 1, 4);
            this.mainLayout.Controls.Add(this.buttonPanel, 1, 5);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Margin = new System.Windows.Forms.Padding(4);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(13);
            this.mainLayout.RowCount = 6;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainLayout.Size = new System.Drawing.Size(643, 520);
            this.mainLayout.TabIndex = 0;
            // 
            // summaryLabel
            // 
            this.summaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryLabel.Location = new System.Drawing.Point(17, 13);
            this.summaryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(177, 100);
            this.summaryLabel.TabIndex = 0;
            this.summaryLabel.Text = "Summary:";
            // 
            // summaryTextBox
            // 
            this.summaryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryTextBox.Location = new System.Drawing.Point(202, 17);
            this.summaryTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.summaryTextBox.Multiline = true;
            this.summaryTextBox.Name = "summaryTextBox";
            this.summaryTextBox.ReadOnly = true;
            this.summaryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.summaryTextBox.Size = new System.Drawing.Size(424, 92);
            this.summaryTextBox.TabIndex = 1;
            // 
            // cpuLabel
            // 
            this.cpuLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpuLabel.Location = new System.Drawing.Point(17, 113);
            this.cpuLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cpuLabel.Name = "cpuLabel";
            this.cpuLabel.Size = new System.Drawing.Size(177, 35);
            this.cpuLabel.TabIndex = 2;
            this.cpuLabel.Text = "CPU Name:";
            // 
            // cpuNameTextBox
            // 
            this.cpuNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpuNameTextBox.Location = new System.Drawing.Point(202, 117);
            this.cpuNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.cpuNameTextBox.Name = "cpuNameTextBox";
            this.cpuNameTextBox.Size = new System.Drawing.Size(424, 26);
            this.cpuNameTextBox.TabIndex = 3;
            // 
            // mbLabel
            // 
            this.mbLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbLabel.Location = new System.Drawing.Point(17, 148);
            this.mbLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mbLabel.Name = "mbLabel";
            this.mbLabel.Size = new System.Drawing.Size(177, 35);
            this.mbLabel.TabIndex = 4;
            this.mbLabel.Text = "Motherboard:";
            // 
            // motherboardTextBox
            // 
            this.motherboardTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motherboardTextBox.Location = new System.Drawing.Point(202, 152);
            this.motherboardTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.motherboardTextBox.Name = "motherboardTextBox";
            this.motherboardTextBox.Size = new System.Drawing.Size(424, 26);
            this.motherboardTextBox.TabIndex = 5;
            // 
            // memLabel
            // 
            this.memLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memLabel.Location = new System.Drawing.Point(17, 183);
            this.memLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.memLabel.Name = "memLabel";
            this.memLabel.Size = new System.Drawing.Size(177, 35);
            this.memLabel.TabIndex = 6;
            this.memLabel.Text = "Memory Config:";
            // 
            // memoryConfigTextBox
            // 
            this.memoryConfigTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryConfigTextBox.Location = new System.Drawing.Point(202, 187);
            this.memoryConfigTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.memoryConfigTextBox.Name = "memoryConfigTextBox";
            this.memoryConfigTextBox.Size = new System.Drawing.Size(424, 26);
            this.memoryConfigTextBox.TabIndex = 7;
            // 
            // notesLabel
            // 
            this.notesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notesLabel.Location = new System.Drawing.Point(17, 218);
            this.notesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(177, 209);
            this.notesLabel.TabIndex = 8;
            this.notesLabel.Text = "Notes:";
            // 
            // notesTextBox
            // 
            this.notesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notesTextBox.Location = new System.Drawing.Point(202, 222);
            this.notesTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesTextBox.Size = new System.Drawing.Size(424, 201);
            this.notesTextBox.TabIndex = 9;
            // 
            // buttonPanel
            // 
            this.mainLayout.SetColumnSpan(this.buttonPanel, 2);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.submitButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonPanel.Location = new System.Drawing.Point(17, 471);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(609, 36);
            this.buttonPanel.TabIndex = 10;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(509, 4);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 28);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(405, 4);
            this.submitButton.Margin = new System.Windows.Forms.Padding(4);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(96, 28);
            this.submitButton.TabIndex = 1;
            this.submitButton.Text = "Submit";
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // BenchmarkSubmissionDialog
            // 
            this.AcceptButton = this.submitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(643, 520);
            this.Controls.Add(this.mainLayout);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(637, 550);
            this.Name = "BenchmarkSubmissionDialog";
            this.Text = "Submit Benchmark Results";
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label summaryLabel;
        private Label cpuLabel;
        private Label mbLabel;
        private Label memLabel;
        private Label notesLabel;
        private FlowLayoutPanel buttonPanel;
        private Button cancelButton;
        private Button submitButton;
    }
}