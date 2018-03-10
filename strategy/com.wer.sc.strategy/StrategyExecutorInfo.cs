using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.codeperiod;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器信息
    /// </summary>
    public class StrategyExecutorInfo : IStrategyExecutorInfo
    {
        private ICodePeriod codePeriod;

        private int totalDayCount;

        private int currentDay;

        private int currentDayIndex = 0;

        private IKLineData currentKLineData;

        private bool isFinished;

        public StrategyExecutorInfo(ICodePeriod codePeriod, int totalDayCount)
        {
            this.codePeriod = codePeriod;
            this.totalDayCount = totalDayCount;
        }

        public ICodePeriod CodePeriod
        {
            get
            {
                return codePeriod;
            }
        }

        public int CurrentDay
        {
            get
            {
                return currentDay;
            }
            set
            {
                this.currentDay = value;
            }
        }

        public int CurrentDayIndex
        {
            get
            {
                return currentDayIndex;
            }
            set
            {
                this.currentDayIndex = value;
            }
        }

        public IKLineData CurrentKLineData
        {
            get
            {
                return currentKLineData;
            }
            set
            {
                currentKLineData = value;
            }
        }

        public bool IsFinished
        {
            get
            {
                return isFinished;
            }
            set
            {
                this.isFinished = value;
            }
        }

        public int TotalDayCount
        {
            get
            {
                return totalDayCount;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CodePeriod).Append(",");
            sb.Append(TotalDayCount).Append(",");
            sb.Append(CurrentDay).Append(",");
            sb.Append(CurrentDayIndex).Append(",");
            sb.Append(IsFinished);
            return sb.ToString();
        }
    }
}
