using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common
{
    /// <summary>
    /// 各种均线
    /// 决定均线的参数：引用的K线周期、MA的周期、是否记录在主周期上
    /// </summary>
    public class Strategy_Ma : StrategyAbstract
    {
        private KLinePeriod[] klinePeriod;

        private int[] maPeriod;

        private bool isRecordInMainPeriod;



        /// <summary>
        /// 得到指定周期的MA参数
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <param name="maPeriod"></param>
        /// <returns></returns>
        public IList<float> GetParameter(KLinePeriod klinePeriod, int maPeriod)
        {
            return null;
        }

        public override void OnStrategyStart(object sender, StrategyOnStartArgument argument)
        {
            
        }

        public override void OnStrategyEnd(object sender, StrategyOnEndArgument argument)
        {
            
        }

        public override void OnBar(object sender, StrategyOnBarArgument currentData)
        {
            
        }

        public override void OnTick(object sender, StrategyOnTickArgument currentData)
        {
            
        }
    }
}