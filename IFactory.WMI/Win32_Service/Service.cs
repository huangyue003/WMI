using System.Collections.Frozen;
using System.Management;

namespace IFactory.WMI.Win32_Service
{
    /// <summary>
    /// Windows Service 操作类
    /// </summary>
    public class Service : IDisposable
    {
        public static Service? Get(string name)
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            return Get(name, scope);
        }
        public static Service? Get(string name, ManagementScope scope)
        {
            ManagementObject? instance = null;
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_Service WHERE Name='{WmiValue.Escape(name)}'");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
            {
                instance = item;
                break;
            }
            return instance == null ? null : new Service(instance);
        }

        internal Service(ManagementObject proxy, Func<string, uint>? invoke = null)
        {
            _proxy = proxy;
            _invoke = invoke ?? (method => (uint)_proxy.InvokeMethod(method, []));
        }
        private readonly ManagementObject _proxy;
        private readonly Func<string, uint> _invoke;
        public void Dispose() => _proxy.Dispose();

        #region Properties
        /// <summary>
        /// 指示是否可以暂停服务
        /// </summary>
        public bool AcceptPause => (bool)_proxy[nameof(AcceptPause)];
        /// <summary>
        /// 指示是否可以停止服务
        /// </summary>
        public bool AcceptStop => (bool)_proxy[nameof(AcceptStop)];
        /// <summary>
        /// 服务的简短说明 - 单行字符串
        /// </summary>
        public string? Caption => (string?)_proxy[nameof(Caption)];
        /// <summary>
        /// 服务定期递增以在长时间启动、停止、暂停或继续操作期间报告其进度的值。 例如，服务在启动时完成其初始化的每个步骤时递增此值。 在服务上调用 操作的用户界面程序在长时间操作期间使用此值跟踪服务的进度。 此值无效，当服务没有挂起的启动、停止、暂停或继续操作时，此值应为零。
        /// </summary>
        public uint CheckPoint => (uint)_proxy[nameof(CheckPoint)];
        /// <summary>
        /// 在创建实例时使用的继承链中显示的第一个具体类的名称。 与 类的其他键属性一起使用时，此属性允许对此类的所有实例及其子类进行唯一标识。
        /// </summary>
        public string? CreationClassName => (string?)_proxy[nameof(CreationClassName)];
        /// <summary>
        /// 如果 为 True，则服务在其他自动启动服务启动后启动，并出现短暂延迟。
        /// </summary>
        public bool DelayedAutoStart => (bool)_proxy[nameof(DelayedAutoStart)];
        /// <summary>
        /// 对象的说明。
        /// </summary>
        public string? Description => (string?)_proxy[nameof(Description)];
        /// <summary>
        /// 指示服务是否可以在桌面上创建窗口或与 Windows 通信，从而以某种方式与用户交互。 交互式服务必须在本地系统帐户下运行。 大多数服务不是交互式的;也就是说，它们不会以任何方式与用户通信。
        /// </summary>
        public bool DesktopInteract => (bool)_proxy[nameof(DesktopInteract)];
        /// <summary>
        /// 在“服务”管理单元中查看的服务的名称。 此字符串的最大长度为 256 个字符。 请注意，存储在注册表) 中的显示名称和服务名称 (并不总是相同的。 例如，DHCP 客户端服务的服务名称为 Dhcp，但显示名称为 DHCP 客户端。 名称在服务控制管理器中保留大小写。 但是， DisplayName 比较始终不区分大小写。
        /// </summary>
        public string? DisplayName => (string?)_proxy[nameof(DisplayName)];
        /// <summary>
        /// 如果此服务在启动期间无法启动，则错误的严重性。 值指示启动程序在发生故障时采取的操作。 所有的错误都由计算机系统记录。
        /// </summary>
        public string? ErrorControl => (string?)_proxy[nameof(ErrorControl)];
        /// <summary>
        /// 定义在启动或停止服务时遇到的错误的 Windows 错误代码。
        /// </summary>
        public uint ExitCode => (uint)_proxy[nameof(ExitCode)];
        /// <summary>
        /// 安装日期对象。 此属性不需要值来指示对象已安装。
        /// </summary>
        public DateTime InstallDate => WmiValue.Date(_proxy[nameof(InstallDate)]);
        /// <summary>
        /// 服务的唯一标识符，用于指示所管理的功能。 对象的 Description 属性中介绍了此功能。
        /// </summary>
        public string? Name => (string?)_proxy[nameof(Name)];
        /// <summary>
        /// 实现服务的服务二进制文件的完全限定路径
        /// </summary>
        public string? PathName => (string?)_proxy[nameof(PathName)];
        /// <summary>
        /// 服务的进程标识符。
        /// </summary>
        public uint ProcessId => (uint)_proxy[nameof(ProcessId)];
        /// <summary>
        /// 服务启动或停止时发生的错误的特定于服务的错误代码。 退出代码由此类表示的服务定义
        /// </summary>
        public uint ServiceSpecificExitCode => (uint)_proxy[nameof(ServiceSpecificExitCode)];
        /// <summary>
        /// 用于调用进程的服务的类型
        /// </summary>
        public string? ServiceType => (string?)_proxy[nameof(ServiceType)];
        /// <summary>
        /// 指示服务是否已启动。
        /// </summary>
        public bool Started => (bool)_proxy[nameof(Started)];
        /// <summary>
        /// Windows 基本服务的启动模式。
        /// </summary>
        public string? StartMode => (string?)_proxy[nameof(StartMode)];
        /// <summary>
        /// 运行服务的帐户名称。 根据服务类型，帐户名称可能采用“DomainName\Username”或 UPN 格式
        /// </summary>
        public string? StartName => (string?)_proxy[nameof(StartName)];
        /// <summary>
        /// 基本服务的当前状态。
        /// </summary>
        public string? State => (string?)_proxy[nameof(State)];
        /// <summary>
        /// 对象的当前状态。 可以定义各种操作和非操作状态。
        /// </summary>
        public string? Status => (string?)_proxy[nameof(Status)];
        /// <summary>
        /// 键入承载此服务的系统的名称。
        /// </summary>
        public string? SystemCreationClassName => (string?)_proxy[nameof(SystemCreationClassName)];
        /// <summary>
        /// 承载此服务的系统的名称。
        /// </summary>
        public string? SystemName => (string?)_proxy[nameof(SystemName)];
        /// <summary>
        /// 仅针对具有启动模式或系统启动模式的内核驱动程序和文件系统驱动程序启动类型服务评估标记
        /// </summary>
        public uint TagId => (uint)_proxy[nameof(TagId)];
        /// <summary>
        /// 挂起的开始、停止、暂停或继续操作所需的估计时间（以毫秒为单位）
        /// </summary>
        public uint WaitHint => (uint)_proxy[nameof(WaitHint)];

        #endregion

        public static string GetError(uint errorCode)
        {
            if (ErrorCode.TryGetValue(errorCode, out var error)) return error;
            return "unknown error";
        }
        public static IReadOnlyDictionary<uint, string> ErrorCode { get; } = new Dictionary<uint, string>()
        {
            [0] = "",
            [1] = "The request is not supported.",
            [2] = "The user did not have the necessary access.",
            [3] = "The service cannot be stopped because other services that are running are dependent on it.",
            [4] = "The requested control code is not valid, or it is unacceptable to the service.",
            [5] = "The requested control code cannot be sent to the service because the state of the service (Win32_BaseService.State property) is equal to 0, 1, or 2.",
            [6] = "The service has not been started.",
            [7] = "The service did not respond to the start request in a timely fashion.",
            [8] = "Unknown failure when starting the service.",
            [9] = "The directory path to the service executable file was not found.",
            [10] = "The service is already running.",
            [11] = "The database to add a new service is locked.",
            [12] = "A dependency this service relies on has been removed from the system.",
            [13] = "The service failed to find the service needed from a dependent service.",
            [14] = "The service has been disabled from the system.",
            [15] = "The service does not have the correct authentication to run on the system.",
            [16] = "This service is being removed from the system.",
            [17] = "The service has no execution thread.",
            [18] = "The service has circular dependencies when it starts.",
            [19] = "A service is running under the same name.",
            [20] = "The service name has invalid characters.",
            [21] = "Invalid parameters have been passed to the service.",
            [22] = "The account under which this service runs is either invalid or lacks the permissions to run the service.",
            [23] = "The service exists in the database of services available from the system.",
            [24] = "The service is currently paused in the system.",
        }.ToFrozenDictionary();

        public uint StartService() => _invoke("StartService");
        public uint StopService() => _invoke("StopService");
        public uint PauseService() => _invoke("PauseService");
        public uint ResumeService() => _invoke("ResumeService");
        public uint Change(ChangeOptions options)
        {
            var parameters = WmiParameters.ServiceChange(options);
            var methodName = "Change";
            using var inParams = _proxy.GetMethodParameters(methodName);
            WmiParameters.Apply(inParams, parameters);
            using var outParams = _proxy.InvokeMethod(methodName, inParams, null);
            return (uint)outParams["ReturnValue"];
        }
        public uint ChangeStartMode(StartMode mode)
        {
            var startMode = WmiParameters.StartModeName(mode);
            var methodName = "ChangeStartMode";
            using var inParams = _proxy.GetMethodParameters(methodName);
            inParams["StartMode"] = startMode;
            using var outParams = _proxy.InvokeMethod(methodName, inParams, null);
            return (uint)outParams["ReturnValue"];
        }
        /// <summary>Submits Delete without stopping the service; zero means accepted, not removed.</summary>
        public uint DeleteService() => _invoke("Delete");

        public static uint Create(CreateOptions options, ManagementScope scope)
        {
            var parameters = WmiParameters.ServiceCreate(options);
            ArgumentNullException.ThrowIfNull(scope);
            var methodName = "Create";
            using ManagementClass reg = new ManagementClass(scope, new ManagementPath("Win32_Service"), null);
            using ManagementBaseObject inParams = reg.GetMethodParameters(methodName);
            WmiParameters.Apply(inParams, parameters);
            using var outParams = reg.InvokeMethod(methodName, inParams, null);
            return (uint)outParams["ReturnValue"];
        }
        public static uint Create(CreateOptions options)
        {
            _ = WmiParameters.ServiceCreate(options);
            var scope = ManagementScopeExtensions.CreateDefault();
            return Create(options, scope);
        }
    }

}
