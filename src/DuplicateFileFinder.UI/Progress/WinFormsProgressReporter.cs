using DuplicateFileFinder.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.UI.Progress
{
    public class WinFormsProgressReporter : IProgressReporter
    {
        private readonly Action<int> _updateAction;

        public WinFormsProgressReporter(Action<int> updateAction)
        {
            _updateAction = updateAction;
        }

        public void Report(int percent)
        {
            _updateAction?.Invoke(percent);
        }
    }
}
