using System.Windows.Controls;

namespace OLEDWindowsApplication;

public class ButtonWithId : Button
{
    public readonly int id = 0;
    public ButtonWithId() : base()
    {
        this.id = -1;
    }
    public ButtonWithId(int id) : base()
    {
        this.id = id;
    }
}