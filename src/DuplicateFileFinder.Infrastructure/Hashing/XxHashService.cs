using DuplicateFileFinder.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Infrastructure.Hashing
{
    public class XxHashService : IHashService
    {
        public async Task<string> ComputeHashAsync(
            string filePath,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite,
                bufferSize: 1024 * 64,
                useAsync: true);

            var hasher = new XxHash64();

            var buffer = new byte[1024 * 64];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(
                       buffer, 0, buffer.Length, cancellationToken)
                       .ConfigureAwait(false)) > 0)
            {
                hasher.Append(buffer.AsSpan(0, bytesRead));
            }

            var hashBytes = hasher.GetCurrentHash();
            return Convert.ToHexString(hashBytes);
        }
    }
}
