using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Engine
{
    /// <summary>
	/// Mostly background and static sprites. 
	/// </summary>
    public class Sprite
    {
        public Image Image_;    

        public Sprite(String filepath)
        {
            Image_ = Image.FromFile(filepath);
        }

        public void Draw(int x, int y, int w, int l, int off, BufferedGraphics buff)
        {
            RectangleF r = new RectangleF(off, 0, w, l);
            GraphicsUnit units = GraphicsUnit.Pixel;

            buff.Graphics.DrawImage(Image_, 20, 20, r, units);

        }
    }
}
