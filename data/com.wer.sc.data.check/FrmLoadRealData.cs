using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.check
{
    public partial class FrmLoadRealData : Form
    {
        private DataMgr providerDataMgr;

        private List<ITimeLineData> realdataList;

        public FrmLoadRealData()
        {
            InitializeComponent();

            this.providerDataMgr = new DataMgr();
            List<DataProviderWrap> providers = providerDataMgr.GetProviders();
            for (int i = 0; i < providers.Count; i++)
            {
                DataProviderWrap provider = providers[i];
                cbProvider.Items.Add(provider.GetName());
            }
            cbProvider.SelectedIndex = 0;
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            tbKLineData.Clear();

            String code = tbCode.Text.Trim();
            int startDate = int.Parse(tbStartDate.Text);
            int endDate = int.Parse(tbEndDate.Text);
            DataProviderWrap provider = this.providerDataMgr.GetProviders()[cbProvider.SelectedIndex];
            DataReaderFactory fac = new DataReaderFactory(provider.GetDataPath());

            this.realdataList = fac.TimeLineDataReader.GetData(code, startDate, endDate);
            for (int i = 0; i < realdataList.Count; i++)
            {
                ITimeLineData r = this.realdataList[i];
                printRealData(r);
            }
        }

        private void printRealData(ITimeLineData r)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < r.Length; i++)
            {
                r.BarPos = i;
                sb.Append(r.ToString()).Append("\r\n");
            }
            tbKLineData.AppendText(sb.ToString());
        }
    }
}