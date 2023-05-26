using System;
using System.Collections.Generic;
using System.Drawing;
using OLEDWindowsApplication.Models;

namespace OLEDWindowsApplication;

[Serializable]
public class Layout
{
    public string Name { get; set; } = "Default";
    public int Id { get; set; } = 0;
    public string Application { get; set; } = "";
    public Dictionary<Position, Key> Keys { get; set; } = DefaultLayout.CreateDefaultLayout();
    public List<int> SidebarIcon { get; set; } = DefaultLayout.CreateDefaultIcon();
    
    public double Brightness { get; set; } = 1.0;

    //private Dictionary<Position, double> _brightnessValues { get; set; }

    public Layout()
    {
    }

    public Layout(string name)
    {
        this.Name = name;
    }

    public Layout(string name, int id)
    {
        this.Id = id;
        this.Name = name;
    }

    public Layout(int id)
    {
        this.Id = id;
        this.Name = $"L{id}";
    }
    
    public string GetApplication()
    {
        return Application;
    }

    public string GetName()
    {
        return Name;
    }

    public void SetBrightness(double d)
    {
        Brightness = d;
        foreach (var key in Keys)
        {
            key.Value.Brightness = d;
        }
    }
    
    public void SetSource(Position key, Bitmap? b)
    {
        Keys[key].SetBitmap(b);
    }

    public double GetBrightness()
    {
        return Brightness;
    }

    public Bitmap GetSource(Position key)
    {
        return Keys[key].GetBitmap();
    }

    public Dictionary<Position, Key> GetKeys()
    {
        return Keys;
    }

    public bool SetName(string name)
    {
        this.Name = name;
        return true; // for now
    }

    public bool SetKeys(Dictionary<Position, Key> keysDictionary)
    {
        this.Keys = keysDictionary;
        this.Application = "";
        return true;
    }

    public bool SetBrightness(Dictionary<Position, double> brightnessDictionary)
    {
        throw new NotImplementedException();
        return true;
    }

    public void SetApplication(string empty)
    {
        this.Application = empty;
    }
}