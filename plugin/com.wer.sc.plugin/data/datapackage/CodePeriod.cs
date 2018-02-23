using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class CodePeriod : ICodePeriod
    {
        private string code;

        public string Code
        {
            get { return code; }
            set { this.code = value; }
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
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        private bool isMainContract;

        public bool IsFromContracts
        {
            get
            {
                return isMainContract;
            }

            set
            {
                this.isMainContract = value;
            }
        }

        private IList<ICodePeriod> mainCodes;

        public IList<ICodePeriod> Contracts
        {
            get
            {
                if (mainCodes == null)
                    mainCodes = new List<ICodePeriod>();
                return mainCodes;
            }
        }

        public CodePeriod(string code, int startDate, int endDate)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public CodePeriod(string code, int startDate, int endDate, IList<ICodePeriod> mainCodePeriods) : this(code, startDate, endDate)
        {
            this.isMainContract = true;
            this.mainCodes = mainCodePeriods;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Code + "," + StartDate + "," + EndDate);
            if (isMainContract)
            {
                for (int i = 0; i < Contracts.Count; i++)
                {
                    ICodePeriod codePeriod = Contracts[i];
                    sb.Append("\r\n").Append(codePeriod);
                }
            }
            return sb.ToString();
        }
    }
}
