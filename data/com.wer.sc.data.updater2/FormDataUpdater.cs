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

        private List<IUpdateStepGetter> updateStepGetters = new List<IUpdateStepGetter>();

        public MultiDataUpdater(DataUpdaterPackageConfigInfo package)
        {
            this.package = package;
            for (int i = 0; i < package.DataUpdaters.Count; i++)
            {
                DataUpdaterConfigInfo configInfo = package.DataUpdaters[i];
                names.Add(configInfo.Name);
                updateStepGetters.Add(configInfo.DataUpdater.UpdateStepGetter);
            }
        }

        public List<string> GetDataUpdaterNames()
        {
            return names;
        }

        public List<IUpdateStepGetter> GetDataUpdaters()
        {
            return updateStepGetters;
        }
    }
}
