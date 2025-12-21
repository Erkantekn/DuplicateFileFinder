using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFileFinder.Domain.Entities
{
    public class DuplicateGroup
    {
        public string Hash { get; set; }
        public IReadOnlyList<FileEntry> Files { get; set; }
    }
}
