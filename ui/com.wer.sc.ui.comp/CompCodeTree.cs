using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data;
using com.wer.sc.data.reader;

namespace com.wer.sc.ui.comp
{
    public partial class CompCodeTree : UserControl
    {
        private ICodeReader codeReader;

        private bool multiSelect;

        private bool selectCatelog;

        private HashSet<string> set_Codes = new HashSet<string>();

        private HashSet<string> set_Catelogs = new HashSet<string>();

        public CompCodeTree()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.codeReader = DataCenter.Default.DataReader.CodeReader;
            IList<string> catelogs = codeReader.GetAllCatelogs();
            for (int i = 0; i < catelogs.Count; i++)
            {
                string catelog = catelogs[i];
                AddCatelog(null, catelog);
            }
        }

        private void Clear()
        {
            this.treeView1.Nodes.Clear();
        }

        private void AddCatelog(TreeNode treeNode, string catelog)
        {
            TreeNode subNode;
            if (treeNode == null)
                subNode = this.treeView1.Nodes.Add(catelog);
            else
                subNode = treeNode.Nodes.Add(catelog);
            subNode.Tag = new CodeTreeNodeObj(true, catelog);
            if (!selectCatelog)
            {
                List<string> codes = codeReader.GetCodesByCatelog(catelog);
                for (int i = 0; i < codes.Count; i++)
                {
                    string code = codes[i];
                    TreeNode node = subNode.Nodes.Add(code);
                    node.Tag = new CodeTreeNodeObj(false, code);
                }
            }
        }

        public bool MultiSelect
        {
            get
            {
                return multiSelect;
            }

            set
            {
                if (multiSelect != value)
                {
                    multiSelect = value;
                    this.treeView1.CheckBoxes = value;
                    set_Catelogs.Clear();
                    set_Codes.Clear();
                }
            }
        }

        public bool SelectCatelog
        {
            get
            {
                return selectCatelog;
            }

            set
            {
                if (value != selectCatelog)
                {
                    selectCatelog = value;
                    this.Clear();
                    this.Init();
                }
            }
        }

        public List<string> SelectedCatelogs
        {
            get
            {
                if (multiSelect)
                {
                    List<string> selectedCatelogs = new List<string>();
                    selectedCatelogs.AddRange(set_Catelogs);
                    selectedCatelogs.Sort();
                    return selectedCatelogs;
                }
                else
                {
                    //if (!selectCatelog)
                    //    return null;
                    TreeNode node = this.treeView1.SelectedNode;
                    if (node == null)
                        return null;
                    CodeTreeNodeObj obj = (CodeTreeNodeObj)node.Tag;
                    if (!obj.IsCatelog)
                        return null;
                    List<string> selectedCatelogs = new List<string>();
                    selectedCatelogs.Add(obj.Code);
                    return selectedCatelogs;
                }
            }
        }

        public List<string> SelectedCodes
        {
            get
            {
                if (multiSelect)
                {
                    List<string> selectedCodes = new List<string>();
                    selectedCodes.AddRange(set_Codes);
                    selectedCodes.Sort();
                    return selectedCodes;
                }
                else
                {
                    //if (!selectCatelog)
                    //    return null;
                    TreeNode node = this.treeView1.SelectedNode;
                    if (node == null)
                        return null;
                    CodeTreeNodeObj obj = (CodeTreeNodeObj)node.Tag;
                    if (obj.IsCatelog)
                        return null;
                    List<string> selectedCodes = new List<string>();
                    selectedCodes.Add(obj.Code);
                    return selectedCodes;
                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            object obj = e.Node.Tag;
            if (obj is CodeTreeNodeObj)
            {
                CodeTreeNodeObj codeObj = (CodeTreeNodeObj)obj;
                if (codeObj.IsCatelog)
                {
                    if (e.Node.Checked)
                        set_Catelogs.Add(codeObj.Code);
                    else
                        set_Catelogs.Remove(codeObj.Code);
                }
                else
                {
                    if (e.Node.Checked)
                        set_Codes.Add(codeObj.Code);
                    else
                        set_Codes.Remove(codeObj.Code);
                }
            }
        }
    }

    class CodeTreeNodeObj
    {
        public bool IsCatelog;

        public string Code;

        public CodeTreeNodeObj(bool isCatelog, string code)
        {
            this.IsCatelog = isCatelog;
            this.Code = code;
        }
    }
}