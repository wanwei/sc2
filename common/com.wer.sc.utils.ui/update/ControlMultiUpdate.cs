using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using com.wer.sc.utils.update;

namespace com.wer.sc.utils.ui.update
{
    /// <summary>
    /// 更新控件，用来执行多个更新
    /// </summary>
    public partial class ControlMultiUpdate : UserControl
    {
        private List<ProgressBar> progressBars = new List<ProgressBar>();

        private List<UpdateExecutor> updateExecutors = new List<UpdateExecutor>();

        private int currentUpdateIndex = 0;

        private IMultiUpdater multiUpdater;

        private ProgressBar CurrentProgressBar
        {
            get
            {
                if (currentUpdateIndex >= progressBars.Count)
                    return null;
                return progressBars[currentUpdateIndex];
            }
        }

        private UpdateExecutor CurrentUpdateExecutor
        {
            get
            {
                if (currentUpdateIndex >= updateExecutors.Count)
                    return null;
                return updateExecutors[currentUpdateIndex];
            }
        }

        public ControlMultiUpdate()
        {
            InitializeComponent();
        }

        private void ControlMultiUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isProceeding)
            {
                DialogResult result = MessageBox.Show("正在执行进程，关闭窗口将停止执行，是否关闭", "确认关闭", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    UpdateExecutor currentExecutor = CurrentUpdateExecutor;
                    if (currentExecutor != null)
                        currentExecutor.Cancel();
                }
            }
        }

        public IMultiUpdater MultiUpdater
        {
            get
            {
                return multiUpdater;
            }

            set
            {
                this.multiUpdater = value;
                if (this.multiUpdater != null)
                    InitWithMultiUpdater(multiUpdater);
            }
        }

        private TableLayoutPanel tableLayoutPanel1;

        private void InitWithMultiUpdater(IMultiUpdater multiUpdater)
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            this.splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);

            tableLayoutPanel1.CellPaint += TableLayoutPanel1_CellPaint;
            tableLayoutPanel1.RowCount = multiUpdater.GetDataUpdaters().Count;
            //this.Height = tableLayoutPanel1.RowCount * 45 + 50;
            this.Height = tableLayoutPanel1.RowCount * 30 + 40;
            for (int i = 0; i < multiUpdater.GetDataUpdaters().Count; i++)
            {
                Label lb = new Label();
                lb.Text = multiUpdater.GetDataUpdaterNames()[i];
                lb.AutoEllipsis = true;
                lb.AutoSize = true;
                lb.Margin = new Padding(2);
                lb.MouseEnter += Lb_MouseEnter;

                tableLayoutPanel1.SetRow(lb, i);
                tableLayoutPanel1.SetColumn(lb, 0);
                tableLayoutPanel1.Controls.Add(lb);

                ProgressBar p = new ProgressBar();
                p.Margin = new Padding(2);
                p.Dock = DockStyle.Fill;
                tableLayoutPanel1.SetRow(p, i);
                tableLayoutPanel1.SetColumn(p, 1);
                tableLayoutPanel1.Controls.Add(p);
                progressBars.Add(p);

                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize, 45F));
            }

            for (int i = 0; i < multiUpdater.GetDataUpdaters().Count; i++)
            {
                IUpdateStepGetter stepGetter = multiUpdater.GetDataUpdaters()[i];
                UpdateExecutor executor = new UpdateExecutor(stepGetter);
                executor.AfterPrepared += Executor_AfterPrepared;
                executor.AfterStepBegin += Executor_AfterStepBegin;
                executor.AfterStepFinished += Executor_AfterStepFinished;
                executor.AfterFinished += Executor_AfterFinished;
                executor.AfterCancelled += Executor_AfterCancelled;
                this.updateExecutors.Add(executor);
            }
        }

        private void TableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // 重绘
            Pen pp = new Pen(Color.Black);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1);
        }

        private void Lb_MouseEnter(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            toolTip1.SetToolTip(lb, lb.Text);
        }

        private bool isProceeding;

        private bool isBindForm;

        private void btStart_Click(object sender, EventArgs e)
        {
            if (isProceeding)
            {
                MessageBox.Show("正在执行进程");
                return;
            }
            isProceeding = true;

            if (!isBindForm)
            {
                Form form = this.ParentForm;// FindForm();
                if (form != null)
                    form.FormClosing += ControlMultiUpdate_FormClosing;
                isBindForm = true;
            }

            InitBeforeUpdate();

            //UpdateExecutor executor = new UpdateExecutor()
            ////用户可以在执行更新前初始化更新器
            //if (BeforeProceedStart != null)
            //    BeforeProceedStart();

            this.lbStatus.Text = "正在准备执行进程";

            Thread thread1 = new Thread(new ThreadStart(ProceedInternal));
            thread1.Start();
        }

        private void InitBeforeUpdate()
        {
            for (int i = 0; i < progressBars.Count; i++)
            {
                progressBars[i].Value = 0;
            }
            currentUpdateIndex = 0;
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            DoCancel();
        }

        private void DoCancel()
        {
            if (!isProceeding)
            {
                MessageBox.Show("没有任何进程在执行");
                return;
            }

            //updateExecutor.Cancel();
            UpdateExecutor currentExecutor = CurrentUpdateExecutor;
            if (currentExecutor != null)
                currentExecutor.Cancel();
            UpdateStatusLabel("开始取消进程");
            LogHelper.Info(GetType(), "开始取消进程");
        }

        private void ProceedInternal()
        {
            LogHelper.Info(GetType(), "更新准备完成");
            ProceedInternal(0);
        }

        private void ProceedInternal(int index)
        {
            //if (index >= multiUpdater.GetDataUpdaters().Count)
            //    return;
            //IUpdateStepGetter stepGetter = multiUpdater.GetDataUpdaters()[index];
            //UpdateExecutor executor = new UpdateExecutor(stepGetter);
            //executor.AfterStepBegin += Executor_AfterStepBegin;
            //executor.AfterStepFinished += Executor_AfterStepFinished;
            //executor.AfterFinished += Executor_AfterFinished;

            UpdateExecutor executor = CurrentUpdateExecutor;
            if (executor == null)
                return;
            executor.Update();
        }

        private void Executor_AfterPrepared(object sender, int totalProgress)
        {
            UpdateMaxProgress(CurrentProgressBar, totalProgress);
            if (AfterPrepared != null)
                AfterPrepared(sender, totalProgress);
        }

        private void Executor_AfterStepBegin(object sender, IStep step)
        {
            UpdateStatusLabel(step.StepDesc);
            if (AfterStepBegin != null)
                AfterStepBegin(sender, step);
        }

        private void Executor_AfterStepFinished(object sender, IStep step)
        {
            UpdateProgressStep(progressBars[currentUpdateIndex], step.ProgressStep);
            if (AfterStepBegin != null)
                AfterStepBegin(sender, step);
        }

        private void Executor_AfterCancelled(object sender, int currentCnt)
        {
            UpdateStatusLabel("进程已经被取消");
            isProceeding = false;
            if (AfterCancelled != null)
                AfterCancelled(sender, currentCnt);
        }

        private void Executor_AfterFinished(object sender, int totalProgress)
        {
            //执行下一个更新
            currentUpdateIndex++;
            if (currentUpdateIndex <= multiUpdater.GetDataUpdaters().Count)
                ProceedInternal(currentUpdateIndex);
            else
            {
                UpdateStatusLabel("更新完毕");
            }
        }

        private void UpdateMaxProgress(ProgressBar progressBar, int max)
        {
            if (progressBar == null)
                return;

            if (progressBar.InvokeRequired)
            {
                UpdateProgressInvokeCallback pi = new UpdateProgressInvokeCallback(this.UpdateMaxProgress);
                this.Invoke(pi, new object[] { progressBar, max });
            }
            else
            {
                progressBar.Maximum = max;//设置最大长度值
                progressBar.Value = 0;//设置当前值
                progressBar.Step = 1;//设置没次增长多少                
            }
        }

        private delegate void UpdateProgressInvokeCallback(ProgressBar progressBar, int step);

        private void UpdateProgressStep(ProgressBar progressBar, int step)
        {
            if (progressBar.InvokeRequired)
            {
                UpdateProgressInvokeCallback pi = new UpdateProgressInvokeCallback(this.UpdateProgressStep);
                this.Invoke(pi, new object[] { progressBar, step });
            }
            else
            {
                progressBar.Value += step;
            }
        }

        private void UpdateStatusLabel(string txt)
        {
            if (this.lbStatus.InvokeRequired)
            {
                UpdateStatusInvokeCallback md = new UpdateStatusInvokeCallback(this.UpdateStatusLabel);
                this.Invoke(md, new object[] { txt });
            }
            else
            {
                this.lbStatus.Text = txt;
            }
        }

        private delegate void UpdateStatusInvokeCallback(String txt);


        /// <summary>
        /// 更新准备完成事件
        /// </summary>
        public event DelegateOnAfterPrepared AfterPrepared;

        //每一步更新完成
        public event DelegateOnStepBegin AfterStepBegin;

        //每一步更新完成
        public event DelegateOnStepFinished AfterStepFinished;

        //取消更新完成
        public event DelegateOnAfterCancelled AfterCancelled;

        //更新完成
        public event DelegateOnAfterFinished AfterFinished;

        //所有更新完成
        public event DelegateOnAfterFinished AfterAllFinished;

        /// <summary>
        /// 更新结束委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="currentCnt"></param>
        public delegate void DelegateOnAfterFinished(Object sender, int totalProgress);
    }
}