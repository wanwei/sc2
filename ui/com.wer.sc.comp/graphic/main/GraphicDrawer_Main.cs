using com.wer.sc.comp.graphic.info;
using com.wer.sc.comp.graphic.main;
using com.wer.sc.comp.graphic.timeline;
using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDrawer_Main : GraphicDrawer_Compound
    {
        private GraphicDrawer_Switch_CandleReal graphicDrawer_Left;
        private GraphicDrawer_CurrentInfo graphicDrawer_Right;
        private IGraphicData_Chart dataProvider;

        public IGraphicData_Chart DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
                graphicDrawer_Left.DataProvider = value;
                //graphicDrawer_Right.DataProvider = dataProvider.DataProvider_Info;
            }
        }

        public GraphicDrawer_Main()
        {
            this.IsVertical = false;

            graphicDrawer_Left = new GraphicDrawer_Switch_CandleReal();
            graphicDrawer_Right = new GraphicDrawer_CurrentInfo();

            this.AddGraph(graphicDrawer_Left, 100);
            this.AddGraph(graphicDrawer_Right, 100, true);
        }

        public override void BindControl(Control control)
        {
            base.BindControl(control);
            this.graphicDrawer_Left.BindOthers(control);
        }
    }
}
