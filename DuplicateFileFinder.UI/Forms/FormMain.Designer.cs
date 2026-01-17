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
            btnShowDetails = new Button();
            btnCancel = new Button();
            tvDuplicates = new TreeView();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(12, 264);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(285, 34);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Add Folder";
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
            btnScan.Click += btnScan_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnShowDetails);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(tvDuplicates);
            panel1.Controls.Add(progressBar);
            panel1.Location = new Point(314, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(700, 415);
            panel1.TabIndex = 4;
            // 
            // btnShowDetails
            // 
            btnShowDetails.Location = new Point(15, 341);
            btnShowDetails.Name = "btnShowDetails";
            btnShowDetails.Size = new Size(682, 34);
            btnShowDetails.TabIndex = 7;
            btnShowDetails.Text = "Show Details";
            btnShowDetails.UseVisualStyleBackColor = true;
            btnShowDetails.Visible = false;
            btnShowDetails.Click += btnShowDetails_Click;
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
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblStatus.ForeColor = Color.DarkGray;
            lblStatus.Location = new Point(414, 421);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(600, 20);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Waiting for start";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1026, 450);
            Controls.Add(lblStatus);
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
        private Label lblStatus;
        private Button btnShowDetails;
    }
}
