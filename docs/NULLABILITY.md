# 引用属性可空契约清单

八类包装对象均保守标注 provider 直接返回的引用属性：WMI 类型声明不构成非空保证，权限、设备与 provider 状态可能使其缺失。标注允许 null，不表示每次查询都会为空；数组 null 不转换为空数组。值类型保持既有非可空签名。未支持的属性仍可能由 provider 抛出异常。

## NetAdapter

对应 [WMI schema](https://learn.microsoft.com/en-us/windows/win32/fwp/wmi/netadaptercimprov/msft-netadapter)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| Caption | string? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |
| Name | string? | provider 引用值无统一非空保证，直接保留 null |
| Status | string? | provider 引用值无统一非空保证，直接保留 null |
| CreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| DeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| ErrorDescription | string? | provider 引用值无统一非空保证，直接保留 null |
| PNPDeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| PowerManagementCapabilities | UInt16[]? | provider 引用值无统一非空保证，直接保留 null |
| SystemCreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemName | string? | provider 引用值无统一非空保证，直接保留 null |
| OtherPortType | string? | provider 引用值无统一非空保证，直接保留 null |
| OtherNetworkPortType | string? | provider 引用值无统一非空保证，直接保留 null |
| OtherLinkTechnology | string? | provider 引用值无统一非空保证，直接保留 null |
| PermanentAddress | string? | provider 引用值无统一非空保证，直接保留 null |
| NetworkAddresses | string[]? | provider 引用值无统一非空保证，直接保留 null |
| InterfaceDescription | string? | provider 引用值无统一非空保证，直接保留 null |
| InterfaceName | string? | provider 引用值无统一非空保证，直接保留 null |
| InterfaceGuid | string? | provider 引用值无统一非空保证，直接保留 null |
| DeviceName | string? | provider 引用值无统一非空保证，直接保留 null |
| DriverDate | string? | provider 引用值无统一非空保证，直接保留 null |
| DriverVersionString | string? | provider 引用值无统一非空保证，直接保留 null |
| DriverName | string? | provider 引用值无统一非空保证，直接保留 null |
| DriverDescription | string? | provider 引用值无统一非空保证，直接保留 null |
| DriverProvider | string? | provider 引用值无统一非空保证，直接保留 null |
| ComponentID | string? | provider 引用值无统一非空保证，直接保留 null |
| LowerLayerInterfaceIndices | UInt32[]? | provider 引用值无统一非空保证，直接保留 null |
| HigherLayerInterfaceIndices | UInt32[]? | provider 引用值无统一非空保证，直接保留 null |

## LogicalDisk

对应 [WMI schema](https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-logicaldisk)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| Caption | string? | provider 引用值无统一非空保证，直接保留 null |
| CreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |
| DeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| ErrorDescription | string? | provider 引用值无统一非空保证，直接保留 null |
| ErrorMethodology | string? | provider 引用值无统一非空保证，直接保留 null |
| FileSystem | string? | provider 引用值无统一非空保证，直接保留 null |
| Name | string? | provider 引用值无统一非空保证，直接保留 null |
| PNPDeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| PowerManagementCapabilities | UInt16[]? | provider 引用值无统一非空保证，直接保留 null |
| ProviderName | string? | provider 引用值无统一非空保证，直接保留 null |
| Purpose | string? | provider 引用值无统一非空保证，直接保留 null |
| Status | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemCreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemName | string? | provider 引用值无统一非空保证，直接保留 null |
| VolumeName | string? | provider 引用值无统一非空保证，直接保留 null |
| VolumeSerialNumber | string? | provider 引用值无统一非空保证，直接保留 null |

## NetworkAdapterConfiguration

对应 [WMI schema](https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-networkadapterconfiguration)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| Caption | string? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |
| SettingID | string? | provider 引用值无统一非空保证，直接保留 null |
| DatabasePath | string? | provider 引用值无统一非空保证，直接保留 null |
| DefaultIPGateway | string[]? | provider 引用值无统一非空保证，直接保留 null |
| DHCPServer | string? | provider 引用值无统一非空保证，直接保留 null |
| DNSDomain | string? | provider 引用值无统一非空保证，直接保留 null |
| DNSDomainSuffixSearchOrder | string[]? | provider 引用值无统一非空保证，直接保留 null |
| DNSHostName | string? | provider 引用值无统一非空保证，直接保留 null |
| DNSServerSearchOrder | string[]? | provider 引用值无统一非空保证，直接保留 null |
| GatewayCostMetric | UInt16[]? | provider 引用值无统一非空保证，直接保留 null |
| IPAddress | string[]? | provider 引用值无统一非空保证，直接保留 null |
| IPSecPermitIPProtocols | string[]? | provider 引用值无统一非空保证，直接保留 null |
| IPSecPermitTCPPorts | string[]? | provider 引用值无统一非空保证，直接保留 null |
| IPSecPermitUDPPorts | string[]? | provider 引用值无统一非空保证，直接保留 null |
| IPSubnet | string[]? | provider 引用值无统一非空保证，直接保留 null |
| IPXAddress | string? | provider 引用值无统一非空保证，直接保留 null |
| IPXFrameType | UInt32[]? | provider 引用值无统一非空保证，直接保留 null |
| IPXNetworkNumber | string[]? | provider 引用值无统一非空保证，直接保留 null |
| IPXVirtualNetNumber | string? | provider 引用值无统一非空保证，直接保留 null |
| MACAddress | string? | provider 引用值无统一非空保证，直接保留 null |
| ServiceName | string? | provider 引用值无统一非空保证，直接保留 null |
| WINSHostLookupFile | string? | provider 引用值无统一非空保证，直接保留 null |
| WINSPrimaryServer | string? | provider 引用值无统一非空保证，直接保留 null |
| WINSScopeID | string? | provider 引用值无统一非空保证，直接保留 null |
| WINSSecondaryServer | string? | provider 引用值无统一非空保证，直接保留 null |

## PowerPlan

对应 [WMI schema](https://learn.microsoft.com/en-us/previous-versions/windows/desktop/powerwmiprov/win32-powerplan)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| InstanceID | string? | provider 引用值无统一非空保证，直接保留 null |
| ElementName | string? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |

## Process

对应 [WMI schema](https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-process)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| CreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| Caption | string? | provider 引用值无统一非空保证，直接保留 null |
| CommandLine | string? | provider 引用值无统一非空保证，直接保留 null |
| CSCreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| CSName | string? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |
| ExecutablePath | string? | provider 引用值无统一非空保证，直接保留 null |
| Handle | string? | provider 引用值无统一非空保证，直接保留 null |
| Name | string? | provider 引用值无统一非空保证，直接保留 null |
| OSCreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| OSName | string? | provider 引用值无统一非空保证，直接保留 null |
| Status | string? | provider 引用值无统一非空保证，直接保留 null |
| WindowsVersion | string? | provider 引用值无统一非空保证，直接保留 null |

## Service

对应 [WMI schema](https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-service)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| Caption | string? | provider 引用值无统一非空保证，直接保留 null |
| CreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |
| DisplayName | string? | provider 引用值无统一非空保证，直接保留 null |
| ErrorControl | string? | provider 引用值无统一非空保证，直接保留 null |
| Name | string? | provider 引用值无统一非空保证，直接保留 null |
| PathName | string? | provider 引用值无统一非空保证，直接保留 null |
| ServiceType | string? | provider 引用值无统一非空保证，直接保留 null |
| StartMode | string? | provider 引用值无统一非空保证，直接保留 null |
| StartName | string? | provider 引用值无统一非空保证，直接保留 null |
| State | string? | provider 引用值无统一非空保证，直接保留 null |
| Status | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemCreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemName | string? | provider 引用值无统一非空保证，直接保留 null |

## USBController

对应 [WMI schema](https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-usbcontroller)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| Caption | string? | provider 引用值无统一非空保证，直接保留 null |
| CreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |
| DeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| ErrorDescription | string? | provider 引用值无统一非空保证，直接保留 null |
| Manufacturer | string? | provider 引用值无统一非空保证，直接保留 null |
| Name | string? | provider 引用值无统一非空保证，直接保留 null |
| PNPDeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| PowerManagementCapabilities | UInt16[]? | provider 引用值无统一非空保证，直接保留 null |
| Status | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemCreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemName | string? | provider 引用值无统一非空保证，直接保留 null |

## USBHub

对应 [WMI schema](https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-usbhub)；注解采用保守非空保证策略，不从一次实测推断所有环境。

| 属性 | 签名 | 依据 |
| --- | --- | --- |
| Caption | string? | provider 引用值无统一非空保证，直接保留 null |
| CreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| CurrentAlternateSettings | byte[]? | provider 引用值无统一非空保证，直接保留 null |
| Description | string? | provider 引用值无统一非空保证，直接保留 null |
| ErrorDescription | string? | provider 引用值无统一非空保证，直接保留 null |
| PNPDeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| PowerManagementCapabilities | UInt16[]? | provider 引用值无统一非空保证，直接保留 null |
| Status | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemCreationClassName | string? | provider 引用值无统一非空保证，直接保留 null |
| SystemName | string? | provider 引用值无统一非空保证，直接保留 null |
| DeviceID | string? | provider 引用值无统一非空保证，直接保留 null |
| Name | string? | provider 引用值无统一非空保证，直接保留 null |
