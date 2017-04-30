using com.wer.sc.data.market;
using com.wer.sc.data.market.receiver;
using com.wer.sc.data.receiver2;
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
    public partial class FormReceiver2 : Form
    {
        private IPluginMgr mgr;

        private RealDataReceiver dataReceiver;

        private ConnectionInfo currentConnection;

        public FormReceiver2()
        {
            InitializeComponent();
            this.mgr = PluginMgrFactory.DefaultPluginMgr;
            InitMenu();
        }

        private void InitMenu()
        {
            List<PluginInfo> plugins = mgr.GetPlugins(typeof(IPlugin_Market));
            if (plugins != null)
            {
                for (int i = 0; i < plugins.Count; i++)
                {
                    PluginInfo pluginInfo = plugins[i];
                    ToolStripMenuItem item = (ToolStripMenuItem)menuItemServer.DropDownItems.Add(pluginInfo.PluginName);
                    AddMarket(item, pluginInfo);
                }
            }
            ToolStripItem loginOutItem = menuItemServer.DropDownItems.Add("登出");
            loginOutItem.Click += LoginOutItem_Click;
            ToolStripItem exitItem = menuItemServer.DropDownItems.Add("退出");
            exitItem.Click += ExitItem_Click;
        }

        private void LoginOutItem_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddMarket(ToolStripMenuItem item, PluginInfo pluginInfo)
        {
            IPlugin_Market plugin_Market = mgr.CreatePluginObject<IPlugin_Market>(pluginInfo);
            //RealDataReceiver receiver = new RealDataReceiver(plugin_Market, @"D:\SCPRESENT", true);
            //item.Tag = receiver;

            //List<ConnectionInfo> connectionInfos = receiver.GetAllConnections();
            //if (connectionInfos == null)
            //    return;
            //for (int i = 0; i < connectionInfos.Count; i++)
            //{
            //    ConnectionInfo connectionInfo = connectionInfos[i];
            //    ToolStripItem connItem = item.DropDownItems.Add(connectionInfo.Name);
            //    connItem.Tag = connectionInfo;
            //    connItem.Click += ConnectionItem_Click;
            //}
        }

        private void ConnectionItem_Click(object sender, EventArgs e)
        {
            Disconnect();

            //ToolStripItem connectionItem = (ToolStripItem)sender;
            //ToolStripItem marketItem = connectionItem.OwnerItem;
            //dataReceiver = (DataReceiver)marketItem.Tag;
            //currentConnection = (ConnectionInfo)connectionItem.Tag;
            //Connect();
        }

        private void Connect()
        {
            //if (dataReceiver != null)
            //{
            //    dataReceiver.Connect(currentConnection);
            //}
        }

        private void Disconnect()
        {
            if (dataReceiver != null)
            {
                //dataReceiver.Disconnect();
            }
        }

        private void menuItemLog_Click(object sender, EventArgs e)
        {
            //FormLog2 frmLog = new FormLog2(dataReceiver.LogUtils);
            //frmLog.Show();
        }
    }
}
