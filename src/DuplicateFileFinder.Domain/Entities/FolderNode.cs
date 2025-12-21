using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DuplicateFileFinder.Domain.Entities
{
    public sealed class FolderNode
    {
        public string FullPath { get; }
        public string Name { get; }

        public bool IsChecked { get; set; }

        public IReadOnlyList<FolderNode> Children => _children;
        private readonly List<FolderNode> _children = new List<FolderNode>();

        public FolderNode(string path)
        {
            FullPath = path;
            Name = Path.GetFileName(path);
        }

        public void AddChild(FolderNode child)
            => _children.Add(child);
    }
}
