using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2
{
    /// <summary>
    /// 当前数据接收器
    /// 该接口用于接收当前数据
    /// </summary>
    public interface IDataReceiver : IRealTimeDataReader
    {
        void SubscribeAll();

        void Subscribe(string[] codes);

        void UnSubscribe(string[] codes);

        List<ConnectionInfo> GetAllConnections();

        void Connect(ConnectionInfo connectionInfo);

        void DisConnect();

        void ReConnect();

        DelegateOnConnectionStatus OnMarketDataConnectionStatus { get; set; }

        DelegateOnConnectionStatus OnMarketTraderConnectionStatus { get; set; }
    }
}
