using com.wer.sc.data.update;
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

namespace com.wer.sc.data.update
{
    public partial class FormChoosePlugin : Form
    {
        private IPluginMgr pluginMgr;
        
        private IPlugin_HistoryData choosedPlugin;

        public IPluginMgr PluginMgr
        {
            get
            {
                return pluginMgr;
            }
        }

        public IPlugin_HistoryData ChoosedPlugin
        {
            get
            {
                return choosedPlugin;
            }
        }

        private List<PluginInfo> plugins;

        private List<IPlugin_HistoryData> pluginObjects = new List<IPlugin_HistoryData>();

        public FormChoosePlugin()
        {
            InitializeComponent();
            this.pluginMgr = PluginMgrFactory.DefaultPluginMgr;
            this.plugins = pluginMgr.GetPlugins(typeof(IPlugin_HistoryData));
            for (int i = 0; i < plugins.Count; i++)
            {
                PluginInfo plugin_HistoryData = plugins[i];
                cbProvider.Items.Add(plugin_HistoryData.PluginName);
                pluginObjects.Add(pluginMgr.CreatePluginObject<IPlugin_HistoryData>(plugin_HistoryData));
            }
            cbProvider.SelectedIndex = 0;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.choosedPlugin = this.pluginObjects[cbProvider.SelectedIndex];
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
