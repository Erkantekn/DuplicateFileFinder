using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder.Application.Utils
{
    public static class FileSizeFormatter
    {
        private static readonly string[] Units = { "B", "KB", "MB", "GB", "TB" };

        public static string Format(long bytes)
        {
            if (bytes < 0)
                return "0 B";

            double size = bytes;
            int unitIndex = 0;

            while (size >= 1024 && unitIndex < Units.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }

            return $"{size:N2} {Units[unitIndex]}";
        }
    }
}
