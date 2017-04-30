using com.wer.sc.data.market.receiver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.market
{
    public partial class FormRealDataReceiver : Form
    {
        RealDataReceiver receiver;
        public FormRealDataReceiver()
        {
            InitializeComponent();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            MarketFactory fac = new MarketFactory(plugin.MarketType.CnFutures);
            //if (conn.Id == "SIMNOWMOCK")
            //if (conn.Id == "SIMNOW2")
            ConnectionInfo conn = fac.GetMarketDataConnection("SIMNOWMOCK");
            receiver = new RealDataReceiver(plugin.MarketType.CnFutures, conn);            
            receiver.Start();
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {

        }
    }
}
