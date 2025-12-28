using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Interfaces;
using DuplicateFileFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Application.Services
{
    public class DuplicateAnalysisService
    {
        private readonly IFileScanner _fileScanner;
        private readonly IHashService _hashService;
        private readonly IProgressReporter _progressReporter;
        public DuplicateAnalysisService(
            IFileScanner fileScanner,
            IHashService hashService,
            IProgressReporter progressReporter)
        {
            _fileScanner = fileScanner;
            _hashService = hashService;
            _progressReporter = progressReporter;
        }

        public async Task<IReadOnlyList<DuplicateGroup>> AnalyzeAsync(
      ScanOptionsDto options,
      CancellationToken cancellationToken)
        {
            var files = _fileScanner
                .ScanFiles(options.IncludedDirectories, cancellationToken)
                .ToList();

            var entries = files.Select(path => new FileEntry
            {
                FullPath = path,
                Length = new FileInfo(path).Length
            }).ToList();
            entries = entries
                .GroupBy(e => e.FullPath, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .ToList();
            int processed = 0;
            int total = entries.Count;

            var sizeGroups = entries
                .GroupBy(e => e.Length)
                .Where(g => g.Count() > 1);

            var result = new List<DuplicateGroup>();

            foreach (var group in sizeGroups)
            {
                cancellationToken.ThrowIfCancellationRequested();

                foreach (var entry in group)
                {
                    try
                    {
                        entry.Hash = await _hashService
                       .ComputeHashAsync(entry.FullPath, cancellationToken);

                        processed++;
                        int percent = (int)((processed / (double)total) * 100);
                        _progressReporter?.Report(percent);
                    }
                    catch (IOException)
                    {
                        // skip locked file
                        continue;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // skip protected file
                        continue;
                    }
                   
                }

                var hashGroups = group
                    .GroupBy(e => e.Hash)
                    .Where(g => g.Count() > 1);

                foreach (var hashGroup in hashGroups)
                {
                    result.Add(new DuplicateGroup
                    {
                        Hash = hashGroup.Key,
                        Files = hashGroup.ToList()
                    });
                }
            }

            return result;
        }

       

    }
}
