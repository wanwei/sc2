using com.wer.sc.graphic.timeline;
using com.wer.sc.graphic.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.graphic.main
{
    public class GraphicDrawer_Switch_CandleReal : GraphicDrawer_Switch
    {
        private IGraphicData_Chart dataProvider;

        private GraphicDrawer_Candle drawer_Candle;
        private GraphicDrawer_TimeLine drawer_Real;

        public IGraphicData_Chart DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
                //drawer_Candle.DataProvider = dataProvider.DataProvider_Candle;
                //drawer_Real.DataProvider = dataProvider.DataProvider_Real;
            }
        }

        public GraphicDrawer_Switch_CandleReal()
        {
            drawer_Candle = new GraphicDrawer_Candle();
            drawer_Real = new GraphicDrawer_TimeLine();

            this.Drawers.Add(drawer_Candle);
            this.Drawers.Add(drawer_Real);
            this.Switch(0);
        }

        public override void BindControl(Control control)
        {
            base.BindControl(control);
            BindOthers(control);
        }

        public void BindOthers(Control control)
        {
            //drawer_Candle.BindOthers(control);
            //drawer_Real.BindOthers(control);
            //control.KeyUp += Control_KeyUp;
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //if (this.CurrentIndex == 0)
                //{
                //    CrossHairDrawer currentdrawer = this.drawer_Candle.CrossHairDrawer;
                //    bool showCross = currentdrawer.ShowCrossHair;
                //    if (showCross)
                //    {
                //        this.drawer_Real.CrossHairDrawer.ShowCrossHair = true;
                //        this.drawer_Real.CrossHairDrawer.ChangeCrossPoint(currentdrawer.CrossHairPoint);
                //    }
                //    else
                //    {
                //        this.drawer_Real.CrossHairDrawer.ShowCrossHair = false;
                //    }
                //    this.Switch(1);
                //}
                //else
                //{
                //    CrossHairDrawer currentdrawer = this.drawer_Real.CrossHairDrawer;
                //    bool showCross = currentdrawer.ShowCrossHair;
                //    if (showCross)
                //    {
                //        this.drawer_Candle.CrossHairDrawer.ShowCrossHair = true;
                //        this.drawer_Candle.CrossHairDrawer.ChangeCrossPoint(currentdrawer.CrossHairPoint);
                //    }
                //    else
                //    {
                //        this.drawer_Candle.CrossHairDrawer.ShowCrossHair = false;
                //    }
                //    this.Switch(0);
                //}
                //this.Paint();
            }
        }

        public override void UnBindControl()
        {
            base.UnBindControl();
        }

    }
}
