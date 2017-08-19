using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.utils.param;

namespace com.wer.sc.comp.param
{
    public partial class CompParameters : UserControl
    {
        private IParameters parameter;

        public CompParameters()
        {
            InitializeComponent();
        }

        public IParameters Parameter
        {
            get
            {
                return parameter;
            }
        }

        public void Init()
        {
            for (int i = 0; i < parameter.Count; i++)
            {
                if (i != 0)
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                
            }
        }
    }
}
