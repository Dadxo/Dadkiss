/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 4/8/2016
 * Time: 2:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Engine;
using System.Timers;

namespace Pirate_Emblem
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
        #region Local Variables
        PEngine GameEngine_;

        static System.Timers.Timer Timer_;
        static System.Timers.Timer DrawTimer_;
        private int Count_;

        BufferedGraphicsContext CurrentContext_;
        BufferedGraphics Buffer_;

        ProgressBar p;
        #endregion

        public MainForm()
		{

			InitializeComponent();

            #region Window Settings
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            #endregion

            Button b = new Button();
            b.Text = "Create World";
            b.Width = 200;
            b.Location = new Point(100, 100);
            b.Click += button_Click;
            this.Controls.Add(b);

            p = new ProgressBar();
            p.Value = 0;
            p.Width = 200;
            p.Location = new Point(100, 150);
            this.Controls.Add(p);

            Label l = new Label();
            l.Text = "Creating a world will take a while haha";
            l.BackColor = Color.White;
            l.Location = new Point(100, 200);
            l.Width = 200;
            this.Controls.Add(l);
            

            GameEngine_ = new PEngine();

            Start();

            // Gets a reference to the current BufferedGraphicsContext
            CurrentContext_ = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with Form1, and with 
            // dimensions the same size as the drawing surface of Form1.
            Buffer_ = CurrentContext_.Allocate(this.CreateGraphics(),
               this.DisplayRectangle);


        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Enabled = false;
            GameEngine_.CreateWorld(p);

        }
        public void Start()
        {
            Count_ = 0;

            Timer_ = new System.Timers.Timer(350);
            DrawTimer_ = new System.Timers.Timer(33.3);

            Timer_.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Timer_.Enabled = true; // Enable it

            DrawTimer_.Elapsed += new ElapsedEventHandler(Draw_Timer);
            DrawTimer_.Enabled = true;

        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Count_++;
        }

        public void Draw_Timer(object sender, ElapsedEventArgs e)
        {

            try
            {
                GameEngine_.Draw(Buffer_, Count_);

                Buffer_.Render();
            }
            catch (Exception f)
            {

            }
          
        }

    }
}
