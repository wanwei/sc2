using com.wer.sc.data.market;
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

namespace com.wer.sc.ui.comp.trade
{
    public partial class FormTrade : Form
    {
        private IPlugin_MarketTrader trader;        

        public FormTrade(IPlugin_MarketTrader trader)
        {
            InitializeComponent();
            this.trader = trader;
            this.trader.OnReturnOrder = OnReturnOrder;
            this.trader.OnReturnTrade = OnReturnTrade;
            this.trader.OnReturnInvestorPosition = OnReturnInvestorPosition; 
        }

        public void OnReturnOrder(object sender, ref OrderInfo order)
        {

        }

        public void OnReturnTrade(object sender, ref TradeInfo trade)
        {

        }

        public void OnReturnInvestorPosition(object sender, ref PositionInfo trade)
        {

        }

        private void btOrderUp_MouseUp(object sender, MouseEventArgs e)
        {
            string code = tbCode.Text.Trim();
            int mount = int.Parse(tbMount.Text.Trim());
            float price = float.Parse(tbPrice.Text.Trim());
            double orderTime = 0;
            OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Open, price, mount, OrderSide.Buy);
            this.trader.SendOrder(order);
        }

        private void btOrderDown_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btClose_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btOrderUp_Click(object sender, EventArgs e)
        {

        }

        private void btOrderDown_Click(object sender, EventArgs e)
        {

        }

        private void btClose_Click(object sender, EventArgs e)
        {

        }
    }
}
