using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.codeperiod
{
    /// <summary>
    /// 
    /// </summary>
    public class CodePeriodListChooser : IXmlExchange, ICodePeriodListChooser
    {
        private CodeChooseMethod codeChooseMethod;

        private List<string> codes = new List<string>();

        private int start;

        private int end;

        public CodePeriodListChooser()
        {
        }

        public CodePeriodListChooser(IList<string> codes, int startDate, int endDate, CodeChooseMethod codeChooseMethod)
        {
            this.codes.AddRange(codes);
            this.start = startDate;
            this.end = endDate;
            this.codeChooseMethod = codeChooseMethod;
        }

        public CodeChooseMethod CodeChooseMethod
        {
            get { return codeChooseMethod; }
            set { this.codeChooseMethod = value; }
        }

        public List<string> Codes
        {
            get
            {
                return codes;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(start).Append("-").Append(end).Append(",");
            sb.Append(codeChooseMethod).Append("\r\n");
            for (int i = 0; i < codes.Count; i++)
            {
                sb.Append(codes[i]).Append(",");
            }
            return sb.ToString();
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("start", start.ToString());
            xmlElem.SetAttribute("end", end.ToString());
            xmlElem.SetAttribute("codechoosemethod", codeChooseMethod.ToString());
            xmlElem.SetAttribute("codes", ListUtils.ToString(codes));
        }

        public void Load(XmlElement xmlElem)
        {
            this.start = int.Parse(xmlElem.GetAttribute("start"));
            this.end = int.Parse(xmlElem.GetAttribute("end"));
            this.codeChooseMethod = (CodeChooseMethod)EnumUtils.Parse(typeof(CodeChooseMethod), xmlElem.GetAttribute("codechoosemethod"));
            string codesStr = xmlElem.GetAttribute("codes");
            string[] arr = codesStr.Split(',');
            this.codes.AddRange(arr);
        }
    }
}
