using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public FormTranslate()
        {
            InitializeComponent();
        }


        private List<CodeInfo> GetCodes()
        {
            List<CodeInfo> codes = new List<CodeInfo>();
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            DirectoryInfo[] subdirs = dir.GetDirectories();

            return codes;
        }
    }
}
