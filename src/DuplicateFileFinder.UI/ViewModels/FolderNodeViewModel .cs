using DuplicateFileFinder.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.UI.ViewModels
{
    public class FolderNodeViewModel : ViewModelBase
    {
        private bool _isChecked;

        public string Name { get; }
        public string FullPath { get; }

        public ObservableCollection<FolderNodeViewModel> Children { get; }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (SetProperty(ref _isChecked, value))
                {
                    foreach (var child in Children)
                        child.IsChecked = value;
                }
            }
        }

        public FolderNodeViewModel(FolderNode node)
        {
            Name = node.Name;
            FullPath = node.FullPath;

            Children = new ObservableCollection<FolderNodeViewModel>(
                node.Children.Select(c => new FolderNodeViewModel(c)));
        }
    }

}
