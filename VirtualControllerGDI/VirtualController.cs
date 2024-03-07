using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualControllerGDI
{
    public partial class VirtualController : UserControl
    {


        private BufferedGraphics graphBuffer;

        /// <summary>
        /// 外圈
        /// </summary>
        public ControllerCircle LCC;
        /// <summary>
        /// 内圈
        /// </summary>
        public ControllerCircle SCC;
        /// <summary>
        /// 内圈最大显示区域
        /// </summary>
        private int ContainerWidth = 10;

        /// <summary>
        /// 外圈上各个方向的点坐标
        /// </summary>
        private Dictionary<string, Point> dicDirection = new Dictionary<string, Point>();

        /// <summary>
        /// 当前方向
        /// </summary>
        public  Direction4 CurrentDirection = 0;
        /// <summary>
        /// 是否按下小圆
        /// </summary>
        private bool pressed = false;

        /// <summary>
        /// 当前点击位置
        /// </summary>
        private Point pt_Cur = new Point();
        /// <summary>
        /// 当前点击位置距离外圈圆心距离
        /// </summary>
        public int R_Cur = 0;

        public delegate void PressedDelegate();
        public event PressedDelegate FG_Pressed;
        public event PressedDelegate FG_Lift;

        public VirtualController()
        {
            InitializeComponent();

            //EnableDoubleBuffering();

            graphBuffer = (new BufferedGraphicsContext()).Allocate(this.CreateGraphics(), this.DisplayRectangle);
        }

        private void VirtualController_Load(object sender, EventArgs e)
        {
            Inita();
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        private void Inita()
        {
            if (this.Width > this.Height)
                this.Height = this.Width;
            else
                this.Width = this.Height;

            ContainerWidth = this.Width - 8;
            int R1 = ContainerWidth / 6;


            LCC = new ControllerCircle();
            LCC.Radius = R1 * 2;
            LCC.CenterX = this.Width / 2;
            LCC.CenterY = this.Height / 2;



            SCC = new ControllerCircle();
            SCC.Radius = R1;
            SCC.CenterX = this.Width / 2;
            SCC.CenterY = this.Height / 2;

            int x1_M = (int)(Math.Cos(-1 * Math.PI / 4) * (double)LCC.Radius) + LCC.CenterX;
            int y1_M = (int)(Math.Sin(-1 * Math.PI / 4) * (double)LCC.Radius) + LCC.CenterY;
            int x1_E = (int)(Math.Cos(-1 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y1_E = (int)(Math.Sin(-1 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;
            int x1_N = (int)(Math.Cos(-3 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y1_N = (int)(Math.Sin(-3 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;

            int x2_M = (int)(Math.Cos(-6 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y2_M = (int)(Math.Sin(-6 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;
            int x2_N = (int)(Math.Cos(-5 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y2_N = (int)(Math.Sin(-5 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;
            int x2_W = (int)(Math.Cos(-7 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y2_W = (int)(Math.Sin(-7 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;

            int x3_M = (int)(Math.Cos(6 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y3_M = (int)(Math.Sin(6 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;
            int x3_W = (int)(Math.Cos(7 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y3_W = (int)(Math.Sin(7 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;
            int x3_S = (int)(Math.Cos(5 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y3_S = (int)(Math.Sin(5 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;

            int x4_M = (int)(Math.Cos(2 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y4_M = (int)(Math.Sin(2 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;
            int x4_E = (int)(Math.Cos(1 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y4_E = (int)(Math.Sin(1 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;
            int x4_S = (int)(Math.Cos(3 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterX;
            int y4_S = (int)(Math.Sin(3 * Math.PI / 8) * (double)LCC.Radius) + LCC.CenterY;


            dicDirection.Clear();
            dicDirection.Add("N", new Point(LCC.CenterX, LCC.CenterY - LCC.Radius));
            dicDirection.Add("E", new Point(LCC.CenterX + LCC.Radius, LCC.CenterY));
            dicDirection.Add("S", new Point(LCC.CenterX, LCC.CenterY + LCC.Radius));
            dicDirection.Add("W", new Point(LCC.CenterX - LCC.Radius, LCC.CenterY));

            dicDirection.Add("NE", new Point(x1_M, y1_M));
            dicDirection.Add("NE_E", new Point(x1_E, y1_E));
            dicDirection.Add("NE_N", new Point(x1_N, y1_N));

            dicDirection.Add("SE", new Point(x4_M, y4_M));
            dicDirection.Add("SE_E", new Point(x4_E, y4_E));
            dicDirection.Add("SE_S", new Point(x4_S, y4_S));

            dicDirection.Add("SW", new Point(x3_M, y3_M));
            dicDirection.Add("SW_S", new Point(x3_S, y3_S));
            dicDirection.Add("SW_W", new Point(x3_W, y3_W));

            dicDirection.Add("NW", new Point(x2_M, y2_M));
            dicDirection.Add("NW_W", new Point(x2_W, y2_W));
            dicDirection.Add("NW_N", new Point(x2_N, y2_N));
        }

        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }


        /// <summary>
        /// 绘制
        /// </summary>
        private void PaintChessboard()
        {
            Graphics g = null;

            try
            {
                Bitmap b = new Bitmap(this.DisplayRectangle.Width, this.DisplayRectangle.Height);
                g = Graphics.FromImage((System.Drawing.Image)b);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


                Pen p = new Pen(Color.Purple, 1);
                //int StartPointX = this.Width - 5 - ContainerWidth;
                //int StartPointY = this.Height - 5 - ContainerWidth;
                //int BoardBorderLength = this.Width - 5;

                //g.DrawLine(p, 0, this.Height / 2, this.Width, this.Height / 2);
                //g.DrawLine(p, this.Width / 2, 0, this.Width / 2, this.Height);


                //g.DrawLine(p, dicDirection["N"], dicDirection["S"]);
                //g.DrawLine(p, dicDirection["W"], dicDirection["E"]);

                //p = new Pen(Color.RoyalBlue, 1);
                //g.DrawLine(p, dicDirection["NE"], dicDirection["SW"]);
                //g.DrawLine(p, dicDirection["NW"], dicDirection["SE"]);

                //p = new Pen(Color.Orange, 1);
                //g.DrawLine(p, dicDirection["NE_E"], dicDirection["SW_W"]);
                //g.DrawLine(p, dicDirection["NE_N"], dicDirection["SW_S"]);
                //g.DrawLine(p, dicDirection["NW_N"], dicDirection["SE_S"]);
                //g.DrawLine(p, dicDirection["NW_W"], dicDirection["SE_E"]);


                p = new Pen(Color.CadetBlue, 3);
                g.DrawEllipse(p, LCC.LocationX, LCC.LocationY, LCC.Diameter, LCC.Diameter);

                p = new Pen(Color.Chocolate, 3);
                g.DrawEllipse(p, SCC.LocationX, SCC.LocationY, SCC.Diameter, SCC.Diameter);

                //SolidBrush sb = new SolidBrush(Color.OrangeRed);
                //string speedlevel = R_Cur > (LCC.Radius / 2) ? "快" : "普通";
                //g.DrawString(string.Format("{2}:({0},{1})", pt_Cur.X, pt_Cur.Y, speedlevel), this.Font, sb, 10, 10);
                //g.DrawString(string.Format("{0}", CurrentDirection), this.Font, sb, 10, 25);



                Graphics diaplayGraphic = this.graphBuffer.Graphics;
                diaplayGraphic.Clear(this.BackColor);
                diaplayGraphic.DrawImage(b, 0, 0);
                this.graphBuffer.Render();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                g.Dispose();
                GC.Collect();
            }
        }

        private void VirtualController_Paint(object sender, PaintEventArgs e)
        {
            PaintChessboard();
        }

        private void VirtualController_Resize(object sender, EventArgs e)
        {
            try
            {
                Inita();
                graphBuffer = (new BufferedGraphicsContext()).Allocate(this.CreateGraphics(), this.DisplayRectangle);
                this.Invalidate();
            }
            catch (Exception ex) { }
        }



        private void VirtualController_MouseMove(object sender, MouseEventArgs e)
        {
            if (!pressed)
                return;

            CalPointLocation(e.Location);
            this.Invalidate();

            if (FG_Pressed != null)
                FG_Pressed();
        }

        private void VirtualController_MouseDown(object sender, MouseEventArgs e)
        {
            if (pressed)
                return;

            pressed = true;
            CalPointLocation(e.Location);
            this.Invalidate();
        }

        private void VirtualController_MouseUp(object sender, MouseEventArgs e)
        {
            if (!pressed)
                return;

            pressed = false;
            SCC.CenterX = this.Width / 2;
            SCC.CenterY = this.Height / 2;
            this.Invalidate();

            if (FG_Lift != null)
                FG_Lift();
        }


        /// <summary>
        /// 计算最大点位
        /// </summary>
        /// <param name="ptClicked">点击位置</param>
        private void CalPointLocation(Point ptClicked)
        {
            //求圆或圆弧上点的坐标
            //X=R*cos(θ）
            //Y=R*sin(θ）

            //角度用弧度制表示
            //θ 是用弧度制表示的角度
            //角度转幅度公式 弧度=(角度*π)/180
            //R是半径

            try
            {
                int r = pt2ptDistance(new Point(LCC.CenterX, LCC.CenterY), ptClicked);
                R_Cur = r;
                double CosAngle = (double)(ptClicked.X - LCC.CenterX) / (double)r;
                double SinAngle = (double)(ptClicked.Y - LCC.CenterY) / (double)r;

                int egX = (int)(LCC.Radius * CosAngle);
                int egY = (int)(LCC.Radius * SinAngle);

                //点击位置延伸到外圆上的交点
                int EgX = egX + LCC.CenterX;
                int EgY = egY + LCC.CenterY;

                pt_Cur = new Point(EgX, EgY);

                if (ptClicked.X > LCC.CenterX)
                {
                    if (ptClicked.Y > LCC.CenterY)
                    {
                        //第4象限
                        SCC.CenterX = ptClicked.X > EgX ? EgX : ptClicked.X;
                        SCC.CenterY = ptClicked.Y > EgY ? EgY : ptClicked.Y;

                        if (EgY > dicDirection["SE"].Y)  //SE_S
                        {
                            CurrentDirection = (EgY > dicDirection["SE_S"].Y) ? Direction4.S : Direction4.SE;
                        }
                        else    //SE_E
                        {
                            CurrentDirection = (EgY > dicDirection["SE_E"].Y) ? Direction4.SE : Direction4.E;
                        }
                    }
                    else
                    {
                        //第1象限
                        SCC.CenterX = ptClicked.X > EgX ? EgX : ptClicked.X;
                        SCC.CenterY = ptClicked.Y < EgY ? EgY : ptClicked.Y;

                        if (EgY > dicDirection["NE"].Y)  //NE_E
                        {
                            CurrentDirection = (EgY > dicDirection["NE_E"].Y) ? Direction4.E : Direction4.NE;
                        }
                        else    //NE_N
                        {
                            CurrentDirection = (EgY > dicDirection["NE_N"].Y) ? Direction4.NE : Direction4.N;
                        }
                    }
                }
                else
                {
                    if (ptClicked.Y > LCC.CenterY)
                    {
                        //第3象限
                        SCC.CenterX = ptClicked.X < EgX ? EgX : ptClicked.X;
                        SCC.CenterY = ptClicked.Y > EgY ? EgY : ptClicked.Y;

                        if (EgY > dicDirection["SW"].Y)  //SW_S
                        {
                            CurrentDirection = (EgY > dicDirection["SW_S"].Y) ? Direction4.S : Direction4.SW;
                        }
                        else    //SW_W
                        {
                            CurrentDirection = (EgY > dicDirection["SW_W"].Y) ? Direction4.SW : Direction4.W;
                        }
                    }
                    else
                    {
                        //第2象限
                        SCC.CenterX = ptClicked.X < EgX ? EgX : ptClicked.X;
                        SCC.CenterY = ptClicked.Y < EgY ? EgY : ptClicked.Y;

                        if (EgY > dicDirection["NW"].Y)  //NE_E
                        {
                            CurrentDirection = (EgY > dicDirection["NW_W"].Y) ? Direction4.W : Direction4.NW;
                        }
                        else    //NE_N
                        {
                            CurrentDirection = (EgY > dicDirection["NW_N"].Y) ? Direction4.NW : Direction4.N;
                        }
                    }
                }
            }
            catch { }
        }


        /// <summary>
        ///  求两点间的距离
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns></returns>
        private int pt2ptDistance(Point p1, Point p2)
        {
            double d1 = Math.Pow((p1.X - p2.X), 2);
            double d2 = Math.Pow((p1.Y - p2.Y), 2);
            double d = Math.Sqrt(d1 + d2);
            return (int)d;
        }
    }


    public enum Direction4
    {
        STOP = 0,

        N = 1,
        E = 2,
        S = 3,
        W = 4,

        NE = 5,
        SE = 6,
        SW = 7,
        NW = 8


    }


    /// <summary>
    /// 圆
    /// </summary>
    public class ControllerCircle
    {
        /// <summary>
        /// 半径
        /// </summary>
        public int Radius { get; set; }
        /// <summary>
        /// 直径
        /// </summary>
        public int Diameter { get { return Radius * 2; } }

        /// <summary>
        /// 左上角起点X
        /// </summary>
        public int LocationX { get { return CenterX - Radius; } }
        /// <summary>
        /// 左上角起点Y
        /// </summary>
        public int LocationY { get { return CenterY - Radius; } }

        /// <summary>
        /// 圆心X
        /// </summary>
        public int CenterX { get; set; }
        /// <summary>
        /// 圆心Y
        /// </summary>
        public int CenterY { get; set; }
    }
}
