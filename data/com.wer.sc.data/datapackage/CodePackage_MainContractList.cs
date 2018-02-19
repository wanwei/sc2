using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class CodePackage_MainContractList
    {
        private List<CodePackage_MainContract> varieties = new List<CodePackage_MainContract>();

        public List<CodePackage_MainContract> Varieties
        {
            get { return varieties; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Varieties.Count; i++)
            {
                sb.Append(Varieties[i]).Append("\r\n");
            }
            return sb.ToString();
        }
    }

    public class CodePackage_MainContract
    {
        private List<CodePackageInfo_Code> mainCodes = new List<CodePackageInfo_Code>();

        public string Variety;

        public int StartDate;

        public int EndDate;

        public List<CodePackageInfo_Code> MainCodes
        {
            get { return mainCodes; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Variety).Append(",").Append(StartDate)
                .Append(",").Append(EndDate).Append("\r\n");
            for (int i = 0; i < mainCodes.Count; i++)
            {
                sb.Append(mainCodes[i]);
                if (i != mainCodes.Count - 1)
                    sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }

}
