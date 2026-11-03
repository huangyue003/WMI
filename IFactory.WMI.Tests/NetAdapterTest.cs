using IFactory.WMI.MSFT_NetAdapter;
using Xunit;
namespace IFactory.WMI.Tests;
public class NetAdapterTest
{
 [Fact]
 public void EnumeratesLocalProvider()
 {
  foreach (var item in NetAdapter.Get())
  {
   using (item) { Assert.False(string.IsNullOrEmpty(item.InterfaceGuid)); }
  }
 }
}
