using System.Management;

namespace IFactory.WMI.Win32_USBController
{
    public class USBController : IDisposable
    {
        public static USBController? Get(string name)
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            return Get(name, scope);
        }
        public static USBController? Get(string name, ManagementScope scope)
        {
            ManagementObject? instance = null;
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_USBController WHERE Name='{WmiValue.Escape(name)}'");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
            {
                instance = item;
                break;
            }
            return instance == null ? null : new USBController(instance);
        }

        public static IEnumerable<USBController> Get(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_USBController");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
                yield return new USBController(item);
        }

        private USBController(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private readonly ManagementObject _proxy;
        public void Dispose() => _proxy.Dispose();

        #region Properties

        public UInt16 Availability=> (UInt16)_proxy[nameof(Availability)];
        public string? Caption => (string?)_proxy[nameof(Caption)];
        public uint ConfigManagerErrorCode => (uint)_proxy[nameof(ConfigManagerErrorCode)];
        public bool ConfigManagerUserConfig => (bool)_proxy[nameof(ConfigManagerUserConfig)];
        public string? CreationClassName => (string?)_proxy[nameof(CreationClassName)];
        public string? Description => (string?)_proxy[nameof(Description)];
        public string? DeviceID => (string?)_proxy[nameof(DeviceID)];
        public bool ErrorCleared => (bool)_proxy[nameof(ErrorCleared)];
        public string? ErrorDescription => (string?)_proxy[nameof(ErrorDescription)];
        public DateTime InstallDate => WmiValue.Date(_proxy[nameof(InstallDate)]);
        public uint LastErrorCode => (uint)_proxy[nameof(LastErrorCode)];
        public string? Manufacturer => (string?)_proxy[nameof(Manufacturer)];
        public uint MaxNumberControlled => (uint)_proxy[nameof(MaxNumberControlled)];
        public string? Name => (string?)_proxy[nameof(Name)];
        public string? PNPDeviceID => (string?)_proxy[nameof(PNPDeviceID)];
        public UInt16[]? PowerManagementCapabilities => (UInt16[]?)_proxy[nameof(PowerManagementCapabilities)];
        public bool PowerManagementSupported => (bool)_proxy[nameof(PowerManagementSupported)];
        public UInt16 ProtocolSupported => (UInt16)_proxy[nameof(ProtocolSupported)];
        public string? Status => (string?)_proxy[nameof(Status)];
        public UInt16 StatusInfo => (UInt16)_proxy[nameof(StatusInfo)];
        public string? SystemCreationClassName => (string?)_proxy[nameof(SystemCreationClassName)];
        public string? SystemName => (string?)_proxy[nameof(SystemName)];
        public DateTime TimeOfLastReset => WmiValue.Date(_proxy[nameof(TimeOfLastReset)]);

        #endregion
    }
}
