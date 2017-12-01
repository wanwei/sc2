using com.wer.sc.comp.graphic;
using com.wer.sc.comp.graphic.timeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentView : IChartComponentView
    {
        private Control control;

        private GraphicDrawer_Switch drawer_Switch;

        //蜡烛图画图器
        private IGraphicDrawer_Candle drawer_Candle;

        private IGraphicData_Candle graphicData_Candle;

        private GraphicDrawer_TimeLine drawer_TimeLine;

        private IGraphicData_TimeLine graphicData_TimeLine;

        private ChartComponentInfo chartComponentInfo;

        public ChartComponentView(Control control, ChartComponentInfo chartComponentInfo)
        {
            this.control = control;
            this.chartComponentInfo = chartComponentInfo;
            this.PaintChart();
        }

        public IGraphicDrawer_Candle Drawer_Candle
        {
            get { return drawer_Candle; }
        }

        public IGraphicData_Candle GraphicData_Candle
        {
            get
            {
                return null;
            }
        }

        public void PaintChart()
        {
            if (!this.chartComponentInfo.CheckData())
            {
                PaintEmpty();
                return;
            }
        }

        private void PaintEmpty()
        {
            Graphics g = this.control.CreateGraphics();
            Rectangle rect = this.control.DisplayRectangle;
            g.FillRectangle(new SolidBrush(Color.Black), rect);
            SolidBrush brush = new SolidBrush(Color.White);
            SolidBrush redbrush = new SolidBrush(Color.Red);
            Font MyFontTitle = new Font("宋体", 16, FontStyle.Regular);
            Font MyFont1 = new Font("宋体", 14, FontStyle.Regular);

            int x = 20;
            int y = 20;
            g.DrawString("数据无法正常显示，参数：", MyFontTitle, redbrush, new PointF(x, y));
            y += 30;
            int between = 25;
            //g.DrawString("数据中心地址: " + GetObjectStr(DataCenterUri), MyFont1, brush, new PointF(x, y));
            //y += between;
            g.DrawString("合约或股票ID: " + GetObjectStr(chartComponentInfo.DataPackage), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("时间: " + GetObjectStr(chartComponentInfo.CurrentTime), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("图表类型: " + GetObjectStr(chartComponentInfo.ChartType), MyFont1, brush, new PointF(x, y));
            y += between;
            g.DrawString("K线周期: " + GetObjectStr(chartComponentInfo.KLinePeriod), MyFont1, brush, new PointF(x, y));
        }

        private string GetObjectStr(Object obj)
        {
            return obj == null ? "----" : obj.ToString();
        }
    }
}