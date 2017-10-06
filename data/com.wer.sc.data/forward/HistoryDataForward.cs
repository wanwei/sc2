using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class HistoryDataForward : IHistoryDataForward
    {
        public const double TIME_DAYEND = 0;

        public const double TIME_END = -1;

        private IDataReader dataReader;

        private double time;

        private IList<string> codes;

        private List<HistoryDataForward_Code> forwardList = new List<HistoryDataForward_Code>();

        private Dictionary<string, HistoryDataForward_Code> dic_Code_Forward = new Dictionary<string, HistoryDataForward_Code>();

        private HistoryDataForwardArguments args;

        private ForwardPeriod forwardPeriod;

        private IList<int> tradingDays;

        private Dictionary<string, double> dic_Code_NextTime = new Dictionary<string, double>();

        public HistoryDataForward(IDataReader dataReader, IList<string> codes, HistoryDataForwardArguments args)
        {
            this.dataReader = dataReader;
            this.codes = codes;
            this.args = args;
            //this.tradingDays = args.
            this.forwardPeriod = new ForwardPeriod(args.IsTickForward, args.ForwardKLinePeriod);
            for (int i = 0; i < codes.Count; i++)
            {
                string code = codes[i];
                HistoryDataForward_Code forward = new HistoryDataForward_Code(dataReader, code, args);                
                dic_Code_Forward.Add(code, forward);
                forwardList.Add(forward);
                forward.OnBar += Forward_OnBar;
                forward.OnTick += Forward_OnTick;
                dic_Code_NextTime.Add(code, forward.Time);
            }
        }

        private void Forward_OnTick(object sender, ITickData tickData, int index)
        {
            if (OnTick != null)
                OnTick(sender, tickData, index);
        }

        private void Forward_OnBar(object sender, IKLineData klineData, int index)
        {
            if (OnBar != null)
                OnBar(sender, klineData, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Forward()
        {
            if (forwardPeriod.IsTickForward)
            {
                return Forward_Tick();
            }
            else
            {
                return Forward_KLine();
            }
        }

        private bool Forward_Tick()
        {
            double nextTime = GetNextTime(dic_Code_NextTime);
            if (nextTime < 0)
                return false;
            for (int i = 0; i < codes.Count; i++)
            {
                double nextTime_Code = dic_Code_NextTime[codes[i]];
                if (nextTime == nextTime_Code)
                {
                    forwardList[i].Forward();
                }
            }
            return true;
        }

        private double GetNextTime(Dictionary<string, double> dic_Code_NextTime)
        {
            return -1;
        }

        private double GetNextTime(HistoryDataForward_Code forward_Code)
        {
            ITickData tickData = forward_Code.GetTickData();
            if (tickData == null)
                return TIME_END;
            int nextBarPos = tickData.BarPos;
            
            return -1;
        }

        private bool Forward_KLine()
        {
            bool isKLineEnd = true;
            for (int i = 0; i < forwardList.Count; i++)
            {
                bool isCurrentEnd = forwardList[i].Forward();
                if (!isCurrentEnd)
                    isKLineEnd = false;
            }
            return isKLineEnd;
        }

        /// <summary>
        /// 是否不能再前进了
        /// </summary>
        public bool IsEnd
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 是否到一天的结束
        /// </summary>
        public bool IsDayEnd
        {
            get
            {
                return false;
            }
        }

        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        /// <summary>
        /// 得到前进时包含的所有code
        /// </summary>
        /// <returns></returns>
        public IList<string> GetAllCodes()
        {
            return codes;
        }

        /// <summary>
        /// 得到指定的
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IHistoryDataForward_Code GetHistoryDataForward(string code)
        {
            HistoryDataForward_Code value = null;
            dic_Code_Forward.TryGetValue(code, out value);
            return value;
        }

        public IRealTimeDataReader_Code GetRealTimeData(string code)
        {
            return GetHistoryDataForward(code);
        }

        /// <summary>
        /// 接收到了tick数据触发该响应
        /// 按照AllCodes里合约的顺序依次响应该事件
        /// </summary>
        public event DelegateOnTick OnTick;

        /// <summary>
        /// 接收到了新K线数据触发该响应
        /// 按照AllCodes里合约的顺序依次响应该事件
        /// </summary>
        public event DelegateOnBar OnBar;
    }
}
