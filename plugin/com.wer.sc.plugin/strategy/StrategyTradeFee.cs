using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    public class StrategyTradeFee
    {
        private Dictionary<String, StrategyTradeFee_Code> mapFee = new Dictionary<String, StrategyTradeFee_Code>();

        private StrategyTradeFee_Code defaultFee;

        public StrategyTradeFee()
        {
            this.defaultFee = new StrategyTradeFee_Code(null, 10, 1, 3, 3, false, 10);
        }

        public StrategyTradeFee(StrategyTradeFee_Code defaultFee)
        {
            this.defaultFee = defaultFee;
        }

        public StrategyTradeFee_Code getFee(String code)
        {
            if (!mapFee.ContainsKey(code))
                return defaultFee;
            StrategyTradeFee_Code fee = mapFee[code];
            if (fee == null)
            {
                if (defaultFee != null)
                    fee = defaultFee;
                else
                    throw new ApplicationException("没有合约" + code + "的交易信息");
            }
            return fee;
        }

        public void AddFee_Code(StrategyTradeFee_Code feeContract)
        {
            mapFee.Add(feeContract.Code, feeContract);
        }

        public void Save(XmlElement elem)
        {
            foreach (string code in mapFee.Keys)
            {
                XmlElement subElem = elem.OwnerDocument.CreateElement("tradefee");
                elem.AppendChild(subElem);
                StrategyTradeFee_Code tradeFee = mapFee[code];
                tradeFee.Save(subElem);
            }
        }

        public void Load(XmlElement elem)
        {
            XmlNodeList list = elem.ChildNodes;
            foreach (Object obj in list)
            {
                XmlElement subElem = (XmlElement)obj;
                StrategyTradeFee_Code fee_Code = new StrategyTradeFee_Code();
                fee_Code.Load(subElem);
                mapFee.Add(fee_Code.Code, fee_Code);
            }
        }
    }
}