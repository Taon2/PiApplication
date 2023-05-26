using System;
using System.Collections.Generic;
using System.Linq;

namespace OLEDWindowsApplication;

public class LayoutManager
{
    public List<Layout> layouts;
    private readonly Configuration configuration;
    private const string DefaultName = "Default";
    public int Count => layouts.Count;
    public LayoutManager(Configuration configuration)
    {
        this.configuration = configuration;
        layouts = this.configuration.Settings.Layouts;
        // Setup Default Stuff Here
        if (layouts.Count == 0)
        { 
            layouts.Add(new Layout(DefaultName, 0));
            this.configuration.Settings.CurrentLayout = 0;
            this.configuration.Settings.SelectedLayout = 0;
        }
    }

    public Layout GetLayout(int id)
    {
        return layouts.First(x => x.Id == id);
    }
    
    public Layout GetLayout(string name)
    {
        try
        {
            return layouts.First(x => x.Name == name);
        }
        catch (InvalidOperationException e)
        {
            return null;
        }
    }

    public List<Layout> GetAllLayouts()
    {
        return layouts;
    }
    
    public Layout AddLayout()
    {
        var maxId = layouts.Max(x => x.Id) + 1;
        var newLayout = new Layout(maxId);
        layouts.Add(newLayout);
        configuration.Save();
        return newLayout;
    }
    
    public void AddLayout(Layout newLayout)
    {
        layouts.Add(newLayout);
        configuration.Save();
    }

    public void RemoveLayout(int selectedLayout)
    {
        layouts.Remove(layouts.First(x => x.Id == selectedLayout));
        // todo: error handling
        configuration.Save();
    }
}