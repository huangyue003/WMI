using IFactory.WMI.Win32_PowerPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IFactory.WMI.Tests
{
    public class PowerPlanTest
    {
        [Fact]
        public void GetPowerPlans()
        {
            var powerPlans = PowerPlan.Get();
            Assert.True(powerPlans.Any());
        }
        //[Fact]
        //public void SetMonitorTimeout()
        //{
        //    var powerPlans = PowerPlan.Get();
        //    var actived = powerPlans.Where(x => x.IsActive).FirstOrDefault();
        //    actived?.SetMonitorTimeout(1 * 60 * 1000);//1h
        //}
       
    }
}
