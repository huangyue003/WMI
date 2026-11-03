using System.Management;
using IFactory.WMI.Win32_Service;
using ServiceCreateOptions = IFactory.WMI.Win32_Service.CreateOptions;
using IFactory.WMI.Win32_Process;
namespace IFactory.WMI;

internal static class WmiParameters
{
    internal static void Apply(ManagementBaseObject target, IReadOnlyDictionary<string, object> parameters)
    {
        foreach (var (name, value) in parameters) target.SetPropertyValue(name, value);
    }

    internal static string StartModeName(StartMode mode)
    {
        if (!Enum.IsDefined(mode)) throw new ArgumentOutOfRangeException(nameof(mode));
        return mode.ToString();
    }

    private static uint? TypeValue(ServiceType? value)
    {
        if (value is null) return null;
        const ServiceType known = ServiceType.KernelDriver | ServiceType.FileSystemDriver | ServiceType.Adapter |
            ServiceType.RecognizerDriver | ServiceType.OwnProcess | ServiceType.ShareProcess | ServiceType.InteractiveProcess;
        if (value == 0 || (value.Value & ~known) != 0) throw new ArgumentOutOfRangeException(nameof(value));
        // The provider validates combinations and mode/service-type compatibility.
        return (uint)value.Value;
    }
    private static uint? ErrorValue(ErrorControl? value)
    {
        if (value is null) return null;
        if (!Enum.IsDefined(value.Value)) throw new ArgumentOutOfRangeException(nameof(value));
        return (uint)value.Value;
    }
    private static void Required(string? value, string member)
    {
        if (value is null) throw new ArgumentNullException("options", $"{member} is required.");
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"{member} cannot be blank.", "options");
    }
    private static void Add(Dictionary<string, object> result, string name, object? value)
    {
        if (value is not null) result.Add(name, value);
    }

    internal static Dictionary<string, object> ServiceCreate(ServiceCreateOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        Required(options.Name, nameof(options.Name));
        Required(options.PathName, nameof(options.PathName));
        Required(options.DisplayName, nameof(options.DisplayName));
        var result = new Dictionary<string, object>();
        Add(result, "Name", options.Name);
        Add(result, "DisplayName", options.DisplayName);
        Add(result, "PathName", options.PathName);
        Add(result, "DesktopInteract", options.DesktopInteract);
        Add(result, "StartName", options.StartName);
        Add(result, "StartPassword", options.StartPassword);
        Add(result, "LoadOrderGroup", options.LoadOrderGroup);
        Add(result, "LoadOrderGroupDependencies", options.LoadOrderGroupDependencies);
        Add(result, "ServiceDependencies", options.ServiceDependencies);
        Add(result, "ServiceType", TypeValue(options.ServiceType ?? ServiceType.OwnProcess));
        Add(result, "ErrorControl", ErrorValue(options.ErrorControl ?? ErrorControl.Normal));
        Add(result, "StartMode", StartModeName(options.StartMode ?? StartMode.Automatic));
        return result;
    }

    internal static Dictionary<string, object> ServiceChange(ChangeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var result = new Dictionary<string, object>();
        Add(result, "DisplayName", options.DisplayName);
        Add(result, "PathName", options.PathName);
        Add(result, "DesktopInteract", options.DesktopInteract);
        Add(result, "StartName", options.StartName);
        Add(result, "StartPassword", options.StartPassword);
        Add(result, "LoadOrderGroup", options.LoadOrderGroup);
        Add(result, "LoadOrderGroupDependencies", options.LoadOrderGroupDependencies);
        Add(result, "ServiceDependencies", options.ServiceDependencies);
        Add(result, "ServiceType", TypeValue(options.ServiceType));
        Add(result, "ErrorControl", ErrorValue(options.ErrorControl));
        Add(result, "StartMode", options.StartMode is { } mode ? StartModeName(mode) : null);
        return result;
    }

    internal static Dictionary<string, object> ProcessStartup(ProcessStartup options)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (options.PriorityClass is { } priority && !Enum.IsDefined(priority))
            throw new ArgumentOutOfRangeException(nameof(options), "Unknown priority class.");
        var result = new Dictionary<string, object>();
        Add(result, "CreateFlags", options.CreateFlags);
        Add(result, "EnvironmentVariables", options.EnvironmentVariables);
        Add(result, "ErrorMode", options.ErrorMode);
        Add(result, "FillAttribute", options.FillAttribute);
        Add(result, "ShowWindow", options.ShowWindow);
        Add(result, "Title", options.Title);
        Add(result, "WinstationDesktop", options.WinstationDesktop);
        Add(result, "X", options.X);
        Add(result, "XCountChars", options.XCountChars);
        Add(result, "XSize", options.XSize);
        Add(result, "Y", options.Y);
        Add(result, "YCountChars", options.YCountChars);
        Add(result, "YSize", options.YSize);
        Add(result, "PriorityClass", options.PriorityClass is { } value ? (uint)value : null);
        return result;
    }
}
