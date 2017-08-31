using com.wer.sc.data.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    public class AccountPosition
    {
        private List<PositionInfo> positions = new List<PositionInfo>();

        private Dictionary<String, PositionInfo> mapPosition_Buy = new Dictionary<String, PositionInfo>();

        private Dictionary<String, PositionInfo> mapPosition_Sell = new Dictionary<String, PositionInfo>();

        private Account account;

        public AccountPosition(Account account)
        {
            this.account = account;
        }

        public int GetPosition(string code, OrderSide orderSide)
        {
            PositionInfo positionInfo = GetPositionInfo(code, orderSide);
            if (positionInfo == null)
                return 0;
            return positionInfo.Position;
        }

        public PositionInfo GetPositionInfo(String code, OrderSide orderSide)
        {
            Dictionary<string, PositionInfo> dic_position = orderSide == OrderSide.Buy ? mapPosition_Buy : mapPosition_Sell;
            if (!dic_position.ContainsKey(code))
                return null;
            return dic_position[code];
        }

        public void DoTradingDayChange()
        {

        }
    }
}
