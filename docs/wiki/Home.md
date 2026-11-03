# IFactory.WMI

IFactory.WMI 是面向 .NET 10 的 Windows WMI 封装，适合需要在 C# 中查询系统信息、管理进程与服务的 Windows 应用。

## 功能与入口

| 功能 | 类型 | 默认 WMI 命名空间 |
| --- | --- | --- |
| 服务查询、创建、修改、启动、停止、暂停、继续、删除请求 | Win32_Service.Service | root/cimv2 |
| 进程查询、创建、优先级设置、终止 | Win32_Process.Process | root/cimv2 |
| 逻辑磁盘 | Win32_LogicalDisk.LogicalDisk | root/cimv2 |
| USB 控制器、集线器 | Win32_USBController.USBController / Win32_USBHub.USBHub | root/cimv2 |
| 网卡 | MSFT_NetAdapter.NetAdapter | root/StandardCimv2 |
| IP 启用的网络配置 | Win32_NetworkAdapterConfiguration.NetworkAdapterConfiguration | root/cimv2 |
| 电源计划 | Win32_PowerPlan.PowerPlan | root/cimv2/power |

## 快速开始

安装 Windows .NET SDK 10.0.301 或同 feature band 更新补丁，将主工程作为 ProjectReference 引入 `net10.0-windows` 应用。

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

## 服务与进程管理约定

返回 0 表示对应请求成功/被受理，不保证异步状态变化已经完成。删除服务只提交 Delete，不自动 Stop；运行中服务可标记删除，真正移除需停止并关闭相关句柄。应用自行设置等待期限并重新查询。库不提升权限，不自动重试或重放管理操作。

创建服务时使用强类型选项，例如 `ServiceType.OwnProcess`、`StartMode.Manual`、`ErrorControl.Normal`。默认分别为 OwnProcess、Automatic、Normal，不修改调用方对象。业务方法只映射明确支持的参数；可选 null 表示省略。

进程名可能重复，执行终止或修改优先级前核对 ProcessId。为避免与 System.Diagnostics.Process 重名，可用 `using WmiProcess = IFactory.WMI.Win32_Process.Process;`。

## 资源与属性

每个返回包装对象都需要 Dispose。引用属性允许 null，值类型属性可能因 provider 不提供值而抛异常。CIM datetime 转为本地 DateTime；缺失日期是 DateTime.MinValue。DriverDate 是字符串，保留原文。远程访问使用显式 scope 与 ConnectionOptions；不要在源码或文档中填写真实凭据。

## 构建与验证

```powershell
dotnet build IFactory.WMI.slnx -c Release -warnaserror
dotnet test IFactory.WMI.slnx -c Release --no-build
```

主工程支持 AnyCPU/x86/x64；x86 测试另需 x86 .NET 10 runtime。CI 配置覆盖 AnyCPU、x64；本机三平台命令、权限测试开关及实测边界见验证记录。普通测试只管理自建进程，不操作现有业务服务。电源 provider 和唯一测试服务配置变更需显式启用及相应权限，不能把跳过报告为通过。

## 进一步阅读

- [源码与 README](https://github.com/huangyue003/WMI)
- [验证记录](https://github.com/huangyue003/WMI/blob/main/VALIDATION.md)
- [属性可空清单](https://github.com/huangyue003/WMI/blob/main/docs/NULLABILITY.md)
- [公开内容检查](https://github.com/huangyue003/WMI/blob/main/docs/PUBLIC-CONTENT-CHECK.md)

此文件为已发布的 [GitHub Wiki 首页](https://github.com/huangyue003/WMI/wiki) 的仓库内正文副本。
