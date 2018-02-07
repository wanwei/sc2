using com.wer.sc.data.account;
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
    public partial class FormAccountDetail : Form
    {
        public FormAccountDetail(string accountName, IAccount account)
        {
            InitializeComponent();

            this.lbAccount.Text = accountName;
            this.lbMoney.Text = account.Money.ToString();
            this.lbTime.Text = account.Time.ToString();
            this.lbTradeType.Text = AccountTradeTypeUtils.GetName(account.AccountSetting.TradeType);
            this.lbLateTrading.Text = account.AccountSetting.DelayTime.ToString();
            this.lbLateTickTrading.Text = account.AccountSetting.DelayTick.ToString();

            this.lbSlipType.Text = AccountSlipTypeUtils.GetName(account.AccountSetting.SlipType);
            this.lbSlipMinPrice.Text = account.AccountSetting.SlipMinPrice.ToString();
            this.lbSlipPercent.Text = account.AccountSetting.SlipPerccent.ToString();
            this.lbSlipPrice.Text = account.AccountSetting.SlipPrice.ToString();
        }
    }
}
