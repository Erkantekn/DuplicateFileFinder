using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateFileFinder.Domain.Enums
{
    public enum ScanStatus
    {
        Idle,
        Preparing,
        Scanning,
        Hashing,
        Completed,
        Cancelled
    }
}
