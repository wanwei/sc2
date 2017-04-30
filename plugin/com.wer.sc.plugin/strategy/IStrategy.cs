using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略接口
    /// </summary>
    public interface IStrategy
    {
        void ModelStart();

        void ModelEnd();

        void OnTick(IRealTimeDataReader currentData);

        void OnBar(IRealTimeDataReader currentData);

        StrategyReferdPeriods GetStrategyPeriods();
    }

    public class StrategyReferdPeriods
    {
        public bool UseTickData = false;

        public bool isReferTimeLineData = false;

        public List<KLinePeriod> UsedKLinePeriods = new List<KLinePeriod>();        
    }
}