using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyQueryColumn
    {
        /// <summary>
        /// 得到列标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 得到该列的数据类型
        /// </summary>
        ObjectType DataType { get; }
    }
}