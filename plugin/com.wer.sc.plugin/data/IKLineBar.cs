namespace com.wer.sc.data
{
    /// <summary>
    /// 该接口表示K线的一根柱子
    /// </summary>
    public interface IKLineBar
    {
        /// <summary>
        /// 得到当前K线的代码
        /// 如M05在大陆期货市场表示豆粕期货05合约）
        /// 如sz000002在大陆股票市场表示万科股票
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 得到当前K线柱子的时间，用浮点数表示
        /// 如20130105.093005 表示2013年1月5日早上9点30分零5秒
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 得到该K线柱子的开盘价格
        /// </summary>
        float Start { get; }

        /// <summary>
        /// 得到该K线柱子的最高价格
        /// </summary>
        float High { get; }

        /// <summary>
        /// 得到该K线柱子的最低价格
        /// </summary>
        float Low { get; }

        /// <summary>
        /// 得到该K线柱子的收盘价格
        /// </summary>
        float End { get; }

        /// <summary>
        /// 得到该K线柱子的成交量
        /// </summary>
        int Mount { get; }

        /// <summary>
        /// 得到该K线柱子的成交额
        /// </summary>
        float Money { get; }

        /// <summary>
        /// 得到该K线柱子的持仓
        /// </summary>
        int Hold { get; }

        /// <summary>
        /// 得到该K线柱子柱状体的高度
        /// </summary>
        float BlockHeight { get; }

        /// <summary>
        /// 得到该K线柱子柱状体的上沿
        /// </summary>
        float BlockHigh { get; }

        /// <summary>
        /// 得到该K线柱子柱状体的下沿
        /// </summary>
        float BlockLow { get; }

        /// <summary>
        /// 得到该K线柱子柱状体的中心
        /// </summary>
        float BlockMiddle { get; }

        /// <summary>
        /// 得到该K线柱子下影线长度
        /// </summary>
        float BottomShadow { get; }

        /// <summary>
        /// 得到该K线柱子上影线长度
        /// </summary>        
        float TopShadow { get; }

        /// <summary>
        /// 得到该K线柱子的完整高度（包括上下影线）
        /// </summary>
        float Height { get; }

        /// <summary>
        /// 得到该K线柱子的中间位置
        /// </summary>
        float Middle { get; }

        /// <summary>
        /// 得到该K线柱子是否为红色，红色表示上涨
        /// 如果是收盘和开盘价格一样，也算上涨
        /// </summary>
        /// <returns></returns>
        bool isRed();

        /// <summary>
        /// 得到该K线柱子的百分比振幅，即K线高度/K线收盘价
        /// </summary>
        float HeightPercent { get; }
        
        /// <summary>
        /// 得到该K线柱子中间柱状部分的百分比，即开盘和收盘的差价/K线收盘价
        /// </summary>
        float BlockHeightPercent { get; }
    }
}