using Xunit;
namespace IFactory.WMI.Tests;
public sealed class PowerProviderFactAttribute : FactAttribute
{
 public PowerProviderFactAttribute()
 {
  if (Environment.GetEnvironmentVariable("WMI_TEST_POWER_PROVIDER") != "1")
   Skip = "Set WMI_TEST_POWER_PROVIDER=1 in a session permitted to query root/cimv2/power; see VALIDATION.md.";
 }
}
