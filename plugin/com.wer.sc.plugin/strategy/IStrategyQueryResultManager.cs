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
        void AddQueryResult(IStrategyQueryResult strategyResult);

        void RemoveQueryResult(IStrategyQueryResult strategyResult);

        IList<IStrategyQueryResult> GetQueryResults();

        IStrategyQueryResult GetQueryResultByName(string name);
    }
}