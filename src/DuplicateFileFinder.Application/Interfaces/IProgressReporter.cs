using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Application.Interfaces
{
    public interface IProgressReporter
    {
        void Report(int percent);
    }
}
