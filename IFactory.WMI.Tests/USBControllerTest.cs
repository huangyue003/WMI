using IFactory.WMI.Win32_USBController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IFactory.WMI.Tests
{
    public class USBControllerTest
    {
        [Fact]
        public void GetController()
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            var controllers = USBController.Get(scope);
            Assert.True(controllers.Any());
        }
    }
}
