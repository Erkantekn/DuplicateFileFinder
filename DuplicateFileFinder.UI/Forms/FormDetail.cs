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
    public partial class FormDetail : Form
    {
        public FormDetail(ScanResult result)
        {
            InitializeComponent();
            lblDFileCount.Text = result.Summary.DuplicateFileCount.ToString();
            lblDFileSize.Text = FileSizeFormatter.Format(result.Summary.DuplicateSizeBytes);
            btnDelete.Text = string.Format(btnDelete.Text, lblDFileSize.Text);

            tvDuplicates.BeginUpdate();
            tvDuplicates.Nodes.Clear();

            foreach (var group in result.Duplicates)
            {
                var root = new TreeNode(
                    $"{group.Files.Count} copies - {FileSizeFormatter.Format(group.Files[0].Length)}");

                foreach (var file in group.Files)
                {
                    var node = new TreeNode(file.FullPath)
                    {
                        Tag = file
                    };

                    root.Nodes.Add(node);
                }

                tvDuplicates.Nodes.Add(root);
            }

            tvDuplicates.EndUpdate();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
        }
    }
}
