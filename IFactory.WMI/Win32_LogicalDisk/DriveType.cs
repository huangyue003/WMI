using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_LogicalDisk
{
    public enum DriveType
    {
        Unknown,
        NoRootDirectory,
        RemovableDisk,
        LocalDisk,
        NetworkDrive,
        CompactDisc,
        RAMDisk,
        ErrorCleared
    }
}
