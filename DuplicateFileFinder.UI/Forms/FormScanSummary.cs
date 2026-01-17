using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuplicateFileFinder.UI.Forms
{
    public partial class FormScanSummary : Form
    {
        public FormScanSummary(ScanSummaryDto summary)
        {
            InitializeComponent();

            lblFile.Text = summary.FileCount.ToString();
            lblFileSize.Text = FileSizeFormatter.Format(summary.TotalSizeBytes);

            lblDFileCount.Text = summary.DuplicateFileCount.ToString();
            lblDFileSize.Text = FileSizeFormatter.Format(summary.DuplicateSizeBytes);
        }
    

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
