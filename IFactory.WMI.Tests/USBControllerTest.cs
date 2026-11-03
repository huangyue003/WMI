using IFactory.WMI.Win32_USBController;
using Xunit;
namespace IFactory.WMI.Tests;
public class USBControllerTest
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  foreach (var item in USBController.Get(ManagementScopeExtensions.CreateDefault()))
  {
   using (item) { Assert.False(string.IsNullOrEmpty(item.DeviceID)); }
  }
 }
}
