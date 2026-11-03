using System.Management;

namespace IFactory.WMI.Win32_LogicalDisk
{
    public class LogicalDisk : IDisposable
    {
        public static LogicalDisk? Get(string name)
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            return Get(name, scope);
        }
        public static LogicalDisk? Get(string name, ManagementScope scope)
        {
            ManagementObject? instance = null;
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_LogicalDisk WHERE Name='{WmiValue.Escape(name)}'");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
            {
                instance = item;
                break;
            }
            return instance == null ? null : new LogicalDisk(instance);
        }

        public static IEnumerable<LogicalDisk> Get(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_LogicalDisk");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
                yield return new LogicalDisk(item);
        }

        private LogicalDisk(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private readonly ManagementObject _proxy;
        public void Dispose() => _proxy.Dispose();

        #region Properties
        public UInt16 Access=> (UInt16)_proxy[nameof(Access)];
        public UInt16 Availability=> (UInt16)_proxy[nameof(Availability)];
        public UInt64 BlockSize=> (UInt64)_proxy[nameof(BlockSize)];
        public string? Caption => (string?)_proxy[nameof(Caption)];
        public bool Compressed=> (bool)_proxy[nameof(Compressed)];
        public UInt32 ConfigManagerErrorCode=> (UInt32)_proxy[nameof(ConfigManagerErrorCode)];
        public bool ConfigManagerUserConfig=> (bool)_proxy[nameof(ConfigManagerUserConfig)];
        public string? CreationClassName => (string?)_proxy[nameof(CreationClassName)];
        public string? Description => (string?)_proxy[nameof(Description)];
        public string? DeviceID => (string?)_proxy[nameof(DeviceID)];
        public DriveType DriveType => (DriveType)Convert.ToInt32(_proxy[nameof(DriveType)]);
        public bool ErrorCleared=> (bool)_proxy[nameof(ErrorCleared)];
        public string? ErrorDescription => (string?)_proxy[nameof(ErrorDescription)];
        public string? ErrorMethodology => (string?)_proxy[nameof(ErrorMethodology)];
        public string? FileSystem => (string?)_proxy[nameof(FileSystem)];
        public UInt64 FreeSpace=> (UInt64)_proxy[nameof(FreeSpace)];
        public DateTime InstallDate=> WmiValue.Date(_proxy[nameof(InstallDate)]);
        public UInt32 LastErrorCode=> (UInt32)_proxy[nameof(LastErrorCode)];
        public UInt32 MaximumComponentLength=> (UInt32)_proxy[nameof(MaximumComponentLength)];
        public UInt32 MediaType=> (UInt32)_proxy[nameof(MediaType)];
        public string? Name => (string?)_proxy[nameof(Name)];
        public UInt64 NumberOfBlocks=> (UInt64)_proxy[nameof(NumberOfBlocks)];
        public string? PNPDeviceID => (string?)_proxy[nameof(PNPDeviceID)];
        public UInt16[]? PowerManagementCapabilities => (UInt16[]?)_proxy[nameof(PowerManagementCapabilities)];
        public bool PowerManagementSupported=> (bool)_proxy[nameof(PowerManagementSupported)];
        public string? ProviderName => (string?)_proxy[nameof(ProviderName)];
        public string? Purpose => (string?)_proxy[nameof(Purpose)];
        public bool QuotasDisabled=> (bool)_proxy[nameof(QuotasDisabled)];
        public bool QuotasIncomplete=> (bool)_proxy[nameof(QuotasIncomplete)];
        public bool QuotasRebuilding=> (bool)_proxy[nameof(QuotasRebuilding)];
        public UInt64 Size=> (UInt64)_proxy[nameof(Size)];
        public string? Status => (string?)_proxy[nameof(Status)];
        public UInt16 StatusInfo=> (UInt16)_proxy[nameof(StatusInfo)];
        public bool SupportsDiskQuotas=> (bool)_proxy[nameof(SupportsDiskQuotas)];
        public bool SupportsFileBasedCompression=> (bool)_proxy[nameof(SupportsFileBasedCompression)];
        public string? SystemCreationClassName => (string?)_proxy[nameof(SystemCreationClassName)];
        public string? SystemName => (string?)_proxy[nameof(SystemName)];
        public bool VolumeDirty=> (bool)_proxy[nameof(VolumeDirty)];
        public string? VolumeName => (string?)_proxy[nameof(VolumeName)];
        public string? VolumeSerialNumber => (string?)_proxy[nameof(VolumeSerialNumber)];


        #endregion
    }
}
