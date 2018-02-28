using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        private bool isFromContract;

        public bool IsFromContracts
        {
            get
            {
                return isFromContract;
            }

            set
            {
                this.isFromContract = value;
            }
        }

        private IList<ICodePeriod> contracts;

        public IList<ICodePeriod> Contracts
        {
            get
            {
                if (contracts == null)
                    contracts = new List<ICodePeriod>();
                return contracts;
            }
        }

        public CodePeriod()
        {
        }

        public CodePeriod(string code, int startDate, int endDate)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public CodePeriod(string code, int startDate, int endDate, IList<ICodePeriod> contracts) : this(code, startDate, endDate)
        {
            this.isFromContract = true;
            this.contracts = contracts;
        }

        public override int GetHashCode()
        {
            int hash = Code.GetHashCode();
            hash = hash * 10 + startDate;
            hash = hash * 10 + endDate;
            if (isFromContract)
            {
                for (int i = 0; i < Contracts.Count; i++)
                {
                    ICodePeriod codePeriod = Contracts[i];
                    hash = hash * 10 + codePeriod.GetHashCode();
                }
            }
            return hash;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Code + "," + StartDate + "," + EndDate);
            if (isFromContract)
            {
                for (int i = 0; i < Contracts.Count; i++)
                {
                    ICodePeriod codePeriod = Contracts[i];
                    sb.Append("\r\n").Append(codePeriod);
                }
            }
            return sb.ToString();
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("code", code);
            xmlElem.SetAttribute("start", startDate.ToString());
            xmlElem.SetAttribute("end", endDate.ToString());
            xmlElem.SetAttribute("isfromcontract", IsFromContracts.ToString());
            if (this.Contracts != null)
            {
                for (int i = 0; i < Contracts.Count; i++)
                {
                    XmlElement elemContract = xmlElem.OwnerDocument.CreateElement("contract");
                    xmlElem.AppendChild(elemContract);
                    Contracts[i].Save(elemContract);
                }
            }
        }

        public void Load(XmlElement xmlElem)
        {
            this.code = xmlElem.GetAttribute("code");
            this.startDate = int.Parse(xmlElem.GetAttribute("start"));
            this.endDate = int.Parse(xmlElem.GetAttribute("end"));
            this.isFromContract = Boolean.Parse(xmlElem.GetAttribute("isfromcontract"));
            if (this.isFromContract)
            {
                XmlNodeList nodes = xmlElem.GetElementsByTagName("contract");
                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlElement elem = (XmlElement)nodes[i];
                    CodePeriod c = new CodePeriod();
                    c.Load(elem);
                    contracts.Add(c);
                }
            }
        }
    }
}
