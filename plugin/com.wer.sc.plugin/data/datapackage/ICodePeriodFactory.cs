using System.Collections.Generic;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICodePeriodFactory
    {
        ICodePeriodPackage CreateCodePeriodPackage(ICodePeriodPackageInfo codePackageInfo);

        ICodePeriod CreateCodePeriod(string code, int startDate, int endDate);

        ICodePeriod CreateCodePeriod_MainContract(string variety, int startDate, int endDate, IList<ICodePeriod> mainContracts);
    }
}