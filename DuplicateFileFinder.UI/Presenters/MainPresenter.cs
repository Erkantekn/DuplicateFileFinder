using DuplicateFileFinder.Domain;
using DuplicateFileFinder.Infrastructure.FileSystem;
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

        public MainPresenter(
            IMainView view,
            DirectoryTreeBuilder treeBuilder)
        {
            _view = view;
            _treeBuilder = treeBuilder;
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
    }
}
