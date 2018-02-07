using com.wer.sc.data;
using com.wer.sc.data.account;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTrader_Account //: IStrategyTrader
    {
        private List<String> codes = new List<string>();

        private IAccount account;

        private IRealTimeData_Code realTimeDataReader;

        private Dictionary<String, StrategyTrader_History> dic_Code_Trader = new Dictionary<string, StrategyTrader_History>();

        public StrategyTrader_Account(double money, IDataForward_Code realTimeDataReader)
        {
            this.account = DataCenter.Default.AccountManager.CreateAccount(money, realTimeDataReader);
            this.account.AccountSetting.TradeType = AccountTradeType.IMMEDIATELY;
            this.realTimeDataReader = realTimeDataReader;
        }

        public IAccount Account
        {
            get
            {
                return account;
            }
        }

        public IRealTimeData_Code RealTimeDataReader
        {
            get
            {
                return realTimeDataReader;
            }
        }

        public IList<string> GetAllCodes()
        {
            return codes;
        }

        public IStrategyTrader GetStrategyTrader(string code)
        {
            if (dic_Code_Trader.ContainsKey(code))
                return dic_Code_Trader[code];
            StrategyTrader_History trader = new StrategyTrader_History(this, code);            
            this.dic_Code_Trader.Add(code, trader);
            this.codes.Add(code);
            return trader;
        }
    }
}