namespace IFactory.WMI.Win32_Service
{
    public class ChangeOptions
    {
        /// <summary>
        /// 服务的显示名称
        /// </summary>
        public string? DisplayName { get; set; }
        /// <summary>
        /// 实现服务的可执行文件的完全限定路径
        /// </summary>
        public string? PathName { get; set; }
        /// <summary>
        /// 提供给调用它们的进程的服务类型
        /// </summary>
        public ServiceType? ServiceType{ get; set; }
        /// <summary>
        /// 如果此服务在启动期间无法启动，则错误的严重性。 该值指示启动程序在发生故障时采取的操作。 所有错误都由系统记录。
        /// </summary>
        public ErrorControl? ErrorControl{ get; set; }
        /// <summary>
        /// Windows 基本服务的启动模式
        /// </summary>
        public StartMode? StartMode{ get; set; }
        public bool? DesktopInteract{ get; set; }
        public string? StartName{ get; set; }
        public string? StartPassword{ get; set; }
        public string? LoadOrderGroup{ get; set; }
        /// <summary>
        /// 此服务启动之前必须启动的负载排序组的列表。 数组以 null 结尾为双倍。 如果指针为 NULL，或者如果指针指向空字符串，则服务没有依赖项。 组名必须以 Winsvc.h 文件中定义的 SC_GROUP_IDENTIFIER () 字符作为前缀，以便将其与服务名称区分开来，因为服务和服务组共享相同的命名空间。 对组的依赖意味着，如果至少一个组成员在尝试启动组的所有成员后正在运行，则此服务可以运行。
        /// </summary>
        public string[]? LoadOrderGroupDependencies{ get; set; }
        /// <summary>
        /// 包含此服务启动前必须启动的服务名称的列表。 数组以 NULL 结尾为双倍。 如果指针为 NULL，或者如果指针指向空字符串，则服务没有依赖项。 对服务的依赖关系指示此服务仅当它所依赖的服务正在运行时才能运行。
        /// </summary>
        public string[]? ServiceDependencies{ get; set; }
    }
}
