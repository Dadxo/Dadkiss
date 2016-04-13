using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Engine
{
    public class WorldMap
    {
        List<List<double>> HeightMap_;
        List<List<UInt16>> WorldMap_;
        List<Image> Images_;
        Background Background_;

        public WorldMap(int width, int height, ProgressBar pg)
        {
            Images_ = new List<Image>();
            WorldMap_ = new List<List<ushort>>();

            Images_.Add(Image.FromFile("Images/Water.png"));
            Images_.Add(Image.FromFile("Images/WaterSand.png"));
            Images_.Add(Image.FromFile("Images/Sand.png"));
            Images_.Add(Image.FromFile("Images/Grass.png"));
            Images_.Add(Image.FromFile("Images/Mountain.png"));

            Perlin p = new Perlin(width, height, 8);
            HeightMap_ = p.GetPerlinNoise();

            int curHeight = 0;

            for (int i = 0; i < width; i++)
            {
                List<UInt16> tempVec = new List<ushort>();
                for (int j = 0; j < height; j++)
                {
                    tempVec.Add(0);
                }
                WorldMap_.Add(tempVec);
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    curHeight = (int)(1000 * HeightMap_[x][y]);

                    if (curHeight >= 850)
                    {
                        WorldMap_[x][y] = 4;
                    }
                    else if (curHeight >= 650 && curHeight < 850)
                    {
                        WorldMap_[x][y] = 3;
                    }
                    else if (curHeight >= 635 && curHeight < 650)
                    {
                        WorldMap_[x][y] = 2;
                    }
                    else if (curHeight >= 600 && curHeight < 635)
                    {
                        WorldMap_[x][y] = 1;
                    }
                    else
                    {
                        WorldMap_[x][y] = 0;
                    }
                }
            }

            pg.Value = 5;


            Background_ = new Background(WorldMap_, Images_);
            Background_.CreateBackgroundImages("test1", pg);


        }
    }
}
