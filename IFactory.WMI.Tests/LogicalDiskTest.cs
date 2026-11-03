using IFactory.WMI.Win32_LogicalDisk;
using Xunit;
namespace IFactory.WMI.Tests;
public class LogicalDiskTest
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  foreach (var item in LogicalDisk.Get(ManagementScopeExtensions.CreateDefault()))
  {
   using (item) { Assert.False(string.IsNullOrEmpty(item.DeviceID)); Assert.True(Enum.IsDefined(item.DriveType)); }
  }
 }
}
