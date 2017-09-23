using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略的搜寻结果
    /// </summary>
    public interface IStrategyResult_Single
    {
        string Code { get; }

        double Time { get; }

        string Name { get; }

        string Description { get; }
    }
}