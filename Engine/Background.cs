using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Engine
{
    public class Background
    {
        List<List<UInt16>> Map_;
        List<Image> Images_;

        public Background(List<List<UInt16>> map, List<Image> images)
        {
            Map_ = map;
            Images_ = images;
        }

        public List<List<UInt16>> GrabSection(int x, int y)
        {
            List<List<UInt16>> tempSection = new List<List<ushort>>();
            for (int i = (x*200); i < (x*200) + 200; i++)
            {
                List<UInt16> tempRow = new List<ushort>();
                for (int j = (y*200); j < (y*200) + 200; j++)
                {
                    tempRow.Add(Map_[i][j]);
                }
                tempSection.Add(tempRow);
            }

            return tempSection;
        }

        public void CreateBackgroundImages(string filename, ProgressBar p)
        {
            int x = 0; int y = 0;

            double accValue = 0;
            
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    List<List<UInt16>> tempSection = GrabSection(i, j);

                    Image blank = Image.FromFile("Images/Blank.png");
                    Image background = (Image)ResizeImage(blank, 6400, 6400);
                    Graphics GraphicsObject = Graphics.FromImage(background);

                    y = 0;

                    foreach (List<UInt16> row in tempSection)
                    {
                        x = 0;
                        foreach (UInt16 cell in row)
                        {
                            RectangleF r = new RectangleF(0, 0, 32, 32);
                            GraphicsUnit units = GraphicsUnit.Pixel;

                            GraphicsObject.DrawImage(Images_[cell], x * 32, y * 32, r, units);
                            x++;
                        }
                        y++;
                    }

                    background.Save("Images/World" + i.ToString() + "-" + j.ToString() +  ".png", ImageFormat.Png);
                    background.Dispose();
                    blank.Dispose();
                    GraphicsObject.Dispose();

                    accValue += 0.26;

                    if (accValue > 1.0)
                    {
                        p.Value += 1;
                        accValue -= 1.0;
                    }

                }
            }
            

            
            Image blank2 = Image.FromFile("Images/Blank.png");
            Image background2 = (Image)ResizeImage(blank2, 3000, 3000);
            Graphics GraphicsObject2 = Graphics.FromImage(background2);

            y = 0;

            accValue = 0;

            foreach (List<UInt16> row in Map_)
            {
                x = 0;
                foreach (UInt16 cell in row)
                {
                    RectangleF r = new RectangleF(0, 0, 1, 1);
                    GraphicsUnit units = GraphicsUnit.Pixel;

                    GraphicsObject2.DrawImage(Images_[cell], x, y, r, units);
                    x++;

                    accValue += 0.00000388888;

                    if (accValue > 1.0)
                    {
                        if (p.Value < 100)
                            p.Value += 1;
                        accValue -= 1.0;
                    }
                }
                y++;
            }

            background2.Save("Images/" + filename + "FullLowScale.png", ImageFormat.Png);
            background2.Dispose();
            blank2.Dispose();
            GraphicsObject2.Dispose();

        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
