using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFactory.WMI.MSFT_NetAdapter;
using Xunit;

namespace IFactory.WMI.Tests
{
    public class NetAdapterTest
    {
        [Fact]
        public void GetAdapters()
        {
            var adapters = NetAdapter.Get();
        }
    }
}
