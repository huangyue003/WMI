using IFactory.WMI.Win32_LogicalDisk;
using IFactory.WMI.Win32_USBController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IFactory.WMI.Tests
{
    public class LogicalDiskTest
    {
        [Fact]
        public void GetLogicalDisks()
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            var logicalDisks = LogicalDisk.Get(scope);
            Assert.True(logicalDisks.Any());
        }
    }
}
