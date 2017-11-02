using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.account
{
    public class TradeFee : IXmlExchange
    {
        private Dictionary<String, TradeFee_Code> mapFee = new Dictionary<String, TradeFee_Code>();

        private TradeFee_Code defaultFee;

        public TradeFee()
        {
            this.defaultFee = new TradeFee_Code(null, 10, 1, 3, 3, false, 10);
        }

        public TradeFee(TradeFee_Code defaultFee)
        {
            this.defaultFee = defaultFee;
        }

        public TradeFee_Code GetFee(String code)
        {
            if (!mapFee.ContainsKey(code))
                return defaultFee;
            TradeFee_Code fee = mapFee[code];
            if (fee == null)
            {
                if (defaultFee != null)
                    fee = defaultFee;
                else
                    throw new ApplicationException("没有合约" + code + "的交易信息");
            }
            return fee;
        }

        public void AddFee_Code(TradeFee_Code feeContract)
        {
            mapFee.Add(feeContract.Code, feeContract);
        }

        public void Save(List<string> codes, XmlElement xmlElem)
        {
            foreach (string code in codes)
            {
                XmlElement subElem = xmlElem.OwnerDocument.CreateElement("tradefee");
                xmlElem.AppendChild(subElem);
                TradeFee_Code tradeFee = mapFee[code];
                tradeFee.Save(subElem);
            }
        }

        public void Save(XmlElement elem)
        {
            foreach (string code in mapFee.Keys)
            {
                XmlElement subElem = elem.OwnerDocument.CreateElement("tradefee");
                elem.AppendChild(subElem);
                TradeFee_Code tradeFee = mapFee[code];
                tradeFee.Save(subElem);
            }
        }

        public void Load(XmlElement elem)
        {
            XmlNodeList list = elem.ChildNodes;
            foreach (Object obj in list)
            {
                XmlElement subElem = (XmlElement)obj;
                TradeFee_Code fee_Code = new TradeFee_Code();
                fee_Code.Load(subElem);
                mapFee.Add(fee_Code.Code, fee_Code);
            }
        }
    }
}