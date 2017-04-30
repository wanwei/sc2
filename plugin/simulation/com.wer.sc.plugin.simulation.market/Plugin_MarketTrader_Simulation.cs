using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;
using com.wer.sc.data.market;

namespace com.wer.sc.plugin.market.history
{
    public class Plugin_MarketTrader_Simulation : IPlugin_MarketTrader
    {
        private DelegateOnConnectionStatus onConnectionStatus;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                onConnectionStatus = value;
            }
        }

        private DelegateOnReturnInstrument onReturnInstruments;

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                return onReturnInstruments;
            }

            set
            {
                onReturnInstruments = value;
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

        public DelegateOnReturnInvestorPosition OnReturnInvestorPosition
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

        public void Connect(ConnectionInfo connectionInfo)
        {
        }

        public void DisConnect()
        {
        }

        public List<ConnectionInfo> GetAllConnections()
        {
            return null;
        }

        public void QueryInstruments(string[] instruments = null)
        {
        }

        public string SendOrder(OrderInfo order)
        {
            throw new NotImplementedException();
        }

        public string CancelOrder(string orderid)
        {
            throw new NotImplementedException();
        }

        public void QueryAccount()
        {
            throw new NotImplementedException();
        }

        public void QueryPosition()
        {
            throw new NotImplementedException();
        }
    }
}
