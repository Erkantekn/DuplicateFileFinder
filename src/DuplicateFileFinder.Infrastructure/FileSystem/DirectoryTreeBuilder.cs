using DuplicateFileFinder.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DuplicateFileFinder.Infrastructure.FileSystem
{
    public class DirectoryTreeBuilder
    {
        public FolderNode Build(string rootPath)
        {
            var root = new FolderNode(rootPath);
            BuildInternal(root);
            return root;
        }

        private void BuildInternal(FolderNode parent)
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(parent.FullPath))
                {
                    var child = new FolderNode(dir);
                    parent.AddChild(child);
                    BuildInternal(child);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Yetkisiz klasörler bilinçli olarak atlanır
            }
        }
    }
}
