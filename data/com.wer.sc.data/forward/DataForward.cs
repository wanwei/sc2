using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class DataForward : IDataForward
    {        
        private IDataPackage_Code[] dataPackage;

        private ForwardReferedPeriods[] referedPeriods;

        private List<IDataForward_Code> historyDataForwards;

        public const double TIME_DAYEND = 0;

        public const double TIME_END = -1;        

        private double time;

        private Dictionary<string, double> dic_Code_NextTime;

        private List<string> codes;

        //private HistoryDataForwardArguments args;

        private ForwardPeriod forwardPeriod;

        private IList<int> tradingDays;

        private List<int> forwardIndeies = new List<int>();

        public DataForward(IDataForwardFactory fac, IDataPackage_Code[] dataPackage, ForwardReferedPeriods[] referedPeriods, ForwardPeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
            this.tradingDays = dataPackage[0].GetTradingDays();

            this.historyDataForwards = new List<IDataForward_Code>();

            for (int i = 0; i < dataPackage.Length; i++)
            {
                ForwardReferedPeriods referedPeriod = referedPeriods[i];
                ForwardPeriod currentForwardPeriod= new ForwardPeriod(referedPeriod.UseTickData, referedPeriod.GetMinPeriod());
                IDataForward_Code historyDataForward_Code = fac.CreateDataForward_Code(dataPackage[i], referedPeriod, currentForwardPeriod);
                this.historyDataForwards.Add(historyDataForward_Code);
            }
        }

        private void Forward_OnTick(object sender, ITickData tickData, int index)
        {
            //if (OnTick != null)
            //    OnTick(sender, tickData, index);
        }

        private void Forward_OnBar(object sender, IKLineData klineData, int index)
        {
            //if (OnBar != null)
            //    OnBar(sender, klineData, index);
        }

        /// <summary>
        /// 多前进
        /// </summary>
        /// <returns></returns>
        public bool Forward()
        {
            List<int> forwardIndeies = GetForwardIndeies();
            if (forwardIndeies == null)
                return false;
            for(int i = 0; i < forwardIndeies.Count; i++)
            {
                historyDataForwards[i].Forward();
            }
            return true;
        }

        private List<int> GetForwardIndeies()
        {
            return null;
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
                    //forwardList[i].Forward();
                }
            }
            return true;
        }

        private double GetNextTime(Dictionary<string, double> dic_Code_NextTime)
        {
            return -1;
        }

        private double GetNextTime(IDataForward_Code forward_Code)
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
            //for (int i = 0; i < forwardList.Count; i++)
            //{
            //    bool isCurrentEnd = forwardList[i].Forward();
            //    if (!isCurrentEnd)
            //        isKLineEnd = false;
            //}
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

        public IList<string> ListenedCodes
        {
            get
            {
                return codes;
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
        public IDataForward_Code GetHistoryDataForward(string code)
        {
            IDataForward_Code value = null;
            //dic_Code_Forward.TryGetValue(code, out value);
            return value;
        }

        public IRealTimeData_Code GetRealTimeData(string code)
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

        public event DelegateOnRealTimeChanged OnRealTimeChanged;
    }

    public class NextTimeCalc
    {
        private List<IDataForward_Code> historyDataForwards;

        public NextTimeCalc()
        {

        }   
        
             

        public bool Forward()
        {
            return false;
        }
    }
}
