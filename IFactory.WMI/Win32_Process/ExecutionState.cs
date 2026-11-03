using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_Process
{
    public enum ExecutionState
    {
        Unknown,
        Other,
        Ready,
        Running,
        Blocked,
        SuspendedBlocked,
        SuspendedReady,
        Terminated,
        Stopped,
        Growing,
        Handle,
    }
}
