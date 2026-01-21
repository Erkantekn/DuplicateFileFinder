namespace DuplicateFileFinder.UI.Forms
{
    partial class FormDetail
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
            lblDFileSize = new Label();
            lblDFileCount = new Label();
            label5 = new Label();
            label4 = new Label();
            tvDuplicates = new TreeView();
            btnDelete = new Button();
            lblInfo = new Label();
            SuspendLayout();
            // 
            // lblDFileSize
            // 
            lblDFileSize.AutoSize = true;
            lblDFileSize.Location = new Point(179, 29);
            lblDFileSize.Name = "lblDFileSize";
            lblDFileSize.Size = new Size(43, 20);
            lblDFileSize.TabIndex = 13;
            lblDFileSize.Text = "0 MB";
            // 
            // lblDFileCount
            // 
            lblDFileCount.AutoSize = true;
            lblDFileCount.Location = new Point(179, 9);
            lblDFileCount.Name = "lblDFileCount";
            lblDFileCount.Size = new Size(17, 20);
            lblDFileCount.TabIndex = 12;
            lblDFileCount.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 29);
            label5.Name = "label5";
            label5.Size = new Size(159, 20);
            label5.TabIndex = 11;
            label5.Text = "Duplicated Files Sizes: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 9);
            label4.Name = "label4";
            label4.Size = new Size(165, 20);
            label4.TabIndex = 10;
            label4.Text = "Duplicated Files Count: ";
            // 
            // tvDuplicates
            // 
            tvDuplicates.Location = new Point(12, 64);
            tvDuplicates.Name = "tvDuplicates";
            tvDuplicates.Size = new Size(682, 246);
            tvDuplicates.TabIndex = 14;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(12, 316);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(682, 36);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete Duplicated Files And Save {0}";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblInfo
            // 
            lblInfo.BackColor = Color.IndianRed;
            lblInfo.Location = new Point(12, 355);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(682, 33);
            lblInfo.TabIndex = 16;
            lblInfo.Text = "The oldest file will be kept based on its creation date, and the others will be deleted.";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(706, 397);
            Controls.Add(lblInfo);
            Controls.Add(btnDelete);
            Controls.Add(tvDuplicates);
            Controls.Add(lblDFileSize);
            Controls.Add(lblDFileCount);
            Controls.Add(label5);
            Controls.Add(label4);
            Name = "FormDetail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Details";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDFileSize;
        private Label lblDFileCount;
        private Label label5;
        private Label label4;
        private TreeView tvDuplicates;
        private Button btnDelete;
        private Label lblInfo;
    }
}