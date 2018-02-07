using com.wer.sc.data.account;
using com.wer.sc.data.market;
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
    public partial class FormAccountAna : Form
    {
        private IAccount account;

        private ChartComponent chartComponent;

        public FormAccountAna(IAccount account, ChartComponent chartComponent)
        {
            InitializeComponent();
            this.account = account;
            this.chartComponent = chartComponent;
            this.Init();
        }

        private void Init()
        {
            IList<TradeInfo> trades = account.CurrentTradeInfo;
            for (int i = 0; i < trades.Count; i++)
            {
                TradeInfo trade = trades[i];
                AddTradeToGrid(trade);
            }            
        }

        private void AddTradeToGrid(TradeInfo trade)
        {
            int rowNum = this.gridTrade.Rows.Add();
            DataGridViewRow row = this.gridTrade.Rows[rowNum];

            row.Cells[columnTradeID.Name].Value = trade.TradeID;
            row.Cells[columnTradeCode.Name].Value = trade.InstrumentID;
            row.Cells[columnTradeTime.Name].Value = trade.Time;
            row.Cells[columnTradePrice.Name].Value = trade.Price;
            row.Cells[columnTradeMount.Name].Value = trade.Qty;
            row.Cells[columnTradeDirection.Name].Value = trade.Side;
            row.Cells[columnTradeOpenClose.Name].Value = trade.OpenClose;
        }

        private void gridTrade_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.gridTrade.SelectedRows.Count == 0)
                return;
            DataGridViewRow row = this.gridTrade.SelectedRows[0];
            string code = (string)row.Cells[columnTradeCode.Name].Value;
            double time = (double)row.Cells[columnTradeTime.Name].Value;
            chartComponent.Controller.Change(code, time);
        }
    }
}
