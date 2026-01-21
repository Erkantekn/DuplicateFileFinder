using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Utils;
using DuplicateFileFinder.Domain.Entities;
using DuplicateFileFinder.UI.Views;
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
        private readonly IMainView _mainForm;
        public FormDetail(ScanResult result, IMainView mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
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
            var filesToDelete = GetCheckedFiles();

            if (!filesToDelete.Any())
            {
                MessageBox.Show("No file to delete has been selected.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long totalSize = filesToDelete.Sum(f => f.Length);

            var confirm = MessageBox.Show(
                $"{filesToDelete.Count} files will be deleted.\n" +
                $"Total Size: {FileSizeFormatter.Format(totalSize)}\n\n" +
                $"Do you want to continue?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            int deletedCount = 0;

            foreach (var file in filesToDelete)
            {
                try
                {
                    File.Delete(file.FullPath);
                    deletedCount++;
                }
                catch
                {
                    // erişilemeyen dosyalar atlanır
                }
            }

            MessageBox.Show(
                $"{deletedCount} files deleted.",
                "Process Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            RemoveDeletedNodes();
            _mainForm.Reset();
            this.Close();
        }
        private List<FileEntry> GetCheckedFiles()
        {
            var result = new List<FileEntry>();

            foreach (TreeNode root in tvDuplicates.Nodes)
            {
                foreach (TreeNode node in root.Nodes)
                {
                    if (/*node.Checked && */node.Tag is FileEntry file)
                    {
                        result.Add(file);
                    }
                }
            }

            return result;
        }
        private void RemoveDeletedNodes()
        {
            foreach (TreeNode root in tvDuplicates.Nodes.Cast<TreeNode>().ToList())
            {
                foreach (TreeNode node in root.Nodes.Cast<TreeNode>().ToList())
                {
                    if (node.Checked)
                        root.Nodes.Remove(node);
                }

                if (root.Nodes.Count == 0)
                    tvDuplicates.Nodes.Remove(root);
            }
        }


    }
}
