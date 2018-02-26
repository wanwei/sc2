using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class CodePeriodFactory : ICodePeriodFactory
    {
        private IDataReader dataReader;

        public CodePeriodFactory(IDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public ICodePeriodPackageInfo CreateCodePeriodPackageInfo(List<string> codes, int startDate, int endDate, CodeChooseMethod codeChooseMethod)
        {
            return new CodePeriodPackageInfo(codes, startDate, endDate, codeChooseMethod);
        }

        public ICodePeriodPackage CreateCodePeriodPackage(List<string> codes, int startDate, int endDate, CodeChooseMethod codeChooseMethod)
        {
            return CreateCodePeriodPackage(new CodePeriodPackageInfo(codes, startDate, endDate, codeChooseMethod));
        }

        public ICodePeriodPackage CreateCodePeriodPackage(ICodePeriodPackageInfo codePackageInfo)
        {
            switch (codePackageInfo.CodeChooseMethod)
            {
                case CodeChooseMethod.Normal:
                    return CreateCodePackage_Normal(codePackageInfo);
                case CodeChooseMethod.Maincontract:
                    return CreateCodePackage_MainContract(codePackageInfo);
                case CodeChooseMethod.Catelog:
                    return CreateCodePackage_Catelog(codePackageInfo);
            }
            return null;
        }

        private CodePeriodPackage CreateCodePackage_MainContract(ICodePeriodPackageInfo codePackageInfo)
        {
            CodePeriodPackage codePackage = new CodePeriodPackage();
            List<string> codes = codePackageInfo.Codes;
            for (int i = 0; i < codes.Count; i++)
            {
                string variety = codes[i];
                codePackage.CodePeriods.Add(GetCodePackage_MainContract(variety, codePackageInfo.Start, codePackageInfo.End));
            }
            return codePackage;
        }

        private ICodePeriod GetCodePackage_MainContract(string variety, int start, int end)
        {
            List<MainContractInfo> contractInfos = dataReader.MainContractReader.GetMainContractInfos(variety, start, end);
            List<ICodePeriod> mainContracts = new List<ICodePeriod>();
            for (int i = 0; i < contractInfos.Count; i++)
            {
                MainContractInfo mainContractInfo = contractInfos[i];
                ICodePeriod codePeriod = CreateCodePeriod(mainContractInfo.Code, mainContractInfo.Start, mainContractInfo.End);
                mainContracts.Add(codePeriod);
            }
            return CreateCodePeriod_MainContract(variety, start, end, mainContracts);
        }

        private CodePeriodPackage CreateCodePackage_Catelog(ICodePeriodPackageInfo codePackageInfo)
        {

            int start = codePackageInfo.Start;
            int end = codePackageInfo.End;
            List<string> allCodes = new List<string>();
            IList<string> varieties = codePackageInfo.Codes;
            for (int i = 0; i < varieties.Count; i++)
            {
                string catelog = varieties[i];
                List<string> catelogCodes = dataReader.CodeReader.GetCodesByCatelog(catelog);
                allCodes.AddRange(allCodes);
            }
            return CreateCodePeriodPackage(allCodes, start, end);
        }

        private CodePeriodPackage CreateCodePackage_Normal(ICodePeriodPackageInfo codePackageInfo)
        {
            return CreateCodePeriodPackage(codePackageInfo.Codes, codePackageInfo.Start, codePackageInfo.End);
        }

        private CodePeriodPackage CreateCodePeriodPackage(IList<string> codes, int startDate, int endDate)
        {
            CodePeriodPackage codePackage = new CodePeriodPackage();
            for (int i = 0; i < codes.Count; i++)
            {
                codePackage.CodePeriods.Add(CreateCodePeriod(codes[i], startDate, endDate));
            }
            return codePackage;
        }

        public ICodePeriod CreateCodePeriod(string code, int startDate, int endDate)
        {
            return new CodePeriod(code, startDate, endDate);
        }

        public ICodePeriod CreateCodePeriod_MainContract(string variety, int startDate, int endDate, IList<ICodePeriod> mainContracts)
        {
            return new CodePeriod(variety, startDate, endDate, mainContracts);
        }
    }
}