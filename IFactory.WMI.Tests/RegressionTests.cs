using System.Management;
using Xunit;
using WmiProcess = IFactory.WMI.Win32_Process.Process;
using IFactory.WMI.Win32_Process;
using IFactory.WMI.Win32_Service;
namespace IFactory.WMI.Tests;
public class RegressionTests
{
 [Theory]
 [InlineData("normal", "normal")]
 [InlineData("a'b", "a\\'b")]
 [InlineData("a\\b", "a\\\\b")]
 public void EscapesWqlLiterals(string input, string expected) => Assert.Equal(expected, WmiValue.Escape(input));
 [Fact] public void NullNameRejected() => Assert.Throws<ArgumentNullException>(() => WmiValue.Escape(null!));
 [Fact] public void ConvertsDmtfDate() => Assert.Equal(new DateTime(2026, 1, 2, 3, 4, 5), WmiValue.Date("20260102030405.000000+000").ToUniversalTime());
 [Fact] public void MissingDateUsesCompatibilitySentinel() => Assert.Equal(DateTime.MinValue, WmiValue.Date(null));
 [Fact] public void UnknownErrorsRemainDefined() { Assert.Equal("unknown error", WmiProcess.GetError(uint.MaxValue)); Assert.Equal("unknown error", Service.GetError(uint.MaxValue)); }
 [Fact] public void QueryMetacharactersAreLiteral() { using var item = WmiProcess.Get("missing' OR Name='cmd.exe"); Assert.Null(item); }
 [Fact] public void ReadsCurrentProcessAndDates()
 {
  var name = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
  using var item = WmiProcess.Get(name);
  Assert.NotNull(item);
  Assert.Equal(name, item.Name, ignoreCase:true);
  Assert.NotEqual(DateTime.MinValue, item.CreationDate);
 }
 [Fact] public void ReadsKnownService()
 {
  using var service = Service.Get("Winmgmt"); Assert.NotNull(service); Assert.Equal("Winmgmt", service.Name, ignoreCase:true);
 }
 [Fact] public void ReadsAdapterByNameAndConfigurationBySettingId()
 {
  foreach(var item in MSFT_NetAdapter.NetAdapter.Get()) using(item) { using var found = MSFT_NetAdapter.NetAdapter.Get(item.Name ?? throw new InvalidOperationException("Missing adapter Name")); Assert.NotNull(found); Assert.Equal(item.InterfaceGuid,found.InterfaceGuid); }
  foreach(var item in Win32_NetworkAdapterConfiguration.NetworkAdapterConfiguration.Get()) using(item) { using var found = Win32_NetworkAdapterConfiguration.NetworkAdapterConfiguration.Get(item.SettingID ?? throw new InvalidOperationException("Missing SettingID")); Assert.NotNull(found); Assert.Equal(item.Index,found.Index); }
 }
 [Fact] public void CreatesAndTerminatesOnlyOwnedProcess()
 {
  var scope = ManagementScopeExtensions.CreateDefault();
  string exe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "WindowsPowerShell", "v1.0", "powershell.exe");
  var result = WmiProcess.Create(new Win32_Process.CreateOptions($"\"{exe}\" -NoProfile -NonInteractive -Command Start-Sleep -Seconds 60", Environment.CurrentDirectory, new ProcessStartup { ShowWindow = 0, PriorityClass = PriorityClass.Normal }), scope, out var pid);
  Assert.Equal(0u, result);
  using var child = System.Diagnostics.Process.GetProcessById(checked((int)pid));
  try
  {
   using var proxy = new ManagementObject(scope, new ManagementPath($"Win32_Process.Handle='{pid}'"), null);
   var constructor = typeof(WmiProcess).GetConstructor(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, [typeof(ManagementObject)], null)!;
   using var item = (WmiProcess)constructor.Invoke([proxy]);
   Assert.Equal(pid,item.ProcessId); Assert.Equal(0u,item.SetPriority(PriorityClass.Normal)); Assert.Equal(0u,item.Terminate(0)); Assert.True(child.WaitForExit(10000));
  }
  finally { if(!child.HasExited) { child.Kill(); Assert.True(child.WaitForExit(10000)); } }
 }
}
