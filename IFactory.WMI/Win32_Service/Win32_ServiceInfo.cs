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
        public string Caption;
        public int CheckPoint;
        public string CreationClassName;
        public bool DelayedAutoStart;
        public string Description;
        public bool DesktopInteract;
        public string DisplayName;
        public string ErrorControl;
        public int ExitCode;
        public DateTime InstallDate;
        public string Name;
        public string PathName;
        public int ProcessId;
        public int ServiceSpecificExitCode;
        public string ServiceType;
        public bool Started;
        public string StartMode;
        public string StartName;
        public string State;
        public string Status;
        public string SystemCreationClassName;
        public string SystemName;
        public int TagId;
        public int WaitHint;
    }
}
