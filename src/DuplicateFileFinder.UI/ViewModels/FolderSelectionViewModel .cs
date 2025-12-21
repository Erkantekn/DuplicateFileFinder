using DuplicateFileFinder.Infrastructure.FileSystem;
using DuplicateFileFinder.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
namespace DuplicateFileFinder.UI.ViewModels
{
    public class FolderSelectionViewModel : ViewModelBase
    {

        private readonly DirectoryTreeBuilder _treeBuilder;

        public ObservableCollection<FolderNodeViewModel> RootNodes { get; }
            = new ObservableCollection<FolderNodeViewModel>();

        public RelayCommand SelectRootFolderCommand { get; }

        public FolderSelectionViewModel(DirectoryTreeBuilder treeBuilder)
        {
            _treeBuilder = treeBuilder;
            SelectRootFolderCommand = new RelayCommand(SelectRootFolder);
        }

        private void SelectRootFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                RootNodes.Clear();

                var root = _treeBuilder.Build(dialog.SelectedPath);
                RootNodes.Add(new FolderNodeViewModel(root));
            }
        }

        public IReadOnlyList<string> GetSelectedFolders()
        {
            var result = new List<string>();

            foreach (var root in RootNodes)
                Traverse(root, result);

            return result;
        }

        private void Traverse(
            FolderNodeViewModel node,
            IList<string> result)
        {
            if (node.IsChecked)
                result.Add(node.FullPath);

            foreach (var child in node.Children)
                Traverse(child, result);
        }
    }

}
