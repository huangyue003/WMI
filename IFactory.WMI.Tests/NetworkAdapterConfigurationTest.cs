using IFactory.WMI.Win32_NetworkAdapterConfiguration;
using Xunit;
namespace IFactory.WMI.Tests;
public class NetworkAdapterConfigurationTest
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  foreach (var item in NetworkAdapterConfiguration.Get())
  {
   using (item) { Assert.False(string.IsNullOrEmpty(item.SettingID)); Assert.True(item.IPEnabled); }
  }
 }
}
