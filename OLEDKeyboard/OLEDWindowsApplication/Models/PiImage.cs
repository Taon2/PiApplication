using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Media.Imaging;

namespace OLEDWindowsApplication.Models;

[Serializable]
public class PiImage
{
    public byte[] image { get; set; }

    public PiImage(Bitmap bitmapImage)
    {
        var height = bitmapImage.Height;
        var width = bitmapImage.Width;
        if (height != 64 || width != 64)
        {
            throw new ArgumentException("Invalid dimensions");
        }

        int count = 0;
        image = new byte[8192];
        for (var col = 0; col < height; col++)
        {
            for (var row = width - 1; row >= 0; row--)
            {
                var pixel = bitmapImage.GetPixel(row, col);
                ushort value = RGB565.GetRgb565FromRGBA(pixel.B, pixel.G, pixel.R);
                image[count] = BitConverter.GetBytes(value)[1];
                count++;
                image[count] = BitConverter.GetBytes(value)[0];
                count++;
            }
        }
    }
}