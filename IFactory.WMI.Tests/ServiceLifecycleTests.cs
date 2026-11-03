using Xunit;
using IFactory.WMI.Win32_Service;
namespace IFactory.WMI.Tests;
public class ServiceLifecycleTests
{
 [ServiceMutationFact]
 public void CreatesChangesAndDeletesOnlyUniqueTestService()
 {
  var name = "WmiLibraryTest_" + Guid.NewGuid().ToString("N");
  var executable = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
  Assert.Null(Service.Get(name));
  Assert.Equal(0u, Service.Create(new CreateOptions { Name=name, DisplayName=name, PathName=$"\"{executable}\"", StartMode=StartMode.Manual }));
  try
  {
   using var service=Service.Get(name); Assert.NotNull(service);
   Assert.Equal(0u,service.Change(new ChangeOptions { DisplayName=name+"_changed" }));
   Assert.Equal(0u,service.ChangeStartMode(StartMode.Disabled));
   using var refreshed=Service.Get(name); Assert.NotNull(refreshed); Assert.Equal(name+"_changed",refreshed.DisplayName); Assert.Equal("Disabled",refreshed.StartMode);
  }
  finally { using var service=Service.Get(name); if(service != null) Assert.Equal(0u,service.DeleteService()); }
  using var deleted=Service.Get(name); Assert.Null(deleted);
 }
}
public sealed class ServiceMutationFactAttribute : FactAttribute
{
 public ServiceMutationFactAttribute() { if(Environment.GetEnvironmentVariable("WMI_TEST_SERVICE_MUTATION") != "1") Skip="Requires explicit opt-in WMI_TEST_SERVICE_MUTATION=1 and service administration rights."; }
}
