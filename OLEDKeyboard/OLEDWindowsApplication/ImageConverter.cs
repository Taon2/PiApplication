using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace OLEDWindowsApplication;

public class ImageConverter
{
    public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
    {
        using (MemoryStream memory = new MemoryStream())
        {
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
            memory.Position = 0;
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = memory;
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapimage.EndInit();

            return bitmapimage;
        }
    }
    
    public static BitmapImage BitmapToPNG(Bitmap bitmap)
    {
        using (MemoryStream memory = new MemoryStream())
        {
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            memory.Position = 0;
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = memory;
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapimage.EndInit();

            return bitmapimage;
        }
    }
    
    public static string ImageToBase64(Bitmap image)
    {
        System.IO.MemoryStream ms = new MemoryStream();
        image.Save(ms, ImageFormat.Jpeg);
        byte[] byteImage = ms.ToArray();
        var SigBase64= Convert.ToBase64String(byteImage); // Get Base64

        return SigBase64;
    }
}
