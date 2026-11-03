using System.Management;

namespace IFactory.WMI.MSFT_NetAdapter
{
    public class NetAdapter : IDisposable
    {
        public static NetAdapter? Get(string name)
        {
            var scope = ManagementScopeExtensions.CreateDefault(@"root\StandardCimv2");
            return Get(name, scope);
        }
        public static NetAdapter? Get(string name, ManagementScope scope)
        {
            ManagementObject? instance = null;
            ObjectQuery query = new ObjectQuery($"SELECT * FROM MSFT_NetAdapter  WHERE Name='{WmiValue.Escape(name)}'");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
            {
                instance = item;
                break;
            }
            return instance == null ? null : new NetAdapter(instance);
        }

        public static IEnumerable<NetAdapter> Get()
        {
            var scope = ManagementScopeExtensions.CreateDefault(@"root\StandardCimv2");
            return Get(scope);
        }
        public static IEnumerable<NetAdapter> Get(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery($"SELECT * FROM MSFT_NetAdapter ");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
                yield return new NetAdapter(item);
        }

        private NetAdapter(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private readonly ManagementObject _proxy;
        public void Dispose() => _proxy.Dispose();

        #region Properties

        public string? Caption => (string?)_proxy[nameof(Caption)];
        public string? Description => (string?)_proxy[nameof(Description)];
        public DateTime InstallDate=> WmiValue.Date(_proxy[nameof(InstallDate)]);
        public string? Name => (string?)_proxy[nameof(Name)];
        public string? Status => (string?)_proxy[nameof(Status)];
        public UInt16 Availability=> (UInt16)_proxy[nameof(Availability)];
        public UInt32 ConfigManagerErrorCode=> (UInt32)_proxy[nameof(ConfigManagerErrorCode)];
        public bool ConfigManagerUserConfig=> (bool)_proxy[nameof(ConfigManagerUserConfig)];
        public string? CreationClassName => (string?)_proxy[nameof(CreationClassName)];
        public string? DeviceID => (string?)_proxy[nameof(DeviceID)];
        public bool ErrorCleared=> (bool)_proxy[nameof(ErrorCleared)];
        public string? ErrorDescription => (string?)_proxy[nameof(ErrorDescription)];
        public UInt32 LastErrorCode=> (UInt32)_proxy[nameof(LastErrorCode)];
        // The local provider exposes one case-insensitive PnPDeviceID property.
        public string? PNPDeviceID => (string?)_proxy[nameof(PNPDeviceID)];
        public UInt16[]? PowerManagementCapabilities => (UInt16[]?)_proxy[nameof(PowerManagementCapabilities)];
        public bool PowerManagementSupported=> (bool)_proxy[nameof(PowerManagementSupported)];
        public UInt16 StatusInfo=> (UInt16)_proxy[nameof(StatusInfo)];
        public string? SystemCreationClassName => (string?)_proxy[nameof(SystemCreationClassName)];
        public string? SystemName => (string?)_proxy[nameof(SystemName)];
        public UInt64 Speed=> (UInt64)_proxy[nameof(Speed)];
        public UInt64 MaxSpeed=> (UInt64)_proxy[nameof(MaxSpeed)];
        public UInt64 RequestedSpeed=> (UInt64)_proxy[nameof(RequestedSpeed)];
        public UInt16 UsageRestriction=> (UInt16)_proxy[nameof(UsageRestriction)];
        public UInt16 PortType=> (UInt16)_proxy[nameof(PortType)];
        public string? OtherPortType => (string?)_proxy[nameof(OtherPortType)];
        public string? OtherNetworkPortType => (string?)_proxy[nameof(OtherNetworkPortType)];
        public UInt16 PortNumber=> (UInt16)_proxy[nameof(PortNumber)];
        public UInt16 LinkTechnology=> (UInt16)_proxy[nameof(LinkTechnology)];
        public string? OtherLinkTechnology => (string?)_proxy[nameof(OtherLinkTechnology)];
        public string? PermanentAddress => (string?)_proxy[nameof(PermanentAddress)];
        public string[]? NetworkAddresses => (string[]?)_proxy[nameof(NetworkAddresses)];
        public bool FullDuplex=> (bool)_proxy[nameof(FullDuplex)];
        public bool AutoSense=> (bool)_proxy[nameof(AutoSense)];
        public UInt64 SupportedMaximumTransmissionUnit=> (UInt64)_proxy[nameof(SupportedMaximumTransmissionUnit)];
        public UInt64 ActiveMaximumTransmissionUnit=> (UInt64)_proxy[nameof(ActiveMaximumTransmissionUnit)];
        public string? InterfaceDescription => (string?)_proxy[nameof(InterfaceDescription)];
        public string? InterfaceName => (string?)_proxy[nameof(InterfaceName)];
        public UInt64 NetLuid=> (UInt64)_proxy[nameof(NetLuid)];
        public string? InterfaceGuid => (string?)_proxy[nameof(InterfaceGuid)];
        public UInt32 InterfaceIndex=> (UInt32)_proxy[nameof(InterfaceIndex)];
        public string? DeviceName => (string?)_proxy[nameof(DeviceName)];
        public UInt32 NetLuidIndex=> (UInt32)_proxy[nameof(NetLuidIndex)];
        public bool Virtual=> (bool)_proxy[nameof(Virtual)];
        public bool Hidden=> (bool)_proxy[nameof(Hidden)];
        public bool NotUserRemovable=> (bool)_proxy[nameof(NotUserRemovable)];
        public bool IMFilter=> (bool)_proxy[nameof(IMFilter)];
        public UInt32 InterfaceType=> (UInt32)_proxy[nameof(InterfaceType)];
        public bool HardwareInterface=> (bool)_proxy[nameof(HardwareInterface)];
        public bool WdmInterface=> (bool)_proxy[nameof(WdmInterface)];
        public bool EndPointInterface=> (bool)_proxy[nameof(EndPointInterface)];
        public bool iSCSIInterface=> (bool)_proxy[nameof(iSCSIInterface)];
        public UInt32 State=> (UInt32)_proxy[nameof(State)];
        public UInt32 NdisMedium=> (UInt32)_proxy[nameof(NdisMedium)];
        public UInt32 NdisPhysicalMedium=> (UInt32)_proxy[nameof(NdisPhysicalMedium)];
        public UInt32 InterfaceOperationalStatus=> (UInt32)_proxy[nameof(InterfaceOperationalStatus)];
        public bool OperationalStatusDownDefaultPortNotAuthenticated=> (bool)_proxy[nameof(OperationalStatusDownDefaultPortNotAuthenticated)];
        public bool OperationalStatusDownMediaDisconnected=> (bool)_proxy[nameof(OperationalStatusDownMediaDisconnected)];
        public bool OperationalStatusDownInterfacePaused=> (bool)_proxy[nameof(OperationalStatusDownInterfacePaused)];
        public bool OperationalStatusDownLowPowerState=> (bool)_proxy[nameof(OperationalStatusDownLowPowerState)];
        public UInt32 InterfaceAdminStatus=> (UInt32)_proxy[nameof(InterfaceAdminStatus)];
        public UInt32 MediaConnectState=> (UInt32)_proxy[nameof(MediaConnectState)];
        public UInt32 MtuSize=> (UInt32)_proxy[nameof(MtuSize)];
        public UInt16 VlanID=> (UInt16)_proxy[nameof(VlanID)];
        public UInt64 TransmitLinkSpeed=> (UInt64)_proxy[nameof(TransmitLinkSpeed)];
        public UInt64 ReceiveLinkSpeed=> (UInt64)_proxy[nameof(ReceiveLinkSpeed)];
        public bool PromiscuousMode=> (bool)_proxy[nameof(PromiscuousMode)];
        public bool DeviceWakeUpEnable=> (bool)_proxy[nameof(DeviceWakeUpEnable)];
        public bool ConnectorPresent=> (bool)_proxy[nameof(ConnectorPresent)];
        public UInt32 MediaDuplexState=> (UInt32)_proxy[nameof(MediaDuplexState)];
        public string? DriverDate => (string?)_proxy[nameof(DriverDate)];
        public UInt64 DriverDateData=> (UInt64)_proxy[nameof(DriverDateData)];
        public string? DriverVersionString => (string?)_proxy[nameof(DriverVersionString)];
        public string? DriverName => (string?)_proxy[nameof(DriverName)];
        public string? DriverDescription => (string?)_proxy[nameof(DriverDescription)];
        public UInt16 MajorDriverVersion=> (UInt16)_proxy[nameof(MajorDriverVersion)];
        public UInt16 MinorDriverVersion=> (UInt16)_proxy[nameof(MinorDriverVersion)];
        public byte DriverMajorNdisVersion=> (byte)_proxy[nameof(DriverMajorNdisVersion)];
        public byte DriverMinorNdisVersion=> (byte)_proxy[nameof(DriverMinorNdisVersion)];
        public string? DriverProvider => (string?)_proxy[nameof(DriverProvider)];
        public string? ComponentID => (string?)_proxy[nameof(ComponentID)];
        public UInt32[]? LowerLayerInterfaceIndices => (UInt32[]?)_proxy[nameof(LowerLayerInterfaceIndices)];
        public UInt32[]? HigherLayerInterfaceIndices => (UInt32[]?)_proxy[nameof(HigherLayerInterfaceIndices)];
        public bool AdminLocked=> (bool)_proxy[nameof(AdminLocked)];

        #endregion
    }
}
