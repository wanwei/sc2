using System.Collections.Generic;

namespace com.wer.sc.data.codeperiod
{
    /// <summary>
    /// 一个描述股票或期货代码和日期数据的一个打包的信息类
    /// </summary>
    public interface ICodePeriodList
    {
        List<ICodePeriod> CodePeriods { get; }
    }
}