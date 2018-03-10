using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyQueryResultManager : IStrategyQueryResultManager
    {
        private Dictionary<string, IStrategyQueryResult> dic_Name_QueryResult = new Dictionary<string, IStrategyQueryResult>();

        private List<IStrategyQueryResult> queryResults = new List<IStrategyQueryResult>();

        public IStrategyQueryResult NewQueryResult(string name, string[] titles, ObjectType[] types)
        {
            if (this.dic_Name_QueryResult.ContainsKey(name))
                return this.dic_Name_QueryResult[name];
            StrategyQueryResult result = new StrategyQueryResult(name, titles, types);
            this.queryResults.Add(result);
            this.dic_Name_QueryResult.Add(result.Name, result);
            return result;
        }

        public void AddQueryResult(IStrategyQueryResult strategyResult)
        {
            this.queryResults.Add(strategyResult);
            this.dic_Name_QueryResult.Add(strategyResult.Name, strategyResult);
        }

        public IList<IStrategyQueryResult> GetQueryResults()
        {
            return queryResults;
        }

        public void RemoveQueryResult(IStrategyQueryResult strategyResult)
        {
            this.queryResults.Remove(strategyResult);
            this.dic_Name_QueryResult.Remove(strategyResult.Name);
        }

        public IStrategyQueryResult GetQueryResultByName(string name)
        {
            if (!dic_Name_QueryResult.ContainsKey(name))
                return null;
            return dic_Name_QueryResult[name];
        }
    }
}