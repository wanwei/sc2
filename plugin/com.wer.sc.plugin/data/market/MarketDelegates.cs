using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 数据连接状态修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="status"></param>
    /// <param name="userLogin"></param>
    /// <param name="size1"></param>
    public delegate void DelegateOnConnectionStatus(object sender, ConnectionStatus status, ref LoginInfo userLogin);

    /// <summary>
    /// 返回市场数据修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="marketData"></param>
    public delegate void DelegateOnReturnMarketData(object sender, ref ITickBar marketData);

    /// <summary>
    /// 返回合约信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="instruments"></param>
    public delegate void DelegateOnReturnInstrument(object sender, ref List<InstrumentInfo> instruments);

    /// <summary>
    /// 返回下单委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="order"></param>
    public delegate void DelegateOnReturnOrder(object sender, ref OrderInfo order);

    /// <summary>
    /// 返回成交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="trade"></param>
    public delegate void DelegateOnReturnTrade(object sender, ref TradeInfo trade);

    /// <summary>
    /// 返回投资者持仓
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="trade"></param>
    public delegate void DelegateOnReturnInvestorPosition(object sender, ref PositionInfo trade);
    
    /// <summary>
    /// 返回账号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="trade"></param>
    public delegate void DelegateOnReturnAccount(object sender, ref AccountInfo trade);
}