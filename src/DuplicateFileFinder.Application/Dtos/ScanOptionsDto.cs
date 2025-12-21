using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFileFinder.Application.Dtos
{
    public class ScanOptionsDto
    {
        public IReadOnlyList<string> IncludedDirectories { get; set; }
    }
}
