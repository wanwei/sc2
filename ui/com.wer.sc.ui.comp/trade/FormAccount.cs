using com.wer.sc.data;
using com.wer.sc.data.account;
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
    public partial class FormAccount : Form
    {
        public const string PATH_MOCK = "模拟交易";

        public const string PATH_SIMU = "策略交易";

        private IAccountManager accountManager;

        private IAccount selectedAccount;

        private string selectedAccountName;

        private string selectedAccountPath;

        public IAccount SelectedAccount
        {
            get { return selectedAccount; }
        }

        public string SelectedAccountName
        {
            get { return selectedAccountName; }
        }

        public string SelectedAccountPath
        {
            get { return selectedAccountPath; }
        }

        public FormAccount(IAccountManager accountManager, string path)
        {
            InitializeComponent();
            this.accountManager = accountManager;
            this.accountTree1.Init(path);
            this.accountTree1.AccountTreeView.MouseDoubleClick += AccountTreeView_MouseDoubleClick;
        }

        private void AccountTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DoSelectAccount();
        }

        private void itemNewAccount_Click(object sender, EventArgs e)
        {
            string currentPath = GetCurrentPath();
            FormNewAccount frmNewAccount = new FormNewAccount(accountManager, currentPath);
            DialogResult result = frmNewAccount.ShowDialog();
            if (result == DialogResult.OK)
            {
                TreeNode node = GetSelectedTreeNode();
                this.accountTree1.AddNewAccountNode(node, frmNewAccount.AccountName);
            }
        }

        private string GetCurrentPath()
        {
            TreeNode node = GetSelectedTreeNode();
            string path = "";
            if (node == null)
            {
                path = this.accountTree1.RootPath;
            }
            else
            {
                AccountNodeInfo nodeInfo = (AccountNodeInfo)node.Tag;
                path = nodeInfo.Path;
            }

            return path;
        }

        private TreeNode GetSelectedTreeNode()
        {
            return this.accountTree1.AccountTreeView.SelectedNode;
        }

        private void itemNewPath_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = GetSelectedTreeNode();
            string currentPath = GetCurrentPath();
            FormNewAccountPath formNewPath = new FormNewAccountPath(accountManager, currentPath);
            DialogResult result = formNewPath.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (formNewPath.PathCreated)
                {
                    string path = currentPath + "\\" + formNewPath.Path;
                    TreeNode newNode = accountTree1.AddNewPathNode(selectedNode, path);
                    accountTree1.AccountTreeView.SelectedNode = newNode;
                }
            }
        }

        private void itemDelete_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = accountTree1.AccountTreeView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("没有选中任何账户或目录");
                return;
            }

            AccountNodeInfo nodeInfo = (AccountNodeInfo)selectedNode.Tag;
            string path = GetCurrentPath();
            if (nodeInfo.IsPath)
            {
                accountManager.DeleteAccountPath(path);
            }
            else
            {
                accountManager.DeleteAccount(path, selectedNode.Text);
            }
            selectedNode.Remove();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            DoSelectAccount();
        }

        private void DoSelectAccount()
        {
            TreeNode selectedNode = accountTree1.AccountTreeView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("没有选中任何账户");
                return;
            }

            AccountNodeInfo nodeInfo = (AccountNodeInfo)selectedNode.Tag;
            if (nodeInfo.IsPath)
            {
                MessageBox.Show("选中的是目录");
                return;
            }
            string path = GetCurrentPath();
            this.selectedAccountPath = path;
            this.selectedAccountName = selectedNode.Text;
            this.selectedAccount = accountManager.Load(SelectedAccountPath, SelectedAccountName);
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void itemAccountDetail_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = accountTree1.AccountTreeView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("没有选中任何账户");
                return;
            }
            AccountNodeInfo nodeInfo = (AccountNodeInfo)selectedNode.Tag;
            if (nodeInfo.IsPath)
            {
                MessageBox.Show("选中的是目录");
                return;
            }

            string path = GetCurrentPath();
            string name = selectedNode.Text;
            IAccount account = accountManager.Load(path, name);
            FormAccountDetail formDetail = new FormAccountDetail(name, account);
            formDetail.ShowDialog();
        }
    }
}
