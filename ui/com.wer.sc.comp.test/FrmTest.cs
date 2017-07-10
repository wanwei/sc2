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
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
            panel1.Paint += Panel1_Paint;
            panel1.Resize += Panel1_Resize;
        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            Draw();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }
        private bool drawing;
        private void Draw()
        {
            if (drawing)
                return;
            drawing = true;
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(panel1.CreateGraphics(), panel1.DisplayRectangle);
            Graphics g = myBuffer.Graphics;

            Rectangle rect = panel1.DisplayRectangle;
            g.DrawLine(new Pen(Color.Red), new Point(0, 0), new Point(rect.Right, rect.Bottom));

            myBuffer.Render();
            myBuffer.Dispose();
            drawing = false;
        }
    }
}
