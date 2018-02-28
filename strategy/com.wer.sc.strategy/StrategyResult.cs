using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略报告
    /// </summary>
    public class StrategyResult : IStrategyResult
    {
        private Dictionary<ICodePeriod, StrategyResult_CodePeriod> dic_CodePeriod_StrategyResult = new Dictionary<ICodePeriod, StrategyResult_CodePeriod>();

        private List<ICodePeriod> codePeriods;

        private int startDate;

        private int endDate;

        private ForwardPeriod forwardPeriod;

        private StrategyReferedPeriods referedPeriods;

        /// <summary>
        /// 策略里使用的
        /// </summary>
        public IList<ICodePeriod> CodePeriods
        {
            get
            {
                return codePeriods;
            }
        }

        /// <summary>
        /// 策略执行开始时间
        /// </summary>
        public int StartDate
        {
            get
            {
                return startDate;
            }
        }

        /// <summary>
        /// 策略执行结束时间
        /// </summary>
        public int EndDate
        {
            get
            {
                return endDate;
            }
        }

        /// <summary>
        /// 回测前进的周期
        /// </summary>
        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        /// <summary>
        /// 回测引用的周期
        /// </summary>
        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return referedPeriods;
            }
        }

        /// <summary>
        /// 回测使用的参数
        /// </summary>
        public IParameters Parameters { get; }

        /// <summary>
        /// 回测找到的总结果集
        /// </summary>
        public IStrategyQueryResultManager StrategyQueryResults
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 得到代码周期的单独运行结果
        /// 有绘图或者交易的策略才会有单独的代码周期结果，否则没有
        /// </summary>
        public IList<IStrategyResult_CodePeriod> StrategyResult_Codes { get; }

        /// <summary>
        /// 得到指定代码周期的运行结果
        /// </summary>
        /// <param name="codePeriod"></param>
        /// <returns></returns>
        public IStrategyResult_CodePeriod GetStrategyResult_Code(ICodePeriod codePeriod)
        {
            return null;
        }

        /// <summary>
        /// 将数据保存为xml
        /// </summary>
        /// <param name="xmlElem"></param>
        public void Save(string filePath, XmlElement xmlElem)
        {
            
        }

        /// <summary>
        /// 从xml装载数据
        /// </summary>
        /// <param name="xmlElem"></param>
        public void Load(string filePath, XmlElement xmlElem)
        {
        }
    }
}
