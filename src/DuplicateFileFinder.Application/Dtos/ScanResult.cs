using DuplicateFileFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Application.Dtos
{
    public class ScanResult
    {
        public IReadOnlyList<DuplicateGroup> Duplicates { get; set; }
        public ScanSummaryDto Summary { get; set; }
    }
}
