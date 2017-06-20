using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.strategy.realtimereader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器
    /// </summary>
    public class StrategyRunner_History : IStrategyRunner
    {
        private IStrategy strategy;

        private bool isRunning;

        private StrategyReferedPeriods referedPeriods;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();        

        public StrategyRunner_History(string code, int startDate, int endDate)
        {

        }

        public void PrepareData()
        {
            //StrategyReferdPeriods referedPeriods = strategy.GetStrategyPeriods();
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
            this.referedPeriods = strategy.GetStrategyPeriods();
        }

        public void Run()
        {
            if (isRunning)
            {
                return;
            }
            isRunning = true;

            RealTimeReader_Strategy realTimeReader = null;
            realTimeReader.OnBar += RealTimeReader_OnBar;
            realTimeReader.OnTick += RealTimeReader_OnTick;

            while (!realTimeReader.IsEnd)
            {
                realTimeReader.Forward();
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