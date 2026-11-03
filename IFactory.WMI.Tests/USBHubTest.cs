using IFactory.WMI.Win32_USBHub;
using Xunit;
namespace IFactory.WMI.Tests;
public class USBHubTest
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  foreach (var item in USBHub.Get(ManagementScopeExtensions.CreateDefault()))
  {
   using (item) { Assert.False(string.IsNullOrEmpty(item.PNPDeviceID)); }
  }
 }
}
