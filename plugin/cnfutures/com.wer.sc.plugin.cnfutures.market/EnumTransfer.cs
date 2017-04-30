using com.wer.sc.data.market;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.market
{
    public class EnumTransfer
    {
        public static ConnectionStatus TransferConnectionStatus(XAPI.ConnectionStatus connectionStatus)
        {
            switch (connectionStatus)
            {
                case XAPI.ConnectionStatus.Authorized:
                    return ConnectionStatus.Authorized;
                case XAPI.ConnectionStatus.Authorizing:
                    return ConnectionStatus.Authorizing;
                case XAPI.ConnectionStatus.Confirmed:
                    return ConnectionStatus.Confirmed;
                case XAPI.ConnectionStatus.Confirming:
                    return ConnectionStatus.Confirming;
                case XAPI.ConnectionStatus.Connecting:
                    return ConnectionStatus.Connecting;
                case XAPI.ConnectionStatus.Connected:
                    return ConnectionStatus.Connected;
                case XAPI.ConnectionStatus.Disconnected:
                    return ConnectionStatus.Disconnected;
                case XAPI.ConnectionStatus.Doing:
                    return ConnectionStatus.Doing;
                case XAPI.ConnectionStatus.Done:
                    return ConnectionStatus.Done;
                case XAPI.ConnectionStatus.Initialized:
                    return ConnectionStatus.Initialized;
                case XAPI.ConnectionStatus.Logined:
                    return ConnectionStatus.Logined;
                case XAPI.ConnectionStatus.Logining:
                    return ConnectionStatus.Logining;
                case XAPI.ConnectionStatus.Uninitialized:
                    return ConnectionStatus.Uninitialized;
                case XAPI.ConnectionStatus.Unknown:
                    return ConnectionStatus.Unknown;

            }
            return ConnectionStatus.Unknown;
        }

        public static InstLifePhaseType TransferInstLifePhaseType(XAPI.InstLifePhaseType instLifePhaseType)
        {
            switch (instLifePhaseType)
            {
                case XAPI.InstLifePhaseType.NotStart:
                    return InstLifePhaseType.NotStart;
                case XAPI.InstLifePhaseType.FirstList:
                    return InstLifePhaseType.FirstList;
                case XAPI.InstLifePhaseType.Expired:
                    return InstLifePhaseType.Expired;
                case XAPI.InstLifePhaseType.Issue:
                    return InstLifePhaseType.Issue;
                case XAPI.InstLifePhaseType.Pause:
                    return InstLifePhaseType.Pause;
                case XAPI.InstLifePhaseType.Started:
                    return InstLifePhaseType.Started;
                case XAPI.InstLifePhaseType.UnList:
                    return InstLifePhaseType.UnList;
            }
            return InstLifePhaseType.NotStart;
        }

        public static OrderSide TransferOrderSide(XAPI.OrderSide orderSide)
        {
            switch (orderSide)
            {
                case XAPI.OrderSide.Buy:
                    return OrderSide.Buy;
                case XAPI.OrderSide.Sell:
                    return OrderSide.Sell;                    
            }
            return OrderSide.Unknown;
        }

        public static OpenCloseType TransferOpenCloseType(XAPI.OpenCloseType orderSide)
        {
            switch (orderSide)
            {
                case XAPI.OpenCloseType.Close:
                    return OpenCloseType.Close;
                case XAPI.OpenCloseType.CloseToday:
                    return OpenCloseType.CloseToday;
            }
            return OpenCloseType.Open;
        }

        public static PositionSide TransferPositionSide(XAPI.PositionSide positionSide)
        {
            switch (positionSide)
            {
                case XAPI.PositionSide.Long:
                    return PositionSide.Long;
                case XAPI.PositionSide.Short:
                    return PositionSide.Short;
            }
            return PositionSide.Long;
        }
    }
}
