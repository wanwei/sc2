using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.tools
{
    public partial class FormTranslate : Form
    {
        private string srcPath = @"E:\FUTURES\CSV\TICKADJUSTED";
        private string targetPath = @"E:\FUTURES\CSV\DATA";

        private List<CodeInfo> codes;

        public FormTranslate()
        {
            InitializeComponent();
        }

        private List<CodeInfo> GetCodes()
        {
            
            return null;
        }
    }
}
