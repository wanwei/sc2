using com.wer.sc.data.market;
using com.wer.sc.strategy;
using com.wer.sc.ui.comp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.strategy
{
    public partial class FormStrategyTrader : Form
    {
        private ChartComponent compChart;

        private IStrategyTrader strategyTrader;

        public FormStrategyTrader(ChartComponent compChart, IStrategyTrader strategyTrader)
        {
            InitializeComponent();
            this.compChart = compChart;
            this.strategyTrader = strategyTrader;

            //IList<c this.strategyTrader.GetAllCodes();
            //this.strategyTrader.GetStrategyTrader(
        }

        private void Init()
        {
            if (this.strategyTrader.GetAllCodes().Count == 0)
                return;
            string code = this.strategyTrader.GetAllCodes()[0];
            IStrategyTrader_Code trader = this.strategyTrader.GetStrategyTrader(code);

            IList<TradeInfo> trades = trader.CurrentTradeInfo;
            for (int i = 0; i < trades.Count; i++)
            {

            }
        }
    }
}
