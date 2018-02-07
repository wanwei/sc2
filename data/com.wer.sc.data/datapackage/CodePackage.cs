using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 数据包
    /// </summary>
    public class CodePackage
    {
        public bool IsMainContract;

        public CodePackage_MainContractList MainContracts;

        private List<string> codes = new List<string>();

        public List<string> Codes
        {
            get
            {
                return codes;
            }
        }

        public int StartDate;

        public int EndDate;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("isMainContract:").Append(IsMainContract).Append("\r\n");
            sb.Append("start:").Append(StartDate).Append("\r\n");
            sb.Append("end:").Append(EndDate).Append("\r\n");
            if (IsMainContract)
            {
                sb.Append(MainContracts.ToString());
            }
            else
            {
                for(int i = 0; i < codes.Count; i++)
                {
                    sb.Append(codes[i]).Append("\r\n");
                }
            }
            return sb.ToString();
        }
    }

    public class CodePackage_CodeList
    {
        private List<string> codes = new List<string>();

        public List<string> Codes
        {
            get
            {
                return codes;
            }
        }
    }

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

    public class CodePackageInfo_Code
    {
        public string Code;

        public int StartDate;

        public int EndDate;

        public override string ToString()
        {
            return Code + "," + StartDate + "," + EndDate;
        }
    }
}