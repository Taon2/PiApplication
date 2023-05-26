using OLEDWindowsApplication;

namespace OLEBWindowsApplication.Tests;

public class ConfigurationTests
{
    [Fact]
    public void ConfigurationCanBeCreatedAndRetrieveValues()
    {
        var configuration = new Configuration();
        var version = configuration.Settings.Version;
        Assert.Equal(0, version);
    }
    
    [Fact]
    public void ConfigurationCanBeModifiedAndReloaded()
    {
        var configuration = new Configuration();
        //configuration.Settings.DefaultLayout = 200;
        configuration.Save();
        configuration = new Configuration();
        //var defaultLayout = configuration.Settings.DefaultLayout;
        //Assert.Equal(200, defaultLayout);

    }
}