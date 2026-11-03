using IFactory.WMI.Win32_USBHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IFactory.WMI.Tests
{
    public class USBHubTest
    {
        [Fact]
        public void GetHub()
        {
            var scope = ManagementScopeExtensions.CreateDefault();
            var controllers = USBHub.Get(scope);
            Assert.True(controllers.Any());
        }

        [Fact]
        public void Y()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk"))
            {
                // 执行查询并获取结果集合
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // 获取并显示磁盘分区的详细信息
                    Console.WriteLine("DeviceID: " + queryObj["DeviceID"]);
                    Console.WriteLine("DriveType: " + queryObj["DriveType"]); // 例如：2 表示本地磁盘
                    Console.WriteLine("VolumeName: " + queryObj["VolumeName"]);
                    Console.WriteLine("FileSystem: " + queryObj["FileSystem"]); // 例如：NTFS 或 FAT32
                    Console.WriteLine("Size: " + queryObj["Size"]); // 以字节为单位
                    Console.WriteLine("FreeSpace: " + queryObj["FreeSpace"]); // 以字节为单位
                    Console.WriteLine("VolumeSerialNumber: " + queryObj["VolumeSerialNumber"]);
                    Console.WriteLine(); // 打印一个空行以便于阅读
                }
            }
        }
    }
}
