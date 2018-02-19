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