using IFactory.WMI.Win32_USBHub;
using Xunit;
using Xunit.Abstractions;
namespace IFactory.WMI.Tests;
public class USBHubTest(ITestOutputHelper output)
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  int count = 0;
  foreach (var item in USBHub.Get(ManagementScopeExtensions.CreateDefault()))
  {
   count++;
   using (item) { Assert.False(string.IsNullOrEmpty(item.PNPDeviceID)); }
  }
  output.WriteLine($"Enumerated instances: {count}");
 }
}
