namespace DuplicateFileFinder.UI
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectFolder = new Button();
            tvFolders = new TreeView();
            btnScan = new Button();
            panel1 = new Panel();
            btnCancel = new Button();
            tvDuplicates = new TreeView();
            progressBar = new ProgressBar();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(12, 264);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(285, 34);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click_1;
            // 
            // tvFolders
            // 
            tvFolders.CheckBoxes = true;
            tvFolders.Location = new Point(12, 12);
            tvFolders.Name = "tvFolders";
            tvFolders.Size = new Size(285, 246);
            tvFolders.TabIndex = 1;
            tvFolders.AfterCheck += tvFolders_AfterCheck;
            // 
            // btnScan
            // 
            btnScan.Enabled = false;
            btnScan.Location = new Point(12, 304);
            btnScan.Name = "btnScan";
            btnScan.Size = new Size(285, 34);
            btnScan.TabIndex = 2;
            btnScan.Text = "Scan";
            btnScan.UseVisualStyleBackColor = true;
            btnScan.Click += btnScan_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(tvDuplicates);
            panel1.Controls.Add(progressBar);
            panel1.Location = new Point(314, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(700, 335);
            panel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Enabled = false;
            btnCancel.Location = new Point(15, 301);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(682, 34);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // tvDuplicates
            // 
            tvDuplicates.Location = new Point(15, 9);
            tvDuplicates.Name = "tvDuplicates";
            tvDuplicates.Size = new Size(682, 246);
            tvDuplicates.TabIndex = 5;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(15, 261);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(682, 34);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 4;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1026, 450);
            Controls.Add(panel1);
            Controls.Add(btnScan);
            Controls.Add(tvFolders);
            Controls.Add(btnSelectFolder);
            Name = "FormMain";
            Text = "Duplicate File Finder";
            Load += FormMain_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnSelectFolder;
        private TreeView tvFolders;
        private Button btnScan;
        private Panel panel1;
        private ProgressBar progressBar;
        private TreeView tvDuplicates;
        private Button btnCancel;
    }
}
