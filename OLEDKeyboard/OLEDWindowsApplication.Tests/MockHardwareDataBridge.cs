using System.IO.Ports;
using OLEDWindowsApplication;

namespace OLEBWindowsApplication.Tests;

public class MockHardwareDataBridge
{

    public MockHardwareDataBridge()
    {
    }

    public bool WriteKey(Key k)
    {
        return true;
    }
}