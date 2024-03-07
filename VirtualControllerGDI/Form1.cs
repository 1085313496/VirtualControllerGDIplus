using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualControllerGDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Disposed += Form1_Disposed;
            GenETank();
        }

        void Form1_Disposed(object sender, EventArgs e)
        {
            blTK = false;
            if (thTk != null)
                thTk.Abort();
        }

        private void virtualController1_FG_Lift()
        {

        }

        private int speed = 2;
        private void virtualController1_FG_Pressed()
        {
            Point pt = pbTank.Location;

            double rad = ((double)VCL.R_Cur) / ((double)VCL.LCC.Radius);
            rad = VCL.SpeedLevel;
            if (rad < 0.4)
                speed = 1;
            else if (rad < 0.6)
                speed = 1;
            else if (rad < 0.8)
                speed = 2;
            else
                speed = 2;

            Point ptLU = new Point(0, 0);
            Point ptRD = new Point(this.DisplayRectangle.Width - pbTank.Width - 2, this.DisplayRectangle.Height - pbTank.Height - 2);

            int tnX = pt.X;
            int tnY = pt.Y;

            switch (VCL.CurrentDirection)
            {
                case Direction4.STOP: break;
                case Direction4.N:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.redup;
                    tnY = pt.Y - speed;
                    break;
                case Direction4.E:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.redright;
                    tnX = pt.X + speed;
                    break;
                case Direction4.S:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.reddown;
                    tnY = pt.Y + speed;
                    break;
                case Direction4.W:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.redleft;
                    tnX = pt.X - speed;
                    break;
                case Direction4.NE:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.redNE;
                    int nX = Math.Abs((int)((double)speed * Math.Cos(-1 * Math.PI / 4)));
                    int nY = Math.Abs((int)((double)speed * Math.Sin(-1 * Math.PI / 4)));

                    tnX = pt.X + nX;
                    tnY = pt.Y - nY;
                    break;
                case Direction4.SE:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.redSE;
                    int nX6 = Math.Abs((int)((double)speed * Math.Cos(1 * Math.PI / 4)));
                    int nY6 = Math.Abs((int)((double)speed * Math.Sin(1 * Math.PI / 4)));

                    tnX = pt.X + nX6;
                    tnY = pt.Y + nY6;

                    break;
                case Direction4.SW:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.redSW;
                    int nX7 = Math.Abs((int)((double)speed * Math.Cos(3 * Math.PI / 4)));
                    int nY7 = Math.Abs((int)((double)speed * Math.Sin(3 * Math.PI / 4)));

                    tnX = pt.X - nX7;
                    tnY = pt.Y + nY7;
                    break;
                case Direction4.NW:
                    pbTank.Image = global::VirtualControllerGDI.Properties.Resources.redNW;
                    int nX8 = Math.Abs((int)((double)speed * Math.Cos(-3 * Math.PI / 4)));
                    int nY8 = Math.Abs((int)((double)speed * Math.Sin(-3 * Math.PI / 4)));

                    tnX = pt.X - nX8;
                    tnY = pt.Y - nY8;
                    break;
            }

            if (tnX <= ptLU.X)
                tnX = ptLU.X;
            if (tnX >= ptRD.X)
                tnX = ptRD.X;

            if (tnY <= ptLU.Y)
                tnY = ptLU.Y;
            if (tnY >= ptRD.Y)
                tnY = ptRD.Y;

            pt.X = tnX;
            pt.Y = tnY;
            pbTank.Location = pt;
        }

        private bool blTK = false;
        private Thread thTk;
        private List<PictureBox> lsPB = new List<PictureBox>();
        private void GenETank()
        {
            PictureBox pb = new PictureBox();
            pb.Location = new Point(350, 120);
            pb.Size = new Size(65, 71);
            pb.BackColor = Color.Transparent;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Image = global::VirtualControllerGDI.Properties.Resources.greenup;
            
            this.Controls.Add(pb);
            pb.Show();
            lsPB.Add(pb);

            blTK = true;
            thTk = new Thread(new ThreadStart(GenETankthr));
            thTk.IsBackground = true;
            thTk.Start();

        }
        private void GenETankthr()
        {
            int drct = 0;
            int dr = 2;
            while (blTK)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    drct++;
                    foreach (PictureBox pb in lsPB)
                    {
                        Random r = new Random(new Random(DateTime.Now.Millisecond).Next());
                        if (drct %12 == 0)
                            dr = r.Next(1,8);
                        int spd = r.Next(8, 12);
                        Point pt = pb.Location;
                        switch (dr)
                        {
                            case 1: pb.Location = new Point(pt.X, pt.Y - spd); pb.Image = global::VirtualControllerGDI.Properties.Resources.greenup; break;
                            case 2: pb.Location = new Point(pt.X + spd, pt.Y); pb.Image = global::VirtualControllerGDI.Properties.Resources.greenright; break;
                            case 3: pb.Location = new Point(pt.X, pt.Y + spd); pb.Image = global::VirtualControllerGDI.Properties.Resources.greendown; break;
                            case 4: pb.Location = new Point(pt.X - spd, pt.Y); pb.Image = global::VirtualControllerGDI.Properties.Resources.greenleft; break;
                            case 5: break;
                            case 6: break;
                            case 7: break;
                            case 8: break;
                        }

                        pb.BringToFront();
                    }
                });

                Thread.Sleep(50);
            }

        }



    }
}
