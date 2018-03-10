using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    public class StrategyExecutorFactory3 : IStrategyExecutorFactory
    {
        private DataCenter dataCenter;

        public StrategyExecutorFactory3(DataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        /// <summary>
        /// 创建一个历史数据回测执行器
        /// </summary>
        /// <param name="code">股票代码</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public IStrategyExecutor CreateExecutor_History(string code, int startDate, int endDate)
        {
            return null;
        }

        /// <summary>
        /// 创建一个历史数据回测执行器
        /// </summary>
        /// <param name="codePeriod"></param>
        /// <returns></returns>
        public IStrategyExecutor CreateExecutor_History(ICodePeriod codePeriod)
        {
            return null;
        }

        /// <summary>
        /// 创建一个历史数据回测执行器
        /// </summary>
        /// <param name="dataPackage_Code">单支股票在一段时间的数据包</param>
        /// <returns></returns>
        public IStrategyExecutor CreateExecutor_History(IDataPackage_Code dataPackage_Code)
        {
            return null;
        }

        /// <summary>
        /// 创建一个多重历史数据回测执行器
        /// </summary>
        /// <param name="code">股票代码</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public IStrategyExecutor_Multi CreateExecutor_Multi_History(IList<string> codes, int startDate, int endDate)
        {
            return null;
        }

        /// <summary>
        /// 创建一个多重历史数据回测执行器
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="chooseMethod"></param>
        /// <returns></returns>
        public IStrategyExecutor_Multi CreateExecutor_Multi_History(IList<string> codes, int startDate, int endDate, CodeChooseMethod chooseMethod)
        {
            return null;
        }

        /// <summary>
        /// 创建一个多重历史数据回测执行器
        /// </summary>
        /// <param name="codePeriodListChooser"></param>
        /// <returns></returns>
        public IStrategyExecutor_Multi CreateExecutor_Multi_History(ICodePeriodListChooser codePeriodListChooser)
        {
            return null;
        }
    }
}
