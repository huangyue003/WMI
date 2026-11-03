# IFactory.WMI

Windows WMI 的 .NET 10 封装，提供服务、进程、逻辑磁盘、USB 控制器/集线器、网卡、网络配置及电源计划查询。进程和服务另提供管理方法。仅支持 Windows，调用权限取决于 Windows 与对应 WMI provider。

## 构建与验证

安装 Windows .NET SDK 10.0.301 或同 feature band 的更新补丁。`global.json` 使用 `latestPatch`。在仓库根目录执行：

```powershell
dotnet restore IFactory.WMI.slnx
dotnet build IFactory.WMI.slnx -c Release -warnaserror
dotnet test IFactory.WMI.slnx -c Release --no-build
dotnet test IFactory.WMI.slnx -c Release -p:Platform=x86 -- RunConfiguration.TargetPlatform=x86
dotnet test IFactory.WMI.slnx -c Release -p:Platform=x64 -- RunConfiguration.TargetPlatform=x64
```

x86 执行需要安装 x86 .NET 10 runtime。工程以 `net10.0-windows` 为目标，AnyCPU/x86/x64 的编译输出及中间文件彼此隔离。包版本统一在 `Directory.Build.targets` 管理。

## 使用

在 Windows .NET 10 应用中引用 `IFactory.WMI/IFactory.WMI.csproj`：

```csharp
using IFactory.WMI;
using IFactory.WMI.Win32_LogicalDisk;
using IFactory.WMI.Win32_Service;

var scope = ManagementScopeExtensions.CreateDefault();
foreach (var disk in LogicalDisk.Get(scope))
{
    using (disk)
        Console.WriteLine($"{disk.DeviceID}: {disk.FreeSpace}/{disk.Size}");
}
using var service = Service.Get("Winmgmt");
Console.WriteLine(service?.State);
```

- `ManagementScopeExtensions.CreateDefault()` 立即连接 localhost 的 `root/cimv2`，使用 Impersonate、PacketPrivacy 和 EnablePrivileges；这些选项不提升当前用户权限。远程连接通过 `Create(host, wmiNamespace, options)` 显式提供，不保存凭据。
- `Process.Get(name)` 按可执行文件名查询；重名时返回首个实例。执行有副作用的方法前应核对 `ProcessId`，不能假设名称唯一。
- `NetAdapter.Get()`/`Get(name)` 使用 `root/StandardCimv2`。`NetworkAdapterConfiguration.Get(string)` 按 `SettingID` 查询，枚举版本仅返回 IPEnabled 的配置。
- `PowerPlan.Get()` 使用 `root/cimv2/power`；部分会话需要额外权限。
- 查询返回的每个包装对象实现 `IDisposable`，由调用者释放；枚举器释放会关闭查询集合，但不代替释放已返回对象。不要在释放后访问属性或方法。
- WMI 的 DMTF 日期转换为本地 `DateTime`；未提供日期时返回 `DateTime.MinValue`。其他属性保持 provider 的原始类型和可空行为：某些可选值可能为 null，直接读取非空值类型属性可能抛出异常，应只读取 provider 实际支持的字段。
- 服务/进程管理方法保留 WMI 的 `uint` 返回码（0 为成功），不自动重试或重放；COM、权限及 provider 异常直接传播。服务停止请求成功并不代表已经停止；删除可能等待现有句柄释放。需要完成状态的调用方应重新查询并设置自己的等待期限。
- 服务创建保留原默认值：OwnProcess、Automatic、Normal；原 API 会将默认值填入传入的 `CreateOptions`。仅非 null 的选项写入 WMI，因此 null 表示不传参，不能用 null 清空属性。

本库不提升权限、不自动连接业务主机，也没有后台重连或队列。更多审查与实测边界见 [VALIDATION.md](VALIDATION.md)。
