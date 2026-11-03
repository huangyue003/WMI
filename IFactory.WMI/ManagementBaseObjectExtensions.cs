using System.Management;

namespace IFactory.WMI
{
    public static class ManagementBaseObjectExtensions
    {
        /// <summary>Writes all readable, non-indexed, non-null public properties by name. Enum values use their numeric type. Unknown WMI properties throw; this is not a business-options mapper.</summary>
        public static void SetPropertyValue(this ManagementBaseObject management, object @object)
        {
            ArgumentNullException.ThrowIfNull(management);
            ArgumentNullException.ThrowIfNull(@object);
            var propertyInfos = @object.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetMethod?.IsPublic != true || propertyInfo.GetIndexParameters().Length != 0) continue;
                var propertyValue = propertyInfo.GetValue(@object);
                if (propertyValue != null)
                    management.SetPropertyValue(propertyInfo.Name, propertyValue is Enum ? Convert.ChangeType(propertyValue, Enum.GetUnderlyingType(propertyValue.GetType())) : propertyValue);
            }
        }
    }
}
