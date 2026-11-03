# IFactory.WMI

Windows WMI 的 .NET 10 封装，提供服务、进程、逻辑磁盘、USB 控制器/集线器、网卡、网络配置及电源计划查询。进程和服务另提供管理方法。仅支持 Windows，权限由 Windows 和对应 WMI provider 决定。

## 构建与验证

安装 Windows .NET SDK 10.0.301 或同 feature band 的更新补丁；`global.json` 使用 `latestPatch`。

```powershell
dotnet restore IFactory.WMI.slnx
dotnet build IFactory.WMI.slnx -c Release -warnaserror
dotnet test IFactory.WMI.slnx -c Release --no-build
dotnet build IFactory.WMI.slnx -c Release -p:Platform=x86 -warnaserror
dotnet test IFactory.WMI.slnx -c Release -p:Platform=x86 --no-build -- RunConfiguration.TargetPlatform=x86
dotnet build IFactory.WMI.slnx -c Release -p:Platform=x64 -warnaserror
dotnet test IFactory.WMI.slnx -c Release -p:Platform=x64 --no-build -- RunConfiguration.TargetPlatform=x64
dotnet test IFactory.WMI.slnx -c Release --no-build
```

x86 执行需要 x86 .NET 10 runtime。输出及中间文件按平台隔离。统一目标为 `net10.0-windows`，包版本由 `Directory.Build.targets` 管理。CI 配置验证 AnyCPU、x64；x86 为本机验证项，不声称 CI 已覆盖。测试权限开关和实际结果见 [VALIDATION.md](VALIDATION.md)。

## 使用

在 Windows .NET 10 应用中引用 `IFactory.WMI/IFactory.WMI.csproj`：

```csharp
using IFactory.WMI;
using IFactory.WMI.Win32_LogicalDisk;
using IFactory.WMI.Win32_Service;
using WmiProcess = IFactory.WMI.Win32_Process.Process;

var scope = ManagementScopeExtensions.CreateDefault();
foreach (var disk in LogicalDisk.Get(scope))
{
    using (disk)
        Console.WriteLine($"{disk.DeviceID}: {disk.FreeSpace}/{disk.Size}");
}
using var service = Service.Get("Winmgmt");
Console.WriteLine(service?.State);
using var process = WmiProcess.Get("example.exe");
Console.WriteLine(process?.ProcessId);
```

## 行为契约

- `ManagementScopeExtensions` 是立即连接的工厂。`CreateDefault()` 使用 localhost、`root/cimv2`；`CreateDefault(wmiNamespace)` 可选择命名空间。两者统一采用 Impersonate、PacketPrivacy、EnablePrivileges，这些选项不提升用户权限。远程连接通过 `Create(host, wmiNamespace, options)` 显式提供设置，库不保存凭据。
- `Process.Get(name)` 按可执行文件名查首个实例；重名时核对 `ProcessId` 后再管理。`NetAdapter.Get()`/`Get(name)` 使用 `root/StandardCimv2`；网络配置 `Get(string)` 按 `SettingID` 查询，枚举仅返回 IPEnabled 的配置。电源计划使用 `root/cimv2/power`，可能需要额外权限。
- 返回的每个包装对象实现 `IDisposable`，由调用者释放；枚举器关闭查询集合，不代替释放已返回对象。释放后不再访问属性或方法。
- 所有直接映射 provider 的引用属性均保守标为可空，缺失值保留 null，null 数组不转为空数组。逐项清单见 [NULLABILITY.md](docs/NULLABILITY.md)。值类型属性仍为非可空，provider 返回 null 或不支持该字段时可能抛出异常。
- CIM datetime 的 DMTF 字符串转为本地时间、`Kind=Local`；输入本来是 DateTime 时保留值和 Kind。缺失日期返回 `DateTime.MinValue`、`Kind=Unspecified`，不要把哨兵当真实时间进行换算。`NetAdapter.DriverDate` 是普通日期文本，保留 `string?`，不作 DMTF 转换。
- 管理方法原样返回 WMI `uint` 返回码，不重试或重放；COM、权限、provider 异常直接传播。`DeleteService()` **只提交 Delete，不隐式停止服务**；0 表示请求被受理，运行中服务可被标记删除，真正移除还需停止且所有相关句柄关闭。调用方负责停止、重新查询及有期限的完成等待。[微软 Delete 契约](https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/delete-method-in-class-win32-service)
- 服务创建默认值为 `ServiceType.OwnProcess`、`StartMode.Automatic`、`ErrorControl.Normal`，只填入局部 WMI 参数，不回写 options。CreateOptions/ChangeOptions 的这三个选项使用可空枚举；StartMode 映射为名称，其余映射为数值。非法枚举/未知位在调用前拒绝，服务类型组合和 Boot/System 等模式的适用性仍由 provider 校验。
- 业务参数采用显式映射，不会因派生选项新增 Remark 等属性而改变传参。可选项 null 表示不传，空字符串/空数组则显式传递；是否支持清空由 provider 决定。公共 `SetPropertyValue(object)` 仍是通用反射扩展：非 null、公开可读实例属性按名称写入，跳过索引器，枚举转底层数值；未知字段抛异常，不能把任意 POCO 当业务选项。

## 本次 API 整理

本次直接调整 API，无 obsolete 过渡：`OwnProces` 改为 `OwnProcess`；选项中的 uint/string 改为枚举；删除无引用 DTO `Win32_ServiceInfo` 和枚举 `ExecutionState`（进程属性仍返回 UInt16）；网卡 PnP 标识只保留 `PNPDeviceID`；错误表 `ErrorCode` 改为不可修改的只读属性。引用属性现带可空注解。原隐式 Stop 和 options 回写行为已移除。

## 文档

- [审查决策与验证](VALIDATION.md)
- [公开内容检查](docs/PUBLIC-CONTENT-CHECK.md)
- [GitHub Description 文稿](docs/GITHUB_DESCRIPTION.txt)
- [GitHub Wiki 首页](https://github.com/huangyue003/WMI/wiki)（[仓库内副本](docs/wiki/Home.md)）
