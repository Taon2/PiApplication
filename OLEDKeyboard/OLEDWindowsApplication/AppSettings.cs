using System;
using System.Collections.Generic;

namespace OLEDWindowsApplication;

[Serializable]
public class AppSettings
{
    public int Version { get; set; } = 0;
    public int CurrentLayout { get; set; } = 0;
    public int SelectedLayout { get; set; } = 0;
    public List<Layout> Layouts { get; set; } = new();
}