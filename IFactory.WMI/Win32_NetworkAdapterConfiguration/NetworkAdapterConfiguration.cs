using IFactory.WMI.MSFT_NetAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_NetworkAdapterConfiguration
{
    public class NetworkAdapterConfiguration
    {
        public static NetworkAdapterConfiguration? Get(string name)
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            return Get(name, scope);
        }
        public static NetworkAdapterConfiguration? Get(string name, ManagementScope scope)
        {
            ManagementObject? instance = null;
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Name='{name}'");
            var collection = new ManagementObjectSearcher(scope, query).Get();
            foreach (ManagementObject item in collection)
            {
                instance = item;
                break;
            }
            return instance == null ? null : new NetworkAdapterConfiguration(instance);
        }

        public static IEnumerable<NetworkAdapterConfiguration> Get()
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            return Get(scope);
        }
        public static IEnumerable<NetworkAdapterConfiguration> Get(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE");
            var collection = new ManagementObjectSearcher(scope, query).Get();
            foreach (ManagementObject item in collection)
                yield return new NetworkAdapterConfiguration(item);
        }

        private NetworkAdapterConfiguration(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private ManagementObject _proxy;

        #region Properties

        public string Caption => (string)_proxy[nameof(Caption)];
        public string Description => (string)_proxy[nameof(Description)];
        public string SettingID => (string)_proxy[nameof(SettingID)];
        public bool ArpAlwaysSourceRoute => (bool)_proxy[nameof(ArpAlwaysSourceRoute)];
        public bool ArpUseEtherSNAP => (bool)_proxy[nameof(ArpUseEtherSNAP)];
        public string DatabasePath => (string)_proxy[nameof(DatabasePath)];
        public bool DeadGWDetectEnabled => (bool)_proxy[nameof(DeadGWDetectEnabled)];
        public string[] DefaultIPGateway => (string[])_proxy[nameof(DefaultIPGateway)];
        public byte DefaultTOS => (byte)_proxy[nameof(DefaultTOS)];
        public byte DefaultTTL => (byte)_proxy[nameof(DefaultTTL)];
        public bool DHCPEnabled => (bool)_proxy[nameof(DHCPEnabled)];
        public DateTime DHCPLeaseExpires => (DateTime)_proxy[nameof(DHCPLeaseExpires)];
        public DateTime DHCPLeaseObtained => (DateTime)_proxy[nameof(DHCPLeaseObtained)];
        public string DHCPServer => (string)_proxy[nameof(DHCPServer)];
        public string DNSDomain => (string)_proxy[nameof(DNSDomain)];
        public string[] DNSDomainSuffixSearchOrder => (string[])_proxy[nameof(DNSDomainSuffixSearchOrder)];
        public bool DNSEnabledForWINSResolution => (bool)_proxy[nameof(DNSEnabledForWINSResolution)];
        public string DNSHostName => (string)_proxy[nameof(DNSHostName)];
        public string[] DNSServerSearchOrder => (string[])_proxy[nameof(DNSServerSearchOrder)];
        public bool DomainDNSRegistrationEnabled => (bool)_proxy[nameof(DomainDNSRegistrationEnabled)];
        public UInt32 ForwardBufferMemory => (UInt32)_proxy[nameof(ForwardBufferMemory)];
        public bool FullDNSRegistrationEnabled => (bool)_proxy[nameof(FullDNSRegistrationEnabled)];
        public UInt16[] GatewayCostMetric => (UInt16[])_proxy[nameof(GatewayCostMetric)];
        public byte IGMPLevel => (byte)_proxy[nameof(IGMPLevel)];
        public UInt32 Index => (UInt32)_proxy[nameof(Index)];
        public UInt32 InterfaceIndex => (UInt32)_proxy[nameof(InterfaceIndex)];
        public string[] IPAddress => (string[])_proxy[nameof(IPAddress)];
        public UInt32 IPConnectionMetric => (UInt32)_proxy[nameof(IPConnectionMetric)];
        public bool IPEnabled => (bool)_proxy[nameof(IPEnabled)];
        public bool IPFilterSecurityEnabled => (bool)_proxy[nameof(IPFilterSecurityEnabled)];
        public bool IPPortSecurityEnabled => (bool)_proxy[nameof(IPPortSecurityEnabled)];
        public string[] IPSecPermitIPProtocols => (string[])_proxy[nameof(IPSecPermitIPProtocols)];
        public string[] IPSecPermitTCPPorts => (string[])_proxy[nameof(IPSecPermitTCPPorts)];
        public string[] IPSecPermitUDPPorts => (string[])_proxy[nameof(IPSecPermitUDPPorts)];
        public string[] IPSubnet => (string[])_proxy[nameof(IPSubnet)];
        public bool IPUseZeroBroadcast => (bool)_proxy[nameof(IPUseZeroBroadcast)];
        public string IPXAddress => (string)_proxy[nameof(IPXAddress)];
        public bool IPXEnabled => (bool)_proxy[nameof(IPXEnabled)];
        public UInt32[] IPXFrameType => (UInt32[])_proxy[nameof(IPXFrameType)];
        public UInt32 IPXMediaType => (UInt32)_proxy[nameof(IPXMediaType)];
        public string[] IPXNetworkNumber => (string[])_proxy[nameof(IPXNetworkNumber)];
        public string IPXVirtualNetNumber => (string)_proxy[nameof(IPXVirtualNetNumber)];
        public UInt32 KeepAliveInterval => (UInt32)_proxy[nameof(KeepAliveInterval)];
        public UInt32 KeepAliveTime => (UInt32)_proxy[nameof(KeepAliveTime)];
        public string MACAddress => (string)_proxy[nameof(MACAddress)];
        public UInt32 MTU => (UInt32)_proxy[nameof(MTU)];
        public UInt32 NumForwardPackets => (UInt32)_proxy[nameof(NumForwardPackets)];
        public bool PMTUBHDetectEnabled => (bool)_proxy[nameof(PMTUBHDetectEnabled)];
        public bool PMTUDiscoveryEnabled => (bool)_proxy[nameof(PMTUDiscoveryEnabled)];
        public string ServiceName => (string)_proxy[nameof(ServiceName)];
        public UInt32 TcpipNetbiosOptions => (UInt32)_proxy[nameof(TcpipNetbiosOptions)];
        public UInt32 TcpMaxConnectRetransmissions => (UInt32)_proxy[nameof(TcpMaxConnectRetransmissions)];
        public UInt32 TcpMaxDataRetransmissions => (UInt32)_proxy[nameof(TcpMaxDataRetransmissions)];
        public UInt32 TcpNumConnections => (UInt32)_proxy[nameof(TcpNumConnections)];
        public bool TcpUseRFC1122UrgentPointer => (bool)_proxy[nameof(TcpUseRFC1122UrgentPointer)];
        public UInt16 TcpWindowSize => (UInt16)_proxy[nameof(TcpWindowSize)];
        public bool WINSEnableLMHostsLookup => (bool)_proxy[nameof(WINSEnableLMHostsLookup)];
        public string WINSHostLookupFile => (string)_proxy[nameof(WINSHostLookupFile)];
        public string WINSPrimaryServer => (string)_proxy[nameof(WINSPrimaryServer)];
        public string WINSScopeID => (string)_proxy[nameof(WINSScopeID)];
        public string WINSSecondaryServer => (string)_proxy[nameof(WINSSecondaryServer)];

        #endregion
    }
}
