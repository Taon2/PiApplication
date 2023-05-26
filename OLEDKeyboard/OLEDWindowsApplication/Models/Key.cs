using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Resources;
using Color = System.Drawing.Color;

[Serializable]
public class Key
{
    public double Brightness { get; set; } = 1.0;
    public List<int> ColorList { get; set; } = new(64 * 64);

    public String CSString = "";

    public Bitmap GetBitmap(double? customBrightness = null)
    {
        var brightness = Brightness;
        if (customBrightness.HasValue)
        {
            brightness = customBrightness.Value;
        }
        var bitmap = new Bitmap(64, 64);
        for (var row = 0; row < 64; row++)
        {
            for (var col = 0; col < 64; col++)
            {
                var color = ColorList[row * 64 + col];
                var r = (color >> 16) & 0xff;
                var g = (color >> 8) & 0xff;
                var b = (color) & 0xff;

                r = (int)(r * brightness) & 0xff;
                g = (int)(g * brightness) & 0xff;
                b = (int)(b * brightness) & 0xff;

                bitmap.SetPixel(row, col, Color.FromArgb(0xff, r, g, b));
            }
        }

        return bitmap;
    }
    
    public static Key KeyFromFile(Uri uri)
    {
        StreamResourceInfo sri = Application.GetResourceStream(uri);
        Bitmap bitmapSource = (Bitmap)Image.FromStream(sri.Stream);
        Bitmap bitmap = new Bitmap(bitmapSource, 64, 64);

        Key key = new Key();
        key.SetBitmap(bitmap);
        return key;
    }

    public static Key KeyFromFile(Uri uri, String csString)
    {
        StreamResourceInfo sri = Application.GetResourceStream(uri);
        Bitmap bitmapSource = (Bitmap)Image.FromStream(sri.Stream);
        Bitmap bitmap = new Bitmap(bitmapSource, 64, 64);

        Key key = new Key();
        key.SetBitmap(bitmap);
        key.SetChipSelect(csString);
        return key;
    }

    public void SetBitmap(Bitmap? bitmapImage)
    {
        var colors = new List<int>();
        for (var row = 0; row < 64; row++)
        {
            for (var col = 0; col < 64; col++)
            {
                colors.Add(bitmapImage.GetPixel(row, col).ToArgb());
            }
        }

        ColorList = colors;
    }

    public void SetChipSelect(String csString)
    {
        CSString = csString;
    }
}