using System.Management;
namespace IFactory.WMI;
internal static class WmiValue
{
    internal static string Escape(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.Replace("\\", "\\\\").Replace("'", "\\'");
    }
    internal static DateTime Date(object? value) => value is string text
        ? ManagementDateTimeConverter.ToDateTime(text)
        : value is DateTime date ? date : DateTime.MinValue;
}
