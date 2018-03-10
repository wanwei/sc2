using System.Collections.Generic;

namespace com.wer.sc.data.codeperiod
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICodePeriodFactory
    {
        ICodePeriodListChooser CreateCodePeriodListChooser(IList<string> codes, int startDate, int endDate, CodeChooseMethod CodeChooseMethod);

        ICodePeriodList CreateCodePeriodList(IList<string> codes, int startDate, int endDate, CodeChooseMethod CodeChooseMethod);

        ICodePeriodList CreateCodePeriodList(ICodePeriodListChooser codePeriodListChooser);

        ICodePeriod CreateCodePeriod(string code, int startDate, int endDate);

        ICodePeriod CreateCodePeriod_MainContract(string variety, int startDate, int endDate, IList<ICodePeriod> contracts);

        ICodePeriod CreateCodePeriod_MainContract(string variety, int startDate, int endDate);
    }
}