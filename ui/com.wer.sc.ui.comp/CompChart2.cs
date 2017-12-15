using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.navigate;
using com.wer.sc.data;
using com.wer.sc.comp.graphic;

namespace com.wer.sc.ui.comp
{
    public partial class CompChart2 : UserControl
    {
        private CompData prevCompData = null;
        private CompDataController controller;
        private CompChartDrawer2 drawer;

        public CompChart2()
        {
            InitializeComponent();
        }

        public void Init(IDataCenter dataCenter, string code, double time)
        {
            Init(dataCenter, code, time, KLinePeriod.KLinePeriod_1Minute);
        }

        public void Init(IDataCenter dataCenter, string code, double time, KLinePeriod klinePeriod)
        {
            IDataNavigate dataNavigater = dataCenter.DataNavigateFactory.CreateDataNavigate(code, time);
            IKLineData klineData = dataNavigater.GetKLineData(klinePeriod);
            int showKLineIndex = klineData.BarPos;
            CompData compData = new CompData(code, time, klinePeriod, showKLineIndex);
            this.controller = new CompDataController(dataNavigater, compData);
            this.drawer = new CompChartDrawer2(this, controller);
            //this.drawer.GraphicData_Candle.OnGraphicDataChange += GraphicData_Candle_OnGraphicDataChange;
            this.drawer.GraphicDrawer.AfterGraphicPaint += GraphicDrawer_AfterGraphicPaint;
        }

        private void GraphicData_Candle_OnGraphicDataChange(object sender, GraphicDataChangeArgument arg)
        {
            if (OnChartRefresh != null)
                OnChartRefresh(this, new CompChartRefreshArguments(prevCompData, controller.CompData));
            if (prevCompData == null)
                prevCompData = new CompData(controller.CompData);
            else
                prevCompData.CopyFrom(controller.CompData);
        }

        private void GraphicDrawer_AfterGraphicPaint(object sender, GraphicRefreshArgs e)
        {
            if (OnChartRefresh != null)
                OnChartRefresh(this, new CompChartRefreshArguments(prevCompData, controller.CompData));
            if (prevCompData == null)
                prevCompData = new CompData(controller.CompData);
            else
                prevCompData.CopyFrom(controller.CompData);
        }

        public CompDataController Controller
        {
            get { return controller; }
        }

        public CompChartDrawer2 Drawer
        {
            get { return drawer; }
        }

        public event DelegateOnCompChartRefresh OnChartRefresh;
    }

    public delegate void DelegateOnCompChartRefresh(object sender, CompChartRefreshArguments arg);

    public class CompChartRefreshArguments
    {
        private bool dataRefreshed;

        private CompData prevCompData;

        private CompData compData;

        public CompChartRefreshArguments(CompData prevCompData, CompData compData)
        {
            this.prevCompData = prevCompData;
            this.compData = compData;
            this.dataRefreshed = this.compData.Equals(prevCompData);
        }

        public bool DataRefreshed
        {
            get
            {
                return dataRefreshed;
            }
        }

        public CompData PrevCompData
        {
            get { return prevCompData; }
        }

        public CompData CurrentCompData
        {
            get
            {
                return compData;
            }
        }
    }
}