using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.account
{
    public class TradeFee : IXmlExchange, ITradeFee
    {
        private TradeFee_Code defaultFee;

        private Dictionary<string, TradeFee_Code> dic_Code_TempFee = new Dictionary<string, TradeFee_Code>();

        private List<string> varieties = new List<string>();

        private Dictionary<String, TradeFee_Code> dic_Variety_Fee = new Dictionary<String, TradeFee_Code>();

        private List<string> codes = new List<string>();

        private Dictionary<String, TradeFee_Code> dic_Code_Fee = new Dictionary<String, TradeFee_Code>();

        public TradeFee()
        {
            this.defaultFee = new TradeFee_Code(null, 10, 1, 3, 3, false, 10);
        }

        public TradeFee(TradeFee_Code defaultFee)
        {
            this.defaultFee = defaultFee;
        }

        public void AddVarietyFee(TradeFee_Code fee)
        {
            this.varieties.Add(fee.Code);
            this.dic_Variety_Fee.Add(fee.Code, fee);
        }

        public void AddCodeFee(TradeFee_Code fee)
        {
            this.codes.Add(fee.Code);
            this.dic_Code_Fee.Add(fee.Code, fee);
        }

        public virtual ITradeFee_Code GetFee(String code)
        {
            if (!dic_Code_Fee.ContainsKey(code))
                return defaultFee;
            TradeFee_Code fee = dic_Code_Fee[code];
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
            dic_Code_Fee.Add(feeContract.Code, feeContract);
        }

        public void Save(XmlElement elem)
        {
            XmlElement defaultElem = elem.OwnerDocument.CreateElement("defaultfee");
            elem.AppendChild(defaultElem);
            defaultFee.Save(defaultElem);

            XmlElement varietyElem = elem.OwnerDocument.CreateElement("varietyfee");
            elem.AppendChild(varietyElem);
            foreach (string code in varieties)
            {
                XmlElement feeElem = varietyElem.OwnerDocument.CreateElement("fee");
                varietyElem.AppendChild(feeElem);
                TradeFee_Code tradeFee = dic_Variety_Fee[code];
                tradeFee.Save(feeElem);
            }

            XmlElement codeElem = elem.OwnerDocument.CreateElement("codefee");
            elem.AppendChild(codeElem);
            foreach (string code in codes)
            {
                XmlElement feeElem = varietyElem.OwnerDocument.CreateElement("fee");
                codeElem.AppendChild(feeElem);
                TradeFee_Code tradeFee = dic_Code_Fee[code];
                tradeFee.Save(feeElem);
            }
        }

        public void Load(XmlElement elem)
        {
            XmlElement elemDefault = (XmlElement)elem.GetElementsByTagName("defaultfee")[0];
            this.defaultFee.Load(elemDefault);


            XmlElement elemVariety = (XmlElement)elem.GetElementsByTagName("varietyfee")[0];
            XmlNodeList list = elemVariety.ChildNodes;
            foreach (Object obj in list)
            {
                XmlElement subElem = (XmlElement)obj;
                TradeFee_Code fee_Code = new TradeFee_Code();
                fee_Code.Load(subElem);
                this.AddVarietyFee(fee_Code);
            }

            XmlElement elemCode = (XmlElement)elem.GetElementsByTagName("codefee")[0];
            list = elemCode.ChildNodes;
            foreach (Object obj in list)
            {
                XmlElement subElem = (XmlElement)obj;
                TradeFee_Code fee_Code = new TradeFee_Code();
                fee_Code.Load(subElem);
                this.AddCodeFee(fee_Code);
            }
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }
    }
}