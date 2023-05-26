using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace OLEDWindowsApplication;

public class Configuration
{
    public AppSettings Settings;
    public Configuration()
    {
        var fileName = "AppSettings.json";
        try
        {
            var text = File.ReadAllText(fileName);
            try
            {
                AppSettings? temp = JsonSerializer.Deserialize<AppSettings>(text);
                Settings = temp ?? new AppSettings();
            }
            catch (JsonException e)
            {
                Settings = new AppSettings();
                MessageBox.Show(
                    "An error occured when loading configuration. Your configuration will be reset.",
                    "Configuration Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Save();
            }
            
        }
        catch (FileNotFoundException e)
        {
            Settings = new AppSettings();
            Save();
        }
    }

    public void Save()
    {
        var jsonString = JsonSerializer.Serialize(Settings);
        File.WriteAllText("AppSettings.json", jsonString);
    }
}