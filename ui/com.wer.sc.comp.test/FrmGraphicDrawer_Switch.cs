using com.wer.sc.comp.graphic;
using com.wer.sc.comp.graphic.timeline;
using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
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
    public partial class FrmGraphicDrawer_Switch : Form
    {
        private GraphicDrawer_Switch drawer_Switch;
        public FrmGraphicDrawer_Switch()
        {
            InitializeComponent();

            GraphicDrawer_Candle drawer_Candle = new GraphicDrawer_Candle();
            MockGraphicData_Candle dataProvider = new MockGraphicData_Candle("m1505", 20140601, 20150401, KLinePeriod.KLinePeriod_1Day);
            dataProvider.BlockMount = 100;
            dataProvider.EndIndex = 200;
            drawer_Candle.DataProvider = dataProvider;

            GraphicDrawer_TimeLine drawer_TimeLine = new GraphicDrawer_TimeLine();
            drawer_TimeLine.MarginInfo = new GraphicMarginInfo(60, 20, 50, 20);
            drawer_TimeLine.Padding = new GraphicPaddingInfo(0, 0, 0, 0);
            MockGraphicData_Real dataProvider_TimeLine = new MockGraphicData_Real();
            drawer_TimeLine.DataProvider = dataProvider_TimeLine;


            drawer_Switch = new GraphicDrawer_Switch();
            drawer_Switch.Drawers.Add(drawer_Candle);
            drawer_Switch.Drawers.Add(drawer_TimeLine);
            drawer_Switch.BindControl(this);

            CrossHairDrawer cdrawer = new CrossHairDrawer();
            cdrawer.Bind(drawer_Switch);

            this.KeyDown += FrmGraphicDrawer_Switch_KeyDown;
            //this.KeyPress += FrmGraphicDrawer_Switch_KeyPress;
        }

        private void FrmGraphicDrawer_Switch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (drawer_Switch.CurrentIndex == 0)
                    drawer_Switch.Switch(1);
                else
                    drawer_Switch.Switch(0);                
            }
        }

        private void FrmGraphicDrawer_Switch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.F5)
            {
                if (drawer_Switch.CurrentIndex == 0)
                    drawer_Switch.Switch(1);
                else
                    drawer_Switch.Switch(0);
            }
        }
    }

    public class GraphicDrawer_TestSwitch : GraphicDrawer_Switch
    {
        private GraphicDrawer_Candle drawer_Candle;
        private GraphicDrawer_TimeLine drawer_Real;

        public GraphicDrawer_TestSwitch()
        {
            drawer_Candle = new GraphicDrawer_Candle();

            MockGraphicData_Candle dataProvider = new MockGraphicData_Candle();
            drawer_Candle.DataProvider = dataProvider;

            drawer_Real = new GraphicDrawer_TimeLine();
            drawer_Real.DataProvider = new MockGraphicData_Real();

            this.Drawers.Add(drawer_Candle);
            this.Drawers.Add(drawer_Real);
            this.Switch(0);
        }

        public override void BindControl(Control control)
        {
            base.BindControl(control);
            drawer_Candle.BindControl(control);
            drawer_Real.BindControl(control);
            control.KeyUp += Control_KeyUp;
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (this.CurrentIndex == 0)
                {
                    //CrossHairDrawer currentdrawer = this.drawer_Candle.CrossHairDrawer;
                    //bool showCross = currentdrawer.ShowCrossHair;
                    //if (showCross)
                    //{
                    //    this.drawer_Real.CrossHairDrawer.ShowCrossHair = true;
                    //    this.drawer_Real.CrossHairDrawer.ChangeCrossPoint(currentdrawer.CrossHairPoint);
                    //}
                    //else
                    //{
                    //    this.drawer_Real.CrossHairDrawer.ShowCrossHair = false;
                    //}
                    //this.Switch(1);
                }
                else
                {
                    //CrossHairDrawer currentdrawer = this.drawer_Real.CrossHairDrawer;
                    //bool showCross = currentdrawer.ShowCrossHair;
                    //if (showCross)
                    //{
                    //    this.drawer_Candle.CrossHairDrawer.ShowCrossHair = true;
                    //    this.drawer_Candle.CrossHairDrawer.ChangeCrossPoint(currentdrawer.CrossHairPoint);
                    //}
                    //else
                    //{
                    //    this.drawer_Candle.CrossHairDrawer.ShowCrossHair = false;
                    //}
                    //this.Switch(0);
                }
                this.Paint();
            }
        }

        public override void UnBindControl()
        {
            base.UnBindControl();
        }
    }
}
