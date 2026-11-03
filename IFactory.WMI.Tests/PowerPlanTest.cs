using IFactory.WMI.Win32_PowerPlan;
using Xunit;
using Xunit.Abstractions;
namespace IFactory.WMI.Tests;
public class PowerPlanTest(ITestOutputHelper output)
{
 [PowerProviderFact]
 public void EnumeratesLocalProvider()
 {
  int count = 0;
  foreach (var item in PowerPlan.Get())
  {
   count++;
   using (item) { Assert.False(string.IsNullOrEmpty(item.InstanceID)); }
  }
  output.WriteLine($"Enumerated instances: {count}");
 }
}
