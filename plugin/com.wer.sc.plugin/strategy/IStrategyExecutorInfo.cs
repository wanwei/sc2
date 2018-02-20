using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyExecutorInfo
    {
        /// <summary>
        /// 获得当前执行的数据包
        /// </summary>
        /// <returns></returns>
        IDataPackage_Code GetDataPackage();

        /// <summary>
        /// 策略执行器准备的数据
        /// </summary>
        /// <returns></returns>
        StrategyReferedPeriods GetReferedPeriods();

        /// <summary>
        /// 策略执行器的前进周期
        /// </summary>
        /// <returns></returns>
        StrategyForwardPeriod GetForwardPeriod();
    }
}