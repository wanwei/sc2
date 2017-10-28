using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.strategy
{
    public partial class FormStrategyDescription : Form
    {
        public FormStrategyDescription(IStrategyInfo strategyInfo)
        {
            InitializeComponent();

            this.lbStrategyId.Text = strategyInfo.StrategyID;
            this.lbStrategyName.Text = strategyInfo.StrategyName;
            this.lbStrategyDesc.Text = strategyInfo.StrategyDesc;
        }
    }
}
