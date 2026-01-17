using DuplicateFileFinder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Application.Utils
{
    public static class StatusTextProvider
    {
        public static string GetText(ScanStatus status, int? current = null, int? total = null)
        {
            return status switch
            {
                ScanStatus.Idle => "Waiting for start",
                ScanStatus.Preparing => "Preparing for scan...",
                ScanStatus.Scanning =>
                    current.HasValue && total.HasValue
                        ? $"Scanning files ({current}/{total})"
                        : "Scanning files...",
                ScanStatus.Hashing => "Hashing files...",
                ScanStatus.Completed => "Scan completed",
                ScanStatus.Cancelled => "Scan cancelled",
                _ => string.Empty
            };
        }
    }
}
