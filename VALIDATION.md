# 审查决策与验证记录

日期：2026-11-03。Windows，SDK 10.0.301，.NET 10.0.9。本文件记录当前结果；迁移首轮结果列为历史，避免混淆。

实现提交：[499a308](https://github.com/huangyue003/WMI/commit/499a308456e6f06d8e6aa516de64a881435bf7b2)。

## 最终决策

| 议题 | 决定 | 实施与验证 |
| --- | --- | --- |
| A1 | 采纳纯 Delete 语义 | 移除隐式 Stop；非管理员回归证明只调用一次 Delete、不读状态、原样返回或抛出，不重试 |
| A2 | 采纳 | 八个包装类的全部 provider 引用属性保守标注可空，含各类数组；[逐项清单](docs/NULLABILITY.md)，用内存 WMI schema 验证实际 null 透传 |
| B1 | 采纳 | 局部映射默认值，不回写 options；null/空白分类，参数错误先于连接；默认值、重复映射、失败不变测试 |
| B2 | 采纳 | 服务类型/错误控制/启动模式使用枚举，数值与字符串分别显式映射；Flags、拼写修正、未知值验证 |
| B3 | 采纳 | CreateDefault(namespace) 统一连接设置，裸选项入口移除；设置与实际查询回归 |
| B4 | 部分采纳 | PnP 双入口合并；DriverDate 保留 string?，拒绝 DMTF 转换建议 |
| B5 | 采纳 | FrozenDictionary + 只读属性；移除无引用 DTO 与 ExecutionState 枚举，数值属性保留 |
| B6 | 采纳显式映射 | 服务 Create/Change、ProcessStartup 仅传协议字段；派生类新增属性、null/空值、枚举类型回归；通用反射扩展保留并写明约定 |
| C1 | 搁置 | 查询 helper 重构非本轮必要改动，保持现有所有权边界 |
| C2 | 采纳 | SetPriority 显式转换 uint，自建进程实测 |
| C3 | 采纳导入清理 | IDE0005 清理并验证无待修改；不为此开启 XML 全量文档或压制 CS1591 |
| C4 | 采纳 | CI 配置 AnyCPU/x64；本机额外验证 x86。远程 CI 状态独立核对，不以 YAML 存在代替执行结果 |
| C5 | 采纳不交付脚本 | 一次性迁移/脱敏辅助脚本有意不保留为交付依赖；源码及文档命令支持独立构建 |
| C6 | 部分采纳 | 工厂参数校验先于连接；公共类名保持，README 给出 Process 别名 |
| C7 | 采纳替代方案 | 允许合法空设备集合，记录数量；系统磁盘必须实际命中，不增加同义反复断言 |
| C8 | 采纳 | UTC/非零偏移 DMTF、DateTime 原值及三种 Kind、缺失哨兵分别验证 |

本机通过 ManagementClass.Properties 核对 MSFT_NetAdapter：仅一个 PnPDeviceID，origin=MSFT_NetAdapter，类型 String；DriverDate 同为 String。保留统一公开名 PNPDeviceID，WMI 名称查询不区分大小写。不输出真实硬件标识。

## 当前验证

- AnyCPU、x86、x64 的 Release `-warnaserror` 均零警告、零错误。
- 三个平台各 **42 通过 / 2 显式跳过 / 0 失败，总计 44**。新增契约回归 25 项全通过。
- x86 适配器日志确认 **32-bit .NET 10.0.9**；x64 确认 **64-bit .NET 10.0.9**。依次执行后默认 AnyCPU `--no-build` 仍通过。
- `dotnet format style IFactory.WMI.slnx --diagnostics IDE0005 --severity info --no-restore --verify-no-changes` 通过。
- Release/AnyCPU，Coverlet `XPlat Code Coverage`，生产程序集 IFactory.WMI：**行 455/702，分支 66/92**。未覆盖的可选属性和系统管理路径仍需特定环境，覆盖率不代表所有功能验证完成。
- 本机只读 provider 数量：逻辑磁盘 3、USB 控制器 6、USB 集线器 8、网卡 4、IPEnabled 网络配置 2。只证明当前查询与关键字段/查回测试成功，不等价于物理故障、远程设备或长期稳定性验证。
- 自建隐藏进程的创建、优先级设置、终止及 finally 清理通过；没有管理现有业务进程。

从该提交的 Git archive 导出无 bin/obj 源码副本，还原、Release 严格构建与测试通过：42 通过、2 显式跳过。当前 NuGet 源未报告直接或传递依赖的已知漏洞。

CI 首次新增矩阵时显式平台名写为 AnyCPU，远程构建失败；现已改为解决方案平台名 `Any CPU` 并加引号，本机按该显式命令构建/测试通过。此配置修复与业务代码无关。`x86`/`x64` 项目平台保持原值。

## 权限与未验证边界

当前令牌不具管理员角色。历史电源计划实测返回 COMException，原生 CIM 查询同样报告会话权限限制；本轮不把这条路径报告为通过。保留显式开关。服务配置变更测试只操作 GUID 唯一服务记录，finally 清理；未在本机启用。非管理员 Delete 单元测试验证派发契约，不代替真实 SCM 状态测试。

在具有对应权限的隔离 Windows 会话分别执行：

```powershell
$env:WMI_TEST_POWER_PROVIDER='1'
dotnet test IFactory.WMI.slnx -c Release --filter FullyQualifiedName~PowerPlanTest
Remove-Item Env:WMI_TEST_POWER_PROVIDER

$env:WMI_TEST_SERVICE_MUTATION='1'
dotnet test IFactory.WMI.slnx -c Release --filter FullyQualifiedName~ServiceLifecycleTests
Remove-Item Env:WMI_TEST_SERVICE_MUTATION
```

服务配置测试使用 cmd.exe 路径作为配置记录，不将它启动为服务。真实服务的启动/停止/暂停/继续、运行中删除与句柄关闭时序，需要专用服务程序，未验证。远程认证、物理故障、长时压测未验证。普通测试的 provider 查询可能受系统配置或权限限制，应保留失败证据，不静默改为通过。

## 历史迁移基线

源码复制后保留原工程，没有外部 ProjectReference；排除原 Git、bin/obj、IDE 状态、未编译的第三方 SharpWMI 示例及机器 manifest。源测试中的硬编码凭据、内部主机与业务进程/服务操作没有导入。原整套测试因此没有直接执行。

此前提交 7564b38 的本机矩阵为 17 通过 / 2 跳过，覆盖率为行 242/665、分支 35/76；这是历史值，不是当前结果。此前完成干净副本还原/构建/测试及 NuGet 传递依赖检查，当前包版本未因本轮审查变更。

## 公开交付

源码、测试、构建和正式文档提交；原始讨论记录、助手状态、一次性脚本、测试结果与覆盖率产物由 .gitignore 排除。GitHub Description 与 Wiki 为生成文稿，不等于已修改仓库设置或发布 Wiki。敏感信息检查范围和结果单列于 [PUBLIC-CONTENT-CHECK.md](docs/PUBLIC-CONTENT-CHECK.md)。
