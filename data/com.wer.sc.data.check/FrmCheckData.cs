using com.wer.sc.data;
using com.wer.sc.data.reader;
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
    public partial class FrmCheckData : Form
    {
        reader.IDataReader dataReader;

        public FrmCheckData()
        {
            InitializeComponent();
            dataReader = DataReaderFactory.CreateDataReader(@"file:D:\SCDATA\CNFUTURES");
            //fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
        }

        private void btShowData_Click(object sender, EventArgs e)
        {
            TickData data = dataReader.TickDataReader.GetTickData(tbCode.Text.Trim(), int.Parse(tbDate.Text.Trim()));
            tbData.Clear();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                sb.Append(data.ToString() + "\r\n");
            }
            tbData.Text = sb.ToString();
        }
    }
}
