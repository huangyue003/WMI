using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_Process
{
    public enum PriorityClass
    {
        /// <summary>
        /// 指示没有特殊计划需求的正常进程。
        /// </summary>
        Normal = 32,
        /// <summary>
        /// 指示一个进程，其线程仅在系统处于空闲状态时运行，并且被在较高优先级类中运行的任何进程的线程抢占。 例如，屏幕保护程序。 空闲优先级类由子进程继承。
        /// </summary>
        Idle = 64,
        /// <summary>
        /// 指示执行必须立即执行才能正确运行的时间关键型任务的进程。 高优先级类进程的线程会抢占正常优先级或空闲优先级类进程的线程。 例如，Windows 任务列表，无论操作系统上的负载如何，它都必须在用户调用时快速响应。 使用高优先级类时要格外小心，因为高优先级类 CPU 绑定应用程序几乎可以使用所有可用的周期。 只有实时优先级会抢占设置为此级别的线程。
        /// </summary>
        High = 128,
        /// <summary>
        /// 指示具有最高优先级的进程。 实时优先级类进程的线程会抢占所有其他进程的线程，包括高优先级线程和执行重要任务的操作系统进程。 例如，执行超过非常短的时间间隔的实时进程可能会导致磁盘缓存无法刷新，或导致鼠标无响应。
        /// </summary>
        Realtime = 256,
        /// <summary>
        /// 指示优先级高于“空闲”但低于“正常”的进程。
        /// </summary>
        Below_Normal = 16384,
        /// <summary>
        /// 指示优先级高于 Normal 但低于 High 的进程。
        /// </summary>
        Above_Normal = 32768,

    }
}
