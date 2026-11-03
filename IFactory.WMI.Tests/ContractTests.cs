using System.Management;
using System.Reflection;
using Xunit;
using IFactory.WMI.Win32_Service;
using IFactory.WMI.Win32_Process;
using ServiceCreateOptions = IFactory.WMI.Win32_Service.CreateOptions;
using WmiProcess = IFactory.WMI.Win32_Process.Process;
namespace IFactory.WMI.Tests;

public class ContractTests
{
    private static ServiceCreateOptions Options() => new() { Name = "ExampleService", DisplayName = "Example service", PathName = "example-service.exe" };

    [Theory]
    [InlineData(0u)]
    [InlineData(2u)]
    [InlineData(16u)]
    public void DeleteSubmitsExactlyOneRequestWithoutReadingUnboundState(uint code)
    {
        using var proxy = new ManagementObject(); // State cannot be read from this unbound object.
        var calls = new List<string>();
        using var service = new Service(proxy, method => { calls.Add(method); return code; });
        Assert.Equal(code, service.DeleteService());
        Assert.Equal(new[] { "Delete" }, calls);
    }

    [Fact]
    public void DeletePropagatesOriginalExceptionWithoutRetry()
    {
        using var proxy = new ManagementObject();
        var error = new ManagementException("Example provider failure");
        int count = 0;
        using var service = new Service(proxy, _ => { count++; throw error; });
        Assert.Same(error, Assert.Throws<ManagementException>(() => service.DeleteService()));
        Assert.Equal(1, count);
    }

    [Fact]
    public void ServiceDefaultsAreAppliedWithoutMutatingCaller()
    {
        var options = Options();
        var values = WmiParameters.ServiceCreate(options);
        Assert.Equal(0x10u, values["ServiceType"]);
        Assert.Equal(1u, values["ErrorControl"]);
        Assert.Equal("Automatic", values["StartMode"]);
        Assert.Null(options.ServiceType); Assert.Null(options.ErrorControl); Assert.Null(options.StartMode);
        Assert.Equal("ExampleService", options.Name);
        Assert.DoesNotContain("StartPassword", values.Keys);
        Assert.Equal(values.OrderBy(x => x.Key), WmiParameters.ServiceCreate(options).OrderBy(x => x.Key));
    }

    [Fact]
    public void ExplicitServiceOptionsUseProtocolTypes()
    {
        var options = Options();
        options.ServiceType = ServiceType.OwnProcess | ServiceType.InteractiveProcess;
        options.ErrorControl = ErrorControl.Severe;
        options.StartMode = StartMode.Manual;
        options.DesktopInteract = true;
        options.ServiceDependencies = ["ExampleDependency"];
        var values = WmiParameters.ServiceCreate(options);
        Assert.Equal(0x110u, Assert.IsType<uint>(values["ServiceType"]));
        Assert.Equal(2u, Assert.IsType<uint>(values["ErrorControl"]));
        Assert.Equal("Manual", Assert.IsType<string>(values["StartMode"]));
        Assert.True(Assert.IsType<bool>(values["DesktopInteract"]));
        Assert.Equal(new[] { "ExampleDependency" }, Assert.IsType<string[]>(values["ServiceDependencies"]));
        Assert.Equal(StartMode.Manual, options.StartMode);
    }

    [Theory]
    [InlineData(StartMode.Boot, "Boot")]
    [InlineData(StartMode.System, "System")]
    [InlineData(StartMode.Automatic, "Automatic")]
    [InlineData(StartMode.Manual, "Manual")]
    [InlineData(StartMode.Disabled, "Disabled")]
    public void StartupModesMapToProviderNames(StartMode mode, string expected)
    {
        Assert.Equal(expected, WmiParameters.ServiceChange(new ChangeOptions { StartMode = mode })["StartMode"]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void InvalidRequiredFieldsFailBeforeConnectingAndDoNotMutate(string? invalid)
    {
        foreach (var member in new[] { "Name", "DisplayName", "PathName" })
        {
            var options = Options();
            typeof(ServiceCreateOptions).GetProperty(member)!.SetValue(options, invalid);
            var error = Record.Exception(() => Service.Create(options));
            Assert.NotNull(error);
            Assert.Equal(invalid is null ? typeof(ArgumentNullException) : typeof(ArgumentException), error.GetType());
            Assert.Equal("options", ((ArgumentException)error).ParamName);
            Assert.Null(options.ServiceType); Assert.Null(options.StartMode); Assert.Null(options.ErrorControl);
        }
    }

    [Fact]
    public void InvalidEnumsAndNullArgumentsFailWithoutProviderCalls()
    {
        Assert.Throws<ArgumentNullException>(() => Service.Create(null!));
        Assert.Throws<ArgumentNullException>(() => Service.Create(Options(), null!));
        Assert.Throws<ArgumentNullException>(() => WmiParameters.ServiceChange(null!));
        Assert.Throws<ArgumentNullException>(() => WmiParameters.ProcessStartup(null!));
        var options = Options(); options.StartMode = (StartMode)100;
        Assert.Throws<ArgumentOutOfRangeException>(() => Service.Create(options));
        options.StartMode = null; options.ErrorControl = (ErrorControl)100;
        Assert.Throws<ArgumentOutOfRangeException>(() => WmiParameters.ServiceCreate(options));
        options.ErrorControl = null; options.ServiceType = (ServiceType)0x8000;
        Assert.Throws<ArgumentOutOfRangeException>(() => WmiParameters.ServiceCreate(options));
        Assert.Throws<ArgumentOutOfRangeException>(() => WmiParameters.ProcessStartup(new ProcessStartup { PriorityClass = (PriorityClass)1 }));
        using var proxy = new ManagementObject(); using var service = new Service(proxy);
        Assert.Throws<ArgumentOutOfRangeException>(() => service.ChangeStartMode((StartMode)100));
    }

    private sealed class ExtendedOptions : ChangeOptions { public string Remark => "Not a WMI parameter"; }
    private sealed class ExtendedStartup : ProcessStartup { public string Remark => "Not a startup field"; }

    [Fact]
    public void BusinessMappingsIgnoreNewNonProtocolPropertiesAndPreserveOmission()
    {
        Assert.Empty(WmiParameters.ServiceChange(new ExtendedOptions()));
        var values = WmiParameters.ProcessStartup(new ExtendedStartup { PriorityClass = PriorityClass.Normal, ShowWindow = 0 });
        Assert.Equal(new[] { "PriorityClass", "ShowWindow" }, values.Keys.OrderBy(x => x));
        Assert.Equal(32u, Assert.IsType<uint>(values["PriorityClass"]));
        Assert.Equal((ushort)0, Assert.IsType<ushort>(values["ShowWindow"]));
        var clear = WmiParameters.ServiceChange(new ChangeOptions { DisplayName = "", ServiceDependencies = [] });
        Assert.Equal("", clear["DisplayName"]); Assert.Empty(Assert.IsType<string[]>(clear["ServiceDependencies"]));
    }

    [Fact]
    public void ErrorTablesCannotBeModified()
    {
        foreach (var table in new[] { Service.ErrorCode, WmiProcess.ErrorCode })
        {
            Assert.Throws<NotSupportedException>(() => ((IDictionary<uint, string>)table).Clear());
            Assert.Equal("", table[0]);
        }
    }

    [Theory]
    [InlineData("", "root\\cimv2")]
    [InlineData("localhost", " ")]
    public void ScopeValidationPrecedesConnection(string host, string ns)
        => Assert.Throws<ArgumentException>(() => ManagementScopeExtensions.Create(host, ns, new ConnectionOptions()));

    [Fact]
    public void DefaultConnectionOptionsAndNullValidationAreConsistent()
    {
        var options = ManagementScopeExtensions.DefaultOptions();
        Assert.Equal(ImpersonationLevel.Impersonate, options.Impersonation);
        Assert.Equal(AuthenticationLevel.PacketPrivacy, options.Authentication); Assert.True(options.EnablePrivileges);
        Assert.Throws<ArgumentNullException>(() => ManagementScopeExtensions.Create("localhost", @"root\cimv2", null!));
    }

    [Theory]
    [InlineData("20260102030405.000000+000", 2026, 1, 2, 3, 4, 5)]
    [InlineData("20260102030405.000000+480", 2026, 1, 1, 19, 4, 5)]
    public void DmtfReturnsLocalKindAndPreservesInstant(string text, int y, int m, int d, int h, int min, int sec)
    {
        var value = WmiValue.Date(text);
        Assert.Equal(DateTimeKind.Local, value.Kind);
        Assert.Equal(new DateTime(y, m, d, h, min, sec, DateTimeKind.Utc), value.ToUniversalTime());
    }

    [Fact]
    public void DateTimeInputRetainsKindAndMissingDateIsUnspecifiedSentinel()
    {
        foreach (var kind in new[] { DateTimeKind.Utc, DateTimeKind.Local, DateTimeKind.Unspecified })
        {
            var value = new DateTime(2026, 1, 2, 3, 4, 5, kind);
            Assert.Equal(value, WmiValue.Date(value)); Assert.Equal(kind, WmiValue.Date(value).Kind);
        }
        Assert.Equal(DateTimeKind.Unspecified, WmiValue.Date(null).Kind);
    }

    [Fact]
    public void AllWrapperReferencePropertiesDeclareNullableAndPreserveNull()
    {
        var context = new NullabilityInfoContext();
        var types = typeof(Service).Assembly.GetTypes().Where(t => t.GetField("_proxy", BindingFlags.NonPublic | BindingFlags.Instance) != null).ToArray();
        Assert.Equal(8, types.Length);
        foreach (var type in types)
        {
            using var proxy = new ManagementClass();
            var properties = type.GetProperties().Where(p => p.GetMethod is { IsStatic: false } && !p.PropertyType.IsValueType).ToArray();
            foreach (var property in properties)
            {
                Assert.Equal(NullabilityState.Nullable, context.Create(property).ReadState);
                proxy.Properties.Add(property.Name, CimType.String, property.PropertyType.IsArray);
            }
            var constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();
            using var wrapper = (IDisposable)constructor.Invoke(constructor.GetParameters().Length == 1 ? [proxy] : new object?[] { proxy, null });
            foreach (var property in properties) Assert.Null(property.GetValue(wrapper));
        }
    }

    private sealed class ReflectionOptions
    {
        public string? Caption => null;
        public string Description => "Example";
        public PriorityClass Priority => PriorityClass.Normal;
        public string this[int index] => throw new InvalidOperationException("Indexer must not be read");
        public static string StaticRemark => "Not an instance field";
    }

    [Fact]
    public void GenericExtensionSkipsNullIndexersAndStaticPropertiesAndConvertsEnums()
    {
        using var target = new ManagementClass();
        target.Properties.Add("Caption", "Retained");
        target.Properties.Add("Description", CimType.String, false);
        target.Properties.Add("Priority", CimType.SInt32, false);
        target.SetPropertyValue(new ReflectionOptions());
        Assert.Equal("Retained", target["Caption"]); Assert.Equal("Example", target["Description"]); Assert.Equal(32, target["Priority"]);
        Assert.Throws<ManagementException>(() => target.SetPropertyValue(new { UnknownProperty = "Example" }));
    }
}
