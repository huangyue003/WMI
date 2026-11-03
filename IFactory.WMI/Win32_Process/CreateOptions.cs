namespace IFactory.WMI.Win32_Process
{
    public readonly struct CreateOptions
    {
        public CreateOptions(string commandLine, string currentDirectory, ProcessStartup processStartup)
        {
            CommandLine = commandLine;
            CurrentDirectory = currentDirectory;
            ProcessStartup = processStartup;
        }
        /// <summary>
        /// 要执行的命令行。 系统将向命令行添加一个空字符，并在必要时剪裁字符串，以指示实际使用的文件。
        /// </summary>
        public readonly string CommandLine = string.Empty;
        /// <summary>
        /// 子进程的当前驱动器和目录。 字符串要求当前目录解析为已知路径。 用户可以指定绝对路径或相对于当前工作目录的路径。 如果此参数为 NULL，则新进程将具有与调用进程相同的路径。 此选项主要用于必须启动应用程序并指定应用程序初始驱动器和工作目录的 shell
        /// </summary>
        public readonly string CurrentDirectory = string.Empty;
        public readonly ProcessStartup ProcessStartup;
    }
}
