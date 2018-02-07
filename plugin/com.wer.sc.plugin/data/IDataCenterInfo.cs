using com.wer.sc.plugin;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据中心信息接口
    /// 
    /// 
    /// </summary>
    public interface IDataCenterInfo
    {
        /// <summary>
        /// 数据的保存方式，文件方式or数据库
        /// 现在默认都是文件保存
        /// </summary>
        StoreMethod DataCenterStoreMethod { get; set; }

        /// <summary>
        /// 市场类型，现在支持中国期货市场，股票市场待支持
        /// </summary>
        MarketType MarketType { get; set; }

        /// <summary>
        /// 存储的数据类型，默认存储K线、分时线、tick数据
        /// </summary>
        StoreDataTypes StoredDataTypes { get; }

        /// <summary>
        /// 数据保存的地址
        /// </summary>
        string Uri { get; set; }
    }
}