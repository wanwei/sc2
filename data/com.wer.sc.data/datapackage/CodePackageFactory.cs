using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class CodePackageFactory
    {
        private IDataReader dataReader;

        public CodePackageFactory(IDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public CodePackage CreateCodePackage(CodePackageInfo codePackageInfo)
        {
            if (codePackageInfo.ChoosedByMainContract)
            {
                return CreateCodePackage_MainContract(codePackageInfo);
            }
            if (codePackageInfo.ChoosedByCatelog)
            {
                return CreateCodePackage_Catelog(codePackageInfo);
            }
            return CreateCodePackage_Normal(codePackageInfo);
        }

        private CodePackage CreateCodePackage_MainContract(CodePackageInfo codePackageInfo)
        {
            CodePackage codePackage = new CodePackage();
            int start = codePackageInfo.Start;
            int end = codePackageInfo.End;
            codePackage.MainContracts = new CodePackage_MainContractList();
            List<string> codes = codePackageInfo.Codes;
            for (int i = 0; i < codes.Count; i++)
            {
                string variety = codes[i];
                CodePackage_MainContract mainContract = GetCodePackage_MainContract(variety, start, end);                
                codePackage.MainContracts.Varieties.Add(mainContract);
            }
            codePackage.IsMainContract = true;
            codePackage.StartDate = start;
            codePackage.EndDate = end;
            return codePackage;
        }

        private CodePackage_MainContract GetCodePackage_MainContract(string variety, int start, int end)
        {
            List<MainContractInfo> contractInfos = dataReader.MainContractReader.GetMainContractInfos(variety, start, end);
            CodePackage_MainContract mainContract = new CodePackage_MainContract();
            for (int i = 0; i < contractInfos.Count; i++)
            {
                MainContractInfo mainContractInfo = contractInfos[i];
                CodePackageInfo_Code item = new CodePackageInfo_Code();
                item.Code = mainContractInfo.Code;
                item.StartDate = mainContractInfo.Start;
                item.EndDate = mainContractInfo.End;
                mainContract.MainCodes.Add(item);
            }
            mainContract.Variety = variety;
            mainContract.StartDate = start;
            mainContract.EndDate = end;
            return mainContract;
        }

        private CodePackage CreateCodePackage_Catelog(CodePackageInfo codePackageInfo)
        {
            CodePackage codePackage = new CodePackage();
            int start = codePackageInfo.Start;
            int end = codePackageInfo.End;
            IList<string> codes = codePackageInfo.Codes;
            for (int i = 0; i < codes.Count; i++)
            {
                string catelog = codes[i];
                List<string> catelogCodes = dataReader.CodeReader.GetCodesByCatelog(catelog);
                codePackage.Codes.AddRange(catelogCodes);
            }
            codePackage.StartDate = start;
            codePackage.EndDate = end;
            return codePackage;
        }

        private CodePackage CreateCodePackage_Normal(CodePackageInfo codePackageInfo)
        {
            CodePackage codePackage = new CodePackage();
            codePackage.IsMainContract = false;
            codePackage.StartDate = codePackageInfo.Start;
            codePackage.EndDate = codePackageInfo.End;
            codePackage.Codes.AddRange(codePackageInfo.Codes);
            return codePackage;
        }

    }
}
