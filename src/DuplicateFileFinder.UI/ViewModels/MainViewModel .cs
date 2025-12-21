using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.UI.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        public FolderSelectionViewModel FolderSelection { get; }

        public MainViewModel(FolderSelectionViewModel folderSelection)
        {
            FolderSelection = folderSelection;
        }
    }
}
