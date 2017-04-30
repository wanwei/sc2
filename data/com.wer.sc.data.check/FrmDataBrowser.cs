using com.wer.sc.data;
using com.wer.sc.data.store;
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
    public partial class FrmDataBrowser : Form
    {
        private KLineData klineData;

        private TickData tickData;

        public FrmDataBrowser()
        {
            InitializeComponent();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbData.Clear();
                tbPath.Clear();
                klineData = null;
                tickData = null;

                String fileName = fileDialog.FileName;
                tbPath.Text = fileName;
                if (fileName.EndsWith("kline"))
                {
                    KLineDataStore store = new KLineDataStore(fileName);
                    klineData = store.LoadAll();
                    int showLen = klineData.Length;
                    showLen = showLen > 5000 ? 5000 : showLen;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < showLen; i++)
                    {
                        klineData.BarPos = i;
                        sb.Append(klineData.ToString()).Append("\r\n");
                    }
                    tbData.Text = sb.ToString();
                }
                else if (fileName.EndsWith("tick"))
                {
                    TickDataStore store = new TickDataStore(fileName);
                    tickData = store.Load();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < tickData.Length; i++)
                    {
                        tickData.BarPos = i;
                        sb.Append(tickData.ToString()).Append("\r\n");
                    }
                    tbData.Text = sb.ToString();
                }
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                String fileName = dialog.FileName;
                if (klineData != null)
                {
                    String[] arr = new string[klineData.Length];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        klineData.BarPos = i;
                        arr[i] = klineData.ToString();
                    }
                    File.WriteAllLines(fileName, arr);
                }
                else if (tickData != null)
                {
                    String[] arr = new string[tickData.Length];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        tickData.BarPos = i;
                        arr[i] = tickData.ToString();
                    }
                    File.WriteAllLines(fileName, arr);
                }
            }
        }
    }
}
