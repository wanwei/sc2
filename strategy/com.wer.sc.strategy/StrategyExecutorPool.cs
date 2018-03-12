using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyExecutorPool : IStrategyExecutorPool
    {
        private object lockExecutingListObj = new object();

        private object lockExecutingObj = new object();

        private int threadCount = 5;

        private int maxExecutorCount = 10;

        private bool isRunning;

        private Queue<IStrategyExecutor_Single> waitingQueue = new Queue<IStrategyExecutor_Single>();

        private List<IStrategyExecutor_Single> executingExecutors = new List<IStrategyExecutor_Single>();

        public int ThreadCount
        {
            get
            {
                return threadCount;
            }

            set
            {
                this.threadCount = value;
                ThreadPool.SetMaxThreads(threadCount, threadCount);
            }
        }

        public int MaxExecutorCount
        {
            get
            {
                return maxExecutorCount;
            }
            set
            {
                maxExecutorCount = value;
            }
        }

        public IList<IStrategyExecutor_Single> ExecutingExecutors
        {
            get
            {
                return executingExecutors;
            }
        }

        public event StrategyStart OnStrategyStart;

        public event StrategyDayFinished OnStrategyDayFinished;

        public event StrategyFinished OnStrategyFinished;

        public event PoolExecuteFinished OnPoolFinished;

        public void Queue(IStrategyExecutor_Single strategyExecutor)
        {
            this.waitingQueue.Enqueue(strategyExecutor);
        }

        public bool IsRunning()
        {
            return isRunning;
        }

        public void Execute()
        {
            if (isRunning)
                throw new ApplicationException("正在执行策略，不能加入新策略");

            lock (lockExecutingObj)
            {
                if (isRunning)
                    throw new ApplicationException("正在执行策略，不能加入新策略");

                isRunning = true;
                try
                {
                    ThreadPool.SetMaxThreads(threadCount, threadCount);
                    while (waitingQueue.Count > 0)
                    {
                        if (executingExecutors.Count >= maxExecutorCount)
                            continue;
                        lock (lockExecutingListObj)
                        {
                            IStrategyExecutor_Single executor = waitingQueue.Dequeue();
                            executor.OnStart += Executor_OnStart;
                            executor.OnDayFinished += Executor_OnDayFinished;
                            executor.OnFinished += Executor_OnFinished;

                            ThreadPool.QueueUserWorkItem(new StrategyExecutorRuner(executor).Run, null);
                            ExecutingExecutors.Add(executor);
                        }
                    }
                    while (executingExecutors.Count > 0)
                    {

                    }
                    if (OnPoolFinished != null)
                        OnPoolFinished(this);
                }
                finally
                {
                    isRunning = false;
                }
            }
        }

        private void Executor_OnFinished(object sender, StrategyFinishedArguments arguments)
        {
            if (OnStrategyFinished != null)
                OnStrategyFinished(sender, arguments);
            if (sender != null && sender is IStrategyExecutor_Single)
            {
                IStrategyExecutor_Single executor = ((IStrategyExecutor_Single)sender);
                executor.OnStart -= Executor_OnStart;
                executor.OnDayFinished -= Executor_OnDayFinished;
                executor.OnFinished -= Executor_OnFinished;
                executingExecutors.Remove(executor);
            }
        }

        private void Executor_OnDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            if (OnStrategyDayFinished != null)
                OnStrategyDayFinished(sender, arguments);
        }

        private void Executor_OnStart(object sender, StrategyStartArguments arguments)
        {
            if (OnStrategyStart != null)
                OnStrategyStart(sender, arguments);
        }

        public void Stop()
        {

        }
    }

    class StrategyExecutorRuner
    {
        private IStrategyExecutor_Single executor;

        public StrategyExecutorRuner(IStrategyExecutor_Single executor)
        {
            this.executor = executor;
        }


        public void Run(object state)
        {
            this.executor.Run();
        }
    }
}