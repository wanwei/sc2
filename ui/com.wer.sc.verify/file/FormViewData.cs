using com.wer.sc.data;
using com.wer.sc.data.store.file;
using com.wer.sc.data.utils;
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

namespace com.wer.sc.verify
{
    public partial class FormViewData : Form
    {
        public FormViewData()
        {
            InitializeComponent();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                if (fileName.ToLower().EndsWith("tick"))
                {
                    TickDataStore_File_Single store = new TickDataStore_File_Single(fileName);
                    TickData tickData = store.Load();
                    tbContent.Clear();

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < tickData.Length; i++)
                    {
                        sb.AppendLine(tickData.GetBar(i).ToString());
                    }
                    tbContent.AppendText(sb.ToString());
                }
                else if (fileName.ToLower().EndsWith("kline"))
                {
                    KLineDataStore_File_Single store = new KLineDataStore_File_Single(fileName);
                    IKLineData tickData = store.LoadAll();
                    tbContent.Clear();

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < tickData.Length; i++)
                    {
                        sb.AppendLine(tickData.GetBar(i).ToString());
                    }
                    tbContent.AppendText(sb.ToString());
                }
                else if (fileName.ToLower().EndsWith("csv"))
                {
                    tbContent.Text = File.ReadAllText(fileName);
                }
                else
                {
                    MessageBox.Show("不支持的格式");
                    return;
                }
                tbFileName.Text = fileName;
            }
        }
    }
}
