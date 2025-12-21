using DuplicateFileFinder.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Infrastructure.Hashing
{
    public class Md5HashService : IHashService
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
                FileShare.Read,
                bufferSize: 1024 * 64,
                useAsync: true);

            using var md5 = MD5.Create();

            var hashBytes = await md5.ComputeHashAsync(stream, cancellationToken)
                                     .ConfigureAwait(false);

            return Convert.ToHexString(hashBytes);
        }
    }
}
