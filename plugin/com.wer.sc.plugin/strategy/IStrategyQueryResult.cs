using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略查询结果
    /// </summary>
    public interface IStrategyQueryResult : IDataStore
    {
        string Name { get; }

        string[] Titles { get; }

        ObjectType[] DataTypes { get; }

        /// <summary>
        /// 得到所有结果
        /// </summary>
        IList<IStrategyQueryResultRow> Rows { get; }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        /// <param name="values"></param>
        void AddRow(string code, double time, object[] values);

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        void RemoveRow(string code, double time);
    }
}