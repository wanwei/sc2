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

namespace com.wer.sc.ui.comp.strategy
{
    public partial class FormStrategyDescription : Form
    {
        public FormStrategyDescription(IStrategyData strategyData)
        {
            InitializeComponent();

            this.lbStrategyId.Text = strategyData.StrategyInfo.StrategyID;
            this.lbStrategyType.Text = strategyData.StrategyInfo.StrategyClassType.ToString();
            this.lbStrategyName.Text = strategyData.StrategyInfo.StrategyName;
            this.lbStrategyDesc.Text = strategyData.StrategyInfo.StrategyDesc;
            this.lbAssembly.Text = strategyData.StrategyInfo.StrategyAssembly.AssemblyName;
            this.lbAssemblyPath.Text = strategyData.StrategyInfo.StrategyAssembly.FullPath;
        }
    }
}
