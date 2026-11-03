using IFactory.WMI.Win32_NetworkAdapterConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IFactory.WMI.Tests
{
    public class NetworkAdapterConfigurationTest
    {
        [Fact]
        public void GetNetworkAdapterConfigurations()
        {
            var configurations = NetworkAdapterConfiguration.Get();
        }
    }
}
