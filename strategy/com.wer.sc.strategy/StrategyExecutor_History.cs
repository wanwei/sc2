using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using com.wer.sc.strategy.realtimereader;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器
    /// 策略执行前进周期：tick或者K线
    /// 
    /// </summary>
    public class StrategyExecutor_History : IStrategyExecutor
    {
        private IStrategy strategy;

        private bool isRunning;

        private StrategyReferedPeriods referedPeriods;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private IDataPackage dataPackage;

        private ForwardPeriod forwardPeriod;

        private StrategyHelper strategyHelper;

        public StrategyExecutor_History(IDataPackage dataPackage, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod) : this(dataPackage, referedPeriods, forwardPeriod, new StrategyHelper(null))
        {

        }

        public StrategyExecutor_History(IDataPackage dataPackage, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod, StrategyHelper strategyHelper)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
            this.strategyHelper = strategyHelper;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
            this.strategy.StrategyHelper = strategyHelper;
            StrategyReferedPeriods rPeriods = strategy.GetStrategyPeriods();
            if (rPeriods != null)
                this.referedPeriods = rPeriods;
        }

        private object lockObj = new object();

        public void Run()
        {
            lock (lockObj)
            {
                if (isRunning)
                    return;
                isRunning = true;

                RealTimeReader_Strategy realTimeReader = new RealTimeReader_Strategy(dataPackage, referedPeriods, forwardPeriod);
                realTimeReader.OnBar += RealTimeReader_OnBar;
                realTimeReader.OnTick += RealTimeReader_OnTick;

                //策略执行前操作
                try
                {
                    this.strategy.StrategyStart();
                }
                catch (Exception e)
                {
                    LogHelper.Warn(GetType(), e);
                }

                if (forwardPeriod.IsTickForward)
                    RealTimeReader_OnTick(realTimeReader, realTimeReader.GetTickData(), 0);
                else
                    RealTimeReader_OnBar(realTimeReader, realTimeReader.GetKLineData(), 0);

                //执行策略
                while (!realTimeReader.IsEnd)
                {
                    try
                    {
                        realTimeReader.Forward();
                    }
                    catch (Exception e)
                    {
                        LogHelper.Warn(GetType(), e);
                    }
                }

                //策略执行完毕
                try
                {
                    this.strategy.StrategyEnd();
                }
                catch (Exception e)
                {
                    LogHelper.Warn(GetType(), e);
                }
            }
        }

        private void RealTimeReader_OnTick(object sender, ITickData tickData, int index)
        {
            this.strategy.OnTick((IRealTimeDataReader)sender);
        }

        private void RealTimeReader_OnBar(object sender, IKLineData klineData, int index)
        {
            this.strategy.OnBar((IRealTimeDataReader)sender);
        }
    }
}