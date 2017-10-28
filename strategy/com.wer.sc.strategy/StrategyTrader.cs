using com.wer.sc.data.account;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTrader : IStrategyTrader
    {
        private List<String> codes = new List<string>();

        private Account account;

        private IRealTimeDataReader_Code realTimeDataReader;

        private Dictionary<String, StrategyTrader_Code> dic_Code_Trader = new Dictionary<string, StrategyTrader_Code>();

        public StrategyTrader(double money, IRealTimeDataReader_Code realTimeDataReader)
        {
            this.account = new Account(money, realTimeDataReader);
            this.realTimeDataReader = realTimeDataReader;
        }

        public IAccount Account
        {
            get
            {
                return account;
            }
        }

        public IRealTimeDataReader_Code RealTimeDataReader
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

        public IStrategyTrader_Code GetStrategyTrader(string code)
        {
            if (dic_Code_Trader.ContainsKey(code))
                return dic_Code_Trader[code];
            StrategyTrader_Code trader = new StrategyTrader_Code(this, code);            
            this.dic_Code_Trader.Add(code, trader);
            this.codes.Add(code);
            return trader;
        }
    }
}