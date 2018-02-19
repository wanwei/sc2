using com.wer.sc.graphic;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.account;

namespace com.wer.sc.ui.comp
{
    public partial class ChartComponent : UserControl
    {
        private bool connectedServer;

        private IDataCenter dataCenter;
        private ChartComponentData prevCompData = null;
        private ChartComponentController controller;
        private ChartComponentDrawer drawer;

        private IStrategyData strategyData = null;

        private ChartComponentStrategy chartComponentStrategy = null;

        //账号
        private IAccount account;

        public ChartComponent()
        {
            InitializeComponent();
        }

        public void Init(IDataCenter dataCenter, string code, double time)
        {
            Init(dataCenter, code, time, KLinePeriod.KLinePeriod_1Minute);
        }

        public void Init(IDataCenter dataCenter, string code, double time, KLinePeriod klinePeriod)
        {
            this.dataCenter = dataCenter;
            IDataNavigate dataNavigater = dataCenter.DataNavigateFactory.CreateDataNavigate(code, time);
            IKLineData klineData = dataNavigater.GetKLineData(klinePeriod);
            int showKLineIndex = klineData.BarPos;
            ChartComponentData compData = new ChartComponentData(code, time, klinePeriod, showKLineIndex);
            this.controller = new ChartComponentController(dataNavigater, compData);
            this.drawer = new ChartComponentDrawer(this, controller);
            //this.drawer.GraphicData_Candle.OnGraphicDataChange += GraphicData_Candle_OnGraphicDataChange;
            this.drawer.GraphicDrawer.AfterGraphicPaint += GraphicDrawer_AfterGraphicPaint;
        }

        private void GraphicData_Candle_OnGraphicDataChange(object sender, GraphicDataChangeArgument arg)
        {
            if (OnChartRefresh != null)
                OnChartRefresh(this, new ChartComponentRefreshArguments(prevCompData, controller.ChartComponentData));
            if (prevCompData == null)
                prevCompData = new ChartComponentData(controller.ChartComponentData);
            else
                prevCompData.CopyFrom(controller.ChartComponentData);
        }

        private void GraphicDrawer_AfterGraphicPaint(object sender, GraphicRefreshArgs e)
        {
            if (OnChartRefresh != null)
                OnChartRefresh(this, new ChartComponentRefreshArguments(prevCompData, controller.ChartComponentData));
            if (prevCompData == null)
                prevCompData = new ChartComponentData(controller.ChartComponentData);
            else
                prevCompData.CopyFrom(controller.ChartComponentData);
        }

        public ChartComponentController Controller
        {
            get { return controller; }
        }

        public ChartComponentDrawer Drawer
        {
            get { return drawer; }
        }

        public IStrategyData StrategyData
        {
            get
            {
                return strategyData;
            }

            set
            {
                strategyData = value;
                this.chartComponentStrategy = new ChartComponentStrategy(this, strategyData);
            }
        }

        public ChartComponentStrategy ChartComponentStrategy
        {
            get
            {
                return chartComponentStrategy;
            }
        }

        public IDataCenter DataCenter
        {
            get { return dataCenter; }
        }

        public bool ConnectedServer
        {
            get
            {
                return connectedServer;
            }

            set
            {
                connectedServer = value;
            }
        }

        public IAccount Account
        {
            get
            {
                return account;
            }

            set
            {
                account = value;
            }
        }

        public event DelegateOnChartComponentRefresh OnChartRefresh;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChartComponent
            // 
            this.Name = "ChartComponent";
            this.Size = new System.Drawing.Size(656, 451);
            this.ResumeLayout(false);
        }
    }

    public delegate void DelegateOnChartComponentRefresh(object sender, ChartComponentRefreshArguments arg);

    public class ChartComponentRefreshArguments
    {
        private bool dataRefreshed;

        private ChartComponentData prevCompData;

        private ChartComponentData compData;

        public ChartComponentRefreshArguments(ChartComponentData prevCompData, ChartComponentData compData)
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

        public ChartComponentData PrevCompData
        {
            get { return prevCompData; }
        }

        public ChartComponentData CurrentCompData
        {
            get
            {
                return compData;
            }
        }
    }
}
