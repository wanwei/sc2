using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略结果管理器
    /// </summary>
    public interface IStrategyResultManager
    {
        void AddStrategyResult(string code, double time, string name, string desc);

        //DataTable 
    }
}
