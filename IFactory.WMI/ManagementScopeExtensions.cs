using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI
{
    public static class ManagementScopeExtensions
    {
        public static ManagementScope Create(string host,string wmiNamespace,ConnectionOptions options)
        {
            var  scope = new ManagementScope(String.Format("\\\\{0}\\{1}", host, wmiNamespace), options);
            scope.Connect();
            return scope;
        }
        public static ManagementScope CreateDefault()
        {
            string host = "localhost";
            string wmiNamespace = "root\\cimv2";
            ConnectionOptions options = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                Authentication = AuthenticationLevel.PacketPrivacy,
                EnablePrivileges = true,
            };
            return Create(host,wmiNamespace,options);
        }
    }
}
