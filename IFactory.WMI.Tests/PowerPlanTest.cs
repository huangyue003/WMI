using IFactory.WMI.Win32_PowerPlan;
using Xunit;
namespace IFactory.WMI.Tests;
public class PowerPlanTest
{
 [PowerProviderFact]
 public void EnumeratesLocalProvider()
 {
  foreach (var item in PowerPlan.Get())
  {
   using (item) { Assert.False(string.IsNullOrEmpty(item.InstanceID)); }
  }
 }
}
