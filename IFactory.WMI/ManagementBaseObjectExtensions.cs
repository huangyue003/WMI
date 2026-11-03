using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI
{
    public static class ManagementBaseObjectExtensions
    {
        public static void SetPropertyValue(this ManagementBaseObject management, object @object)
        {
            ArgumentNullException.ThrowIfNull(management);
            ArgumentNullException.ThrowIfNull(@object);
            var propertyInfos = @object.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.CanRead || propertyInfo.GetIndexParameters().Length != 0) continue;
                var propertyValue = propertyInfo.GetValue(@object);
                if (propertyValue != null)
                    management.SetPropertyValue(propertyInfo.Name, propertyValue is Enum ? Convert.ChangeType(propertyValue, Enum.GetUnderlyingType(propertyValue.GetType())) : propertyValue);
            }
        }
    }
}
