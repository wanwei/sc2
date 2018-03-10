using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略结果保存器
    /// 策略结果的保存以日期为目录，每个日期下平铺存储
    /// </summary>
    public interface IStrategyResultStore
    {
        /// <summary>
        /// 得到所有已保存结果的策略的名称
        /// </summary>
        /// <returns></returns>
        IList<int> GetAllSavedDays();

        /// <summary>
        /// 查看一天里保存了哪些结果集
        /// </summary>
        /// <param name="strategyName"></param>
        /// <returns></returns>
        IList<string> LoadStrategyResultNames(int day);

        /// <summary>
        /// 装载指定的策略结果集
        /// </summary>
        /// <param name="day"></param>
        /// <param name="resultName"></param>
        /// <returns></returns>
        IStrategyResult LoadStrategyResult(int day, string resultName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="resultName"></param>
        /// <returns></returns>
        IList<string> LoadStrategyResultCodes(int day, string resultName);

        IStrategyResult_CodePeriod LoadStrategyResult_CodePeriod(int day, string resultName, string code);

        bool ExistStrategyResult(int day, string resultName);

        bool ExistStrategyResult_CodePeriod(int day, string resultName, string code);

        /// <summary>
        /// 保存策略结果
        /// </summary>
        /// <param name="strategyResult"></param>
        void Save(int day, IStrategyResult strategyResult);

        /// <summary>
        /// 保存该策略结果的query结果集
        /// </summary>
        /// <param name="day"></param>
        /// <param name="strategyResult"></param>
        void SaveQueryResult(int day, IStrategyResult strategyResult);

        /// <summary>
        /// 保存每一个子结果
        /// </summary>
        /// <param name="resultName"></param>
        /// <param name="strategyResult"></param>
        void Save(int day, string resultName, IStrategyResult_CodePeriod strategyResult);
    }
}