using com.wer.sc.plugin;

namespace com.wer.sc.data
{
    public interface IDataCenterInfo
    {
        StoreMethod DataCenterStoreMethod { get; set; }
        MarketType MarketType { get; set; }
        StoreDataTypes StoredDataTypes { get; }
        string Uri { get; set; }
    }
}