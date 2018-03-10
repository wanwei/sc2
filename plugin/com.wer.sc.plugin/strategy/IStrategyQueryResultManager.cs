using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略查询结果的管理器
    /// </summary>
    public interface IStrategyQueryResultManager
    {
        /// <summary>
        /// 新建一个查询结果，并加入查询结果管理器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        IStrategyQueryResult NewQueryResult(string name, string[] titles, ObjectType[] types);

        void AddQueryResult(IStrategyQueryResult strategyResult);

        void RemoveQueryResult(IStrategyQueryResult strategyResult);

        IList<IStrategyQueryResult> GetQueryResults();

        IStrategyQueryResult GetQueryResultByName(string name);
    }
}