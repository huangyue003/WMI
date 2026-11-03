using System.Management;

namespace IFactory.WMI.Win32_PowerPlan
{
    public class PowerPlan : IDisposable
    {
        private PowerPlan(ManagementObject proxy)
        {
            _proxy = proxy;
        }
        private readonly ManagementObject _proxy;
        public void Dispose() => _proxy.Dispose();
        public static IEnumerable<PowerPlan> Get()
        {
            var scope = ManagementScopeExtensions.CreateDefault(@"root\cimv2\power");
            return Get(scope);
        }
        public static IEnumerable<PowerPlan> Get(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_PowerPlan");
            using var searcher = new ManagementObjectSearcher(scope, query);
            using var collection = searcher.Get();
            foreach (ManagementObject item in collection)
                yield return new PowerPlan(item);
        }

        public string? InstanceID => (string?)_proxy[nameof(InstanceID)];
        public string? ElementName => (string?)_proxy[nameof(ElementName)];
        public string? Description => (string?)_proxy[nameof(Description)];
        public bool IsActive => (bool)_proxy[nameof(IsActive)];

    }
}
