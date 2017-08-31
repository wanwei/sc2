using com.wer.sc.data;
using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui
{
    public partial class FormForwardSetting : Form
    {
        private ForwardPeriod forwardPeriod;

        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }

            set
            {
                if (value == null)
                    return;
                forwardPeriod = value;
                if (forwardPeriod.IsTickForward)
                {
                    this.cbTimeType.SelectedIndex = 0;
                }
                else
                {
                    SetComboboxSelectedIndex(forwardPeriod.KlineForwardPeriod.PeriodType);
                    this.tbPeriod.Text = forwardPeriod.KlineForwardPeriod.Period.ToString();
                }
            }
        }

        public FormForwardSetting()
        {
            InitializeComponent();

            //this.cbTimeType.Items.Add("TICK");
            this.cbTimeType.Items.Add("秒");
            this.cbTimeType.Items.Add("分钟");
            this.cbTimeType.Items.Add("小时");
            this.cbTimeType.Items.Add("日");
            this.cbTimeType.Items.Add("星期");

            this.cbTimeType.SelectedIndex = 1;
            this.tbPeriod.Text = "1";
        }

        private void cbTimeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbPeriod.Focus();
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void tbPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //bool isTickForward = this.cbTimeType.SelectedIndex == 0;
                //if (isTickForward)
                //    this.forwardPeriod = new ForwardPeriod(true, null);
                //else
                this.forwardPeriod = new ForwardPeriod(false, new KLinePeriod(GetKLineTimeType(), int.Parse(tbPeriod.Text)));

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private KLineTimeType GetKLineTimeType()
        {
            if (this.cbTimeType.SelectedIndex == 0)
                return KLineTimeType.SECOND;
            if (this.cbTimeType.SelectedIndex == 1)
                return KLineTimeType.MINUTE;
            if (this.cbTimeType.SelectedIndex == 2)
                return KLineTimeType.HOUR;
            if (this.cbTimeType.SelectedIndex == 3)
                return KLineTimeType.DAY;
            if (this.cbTimeType.SelectedIndex == 4)
                return KLineTimeType.WEEK;
            return KLineTimeType.MINUTE;
        }

        private void SetComboboxSelectedIndex(KLineTimeType timeType)
        {
            if (timeType == KLineTimeType.SECOND)
                this.cbTimeType.SelectedIndex = 0;
            else if (timeType == KLineTimeType.MINUTE)
                this.cbTimeType.SelectedIndex = 1;
            else if (timeType == KLineTimeType.HOUR)
                this.cbTimeType.SelectedIndex = 2;
            else if (timeType == KLineTimeType.DAY)
                this.cbTimeType.SelectedIndex = 3;
            else if (timeType == KLineTimeType.WEEK)
                this.cbTimeType.SelectedIndex = 4;
        }
    }
}