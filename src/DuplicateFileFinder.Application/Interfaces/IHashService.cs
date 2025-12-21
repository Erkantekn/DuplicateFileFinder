using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Application.Interfaces
{
    public interface IHashService
    {
        Task<string> ComputeHashAsync(
          string filePath,
          CancellationToken cancellationToken);
    }
}
