using com.wer.sc.data.update;
using com.wer.sc.data.update2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.check
{
    public partial class FrmTestGenData : Form
    {
        private DataMgr providerDataMgr;

        private IKLineData data;

        public FrmTestGenData()
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

            cbPeriod.Items.Add(new CbItem(0, "秒"));
            cbPeriod.Items.Add(new CbItem(1, "分钟"));
            cbPeriod.Items.Add(new CbItem(2, "小时"));
            cbPeriod.Items.Add(new CbItem(3, "天"));
            cbPeriod.Items.Add(new CbItem(4, "周"));
            cbPeriod.SelectedIndex = 1;
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            tbKLineData.Clear();

            int startDate = int.Parse(tbStartDate.Text);
            int endDate = int.Parse(tbEndDate.Text);
            DataProviderWrap provider = this.providerDataMgr.GetProviders()[cbProvider.SelectedIndex];
            DataReaderFactory fac = new DataReaderFactory(provider.GetDataPath());
            
            List<int> allDates = fac.OpenDateReader.GetAllOpenDates();
            int startIndex = allDates.IndexOf(startDate);
            int endIndex = allDates.IndexOf(endDate);
            List<int> dates = allDates.GetRange(startIndex, endIndex - startIndex + 1);

            String code = tbCode.Text;
            KLinePeriod period = new KLinePeriod((KLineTimeType)Enum.ToObject(typeof(KLineTimeType), cbPeriod.SelectedIndex), int.Parse(tbPeriod.Text));
            List<IKLineData> klineDataList = new List<IKLineData>();
            float lastPrice = -1;

            for (int i = 0; i < dates.Count; i++)
            {
                int openDate = dates[i];
                TickData tickdata = fac.TickDataReader.GetTickData(code, openDate);
                if (tickdata != null)
                {
                    List<double[]> openTimes = provider.GetProvider().GetOpenTime(code, openDate);
                    KLineData klineData = DataTransfer_Tick2KLine.Transfer(tickdata, period, openTimes, lastPrice);
                    klineDataList.Add(klineData);
                    lastPrice = klineData.arr_end[klineData.Length - 1];
                }
            }
            if (klineDataList.Count == 0)
                return;
            this.data = KLineData.Merge(klineDataList);
            Show(data);
        }

        private void Show(IKLineData data)
        {
            int len = 1000;
            len = data.Length < 1000 ? data.Length : len;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                data.BarPos = i;
                sb.Append(data.ToString()).Append("\r\n");
            }
            tbKLineData.AppendText(sb.ToString());
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                MessageBox.Show("还未生成数据");
            }
            SaveFileDialog dialog = new SaveFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                String[] strs = new string[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    data.BarPos = i;
                    strs[i] = data.ToString();
                }
                File.WriteAllLines(dialog.FileName, strs);
            }            
        }
    }

    class CbItem
    {
        int index;

        String text;
        public CbItem(int index, String text)
        {
            this.index = index;
            this.text = text;
        }
        public override String ToString()
        {
            return text;
        }
    }
}
