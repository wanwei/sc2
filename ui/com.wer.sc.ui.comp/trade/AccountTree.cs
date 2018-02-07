using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.account;
using com.wer.sc.data;

namespace com.wer.sc.ui.comp.trade
{
    public partial class AccountTree : UserControl
    {
        private IAccountManager accountManager;

        private string rootPath;

        public AccountTree()
        {
            InitializeComponent();
        }

        public void Init(string rootPath)
        {
            accountManager = DataCenter.Default.AccountManager;
            this.rootPath = rootPath;
            LoadSubContent(null, rootPath);
        }

        public TreeView AccountTreeView
        {
            get { return this.treeView1; }
        }

        private void LoadSubContent(TreeNode treeNode, string path)
        {
            IList<string> subPaths = accountManager.LoadSubPaths(path);
            for (int i = 0; i < subPaths.Count; i++)
            {
                string subPath = subPaths[i];
                TreeNode newPathNode = AddNewPathNode(treeNode, subPath);
                LoadSubContent(newPathNode, subPath);
            }
            IList<String> accountNames = accountManager.LoadAccountNames(path);
            for (int i = 0; i < accountNames.Count; i++)
            {
                string accountName = accountNames[i];
                AddNewAccountNode(treeNode, accountName);
            }
        }

        private string GetParentPath(TreeNode parentNode)
        {
            if (parentNode == null)
            {
                return rootPath;
            }
            return ((AccountNodeInfo)parentNode.Tag).Path;
        }

        public TreeNode AddNewPathNode(TreeNode parentNode, string subPath)
        {
            string pathName = subPath.Substring(subPath.LastIndexOf('\\') + 1);
            TreeNode subnode;
            if (parentNode == null)
                subnode = this.treeView1.Nodes.Add(pathName);
            else
                subnode = parentNode.Nodes.Add(pathName);
            subnode.Tag = new AccountNodeInfo(subPath,rootPath+"\\"+ subPath, true);
            return subnode;
        }

        public void AddNewAccountNode(TreeNode parentNode, string accountName)
        {
            TreeNodeCollection nodes = parentNode == null ? treeView1.Nodes : parentNode.Nodes;
            TreeNode node = nodes.Add(accountName);
            string path = GetParentPath(parentNode);
            node.Tag = new AccountNodeInfo(path, rootPath + "\\" + path, false);
            node.ForeColor = Color.Red;
        }

        public IAccountManager AccountManager
        {
            get { return accountManager; }
        }

        public string RootPath
        {
            get { return rootPath; }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode currNode = treeView.GetNodeAt(e.X, e.Y);
            if (currNode == null)
            {
                treeView.SelectedNode = null;
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (currNode != null)
                {
                    treeView.SelectedNode = currNode;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (currNode != null)
                {
                    treeView.SelectedNode = currNode;
                }
            }
        }
    }

    public class AccountNodeInfo
    {
        public string FullPath;

        public string Path;

        public bool IsPath;

        public AccountNodeInfo(string path, string fullPath, bool isPath)
        {
            this.Path = path;
            this.FullPath = fullPath;
            this.IsPath = isPath;
        }
    }
}