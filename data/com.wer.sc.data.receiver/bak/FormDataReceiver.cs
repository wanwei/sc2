using com.wer.sc.data.market;
using com.wer.sc.data.market.receiver;
using com.wer.sc.data.receiver2;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.receiver2
{
    public partial class FormDataReceiver : Form
    {
        private List<RealDataReceiver> dataReceiver = new List<RealDataReceiver>();

        private RealDataReceiver dataReceiver_CnFutures;

        private ConnectionInfo connection_CnFutures;

        private RealDataReceiver dataReceiver_CnStock;

        private ConnectionInfo connection_CnStock;

        private ReceiveTimer receiveTimer;

        public FormDataReceiver()
        {
            InitializeComponent();

            InitCnFutures();
            InitCnStock();
            this.receiveTimer = new ReceiveTimer();
            this.receiveTimer.OnTimerElapsed = ReConnect;
        }

        private void InitCnFutures()
        {
            string pluginName = "MARKET.CNFUTURES";
            string connectionId = "SIMNOW2";

            PluginInfo pluginInfo = PluginMgrFactory.DefaultPluginMgr.GetPlugin(pluginName);
            IPlugin_Market market = PluginMgrFactory.DefaultPluginMgr.CreatePluginObject<IPlugin_Market>(pluginInfo);
            this.connection_CnFutures = GetConnection(market.MarketData.GetAllConnections(), connectionId);
            //this.dataReceiver_CnFutures = new RealDataReceiver(market, @"D:\SCPRESENT", true);
            this.lbConnection_CnFutures.Text = this.connection_CnFutures.Name;
        }

        private void InitCnStock()
        {
            //string pluginName = "MARKET.CNSTOCK";
            //string connectionId = "SIMNOW2";

            //PluginInfo pluginInfo = PluginMgrFactory.DefaultPluginMgr.GetPlugin(pluginName);
            //IPlugin_Market market = PluginMgrFactory.DefaultPluginMgr.CreatePluginObject<IPlugin_Market>(pluginInfo);
            //this.connection_CnFutures = GetConnection(market.MarketData.GetAllConnections(), connectionId);
            //this.dataReceiver_CnFutures = new DataReceiver(market, @"D:\SCPRESENT", true);
            //this.lbConnection_CnStock.Text = this.connection_CnFutures.Name;
            this.lbConnection_CnStock.Text = "暂无";
        }

        private ConnectionInfo GetConnection(List<ConnectionInfo> connections, string id)
        {
            foreach (ConnectionInfo con in connections)
            {
                if (con.Id.Equals(id))
                    return con;
            }
            return null;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            //if (dataReceiver_CnFutures != null)
            //    this.dataReceiver_CnFutures.Connect(this.connection_CnFutures);
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            //if (dataReceiver_CnFutures != null)
            //    this.dataReceiver_CnFutures.Disconnect();
        }

        private void ReConnect()
        {
            if (dataReceiver_CnFutures != null)
            {
                //dataReceiver_CnFutures.Disconnect();
                //dataReceiver_CnFutures.Connect(this.connection_CnFutures);
            }
        }
    }

    class ReceiveTimer
    {
        private System.Timers.Timer timer;

        public ReceiveTimer()
        {
            InitTimer();
        }

        //每天早上8点半或者晚上8点半重连
        private void InitTimer()
        {
            DateTime nextConnectTime = GetNextConnectTime();

            timer = new System.Timers.Timer();
            TimeSpan ts = nextConnectTime - DateTime.Now;
            timer.Interval = ts.TotalMilliseconds;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private bool timerAdjusted = false;

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!timerAdjusted)
            {
                timer.Interval = 12 * 3600 * 1000;
                timerAdjusted = true;
                timeElapse();
            }
            else
            {
                timeElapse();
            }
        }

        private void timeElapse()
        {
            if (onTimerElapsed != null)
                onTimerElapsed();
        }

        private DelegateOnTimerElapsed onTimerElapsed;

        public DelegateOnTimerElapsed OnTimerElapsed
        {
            get
            {
                return onTimerElapsed;
            }

            set
            {
                onTimerElapsed = value;
            }
        }
        const string NIGHTCONNECT = "20:30:00";
        const string MORNINGCONNECT = "08:30:00";

        private static DateTime GetNextConnectTime()
        {
            DateTime nextConnectTime;
            DateTime dtNow = DateTime.Now;
            string today = dtNow.ToString("yyyy-MM-dd");

            DateTime nightConnect = DateTime.Parse(today + " " + NIGHTCONNECT);
            //开启时间晚于晚上8点半，第二天重连
            if (dtNow.CompareTo(nightConnect) > 0)
            {
                string nextDay = dtNow.AddDays(1).ToString("yyyy-MM-dd");
                nextConnectTime = DateTime.Parse(nextDay + " " + MORNINGCONNECT);
            }
            else
            {
                DateTime morningConnect = DateTime.Parse(today + " " + MORNINGCONNECT);
                if (dtNow.CompareTo(morningConnect) > 0)
                {
                    nextConnectTime = nightConnect;
                }
                else
                {
                    nextConnectTime = morningConnect;
                }
            }
            return nextConnectTime;
        }
    }

    public delegate void DelegateOnTimerElapsed();
}
