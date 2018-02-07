using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;
using com.wer.sc.data.market;

namespace com.wer.sc.plugin.mock.market
{
    public class Plugin_MarketTrader_Mock_Web : IPlugin_MarketTrader
    {
        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnReturnAccount OnReturnAccount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnRspInvestorPosition OnRspInvestorPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnReturnOrder OnReturnOrder
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnReturnTrade OnReturnTrade
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnRspOrder OnRspOrder
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnRspTrade OnRspTrade
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string CancelOrder(string orderid)
        {
            throw new NotImplementedException();
        }

        public void Connect(ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }

        public List<ConnectionInfo> GetAllConnections()
        {
            throw new NotImplementedException();
        }

        public void QueryAccount()
        {
            throw new NotImplementedException();
        }

        public void QueryInstruments(string[] instruments)
        {
            throw new NotImplementedException();
        }

        public void QueryPosition()
        {
            throw new NotImplementedException();
        }

        public string SendOrder(OrderInfo order)
        {
            throw new NotImplementedException();
        }

        public void QueryOrders()
        {
            throw new NotImplementedException();
        }

        public void QueryTrades()
        {
            throw new NotImplementedException();
        }
    }
}
