using IFactory.WMI.Win32_LogicalDisk;
using Xunit;
using Xunit.Abstractions;
namespace IFactory.WMI.Tests;
public class LogicalDiskTest(ITestOutputHelper output)
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  int count = 0;
  foreach (var item in LogicalDisk.Get(ManagementScopeExtensions.CreateDefault()))
  {
   count++;
   using (item) { Assert.False(string.IsNullOrEmpty(item.DeviceID)); Assert.True(Enum.IsDefined(item.DriveType)); }
  }
  output.WriteLine($"Enumerated instances: {count}");
  Assert.True(count > 0, "The system has at least one logical disk.");
 }
}
