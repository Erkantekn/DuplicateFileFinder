using DuplicateFileFinder.Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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
            var files = new ConcurrentBag<string>();

            Parallel.ForEach(rootDirectories, new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            },
            root =>
            {
                ScanInternal(root, files, cancellationToken);
            });

            return files;
        }

        private void ScanInternal(
            string directory,
            ConcurrentBag<string> files,
            CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            try
            {
                foreach (var file in Directory.GetFiles(directory))
                    files.Add(file);

                foreach (var dir in Directory.GetDirectories(directory))
                    ScanInternal(dir, files, token);
            }
            catch (UnauthorizedAccessException)
            {
                // bilinçli skip
            }
        }
    }
}
