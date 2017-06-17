using com.wer.sc.utils.ui.update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.utils.update;

namespace com.wer.sc.data.updater
{
    public partial class FormDataUpdater : Form
    {
        private DataUpdaterPackageConfigInfo package;
        private MultiDataUpdater dataUpdater;

        public FormDataUpdater(DataUpdaterPackageConfigInfo package)
        {
            InitializeComponent();
            this.package = package;
            this.Height = this.package.DataUpdaters.Count * 30 + 40 + 40;
            this.dataUpdater = new MultiDataUpdater(package);
            this.controlMultiUpdate1.MultiUpdater = this.dataUpdater;
        }
    }

    class MultiDataUpdater : IMultiUpdater
    {
        private DataUpdaterPackageConfigInfo package;

        private List<String> names = new List<string>();



        public MultiDataUpdater(DataUpdaterPackageConfigInfo package)
        {
            this.package = package;
            for (int i = 0; i < package.DataUpdaters.Count; i++)
            {
                DataUpdaterConfigInfo configInfo = package.DataUpdaters[i];
                names.Add(configInfo.Name);

            }
        }

        public List<string> GetDataUpdaterNames()
        {
            return names;
        }

        private List<IUpdateHelper> updateStepGetters;

        public List<IUpdateHelper> GetDataUpdaters()
        {
            if (updateStepGetters != null)
                return updateStepGetters;
            updateStepGetters = new List<IUpdateHelper>();

            for (int i = 0; i < package.DataUpdaters.Count; i++)
            {
                DataUpdaterConfigInfo configInfo = package.DataUpdaters[i];
                updateStepGetters.Add(configInfo.DataUpdater.PluginHelper);
            }            

            return updateStepGetters;
        }
    }
}
