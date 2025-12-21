using DuplicateFileFinder.Application.Dtos;
using DuplicateFileFinder.Application.Interfaces;
using DuplicateFileFinder.Domain.Entities;
using System;
using System.Collections.Generic;
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

        public DuplicateAnalysisService(
            IFileScanner fileScanner,
            IHashService hashService)
        {
            _fileScanner = fileScanner;
            _hashService = hashService;
        }

        public async Task<IReadOnlyList<DuplicateGroup>> AnalyzeAsync(
            ScanOptionsDto options,
            CancellationToken cancellationToken)
        {
            // 1. Dosyaları topla
            var files = _fileScanner
                .ScanFiles(options.IncludedDirectories, cancellationToken)
                .ToList();

            // 2. FileEntry oluştur
            var entries = files.Select(path => new FileEntry
            {
                FullPath = path,
                Length = new System.IO.FileInfo(path).Length
            }).ToList();

            // 3. Boyuta göre grupla (kritik optimizasyon)
            var sizeGroups = entries
                .GroupBy(e => e.Length)
                .Where(g => g.Count() > 1);

            var duplicateGroups = new List<DuplicateGroup>();

            // 4. Aynı boyuttakiler için hash al
            foreach (var sizeGroup in sizeGroups)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await ComputeHashesAsync(sizeGroup, cancellationToken);

                var hashGroups = sizeGroup
                    .GroupBy(e => e.Hash)
                    .Where(g => g.Count() > 1);

                foreach (var hashGroup in hashGroups)
                {
                    duplicateGroups.Add(new DuplicateGroup
                    {
                        Hash = hashGroup.Key,
                        Files = hashGroup.ToList()
                    });
                }
            }

            return duplicateGroups;
        }

        private async Task ComputeHashesAsync(
            IEnumerable<FileEntry> entries,
            CancellationToken cancellationToken)
        {
            var tasks = entries.Select(async entry =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                entry.Hash = await _hashService
                    .ComputeHashAsync(entry.FullPath, cancellationToken)
                    .ConfigureAwait(false);
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
