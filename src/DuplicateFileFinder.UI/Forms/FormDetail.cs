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
            var filesToDelete = new List<FileEntry>();

            foreach (TreeNode root in tvDuplicates.Nodes)
            {
                var files = root.Nodes
                    .Cast<TreeNode>()
                    .Select(n => n.Tag as FileEntry)
                    .Where(f => f != null)
                    .ToList();

                if (files.Count <= 1)
                    continue;

                var fileToKeep = files
                    .OrderBy(f => File.GetCreationTime(f.FullPath))
                    .First();

                var toDelete = files
                    .Where(f => f.FullPath != fileToKeep.FullPath);

                filesToDelete.AddRange(toDelete);
            }

            if (!filesToDelete.Any())
            {
                MessageBox.Show("No duplicate files found to delete.");
                return;
            }

            long totalSize = filesToDelete.Sum(f => f.Length);

            var confirm = MessageBox.Show(
                $"{filesToDelete.Count} duplicate files will be deleted.\n" +
                $"Total Size: {FileSizeFormatter.Format(totalSize)}\n\n" +
                $"Oldest files will be preserved.\n\nContinue?",
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
                    // continue for not accesable files
                }
            }

            MessageBox.Show(
                $"{deletedCount} files deleted successfully.",
                "Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            RemoveDeletedNodes();
            _mainForm.Reset();
            this.Close();
        }
       
        private void RemoveDeletedNodes()
        {
            foreach (TreeNode root in tvDuplicates.Nodes.Cast<TreeNode>().ToList())
            {
                foreach (TreeNode node in root.Nodes.Cast<TreeNode>().ToList())
                {
                    if (!File.Exists(((FileEntry)node.Tag).FullPath))
                        root.Nodes.Remove(node);
                }

                if (root.Nodes.Count <= 1)
                    tvDuplicates.Nodes.Remove(root);
            }
        }


    }
}
