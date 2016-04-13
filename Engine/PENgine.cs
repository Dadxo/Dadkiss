/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 4/8/2016
 * Time: 2:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Engine
{
	/// <summary>
	/// Game Engine
	/// </summary>
	public class PEngine
	{
        Unit unit;
        WorldMap WorldMap_;
        ProgressBar Progress_;

		public PEngine()
        {
            unit = new Unit(1, 1, 1, 1, 1, 1, 1, 1, 1, "Images/test2.png");

            //WorldMap_ = new WorldMap(3000, 3000);
        }

        public void CreateWorld(ProgressBar p)
        {
            WorldMap_ = new WorldMap(3000, 3000, p);
        }

        public void Draw(BufferedGraphics buff, int count)
        {
            unit.Draw(20, 20, 32, 32, buff, count);
        }
	}
}