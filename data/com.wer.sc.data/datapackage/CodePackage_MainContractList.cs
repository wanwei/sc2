using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class CodePackage_MainContractList : ICodePackage_MainContractList
    {
        private List<ICodePeriod_MainContract> varieties = new List<ICodePeriod_MainContract>();

        public List<ICodePeriod_MainContract> Varieties
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

    public class CodePackage_MainContract : ICodePeriod_MainContract
    {
        private List<ICodePackageInfo_Code> mainCodes = new List<ICodePackageInfo_Code>();

        private string variety;

        public string Variety
        {
            get { return variety; }
            set { this.variety = value; }
        }

        private int startDate;

        public int StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        private int endDate;

        public int EndDate
        {
            get { return endDate; }
            set { this.endDate = value; }
        }

        public List<ICodePackageInfo_Code> MainCodes
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
