using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.account
{
    public class TradeFee_Variety : TradeFee
    {
        private IDataCenter dataCenter;

        private TradeFee_Code defaultTradeFee;

        private List<TradeFee_Code> varietyTradeFee = new List<TradeFee_Code>();

        public TradeFee_Variety(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public override ITradeFee_Code GetFee(string code)
        {
            CodeInfo codeInfo = dataCenter.DataReader.CodeReader.GetCodeInfo(code);
            string catelog = codeInfo.Catelog;
            ITradeFee_Code fee_Code = base.GetFee(catelog);
            return (ITradeFee_Code)fee_Code.Clone();
        }
    }
}
