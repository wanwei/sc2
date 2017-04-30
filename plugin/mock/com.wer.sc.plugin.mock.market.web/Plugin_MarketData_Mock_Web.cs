using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;
using com.wer.sc.data.market;

namespace com.wer.sc.plugin.mock.market
{

    public class Plugin_MarketData_Mock_Web : IPlugin_MarketData
    {
        public List<double[]> GetTradingSession(string code, int date)
        {
            return null;
        }

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

        public DelegateOnReturnMarketData OnReturnMarketData
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
            throw new NotImplementedException();
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }

        public List<ConnectionInfo> GetAllConnections()
        {
            List<ConnectionInfo> connections = new List<ConnectionInfo>();
            ConnectionInfo connection = new ConnectionInfo();
            connection.AddValue("id", "MOCKCONN1");
            connection.AddValue("name", "MOCK连接");
            connection.AddValue("desc", "MOCK连接，测试用");
            connections.Add(connection);
            connections.Add(new ConnectionInfo());
            return connections;
        }

        public void Subscribe(string[] codes)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribe(string[] codes)
        {
            throw new NotImplementedException();
        }
    }
}
