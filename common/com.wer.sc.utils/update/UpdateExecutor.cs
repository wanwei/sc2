using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.update
{
    /// <summary>
    /// 更新执行器
    /// </summary>
    public class UpdateExecutor
    {
        private Object tag;

        private IUpdateHelper updateProceed;

        private bool isCancel = false;

        public object Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
            }
        }

        public UpdateExecutor(IUpdateHelper updateProceed)
        {
            this.updateProceed = updateProceed;
        }

        public void Update()
        {
            if (updateProceed == null)
                return;
            int currentCnt = 0;
            int totalProgressStep = 0;
            if (BeforePrepared != null)
            {
                BeforePrepared(this);
            }
            List<IStep> steps = updateProceed.GetSteps();
            for (int i = 0; i < steps.Count; i++)
            {
                totalProgressStep += steps[i].ProgressStep;
            }

            if (AfterPrepared != null)
            {
                AfterPrepared(this, totalProgressStep);
            }

            for (int i = 0; i < steps.Count; i++)
            {
                if (isCancel)
                {
                    if (AfterCancelled != null)
                        AfterCancelled(this, currentCnt);
                    return;
                }
                IStep step = steps[i];

                if (AfterStepBegin != null)
                {
                    AfterStepBegin(this, step);
                }

                step.Proceed();
                if (AfterStepFinished != null)
                    AfterStepFinished(this, step);

                currentCnt += step.ProgressStep;
            }
            if (isCancel)
            {
                if (AfterCancelled != null)
                    AfterCancelled(this, currentCnt);
                return;
            }
            if (AfterFinished != null)
                AfterFinished(this, currentCnt);
        }

        public void Cancel()
        {
            isCancel = true;
        }

        public event DelegateOnBeforePrepared BeforePrepared;

        /// <summary>
        /// 更新准备完成事件
        /// </summary>
        public event DelegateOnAfterPrepared AfterPrepared;

        //每一步更新完成
        public event DelegateOnStepBegin AfterStepBegin;

        //每一步更新完成
        public event DelegateOnStepFinished AfterStepFinished;

        //所有更新完成
        public event DelegateOnAfterFinished AfterFinished;

        //取消更新完成
        public event DelegateOnAfterCancelled AfterCancelled;
    }

    /// <summary>
    /// 更新准备阶段开始委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="totalProgress"></param>
    public delegate void DelegateOnBeforePrepared(Object sender);

    /// <summary>
    /// 更新准备阶段完成委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="totalProgress"></param>
    public delegate void DelegateOnAfterPrepared(Object sender, int totalProgress);

    /// <summary>
    /// 一个更新步骤开始委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="stepDesc"></param>
    public delegate void DelegateOnStepBegin(Object sender, IStep step);

    /// <summary>
    /// 一个更新步骤结束委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="stepProgress"></param>
    public delegate void DelegateOnStepFinished(Object sender, IStep step);

    /// <summary>
    /// 更新结束委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="currentCnt"></param>
    public delegate void DelegateOnAfterFinished(Object sender, int totalProgress);

    /// <summary>
    /// 更新被取消委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="currentCnt"></param>
    public delegate void DelegateOnAfterCancelled(Object sender, int currentCnt);
}