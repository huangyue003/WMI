using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_Service
{
    public class Win32_ServiceInfo
    {
     public   bool AcceptPause;
        public bool AcceptStop;
        public string Caption = string.Empty;
        public int CheckPoint;
        public string CreationClassName = string.Empty;
        public bool DelayedAutoStart;
        public string Description = string.Empty;
        public bool DesktopInteract;
        public string DisplayName = string.Empty;
        public string ErrorControl = string.Empty;
        public int ExitCode;
        public DateTime InstallDate;
        public string Name = string.Empty;
        public string PathName = string.Empty;
        public int ProcessId;
        public int ServiceSpecificExitCode;
        public string ServiceType = string.Empty;
        public bool Started;
        public string StartMode = string.Empty;
        public string StartName = string.Empty;
        public string State = string.Empty;
        public string Status = string.Empty;
        public string SystemCreationClassName = string.Empty;
        public string SystemName = string.Empty;
        public int TagId;
        public int WaitHint;
    }
}
