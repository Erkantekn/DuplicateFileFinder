using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Application.Dtos
{
    public class ScanSummaryDto
    {
        public int FileCount { get; set; }
        public long TotalSizeBytes { get; set; }

        public int DuplicateFileCount { get; set; }
        public long DuplicateSizeBytes { get; set; }
    }
}
