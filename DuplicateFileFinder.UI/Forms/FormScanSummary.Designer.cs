namespace DuplicateFileFinder.UI.Forms
{
    partial class FormScanSummary
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblFile = new Label();
            lblFileSize = new Label();
            lblDFileCount = new Label();
            lblDFileSize = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 8);
            label2.Name = "label2";
            label2.Size = new Size(119, 20);
            label2.TabIndex = 1;
            label2.Text = "Total File Count: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 28);
            label3.Name = "label3";
            label3.Size = new Size(173, 20);
            label3.TabIndex = 2;
            label3.Text = "Total Scanned File Sizes: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 67);
            label4.Name = "label4";
            label4.Size = new Size(165, 20);
            label4.TabIndex = 3;
            label4.Text = "Duplicated Files Count: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 87);
            label5.Name = "label5";
            label5.Size = new Size(159, 20);
            label5.TabIndex = 4;
            label5.Text = "Duplicated Files Sizes: ";
            // 
            // lblFile
            // 
            lblFile.AutoSize = true;
            lblFile.Location = new Point(179, 8);
            lblFile.Name = "lblFile";
            lblFile.Size = new Size(17, 20);
            lblFile.TabIndex = 6;
            lblFile.Text = "0";
            // 
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Location = new Point(179, 28);
            lblFileSize.Name = "lblFileSize";
            lblFileSize.Size = new Size(43, 20);
            lblFileSize.TabIndex = 7;
            lblFileSize.Text = "0 MB";
            // 
            // lblDFileCount
            // 
            lblDFileCount.AutoSize = true;
            lblDFileCount.Location = new Point(179, 67);
            lblDFileCount.Name = "lblDFileCount";
            lblDFileCount.Size = new Size(17, 20);
            lblDFileCount.TabIndex = 8;
            lblDFileCount.Text = "0";
            // 
            // lblDFileSize
            // 
            lblDFileSize.AutoSize = true;
            lblDFileSize.Location = new Point(179, 87);
            lblDFileSize.Name = "lblDFileSize";
            lblDFileSize.Size = new Size(43, 20);
            lblDFileSize.TabIndex = 9;
            lblDFileSize.Text = "0 MB";
            // 
            // FormScanSummary
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 117);
            Controls.Add(lblDFileSize);
            Controls.Add(lblDFileCount);
            Controls.Add(lblFileSize);
            Controls.Add(lblFile);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Name = "FormScanSummary";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Result";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblFile;
        private Label lblFileSize;
        private Label lblDFileCount;
        private Label lblDFileSize;
    }
}