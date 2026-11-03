using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_Process
{
    public class ProcessStartup
    {
        public uint? CreateFlags { get; set; }
        public string[]? EnvironmentVariables { get; set; }
        public UInt16? ErrorMode { get; set; }
        public uint? FillAttribute { get; set; }
        /// <summary>
        /// 新进程的优先级类。 使用此属性可确定进程中线程的计划优先级。 如果该属性保留为 null，则优先级类默认为 Normal ，除非创建过程的优先级类为 Idle 或 Below_Normal。 在这些情况下，子进程接收调用进程的默认优先级类。
        /// </summary>
        public PriorityClass? PriorityClass { get; set; }
        public UInt16? ShowWindow { get; set; }
        /// <summary>
        /// 创建新控制台窗口时标题栏中显示的文本;用于控制台进程。 如果 为 NULL，则将可执行文件的名称用作窗口标题。 对于不创建新控制台窗口的 GUI 或控制台进程，此属性必须为 NULL 。
        /// </summary>
        public string? Title { get; set; }
        public string? WinstationDesktop { get; set; }
        /// <summary>
        /// 创建新窗口时窗口左上角的 X 偏移量（以像素为单位）。 偏移量从屏幕左上角开始。 对于 GUI 进程，如果 createWindow 的 X 参数CW_USEDEFAULT，则新进程首次调用 CreateWindow 以创建重叠窗口时使用指定位置。
        /// </summary>
        public uint? X { get; set; }
        /// <summary>
        /// 字符列中的屏幕缓冲区宽度。 此属性用于创建控制台窗口的进程，在 GUI 进程中被忽略。
        /// </summary>
        public uint? XCountChars { get; set; }
        public uint? XSize { get; set; }
        /// <summary>
        /// 创建新窗口时窗口左上角的像素偏移量。 偏移量从屏幕左上角开始。 对于 GUI 进程，如果 createWindow 的 y 参数CW_USEDEFAULT，则新进程首次调用 CreateWindow 以创建重叠窗口时，将使用指定位置。
        /// </summary>
        public uint? Y { get; set; }
        /// <summary>
        /// 字符行中的屏幕缓冲区高度。 此属性用于创建控制台窗口的进程，但在 GUI 进程中被忽略。
        /// </summary>
        public uint? YCountChars { get; set; }

        public uint? YSize { get; set; }
    }
}
