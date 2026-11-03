using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_Service
{
    public enum ServiceType
    {
        KernelDriver=0x01,
        FileSystemDriver=0x02,
        Adapter=0x04,
        RecognizerDriver=0x08,
        OwnProces=0x10,
        ShareProcess=0x20,
        InteractiveProcess=0x100,
    }
}
