using IFactory.WMI.MSFT_NetAdapter;
using Xunit;
using Xunit.Abstractions;
namespace IFactory.WMI.Tests;
public class NetAdapterTest(ITestOutputHelper output)
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  int count = 0;
  foreach (var item in NetAdapter.Get())
  {
   count++;
   using (item) { Assert.False(string.IsNullOrEmpty(item.InterfaceGuid)); }
  }
  output.WriteLine($"Enumerated instances: {count}");
 }
}
