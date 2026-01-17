using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Interfaces;
using DuplicateFileFinder.Application.Services;
using DuplicateFileFinder.Application.Utils;
using DuplicateFileFinder.Domain.Entities;
using DuplicateFileFinder.Infrastructure.FileSystem;
using DuplicateFileFinder.Infrastructure.Hashing;
using DuplicateFileFinder.UI.Forms;
using DuplicateFileFinder.UI.Progress;
using DuplicateFileFinder.UI.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private CancellationTokenSource _cts;
        private FormDetail formDetail;
        public MainPresenter(
            IMainView view,
            DirectoryTreeBuilder treeBuilder,
            IFileScanner fileScanner,
           IHashService hashService)
        {
            _view = view;
            _treeBuilder = treeBuilder;
            _fileScanner = fileScanner;
            var progressReporter = new WinFormsProgressReporter(
                                    percent => _view.UpdateProgress(percent));

            _analysisService = new DuplicateAnalysisService(
                fileScanner,
                hashService,
                progressReporter,
                _view);
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
        private IReadOnlyList<string> GetEffectiveRootFolders()
        {
            return _view.GetCheckedFolders()
                .Where(f => !f.ParentChecked) // parent seçiliyse child elenir
                .Select(f => f.FullPath)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
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
        public async void OnStartScan()
        {
            try
            {
                _view.UpdateStatus(StatusTextProvider.GetText(Domain.Enums.ScanStatus.Scanning));
                _cts = new CancellationTokenSource();
                _view.SetBusy(true);

                var selectedRoots = GetEffectiveRootFolders();

                if (!selectedRoots.Any())
                {
                    _view.ShowError("No folder selected.");
                    return;
                }

                var options = new ScanOptionsDto
                {
                    IncludedDirectories = selectedRoots
                };

                var result = await _analysisService
                    .AnalyzeAsync(options, _cts.Token);

                _view.ShowDuplicates(result.Duplicates);
                _view.ShowSummary(result.Summary);

                formDetail = new FormDetail(result);

            }
            catch (OperationCanceledException)
            {
                _view.ShowError("Scan cancelled.");
            }
            finally
            {
                _view.SetBusy(false);
            }
        }

        public void CancelScan()
        {
            _view.UpdateStatus(StatusTextProvider.GetText(Domain.Enums.ScanStatus.Cancelled));
            _cts?.Cancel();
        }
        public void ShowDetails()
        {
            if (formDetail != null) formDetail.ShowDialog();
        }
    }
}
