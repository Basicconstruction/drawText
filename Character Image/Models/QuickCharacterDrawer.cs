using System;
using System.Drawing;

namespace Character_Image.Models;

public class QuickCharacterDrawer: ICharacterDrawer
{
    public Image Draw(string path, GenerateConfig config)
    {
        throw new NotImplementedException();
    }

    public Image DrawOne(string path, GenerateConfig config)
    {
        using var image = Image.FromFile(path);
        using var bit = new Bitmap(image);
        using var res = new Bitmap(image.Width, image.Height);
        for (var i = 0; i < image.Width; i++)
        {
            for (var j = 0; j < image.Height; j++)
            {
                res.SetPixel(i,j,Color.Black);
            }
        }
        using var graphics = Graphics.FromImage(res);
        var lx = config.RectWidth;
        var ly = config.RectHeight;
        var rx = bit.Width / lx;
        var ry = bit.Height / ly;

        var font = new Font(new FontFamily("微软雅黑"), config.FontSize, FontStyle.Regular);
        var brush = new SolidBrush(Color.Black); // 假设文本颜色为黑色

        for (var i = 0; i < rx; i++)
        {
            for (var j = 0; j < ry; j++)
            {
                Color c;
                if (config.Gray)
                {
                    c = AverageColor.GetAverageGray(bit, new Rectangle((int)(i * lx), (int)(j * ly), (int)lx, (int)ly));
                }
                else
                {
                    c = AverageColor.GetAverageColor(bit, new Rectangle((int)(i * lx), (int)(j * ly), (int)lx, (int)ly));
                }
                brush.Color = c;
                graphics.DrawString(config.DrawText, font, brush, i * lx, j * ly);
            }
        }

        return new Bitmap(res);
    }
}