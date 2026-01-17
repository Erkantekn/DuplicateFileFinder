using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Interfaces;
using DuplicateFileFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.UI.Views
{
    public interface IMainView : IMainViewUpdateStatus
    {
        void SetFolderTree(TreeNode rootNode);
        void ShowError(string message);
        void ShowDuplicates(IReadOnlyList<DuplicateGroup> groups);
        void SetBusy(bool isBusy);
        void UpdateProgress(int percent);
        IReadOnlyList<FolderSelection> GetCheckedFolders();
        void ShowSummary(ScanSummaryDto summary);
    }
}
