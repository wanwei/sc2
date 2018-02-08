using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 
    /// </summary>
    public class CodePackageInfo : IXmlExchange
    {
        private bool choosedByMainContract;

        private bool choosedByCatelog;

        private List<string> codes = new List<string>();

        private int start;

        private int end;

        public bool ChoosedByMainContract
        {
            get
            {
                return choosedByMainContract;
            }

            set
            {
                choosedByMainContract = value;
            }
        }

        public bool ChoosedByCatelog
        {
            get
            {
                return choosedByCatelog;
            }

            set
            {
                choosedByCatelog = value;
            }
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
            sb.Append(choosedByCatelog).Append(",").Append(choosedByMainContract).Append("\r\n");
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
            xmlElem.SetAttribute("maincontract", ChoosedByMainContract.ToString());
            xmlElem.SetAttribute("catelog", ChoosedByCatelog.ToString());
            xmlElem.SetAttribute("codes", ListUtils.ToString(codes));
        }

        public void Load(XmlElement xmlElem)
        {
            this.start = int.Parse(xmlElem.GetAttribute("start"));
            this.end = int.Parse(xmlElem.GetAttribute("end"));
            this.choosedByMainContract = this.choosedByMainContract = bool.Parse(xmlElem.GetAttribute("maincontract"));
            this.ChoosedByCatelog = this.choosedByMainContract = bool.Parse(xmlElem.GetAttribute("catelog"));

            string codesStr = xmlElem.GetAttribute("codes");
            string[] arr = codesStr.Split(',');
            this.codes.AddRange(arr);
        }
    }
}
