using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFileFinder.Domain.Entities
{
    public class FileEntry
    {
        public int Id { get; set; }
        public string FullPath { get; set; }
        public long Length { get; set; }
        public string Hash { get; set; }
    }
}
