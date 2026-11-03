# 审查与验证记录

验证日期：2026-11-03。平台 Windows，SDK 10.0.301，运行时 10.0.9。

## 迁移边界与基线

保留源工程，复制主库、必要测试和项目结构；没有 ProjectReference 外部依赖。排除 bin/obj、IDE 状态、机器 manifest 及未参与编译的第三方 SharpWMI 示例目录。未复制原测试中的硬编码凭据、内部主机和机器专属程序路径；原整套测试会管理既有业务服务/进程，因此未执行原始整套测试。初始提交保留生产源码与只读测试基线；该基线仍是原 net9.0 配置，后续提交完成独立构建。

参考构建配置采用统一 props/targets、依赖版本和忽略规则，未继承公司属性、RID 或外部构建文件。SDK 固定补丁策略，目标明确为 Windows .NET 10。依赖通过 NuGet flat-container 实际查询稳定版本：System.Management 10.0.14、Microsoft.NET.Test.Sdk 18.11.0、xunit 2.9.3、xunit.runner.visualstudio 4.0.1、coverlet.collector 10.1.0。删除过时 dotnet-xunit 工具引用及无使用处的注解依赖。

## 审查修复

- 所有名称查询转义 WQL 单引号和反斜杠，回归验证恶意查询片段只按字面值匹配。
- 查询 searcher/collection、方法参数/结果、ManagementClass 及时释放；返回包装对象提供 IDisposable，由调用方释放代理。迭代器使用 using 确保正常完成和提前结束时关闭查询资源。
- WMI 日期实际为 DMTF 字符串，统一转换；缺失日期以 DateTime.MinValue 表示，维持原非可空 API 签名。
- NetAdapter.Get(name) 默认改用 root/StandardCimv2；网络配置按实际 SettingID 查询，修复不存在的 Name 属性查询。
- NetAdapter.LastErrorCode 修正 UInt16/UInt32 拆箱错误；磁盘 DriveType 使用数值转换后转枚举，真实属性读取回归通过。
- Process.Create 创建 Win32_ProcessStartup 实例后传入，避免把类定义当实例；选项映射将枚举转换为底层数值，并跳过反射索引器。
- 解决方案显式映射 x86/x64，避免被默认映射为 AnyCPU；编译输出与中间产物按平台隔离。
- 复审修改及相邻查询/调用生命周期，没有新增可确认、需在本轮修复的问题。此结论不代表已覆盖所有可选 provider 属性或所有系统配置。

## 实际结果

- Release `-warnaserror`：0 警告、0 错误。
- AnyCPU：17 通过、2 显式跳过、0 失败，共 19；x86：相同；x64：相同。适配器日志确认 x86 为 32-bit .NET 10.0.9，x64 为 64-bit .NET 10.0.9。
- 依次执行 x86、x64 后，默认 AnyCPU `--no-build` 仍通过。
- 测试覆盖本机磁盘、USB、网卡与 IP 配置枚举及标识符；进程/服务读取；DMTF 日期、查询转义；自建隐藏进程的创建、优先级设置、终止与 finally 清理。可选硬件允许不存在，因此空枚举不代表已实测到对应物理设备。
- Coverlet `XPlat Code Coverage`，Release/AnyCPU，生产程序集 IFactory.WMI：行命中 242/665，分支命中 35/76。未启用的系统管理路径及大量可选属性没有覆盖；覆盖率不等同功能正确性。

## 环境限制与显式测试

初轮电源计划测试实际执行失败（COMException）；系统 Get-CimInstance 同样返回终端会话权限限制。当前令牌不是管理员。保留测试为显式启用项，不将此路径报告为通过。服务创建/修改/删除测试同样要求管理员权限，并只使用 GUID 唯一服务名，finally 删除；本机未执行该项。

在具有对应权限的隔离 Windows 会话中分别执行：

```powershell
$env:WMI_TEST_POWER_PROVIDER='1'
dotnet test IFactory.WMI.slnx -c Release --filter FullyQualifiedName~PowerPlanTest
Remove-Item Env:WMI_TEST_POWER_PROVIDER

$env:WMI_TEST_SERVICE_MUTATION='1'
dotnet test IFactory.WMI.slnx -c Release --filter FullyQualifiedName~ServiceLifecycleTests
Remove-Item Env:WMI_TEST_SERVICE_MUTATION
```

服务测试创建的 cmd.exe 仅用于服务配置记录，不启动它作为真实服务。启动/停止/暂停/继续需要专用可安装的服务程序，本轮未验证；没有操作现有业务服务。远程认证、远程主机、物理设备故障、长时稳定性未验证。可选属性可能为 null 或不受支持，见 README 的行为约束。

## 公开内容检查

扫描待跟踪文件及基线：未纳入源测试中的凭据、业务端点或个人绝对路径；生成结果、覆盖率、临时脚本不提交。源仓库保持原样，其历史不在本次清理范围内。

最终配置的无 bin/obj 源码副本还原、Release 严格构建与测试通过（17 通过、2 显式跳过）。传递依赖已检查，NuGet 当前源未报告已知易受攻击包。
