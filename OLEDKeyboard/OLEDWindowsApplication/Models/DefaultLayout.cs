using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace OLEDWindowsApplication.Models;

public class DefaultLayout
{
    
    public static List<int> AddLayoutIcon { get; } = GetColorList(Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/addLayout.png")).GetBitmap());
    
    public static Dictionary<Position, Key> CreateDefaultLayout()
    {
        var newLayout = new Dictionary<Position, Key>
        {
            {
                Position.Zero,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/0.png"), "0000000000000000000000")
            },
            {
                Position.One,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/1.png"), "0000000000000000000000")
            },
            {
                Position.Two,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/2.png"), "0000000000000000000000")
            },
            {
                Position.Three,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/3.png"), "0000000000000000000000")
            },
            {
                Position.Four,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/4.png"), "0000000000000000000000")
            },
            {
                Position.Five,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/5.png"), "0000000000000000000000")
            },
            {
                Position.Six,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/6.png"), "0000000000000000000000")
            },
            {
                Position.Seven,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/7.png"), "0000000000000000000000")
            },
            {
                Position.Eight,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/8.png"), "0000000000000000000000")
            },
            {
                Position.Nine,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/9.png"), "0000000000000000000000")
            },
            {
                Position.Dot,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/dot.png"), "0000000000000000000000")
            },
            {
                Position.Enter,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/ent.png"), "0000000000000000000000")
            },
            {
                Position.Plus,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/+.png"), "0000000000000000000000")
            },
            {
                Position.Minus,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/-.png"), "0000000000000000000000")
            },
            {
                Position.Equals,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/=.png"), "0000000000000000000000")
            },
            {
                Position.Star,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/asterisk.png"), "0000000000000000000000")
            },
            {
                Position.Slash,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/slash.png"), "0000000000000000000000")
            },
            {
                Position.Back,
                Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/back.png"), "0000000000000000000000")
            }
        };
        return newLayout;
    }

    public static List<int> CreateDefaultIcon()
    {
        return GetColorList(Key.KeyFromFile(new Uri(@"pack://application:,,,/Resources/icon.png")).GetBitmap());
    }
    
    public static Dictionary<Position, double> defaultBrightnessLayout = new Dictionary<Position, double>()
    {
        { Position.Zero, 100.0 },
        { Position.One, 100.0 },
        { Position.Two, 100.0 },
        { Position.Three, 100.0 },
        { Position.Four, 100.0 },
        { Position.Five, 100.0 },
        { Position.Six, 100.0 },
        { Position.Seven, 100.0 },
        { Position.Eight, 100.0 },
        { Position.Nine, 100.0 },

        { Position.Dot, 100.0 },
        { Position.Enter, 100.0 },
        { Position.Plus, 100.0 },
        { Position.Minus, 100.0 },
        { Position.Star, 100.0 },
        { Position.Slash, 100.0 },
        { Position.Back, 100.0 },
        { Position.Equals, 100.0 }
    };

    public static Dictionary<Position, BitmapImage> defaultSources = new Dictionary<Position, BitmapImage>()
    {
        { Position.Zero, new BitmapImage(new Uri(@"pack://application:,,,/Resources/0.png")) },
        { Position.One, new BitmapImage(new Uri(@"pack://application:,,,/Resources/1.png")) },
        { Position.Two, new BitmapImage(new Uri(@"pack://application:,,,/Resources/2.png")) },
        { Position.Three, new BitmapImage(new Uri(@"pack://application:,,,/Resources/3.png")) },
        { Position.Four, new BitmapImage(new Uri(@"pack://application:,,,/Resources/4.png")) },
        { Position.Five, new BitmapImage(new Uri(@"pack://application:,,,/Resources/5.png")) },
        { Position.Six, new BitmapImage(new Uri(@"pack://application:,,,/Resources/6.png")) },
        { Position.Seven, new BitmapImage(new Uri(@"pack://application:,,,/Resources/7.png")) },
        { Position.Eight, new BitmapImage(new Uri(@"pack://application:,,,/Resources/8.png")) },
        { Position.Nine, new BitmapImage(new Uri(@"pack://application:,,,/Resources/9.png")) },

        { Position.Dot, new BitmapImage(new Uri(@"pack://application:,,,/Resources/dot.png")) },
        { Position.Enter, new BitmapImage(new Uri(@"pack://application:,,,/Resources/ent.png")) },
        { Position.Plus, new BitmapImage(new Uri(@"pack://application:,,,/Resources/+.png")) },
        { Position.Equals, new BitmapImage(new Uri(@"pack://application:,,,/Resources/=.png")) },
        { Position.Minus, new BitmapImage(new Uri(@"pack://application:,,,/Resources/-.png")) },
        { Position.Star, new BitmapImage(new Uri(@"pack://application:,,,/Resources/asterisk.png")) },
        { Position.Slash, new BitmapImage(new Uri(@"pack://application:,,,/Resources/slash.png")) },
        { Position.Back, new BitmapImage(new Uri(@"pack://application:,,,/Resources/back.png")) }
    };
    
    public static List<int> GetColorList(Bitmap? bitmap)
    {
        var colors = new List<int>();
        for (var row = 0; row < 64; row++)
        {
            for (var col = 0; col < 64; col++)
            {
                colors.Add(bitmap.GetPixel(row, col).ToArgb());
            }
        }
        return colors;
    }
    
    public static BitmapImage GetBitmapImage(List<int> ColorList)
    {
        Bitmap bitmap = new Bitmap(64, 64);
        for (var row = 0; row < 64; row++)
        {
            for (var col = 0; col < 64; col++)
            {
                Color color = Color.FromArgb(ColorList[row * 64 + col]);
                bitmap.SetPixel(row, col, color);   
            }
        }
        
        return ImageConverter.BitmapToPNG(bitmap);
    }
}