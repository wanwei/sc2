using com.wer.sc.data;
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
    public class StrategyRunner_History : IStrategyRunner
    {
        private IDataReader dataReader;

        private IStrategy strategy;

        private bool isRunning;

        private StrategyReferedPeriods referedPeriods;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private RealTimeReader_Strategy realTimeReader;

        private StrategyRunnerArguments runnerArgs;

        public StrategyRunner_History(IDataReader dataReader, StrategyRunnerArguments args)
        {
            this.dataReader = dataReader;
            this.runnerArgs = args;
            //this.realTimeReader = new RealTimeReader_Strategy(dataReader, args);
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
            this.referedPeriods = strategy.GetStrategyPeriods();
        }

        public void Run()
        {
            if (isRunning)
                return;
            isRunning = true;

            RealTimeReader_StrategyArguments args = GetRealTimeReaderArgs();
            RealTimeReader_Strategy realTimeReader = new RealTimeReader_Strategy(dataReader, args);
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

        private RealTimeReader_StrategyArguments GetRealTimeReaderArgs()
        {
            RealTimeReader_StrategyArguments args = new RealTimeReader_StrategyArguments();
            args.Code = runnerArgs.Code;
            args.StartDate = runnerArgs.StartDate;
            args.EndDate = runnerArgs.EndDate;

            StrategyReferedPeriods referPeriods = strategy.GetStrategyPeriods();
            if (referedPeriods == null)
            {
                args.IsTickForward = false;
                args.ForwardKLinePeriod = runnerArgs.ForwardKLinePeriod;
                args.ReferedPeriods = new StrategyReferedPeriods();
                args.ReferedPeriods.UsedKLinePeriods.Add(args.ForwardKLinePeriod);
            }
            else
            {
                args.IsTickForward = referedPeriods.UseTickData;
                args.ForwardKLinePeriod = runnerArgs.ForwardKLinePeriod;
                args.ReferedPeriods = referedPeriods;
            }
            return args;
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