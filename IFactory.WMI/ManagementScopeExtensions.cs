using System.Management;
namespace IFactory.WMI;
/// <summary>Factories that immediately connect a WMI scope.</summary>
public static class ManagementScopeExtensions
{
    public static ManagementScope Create(string host, string wmiNamespace, ConnectionOptions options)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(host);
        ArgumentException.ThrowIfNullOrWhiteSpace(wmiNamespace);
        ArgumentNullException.ThrowIfNull(options);
        var scope = new ManagementScope($@"\\{host}\{wmiNamespace}", options);
        scope.Connect();
        return scope;
    }
    public static ManagementScope CreateDefault() => CreateDefault(@"root\cimv2");
    public static ManagementScope CreateDefault(string wmiNamespace) => Create("localhost", wmiNamespace, DefaultOptions());
    internal static ConnectionOptions DefaultOptions() => new()
    {
        Impersonation = ImpersonationLevel.Impersonate,
        Authentication = AuthenticationLevel.PacketPrivacy,
        EnablePrivileges = true
    };
}
