using com.wer.sc.plugin;
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
using System.Threading;

namespace com.wer.sc.utils.ui.test.proceed
{
    public partial class FrmMultiUpdate : Form
    {
        public FrmMultiUpdate()
        {
            InitializeComponent();
            this.controlMultiUpdate1.MultiUpdater = new MockMultiUpdate(this);
        }
        
        class MockMultiUpdate : IMultiUpdater
        {
            private List<string> updaterNames;

            private List<IUpdateHelper> dataUpdaters;

            public MockMultiUpdate(FrmMultiUpdate frm)
            {
                this.updaterNames = new List<string>();
                this.updaterNames.Add(frm.tbDesc1.Text);
                this.updaterNames.Add(frm.tbDesc2.Text);
                this.updaterNames.Add(frm.tbDesc3.Text);
                this.updaterNames.Add(frm.tbDesc4.Text);

                this.dataUpdaters = new List<IUpdateHelper>();
                this.dataUpdaters.Add(GetMockUpdateStepGetter(frm.tbDesc1.Text, frm.tbTime1.Text));
                this.dataUpdaters.Add(GetMockUpdateStepGetter(frm.tbDesc2.Text, frm.tbTime2.Text));
                this.dataUpdaters.Add(GetMockUpdateStepGetter(frm.tbDesc3.Text, frm.tbTime3.Text));
                this.dataUpdaters.Add(GetMockUpdateStepGetter(frm.tbDesc4.Text, frm.tbTime4.Text));
            }

            private MockUpdateStepGetter GetMockUpdateStepGetter(string desc, string proceedText)
            {                
                string[] proceedTimes = proceedText.Split(',');
                MockUpdateStepGetter mockProceed = new MockUpdateStepGetter();
                for (int i = 0; i < proceedTimes.Length; i++)
                {
                    mockProceed.Steps.Add(new MockStepImpl(int.Parse(proceedTimes[i]), "正在执行" + desc + "的第" + (i + 1) + "个步骤：预计花费" + proceedTimes[i] + "秒钟"));
                }
                return mockProceed;
            }

            public List<string> GetDataUpdaterNames()
            {
                return updaterNames;
            }

            public List<IUpdateHelper> GetDataUpdaters()
            {
                return dataUpdaters;
            }
        }
    }

}
