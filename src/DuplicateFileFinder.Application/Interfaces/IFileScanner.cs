using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DuplicateFileFinder.Application.Interfaces
{
    public interface IFileScanner
    {
        IEnumerable<string> ScanFiles(
     IEnumerable<string> rootDirectories,
     CancellationToken cancellationToken);
    }
}
