using IFactory.WMI.Win32_NetworkAdapterConfiguration;
using Xunit;
using Xunit.Abstractions;
namespace IFactory.WMI.Tests;
public class NetworkAdapterConfigurationTest(ITestOutputHelper output)
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  int count = 0;
  foreach (var item in NetworkAdapterConfiguration.Get())
  {
   count++;
   using (item) { Assert.False(string.IsNullOrEmpty(item.SettingID)); Assert.True(item.IPEnabled); }
  }
  output.WriteLine($"Enumerated instances: {count}");
 }
}
