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
    public partial class FrmTestRegion : Form
    {
        private Region lastDrawRegion;

        private Rectangle lastRect;

        private bool isResizeDraw;

        public FrmTestRegion()
        {
            InitializeComponent();

            this.Paint += Panel1_Paint;
            this.Resize += Panel1_Resize;

            this.MouseUp += FrmTestRegion_MouseUp;
        }

        Point crossPoint;

        bool drawCross = false;

        private void FrmTestRegion_MouseUp(object sender, MouseEventArgs e)
        {
            //DrawCrossPoint(e.Location);
            this.crossPoint = e.Location;
            drawCross = !drawCross;
            Draw(new Rectangle(0, crossPoint.Y - 5, this.Right, 10));
        }

        private void DrawCrossPoint(Point crossPoint)
        {
            //if (drawing)
            //    return;
            //drawing = true;

            Graphics controlGraphic = this.CreateGraphics();
            Rectangle controlRect = this.DisplayRectangle;
            Rectangle rect = new Rectangle(0, crossPoint.Y - 2, controlRect.Width, 4);

            //controlGraphic.SetClip(rect);

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(controlGraphic, controlRect);

            Graphics g = myBuffer.Graphics;

            Pen pen = new Pen(Color.White);
            //controlGraphic.DrawLine(pen, new Point(crossPoint.X, rect.Top), new Point(crossPoint.X, rect.Bottom));
            g.DrawLine(pen, new Point(rect.X, crossPoint.Y), new Point(rect.Right, crossPoint.Y));

            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
            //Draw(this.DisplayRectangle);

            Draw(new Rectangle(0, crossPoint.Y - 5, rect.Right, 10));

            //drawing = false;
        }

        private void DrawPoint(Point p)
        {
            //if (drawing)
            //    return;
            drawing = true;
            Graphics controlGraphic = this.CreateGraphics();

            Rectangle rect = new Rectangle(p.X - 20, p.Y - 20, 40, 40);
            controlGraphic.SetClip(rect);
            controlGraphic.ExcludeClip(new Rectangle(p.X - 10, p.Y - 10, 20, 20));


            //g.DrawLine(pen, new Point(crossPoint.X, rec.Top), new Point(crossPoint.X, rec.Bottom));
            //g.DrawLine(pen, new Point(rec.X, crossPoint.Y), new Point(rec.Right, crossPoint.Y));
            //controlGraphic.ExcludeClip

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(controlGraphic, rect);

            Graphics g = myBuffer.Graphics;
            g.FillRectangle(new SolidBrush(Color.Black), rect);

            //Rectangle rect2 = new Rectangle(p.X - 22, p.Y - 22, 44, 44);
            //Rectangle rect2 = new Rectangle(p.X - 30, p.Y - 30, 60, 60);
            g.FillEllipse(new SolidBrush(Color.Red), rect);

            myBuffer.Render();
            myBuffer.Dispose();

            lastRect = rect;

            //drawing = false;
        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            isResizeDraw = true;
            //Draw(this.DisplayRectangle);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            isResizeDraw = false;
            Draw(this.DisplayRectangle);
        }

        private bool drawing;

        private int index = 0;

        private void Draw(Rectangle rect2)
        {
            if (drawing)
                return;
            drawing = true;

            Rectangle rect = this.DisplayRectangle;

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(this.CreateGraphics(), rect2);
            Graphics g = myBuffer.Graphics;

            Color color = index % 2 == 0 ? Color.Red : Color.Green;
            g.DrawLine(new Pen(color), new Point(0, 0), new Point(rect.Right, rect.Bottom));

            if (drawCross)
            {
                Pen pen = new Pen(Color.White);
                g.DrawLine(pen, new Point(rect.X, crossPoint.Y), new Point(rect.Right, crossPoint.Y));
            }
            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
            index++;
            drawing = false;
        }
    }
}
