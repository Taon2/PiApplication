using System;

namespace OLEDWindowsApplication.Models;

public struct ColorRGB565
{
    public byte byte1;
    public byte byte2;
    public byte byte3;
}

public class RGB565
{
    private ColorRGB565 colorHolder;

    public RGB565(byte r, byte g, byte b)
    {
        colorHolder = new ColorRGB565
        {
            byte1 = r,
            byte2 = g,
            byte3 = b
        };
    }

    public ColorRGB565 GetColorHolder()
    {
        return colorHolder;
    }

    public static ushort GetRgb565FromRGBA(byte red, byte green, byte blue)
    {
        var rtmp = ((red & 0b11111000) << 8);
        var gtmp = ((green & 0b11111100) << 3);
        var btmp = (blue >> 3);
        
        var rgb = rtmp | gtmp | btmp;
        return Convert.ToUInt16(rgb);
    }
}