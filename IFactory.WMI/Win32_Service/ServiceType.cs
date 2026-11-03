namespace IFactory.WMI.Win32_Service;
[Flags]
public enum ServiceType : uint
{
    KernelDriver = 0x01,
    FileSystemDriver = 0x02,
    Adapter = 0x04,
    RecognizerDriver = 0x08,
    OwnProcess = 0x10,
    ShareProcess = 0x20,
    InteractiveProcess = 0x100
}
