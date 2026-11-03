using IFactory.WMI.Win32_LogicalDisk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.WMI.Win32_PowerPlan
{
    public class PowerPlan
    {
        private PowerPlan(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private ManagementObject _proxy;
        public static IEnumerable<PowerPlan> Get()
        {
            string host = "localhost";
            string wmiNamespace = "root\\cimv2\\power";
            ConnectionOptions options = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                Authentication = AuthenticationLevel.PacketPrivacy,
                EnablePrivileges = true,
            };
            var scope = ManagementScopeExtensions.Create(host,wmiNamespace,options);
            return Get(scope);  
        }
        public static IEnumerable<PowerPlan> Get(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_PowerPlan");
            var collection = new ManagementObjectSearcher(scope, query).Get();
            foreach (ManagementObject item in collection)
                yield return new PowerPlan(item);
        }

        public string InstanceID => (string)_proxy[nameof(InstanceID)];
        public string ElementName => (string)_proxy[nameof(ElementName)];
        public string Description => (string)_proxy[nameof(Description)];
        public bool IsActive => (bool)_proxy[nameof(IsActive)];

        //public void SetMonitorTimeout(int secondTimeout)
        //{
        //   var powerPlanGuid=InstanceID.Split('\\')[1];
        //    ManagementObjectSearcher settingSearcher = new ManagementObjectSearcher("root\\CIMV2\\power", $"SELECT * FROM Win32_PowerSettingDataIndex WHERE InstanceID LIKE '%{powerPlanGuid}%'");
        //    ManagementObjectCollection settingCollection = settingSearcher.Get();
        //    List<string> elementNames = new List<string>();
        //    List<string> instanceID = new List<string>();
        //    List<uint> settingIndexValue = new List<uint>();
        //    foreach (ManagementObject setting in settingCollection)
        //    {
        //        elementNames.Add((string)setting["ElementName"]);
        //        instanceID.Add((string)setting["InstanceID"]);
        //        settingIndexValue.Add((uint)setting["SettingIndexValue"]);
        //        // 获取当前设置
        //        uint currentValue = Convert.ToUInt32(setting["SettingIndexValue"]);

        //        // 设置新的屏幕关闭时间（以秒为单位）
        //        uint newValue = 3600; // 10分钟

        //        // 更新设置
        //        setting["SettingIndexValue"] = newValue;
        //        setting.Put();

        //    }
        //}
    }
}
