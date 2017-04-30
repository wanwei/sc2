using com.wer.sc.data;
using com.wer.sc.data.market;
using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAPI.Callback;

namespace com.wer.sc.plugin.market.cnstock
{
    public class Plugin_MarketData_CnStock : Plugin_XApi_Base, IPlugin_MarketData
    {
        const string quotePath = @"plugin\CTP\CTP_Quote_x86.dll";

        private XApi api_Quote;

        private DelegateOnConnectionStatus onConnectionStatus;

        private DelegateOnReturnMarketData onReturnMarketData;

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

        public DelegateOnReturnMarketData OnReturnMarketData
        {
            get
            {
                return onReturnMarketData;
            }

            set
            {
                onReturnMarketData = value;
            }
        }

        private List<double[]> openTime;

        public Plugin_MarketData_CnStock()
        {
            api_Quote = new XApi(quotePath);
            openTime = new List<double[]>();
            openTime.Add(new double[] { 0.090000, 0.113000 });
            openTime.Add(new double[] { 0.130000, 0.150000 });
        }

        public List<double[]> GetTradingSession(string code, int date)
        {
            return openTime;
        }

        public void Connect(ConnectionInfo connectionInfo)
        {
            api_Quote.Server.BrokerID = connectionInfo.GetValue("BrokerId");
            api_Quote.Server.Address = connectionInfo.GetValue("DataServer");
            api_Quote.User.UserID = connectionInfo.GetValue("UserID");
            api_Quote.User.Password = connectionInfo.GetValue("Passwd");

            api_Quote.OnConnectionStatus = XApi_OnConnectionStatus;
            api_Quote.OnRtnDepthMarketData = XApi_OnRtnDepthMarketData;

            api_Quote.Connect();
        }

        /// <summary>
        /// 断开市场服务器
        /// </summary>
        public void DisConnect()
        {
            api_Quote.Disconnect();
        }

        private void XApi_OnConnectionStatus(object sender, XAPI.ConnectionStatus status, ref XAPI.RspUserLoginField userLogin, int size1)
        {
            if (onConnectionStatus == null)
                return;

            LoginInfo loginInfo = StructTransfer.TransferUserLogin(userLogin);
            onConnectionStatus(sender, EnumTransfer.TransferConnectionStatus(status), ref loginInfo);
        }

        private void XApi_OnRtnDepthMarketData(object sender, ref XAPI.DepthMarketDataNClass marketData)
        {
            if (onReturnMarketData == null)
                return;
            if (!IsInOpenPeriod(marketData.UpdateTime))
                return;
            ITickBar tickBar = StructTransfer.TransferTickBar(marketData);
            onReturnMarketData(sender, ref tickBar);
        }

        private bool IsInOpenPeriod(int time)
        {
            if (time < 83000 && time > 50000)
                return false;
            if (time > 160000 && time < 203000)
                return false;
            return true;
        }

        public void Subscribe(string[] codes)
        {
            for (int i = 0; i < codes.Length; i++)
            {
                api_Quote.Subscribe(codes[i], "");
            }
        }

        public void UnSubscribe(string[] codes)
        {
            for (int i = 0; i < codes.Length; i++)
            {
                api_Quote.Unsubscribe(codes[i], "");
            }
        }
    }
}
