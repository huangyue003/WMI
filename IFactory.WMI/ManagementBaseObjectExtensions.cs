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
            var propertyInfos = @object.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(@object);
                if (propertyValue != null)
                    management.SetPropertyValue(propertyInfo.Name, propertyValue);
            }
        }
    }
}
