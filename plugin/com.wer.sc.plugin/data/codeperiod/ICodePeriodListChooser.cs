using System.Collections.Generic;

namespace com.wer.sc.data.codeperiod
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICodePeriodListChooser
    {
        int Start { get; set; }

        int End { get; set; }

        List<string> Codes { get; }

        CodeChooseMethod CodeChooseMethod { get; set; }
    }

    /// <summary>
    /// 选择股票或期货的方式
    /// </summary>
    public enum CodeChooseMethod
    {
        Normal = 0,

        Catelog = 1,

        Maincontract = 2
    }
}