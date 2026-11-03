using IFactory.WMI.Win32_USBController;
using Xunit;
using Xunit.Abstractions;
namespace IFactory.WMI.Tests;
public class USBControllerTest(ITestOutputHelper output)
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  int count = 0;
  foreach (var item in USBController.Get(ManagementScopeExtensions.CreateDefault()))
  {
   count++;
   using (item) { Assert.False(string.IsNullOrEmpty(item.DeviceID)); }
  }
  output.WriteLine($"Enumerated instances: {count}");
 }
}
