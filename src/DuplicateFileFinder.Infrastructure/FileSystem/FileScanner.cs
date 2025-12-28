using DuplicateFileFinder.Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Infrastructure.FileSystem
{
    public class FileScanner : IFileScanner
    {
        public IEnumerable<string> ScanFiles(
       IEnumerable<string> rootDirectories,
       CancellationToken cancellationToken)
        {
            var files = new ConcurrentDictionary<string, byte>();
          
            Parallel.ForEach(rootDirectories, new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            },
            root =>
            {
                ScanInternal(root, files, cancellationToken);
            });

            return files.Keys.ToList(); 
        }

        private void ScanInternal(
     string directory,
     ConcurrentDictionary<string, byte> files,
     CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            try
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    files.TryAdd(file, 0); // aynı path bir kez eklenir
                }

                foreach (var dir in Directory.GetDirectories(directory))
                {
                    ScanInternal(dir, files, token);
                }
            }
            catch
            {
                // bilinçli skip
            }
        }

    }
}
