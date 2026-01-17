using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Interfaces;
using DuplicateFileFinder.Application.Utils;
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
        private readonly IMainViewUpdateStatus _viewUpdateStatus;

        public DuplicateAnalysisService(
            IFileScanner fileScanner,
            IHashService hashService,
            IProgressReporter progressReporter,
            IMainViewUpdateStatus viewUpdateStatus)
        {
            _fileScanner = fileScanner;
            _hashService = hashService;
            _progressReporter = progressReporter;
            _viewUpdateStatus = viewUpdateStatus;
        }

        public async Task<ScanResult> AnalyzeAsync(
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
            var sizeGroups = entries
                .GroupBy(e => e.Length)
                .Where(g => g.Count() > 1);
            int total = sizeGroups.Sum(g => g.Count());
            _viewUpdateStatus.UpdateStatus(StatusTextProvider.GetText(Domain.Enums.ScanStatus.Scanning, 0, total));

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
                        _viewUpdateStatus.UpdateStatus(StatusTextProvider.GetText(Domain.Enums.ScanStatus.Scanning,
                            processed, total));
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
            _viewUpdateStatus.UpdateStatus(StatusTextProvider.GetText(Domain.Enums.ScanStatus.Completed));

            var summary = new ScanSummaryDto
            {
                FileCount = entries.Count,
                TotalSizeBytes = entries.Sum(e => e.Length),
                DuplicateFileCount = result.Sum(g => g.Files.Count),
                DuplicateSizeBytes = result.Sum(g => g.Files.Sum(f => f.Length))
            };

            return new ScanResult
            {
                Duplicates = result,
                Summary = summary
            };
        }



    }
}
