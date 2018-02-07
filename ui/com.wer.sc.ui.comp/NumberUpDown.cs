using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp
{
    public partial class NumberUpDown : UserControl
    {
        private bool isInputState = false;

        private double minValue = double.MinValue;

        private double maxValue = double.MaxValue;

        private double minPeriod = 1;

        public double MinPeriod
        {
            get
            {
                return minPeriod;
            }

            set
            {
                minPeriod = value;
            }
        }

        private double value;
        public double Value
        {
            get
            {
                if (!IsInputState)
                    return 0;
                return value;
            }

            set
            {
                this.value = value;                
                ResetItems();
                this.domainUpDown1.Text = value.ToString();
            }
        }

        private string normalText;

        public string NormalText
        {
            get
            {
                return normalText;
            }
            set
            {
                normalText = value;
                this.IsInputState = false;
            }
        }

        public double MinValue
        {
            get
            {
                return minValue;
            }

            set
            {
                minValue = value;
            }
        }

        public double MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                maxValue = value;
            }
        }

        public bool IsInputState
        {
            get
            {
                return isInputState;
            }

            set
            {
                ChangeInputState(value);
            }
        }

        public DomainUpDown InputControl
        {
            get { return domainUpDown1; }
        }

        public NumberUpDown()
        {
            InitializeComponent();
            this.MinPeriod = 1;
            this.Value = 0;
        }

        private void ChangeInputState(bool inputState)
        {
            if (inputState)
            {
                this.domainUpDown1.BackColor = Color.White;
                this.Value = value;
            }
            else
            {
                this.domainUpDown1.BackColor = Color.LightGray;
                this.domainUpDown1.Text = normalText;
            }
            this.isInputState = inputState;
            if (OnStateChange != null)
                OnStateChange(this, IsInputState);
        }

        private void ResetItems()
        {
            this.domainUpDown1.Items.Clear();
            AddItem(value + minPeriod);
            AddItem(value);
            AddItem(value - minPeriod);
            this.domainUpDown1.SelectedIndex = 1;
        }

        private void AddItem(double value)
        {
            if (value < MinValue || value > MaxValue)
                return;
            this.domainUpDown1.Items.Add(value);
        }

        private void domainUpDown1_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeInputState(true);
            this.Value = value;
            this.domainUpDown1.Text = value.ToString();
            this.domainUpDown1.Select(0, domainUpDown1.Text.Length);
        }

        private void domainUpDown1_Enter(object sender, EventArgs e)
        {
            this.domainUpDown1.Select(0, domainUpDown1.Text.Length);
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            //if (!IsInputState)
            //    return;
            string txt = domainUpDown1.Text;
            double result;
            if (double.TryParse(txt, out result))
            {
                this.Value = result;
                //if (!isInputState)
                //    ChangeInputState(true);
            }
        }

        public event DelegateOnStateChange OnStateChange;
    }

    public delegate void DelegateOnStateChange(object sender, bool state);
}
