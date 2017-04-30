using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.updater
{
    public partial class FormDataUpdater3 : Form
    {
        private DataUpdaterPackageConfigInfo package;

        private int currentUpdaterIndex;

        public FormDataUpdater3(DataUpdaterPackageConfigInfo package)
        {
            InitializeComponent();
            this.package = package;

            tableLayoutPanel1.CellPaint += TableLayoutPanel1_CellPaint;
            tableLayoutPanel1.RowCount = package.DataUpdaters.Count;
            this.Height = tableLayoutPanel1.RowCount * 48 + 60;
            for (int i = 0; i < package.DataUpdaters.Count; i++)
            {
                Label lb = new Label();
                lb.Text = package.DataUpdaters[i].Name;
                lb.AutoEllipsis = true;
                lb.AutoSize = true;
                lb.Margin = new Padding(3);
                lb.MouseEnter += Lb_MouseEnter;

                tableLayoutPanel1.SetRow(lb, i);
                tableLayoutPanel1.SetColumn(lb, 0);
                tableLayoutPanel1.Controls.Add(lb);

                ProgressBar p = new ProgressBar();
                p.Margin = new Padding(3);
                p.Dock = DockStyle.Fill;
                tableLayoutPanel1.SetRow(p, i);
                tableLayoutPanel1.SetColumn(p, 1);
                tableLayoutPanel1.Controls.Add(p);

                if (i >= 2)
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            }
        }

        private void TableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // 重绘
            Pen pp = new Pen(Color.Gray);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1);
        }

        private void Lb_MouseEnter(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            toolTip1.SetToolTip(lb, lb.Text);
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            bool isOK = CheckUpdaters();
            if (!isOK)
                return;

            Thread thread1 = new Thread(new ThreadStart(DoUpdate));
            thread1.Start();
            //DataUpdaterConfigInfo config = package.DataUpdaters[0];            
        }

        private void DoUpdate()
        {

        }

        private void DoUpdate(IPlugin_DataUpdater updater)
        {
            UpdateExecutor executor = new UpdateExecutor(updater.UpdateStepGetter);
            executor.Update();
        }

        private bool CheckUpdaters()
        {
            List<string> notLoadedUpdaters = new List<string>();
            for (int i = 0; i < package.DataUpdaters.Count; i++)
            {
                IPlugin_DataUpdater dataUpdater = package.DataUpdaters[i].DataUpdater;
                if (dataUpdater == null)
                {
                    notLoadedUpdaters.Add(package.DataUpdaters[i].Name);
                }
            }
            if (notLoadedUpdaters.Count == 0)
                return true;
            String msg = "";
            for (int i = 0; i < notLoadedUpdaters.Count; i++)
            {
                if (i != 0)
                    msg += ";";
                msg += notLoadedUpdaters[i];
            }
            MessageBox.Show("以下更新器未正确装载：" + msg);
            return false;
        }

        private void btStop_Click(object sender, EventArgs e)
        {

        }
    }

    class UpdateHelper
    {
        private List<SingleUpdateHelper> helpers = new List<SingleUpdateHelper>();

        private int currentUpdater = 0;

        public void Update()
        {
            if (currentUpdater >= helpers.Count)
                return;

        }

        private void UpdateInternal()
        {
            if (currentUpdater >= helpers.Count)
                return;
            SingleUpdateHelper currentHelper = helpers[currentUpdater];
        }

        internal List<SingleUpdateHelper> Helpers
        {
            get
            {
                return helpers;
            }
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    class SingleUpdateHelper
    {
        private IPlugin_DataUpdater dataUpdater;

        private ProgressBar progressBar1;

        private UpdateExecutor executor;

        private Control statusControl;

        public UpdateExecutor Executor
        {
            get
            {
                return executor;
            }
        }

        public SingleUpdateHelper(IPlugin_DataUpdater dataUpdater, ProgressBar progress)
        {
            this.dataUpdater = dataUpdater;
            this.progressBar1 = progress;
            this.executor = new UpdateExecutor(dataUpdater.UpdateStepGetter);
            this.executor.AfterFinished += Executor_AfterFinished;
            this.executor.AfterStepFinished += Executor_AfterStepFinished;
            this.executor.AfterStepBegin += Executor_AfterStepBegin;
        }

        private void Executor_AfterStepFinished(object sender, IStep step)
        {
            
        }

        private void Executor_AfterStepBegin(object sender, IStep step)
        {

        }

        private void Executor_AfterFinished(object sender, int totalProgress)
        {

        }

        public void DoUpdate()
        {

        }

        public void Cancel()
        {

        }

        private void UpdateMaxProgress(int max)
        {
            if (progressBar1.InvokeRequired)
            {
                UpdateProgressInvokeCallback pi = new UpdateProgressInvokeCallback(this.UpdateMaxProgress);
                this.progressBar1.FindForm().Invoke(pi, max);
            }
            else
            {
                progressBar1.Maximum = max;//设置最大长度值
                progressBar1.Value = 0;//设置当前值
                progressBar1.Step = 1;//设置没次增长多少                
            }
        }

        private delegate void UpdateProgressInvokeCallback(int max);

        private void UpdateProgressStep(int step)
        {
            if (progressBar1.InvokeRequired)
            {
                UpdateProgressInvokeCallback pi = new UpdateProgressInvokeCallback(this.UpdateProgressStep);
                this.progressBar1.FindForm().Invoke(pi, step);
            }
            else
            {
                progressBar1.Value += step;
            }
        }

        private void UpdateStatusLabel(string txt)
        {
            if (this.statusControl.InvokeRequired)
            {
                UpdateStatusInvokeCallback md = new UpdateStatusInvokeCallback(this.UpdateStatusLabel);
                this.statusControl.FindForm().Invoke(md, new object[] { txt });
            }
            else
            {
                this.statusControl.Text = txt;
            }
        }

        private delegate void UpdateStatusInvokeCallback(String txt);

    }
}