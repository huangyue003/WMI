using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_USBHub
{
    public class USBHub : IDisposable
    {
        public static USBHub? Get(string name)
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            return Get(name, scope);
        }
        public static USBHub? Get(string name, ManagementScope scope)
        {
            ManagementObject? instance = null;
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_USBHub WHERE Name='{WmiValue.Escape(name)}'");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
            {
                instance = item;
                break;
            }
            return instance == null ? null : new USBHub(instance);
        }

        public static IEnumerable<USBHub> Get(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_USBHub");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
                yield return new USBHub(item);
        }

        private USBHub(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private readonly ManagementObject _proxy;
        public void Dispose() => _proxy.Dispose();

        #region Properties

        public UInt16 Availability => (UInt16)_proxy[nameof(Availability)];
        public string Caption=> (string)_proxy[nameof(Caption)];
        public byte ClassCode=> (byte)_proxy[nameof(ClassCode)];
        public bool ConfigManagerUserConfig=> (bool)_proxy[nameof(ConfigManagerUserConfig)];
        public string CreationClassName=> (string)_proxy[nameof(CreationClassName)];
        public byte[] CurrentAlternateSettings=> (byte[])_proxy[nameof(CurrentAlternateSettings)];
        public byte CurrentConfigValue=> (byte)_proxy[nameof(CurrentConfigValue)];
        public string Description=> (string)_proxy[nameof(Description)];
        public bool ErrorCleared=> (bool)_proxy[nameof(ErrorCleared)];
        public string ErrorDescription=> (string)_proxy[nameof(ErrorDescription)];
        public bool GangSwitched=> (bool)_proxy[nameof(GangSwitched)];
        public DateTime InstallDate=> WmiValue.Date(_proxy[nameof(InstallDate)]);
        public uint LastErrorCode=> (uint)_proxy[nameof(LastErrorCode)];
        public byte NumberOfConfigs=> (byte)_proxy[nameof(NumberOfConfigs)];
        public byte NumberOfPorts=> (byte)_proxy[nameof(NumberOfPorts)];
        public string PNPDeviceID=> (string)_proxy[nameof(PNPDeviceID)];
        public UInt16[] PowerManagementCapabilities=> (UInt16[])_proxy[nameof(PowerManagementCapabilities)];
        public bool PowerManagementSupported=> (bool)_proxy[nameof(PowerManagementSupported)];
        public byte ProtocolCode=> (byte)_proxy[nameof(ProtocolCode)];
        public string Status=> (string)_proxy[nameof(Status)];
        public UInt16 StatusInfo=> (UInt16)_proxy[nameof(StatusInfo)];
        public byte SubclassCode=> (byte)_proxy[nameof(SubclassCode)];
        public string SystemCreationClassName=> (string)_proxy[nameof(SystemCreationClassName)];
        public string SystemName=> (string)_proxy[nameof(SystemName)];
        public UInt16 USBVersion=> (UInt16)_proxy[nameof(USBVersion)];
        public uint ConfigManagerErrorCode=> (uint)_proxy[nameof(ConfigManagerErrorCode)];
        public string DeviceID=> (string)_proxy[nameof(DeviceID)];
        public string Name=> (string)_proxy[nameof(Name)];

        #endregion
    }
}
