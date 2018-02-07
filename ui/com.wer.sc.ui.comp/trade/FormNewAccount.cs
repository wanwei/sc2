using com.wer.sc.data;
using com.wer.sc.data.account;
using com.wer.sc.utils;
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
    public partial class FormNewAccount : Form
    {
        private string path;

        private string accountName;

        private IAccount account;

        private IAccountManager accountManager;

        public IAccount Account
        {
            get { return account; }
        }

        public string AccountName
        {
            get { return accountName; }
        }

        public FormNewAccount(IAccountManager accountManager, string path)
        {
            InitializeComponent();
            this.accountManager = accountManager;
            this.path = path;
            this.cbSlipType.SelectedIndex = 0;
            this.cbTradeType.SelectedIndex = 1;
            this.tbMoney.Text = "100000";
            DisableAllTradeType();
            DisableAllSlipType();
        }

        private void DisableAllTradeType()
        {
            this.tbLateTrading.Enabled = false;
            this.tbLateTickTrading.Enabled = false;
        }

        private void DisableAllSlipType()
        {
            this.tbSlipMinPrice.Enabled = false;
            this.tbSlipPercent.Enabled = false;
            this.tbSlipPrice.Enabled = false;
        }

        private void tbTradeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableAllTradeType();
            if (cbTradeType.SelectedIndex == 2)
            {
                tbLateTrading.Enabled = true;
            }
            else if (cbTradeType.SelectedIndex == 3)
            {
                tbLateTickTrading.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableAllSlipType();
            if (cbSlipType.SelectedIndex == 1)
            {
                tbSlipMinPrice.Enabled = true;
            }
            else if (cbSlipType.SelectedIndex == 2)
            {
                tbSlipPercent.Enabled = true;
            }
            else if (cbSlipType.SelectedIndex == 3)
            {
                tbSlipPrice.Enabled = true;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            string accountName = tbAccountName.Text.Trim();
            if (accountName == "")
            {
                MessageBox.Show("账号名不能为空");
                return;
            }            
            if (accountManager.Exist(path, accountName))
            {
                MessageBox.Show("账号" + accountName + "已经存在");
                return;
            }

            double money = double.Parse(tbMoney.Text);
            this.account = accountManager.CreateAccount(money);

            RefreshAccountSetting(account.AccountSetting);
            this.accountName = tbAccountName.Text.Trim();
            accountManager.Save(path, accountName, account);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RefreshAccountSetting(AccountSetting accountSetting)
        {
            accountSetting.TradeType = (AccountTradeType)EnumUtils.Parse(typeof(AccountTradeType), cbTradeType.SelectedIndex);
            if (accountSetting.TradeType == AccountTradeType.DELAYTIME)
                accountSetting.DelayTime = double.Parse(tbLateTrading.Text);
            else if (accountSetting.TradeType == AccountTradeType.DELAYTICK)
                accountSetting.DelayTime = double.Parse(tbLateTickTrading.Text);

            accountSetting.SlipType = (AccountSlipType)EnumUtils.Parse(typeof(AccountSlipType), cbSlipType.SelectedIndex);
            if (accountSetting.SlipType == AccountSlipType.MINPRICE)
                accountSetting.SlipMinPrice = int.Parse(tbSlipMinPrice.Text);
            else if (accountSetting.SlipType == AccountSlipType.PERCENT)
                accountSetting.SlipPerccent = double.Parse(tbSlipPercent.Text);
            else if (accountSetting.SlipType == AccountSlipType.PRICE)
                accountSetting.SlipPrice = double.Parse(tbSlipPrice.Text);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
