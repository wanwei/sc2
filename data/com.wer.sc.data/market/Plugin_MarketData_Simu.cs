using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    public class Plugin_MarketData_Simu : IPlugin_MarketData
    {
        private DelegateOnConnectionStatus connectionStatus;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return connectionStatus;
            }

            set
            {
                connectionStatus = value;
            }
        }

        private DelegateOnReturnMarketData onReturnMarketData;

        public DelegateOnReturnMarketData OnReturnMarketData
        {
            get
            {
                return onReturnMarketData;
            }

            set
            {
                this.onReturnMarketData = value;
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

        public List<double[]> GetTradingSession(string code, int date)
        {
            return null;
        }

        public void Subscribe(string[] codes)
        {
            
        }

        public void UnSubscribe(string[] codes)
        {
            
        }
    }
}