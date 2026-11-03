using IFactory.WMI.Win32_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_Process
{
    /// <summary>
    /// https://learn.microsoft.com/zh-cn/windows/win32/cimwin32prov/win32-process
    /// </summary>
    public class Process : IDisposable
    {
        public static Process? Get(string name)
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            return Get(name, scope);
        }
        public static Process? Get(string name, ManagementScope scope)
        {
            ManagementObject? instance = null;
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_Process WHERE Name='{WmiValue.Escape(name)}'");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
            {
                instance = item;
                break;
            }
            return instance == null ? null : new Process(instance);
        }

        private Process(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private readonly ManagementObject _proxy;
        public void Dispose() => _proxy.Dispose();

        #region Properties

        public string CreationClassName => (string)_proxy[nameof(CreationClassName)];
        /// <summary>
        /// 对象的简短说明 - 单行字符串。
        /// </summary>
        public string Caption => (string)_proxy[nameof(Caption)];
        /// <summary>
        /// 用于启动特定进程的命令行（如果适用）。
        /// </summary>
        public string CommandLine => (string)_proxy[nameof(CommandLine)];
        /// <summary>
        /// 进程开始执行的日期。
        /// </summary>
        public DateTime CreationDate => WmiValue.Date(_proxy[nameof(CreationDate)]);
        /// <summary>
        /// 范围计算机系统的创建类名称。
        /// </summary>
        public string CSCreationClassName => (string)_proxy[nameof(CSCreationClassName)];
        /// <summary>
        /// 范围计算机系统的名称。
        /// </summary>
        public string CSName => (string)_proxy[nameof(CSName)];
        /// <summary>
        /// 对象的说明。
        /// </summary>
        public string Description => (string)_proxy[nameof(Description)];
        /// <summary>
        /// 进程的可执行文件的路径。
        /// </summary>
        public string ExecutablePath => (string)_proxy[nameof(ExecutablePath)];
        /// <summary>
        /// 进程的当前运行条件。Unknown 0  Other 1  Ready  2  Running  3
        /// </summary>
        public UInt16 ExecutionState => (UInt16)_proxy[nameof(ExecutionState)];
        /// <summary>
        /// 进程标识符。
        /// </summary>
        public string Handle => (string)_proxy[nameof(Handle)];
        /// <summary>
        /// 进程拥有的打开句柄总数。 HandleCount 是进程中每个线程当前打开的句柄之和。 句柄用于检查或修改系统资源。 每个句柄在表中都有一个在内部维护的条目。 条目包含用于标识资源类型的资源和数据的地址。
        /// </summary>
        public uint HandleCount => (uint)_proxy[nameof(HandleCount)];
        /// <summary>
        /// 对象的安装日期。 可以在不将值写入此属性的情况下安装 对象。
        /// </summary>
        public DateTime InstallDate => WmiValue.Date(_proxy[nameof(InstallDate)]);
        /// <summary>
        /// 内核模式下的时间（以毫秒为单位）。 如果此信息不可用，请使用值 0 (零) 。
        /// </summary>
        public UInt64 KernelModeTime => (UInt64)_proxy[nameof(KernelModeTime)];
        /// <summary>
        /// 进程的最大工作集大小。 进程的工作集是物理 RAM 中进程可见的内存页集。 这些页面是常驻的，可供应用程序使用，而不会触发页面错误。
        /// </summary>
        public uint MaximumWorkingSetSize => (uint)_proxy[nameof(MaximumWorkingSetSize)];
        /// <summary>
        /// 进程的最小工作集大小。 进程的工作集是物理 RAM 中进程可见的内存页集。 这些页是常驻的，可供应用程序使用，而不会触发页面错误。
        /// </summary>
        public uint MinimumWorkingSetSize => (uint)_proxy[nameof(MinimumWorkingSetSize)];
        /// <summary>
        /// 负责进程的可执行文件的名称，等效于任务管理器中的“映像名称”属性。
        /// </summary>
        public string Name => (string)_proxy[nameof(Name)];
        /// <summary>
        /// 范围操作系统的创建类名称。
        /// </summary>
        public string OSCreationClassName => (string)_proxy[nameof(OSCreationClassName)];
        /// <summary>
        /// 范围操作系统的名称。
        /// </summary>
        public string OSName => (string)_proxy[nameof(OSName)];
        /// <summary>
        /// 执行的不是读取或写入操作的 I/O 操作数。
        /// </summary>
        public UInt64 OtherOperationCount => (UInt64)_proxy[nameof(OtherOperationCount)];
        /// <summary>
        /// 在非读取或写入操作的操作期间传输的数据量。
        /// </summary>
        public UInt64 OtherTransferCount => (UInt64)_proxy[nameof(OtherTransferCount)];
        /// <summary>
        /// 进程生成的页错误数。
        /// </summary>
        public uint PageFaults => (uint)_proxy[nameof(PageFaults)];
        /// <summary>
        /// 进程当前使用的页面文件空间量
        /// </summary>
        public uint PageFileUsage => (uint)_proxy[nameof(PageFileUsage)];
        public uint ParentProcessId => (uint)_proxy[nameof(ParentProcessId)];
        public uint PeakPageFileUsage => (uint)_proxy[nameof(PeakPageFileUsage)];
        public UInt64 PeakVirtualSize => (UInt64)_proxy[nameof(PeakVirtualSize)];
        public uint PeakWorkingSetSize => (uint)_proxy[nameof(PeakWorkingSetSize)];
        /// <summary>
        /// 在操作系统中计划进程的优先级。 值越高，进程接收的优先级就越高。 优先级值的范围可以是 0 (零) ，这是最低优先级到 31，这是最高优先级。
        /// </summary>
        public uint Priority => (uint)_proxy[nameof(Priority)];
        public UInt64 PrivatePageCount => (UInt64)_proxy[nameof(PrivatePageCount)];
        /// <summary>
        /// 用于区分一个进程与另一个进程的数字标识符。 从进程创建时间到进程终止，ProcessID 都是有效的。 终止后，相同的数字标识符可以应用于新进程。
        /// </summary>
        public uint ProcessId => (uint)_proxy[nameof(ProcessId)];
        public uint QuotaNonPagedPoolUsage => (uint)_proxy[nameof(QuotaNonPagedPoolUsage)];
        public uint QuotaPagedPoolUsage => (uint)_proxy[nameof(QuotaPagedPoolUsage)];
        public uint QuotaPeakNonPagedPoolUsage => (uint)_proxy[nameof(QuotaPeakNonPagedPoolUsage)];
        public uint QuotaPeakPagedPoolUsage => (uint)_proxy[nameof(QuotaPeakPagedPoolUsage)];
        public UInt64 ReadOperationCount => (UInt64)_proxy[nameof(ReadOperationCount)];
        public UInt64 ReadTransferCount => (UInt64)_proxy[nameof(ReadTransferCount)];
        public uint SessionId => (uint)_proxy[nameof(SessionId)];
        public string Status => (string)_proxy[nameof(Status)];
        public DateTime TerminationDate => WmiValue.Date(_proxy[nameof(TerminationDate)]);
        public uint ThreadCount => (uint)_proxy[nameof(ThreadCount)];
        public UInt64 UserModeTime => (UInt64)_proxy[nameof(UserModeTime)];
        public UInt64 VirtualSize => (UInt64)_proxy[nameof(VirtualSize)];
        public string WindowsVersion => (string)_proxy[nameof(WindowsVersion)];
        public UInt64 WorkingSetSize => (UInt64)_proxy[nameof(WorkingSetSize)];
        public UInt64 WriteOperationCount => (UInt64)_proxy[nameof(WriteOperationCount)];
        public UInt64 WriteTransferCount => (UInt64)_proxy[nameof(WriteTransferCount)];

        #endregion

        public static string GetError(uint errorCode)
        {
            if (ErrorCode.TryGetValue(errorCode, out var error)) return error;
            return "unknown error";
        }
        public static Dictionary<uint, string> ErrorCode = new()
        {
            [0] = "",
            [2] = "Access denied",
            [3] = "Insufficient privilege",
            [8] = "Unknown failure",
            [9] = "Path not found",
            [21] = "Invalid parameter",
        };
        public static uint Create(CreateOptions options, ManagementScope scope, out uint processId)
        {
            processId = 0;
            using ManagementClass processStartup = new ManagementClass(scope, new ManagementPath("Win32_ProcessStartup"), null);
            using var startupInstance = processStartup.CreateInstance()!;
            startupInstance.SetPropertyValue(options.ProcessStartup);

            var methodName = "Create";
            using ManagementClass reg = new ManagementClass(scope, new ManagementPath("Win32_Process"), null);
            using ManagementBaseObject inParams = reg.GetMethodParameters(methodName);

            inParams.SetPropertyValue("CommandLine", options.CommandLine);
            inParams.SetPropertyValue("CurrentDirectory", options.CurrentDirectory);
            inParams.SetPropertyValue("ProcessStartupInformation", startupInstance);
            using var outParams = reg.InvokeMethod(methodName, inParams, null);
            var returnValue = (uint)outParams["ReturnValue"];
            if (returnValue == 0)
                processId = (uint)outParams["ProcessId"];
            return returnValue;
        }

        public uint Terminate(uint reason)
        {
            var methodName = "Terminate";
            using var inParams = _proxy.GetMethodParameters(methodName);
            inParams.SetPropertyValue("Reason", reason);
            using var outParams = _proxy.InvokeMethod(methodName, inParams, null);
            return (uint)outParams["ReturnValue"];
        }
        public uint SetPriority(PriorityClass priority)
        {
            var methodName = "SetPriority";
            using var inParams = _proxy.GetMethodParameters(methodName);
            inParams.SetPropertyValue("Priority", priority);
            using var outParams = _proxy.InvokeMethod(methodName, inParams, null);
            return (uint)outParams["ReturnValue"];
        }
    }
}
