using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Interfaces;
using DuplicateFileFinder.Application.Services;
using DuplicateFileFinder.Domain.Entities;
using DuplicateFileFinder.Infrastructure.FileSystem;
using DuplicateFileFinder.Infrastructure.Hashing;
using DuplicateFileFinder.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.UI.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly DirectoryTreeBuilder _treeBuilder;
        private readonly IFileScanner _fileScanner;
        private readonly DuplicateAnalysisService _analysisService;
        public MainPresenter(
            IMainView view,
            DirectoryTreeBuilder treeBuilder,
            IFileScanner fileScanner,
           IHashService hashService)
        {
            _view = view;
            _treeBuilder = treeBuilder;
            _fileScanner = fileScanner;
            _analysisService = new DuplicateAnalysisService(fileScanner, hashService); ;
        }
        public IReadOnlyList<string> GetSelectedFolders(TreeView treeView)
        {
            var result = new List<string>();

            foreach (TreeNode node in treeView.Nodes)
            {
                
                CollectCheckedNodes(node, result);
            }

            return result;
        }

        private void Traverse(TreeNode node, IList<string> result)
        {
            if (node.Checked && node.Tag is string path)
                result.Add(path);

            foreach (TreeNode child in node.Nodes)
                Traverse(child, result);
        }
        public void OnSelectRootFolder()
        {
            using var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var root = _treeBuilder.Build(dialog.SelectedPath);
                var node = BuildTreeNode(root);
                _view.SetFolderTree(node);
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private TreeNode BuildTreeNode(FolderNode folder)
        {
            var node = new TreeNode(folder.Name)
            {
                Tag = folder.FullPath,
                Checked = true
            };

            foreach (var child in folder.Children)
                node.Nodes.Add(BuildTreeNode(child));

            return node;
        }
        private void CollectCheckedNodes(TreeNode node, List<string> result)
        {
            if (node.Checked)
                result.Add(node.Tag.ToString());

            foreach (TreeNode child in node.Nodes)
            {
                CollectCheckedNodes(child, result);
            }
        }
        public async void OnStartScan(IReadOnlyList<string> folders)
        {
            try
            {
                var options = new ScanOptionsDto
                {
                    IncludedDirectories = folders
                };

                using var cts = new CancellationTokenSource();

                var result = await _analysisService
                    .AnalyzeAsync(options, cts.Token);

                _view.ShowDuplicates(result);
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

    }
}
