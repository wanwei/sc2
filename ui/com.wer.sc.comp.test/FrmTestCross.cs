using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.test
{
    public partial class FrmTestCross : Form
    {
        private Point crossPoint;
        private Point lastCrossPoint;

        bool drawCross = false;
        bool shouldRemoveCross = false;

        public FrmTestCross()
        {
            InitializeComponent();
            initRandomData();

            this.Paint += Panel1_Paint;
            this.Resize += Panel1_Resize;

            this.MouseUp += FrmTestRegion_MouseUp;
            this.MouseMove += FrmTestCross_MouseMove;            
        }

        const int len = 1000;
        int max = 100;

        int[] reals = new int[len];
        int[] mounts = new int[len];

        private void initRandomData()
        {
            Random ra = new Random();
            Random ra2 = new Random();

            for (int i = 0; i < len; i++)
            {
                reals[i] = ra.Next(0, max);
                mounts[i] = ra2.Next(0, max);
            }
        }

        private void FrmTestCross_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawCross)
                return;
            Rectangle leftRect = GetLeftRect();
            if (!leftRect.Contains(e.Location))
            {
                drawCross = false;
                RemoveCross(lastCrossPoint);
                return;
            }
            this.crossPoint = e.Location;
            DrawWhenCrossMove();
        }

        private void FrmTestRegion_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle leftRect = GetLeftRect();

            if (!leftRect.Contains(e.Location))
                return;

            this.crossPoint = e.Location;
            drawCross = !drawCross;
            DrawWhenCrossMove();
            //Draw();
            //Draw(new Rectangle(0, crossPoint.Y - 5, this.Right, 10));
        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            //Draw();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private bool drawing;

        private void DrawWhenCrossMove()
        {
            if (lastCrossPoint.X >= 0)
                RemoveCross(lastCrossPoint);
            Graphics g = this.CreateGraphics();
            DrawCross(g);
            //Rectangle rect = this.DisplayRectangle;
            //BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            //BufferedGraphics myBuffer = currentContext.Allocate(this.CreateGraphics(), rect);
            //Graphics g = myBuffer.Graphics;

            //DrawCross(g);

            //myBuffer.Render();
            //myBuffer.Dispose();
            //currentContext.Dispose();
        }

        private void Draw()
        {
            if (drawing)
                return;
            drawing = true;

            //if (lastCrossPoint.X >= 0)
            //    RemoveCross(lastCrossPoint);

            Rectangle rect = this.DisplayRectangle;
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(this.CreateGraphics(), rect);
            Graphics g = myBuffer.Graphics;

            DrawLeft(g);
            DrawRight(g);

            if (drawCross)
                DrawCross(g);

            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
            drawing = false;
        }

        private void DrawCross(Graphics g)
        {
            if (!drawCross)
                return;
            Rectangle rect = this.DisplayRectangle;
            Pen pen = new Pen(Color.White);
            g.DrawLine(pen, new Point(rect.X, crossPoint.Y), new Point(rect.Right / 2 - 2, crossPoint.Y));
            g.DrawLine(pen, new Point(crossPoint.X, rect.Top), new Point(crossPoint.X, rect.Bottom));
            lastCrossPoint = crossPoint;
        }

        private void DrawLeft(Graphics g)
        {
            Rectangle rect = GetLeftRect();

            //Pen p = new Pen(new SolidBrush(Color.Red));
            //int cnt = 3000;
            //for (int i = 0; i < cnt; i++)
            //{
            //    float x = ((float)rect.Width) / cnt * i;
            //    float y = ((float)rect.Height) / cnt * i;
            //    g.DrawLine(p, x, 0, x, y);
            //}
            Pen realp = new Pen(new SolidBrush(Color.Red));
            Pen mountp = new Pen(new SolidBrush(Color.Yellow));
            int cnt = len;
            for (int i = 0; i < reals.Length; i++)
            {
                float x = ((float)rect.Width) / cnt * i;
                float y = ((float)rect.Height) / max * mounts[i];
                g.DrawLine(mountp, x, 0, x, y);

                if (i != 0)
                {
                    float lastx = ((float)rect.Width) / cnt * i - 1;
                    float lastrealy = ((float)rect.Height) / max * reals[i - 1];
                    float realy = ((float)rect.Height) / max * reals[i];
                    g.DrawLine(realp, lastx, lastrealy, x, realy);
                }
            }
        }

        private Rectangle GetLeftRect()
        {
            Rectangle displayrect = this.DisplayRectangle;
            Rectangle rect = new Rectangle(0, 0, displayrect.Width / 2, displayrect.Height);
            return rect;
        }

        private void DrawRight(Graphics g)
        {
            Rectangle displayrect = this.DisplayRectangle;
            int x = displayrect.Width / 2;
            Rectangle rect = new Rectangle(x, 0, displayrect.Width / 2, displayrect.Height);

            g.FillEllipse(new SolidBrush(Color.Green), rect);
        }

        private void RemoveCross(Point point)
        {
            RemoveHorzonalLine(point);
            RemoveVerticalLine(point);
            lastCrossPoint.X = -1;
            lastCrossPoint.Y = -1;
        }

        private void RemoveHorzonalLine(Point point)
        {
            Rectangle drawRect = new Rectangle(0, point.Y - 2, this.Width / 2 - 8, 4);

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(this.CreateGraphics(), drawRect);
            Graphics g = myBuffer.Graphics;

            DrawLeft(g);

            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
        }

        private void RemoveVerticalLine(Point point)
        {
            Rectangle drawRect = new Rectangle(point.X - 2, 0, 4, this.Height);

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(this.CreateGraphics(), drawRect);
            Graphics g = myBuffer.Graphics;

            DrawLeft(g);

            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
        }
    }
}
