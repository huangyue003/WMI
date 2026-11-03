using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_Service
{
    public enum StartMode
    {
        Boot,
        System,
        Automatic,
        Manual,
        Disabled
    }
}
