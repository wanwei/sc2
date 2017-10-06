using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据中心配置信息
    /// 记录了该数据中心保存的市场类型
    /// </summary>
    public class DataCenterInfo : IDataCenterInfo
    {
        private const string ATTRIBUTE_MARKETTYPE = "MarketType";
        private const string ATTRIBUTE_ID = "Id";
        private const string ATTRIBUTE_URI = "Uri";
        private const string ATTRIBUTE_STOREMETHOD = "StoreMethod";

        private MarketType marketType;

        private string id;

        private string uri;

        private StoreMethod storeMethod;

        private StoreDataTypes storeDataTypes = new StoreDataTypes();

        /// <summary>
        /// 获取或设置该数据中心的市场类型
        /// </summary>
        public MarketType MarketType
        {
            get
            {
                return marketType;
            }

            set
            {
                marketType = value;
            }
        }

        /// <summary>
        /// 获取或设置该数据中心的地址
        /// </summary>
        public string Uri
        {
            get
            {
                return uri;
            }

            set
            {
                uri = value;
            }
        }

        public StoreMethod DataCenterStoreMethod
        {
            get
            {
                return storeMethod;
            }

            set
            {
                storeMethod = value;
            }
        }

        public StoreDataTypes StoredDataTypes
        {
            get
            {
                return storeDataTypes;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public void SaveXml(XmlElement elem)
        {
            elem.SetAttribute(ATTRIBUTE_MARKETTYPE, GetMarketTypeString(marketType));
            elem.SetAttribute(ATTRIBUTE_ID, id);
            elem.SetAttribute(ATTRIBUTE_URI, uri);
            elem.SetAttribute(ATTRIBUTE_STOREMETHOD, GetStoreMethodString(storeMethod));
            XmlElement elemStoreTypes = elem.OwnerDocument.CreateElement("StoreTypes");
            this.storeDataTypes.SaveXml(elemStoreTypes);
            elem.AppendChild(elemStoreTypes);
        }

        private MarketType ParseMarketType(string marketTypeStr)
        {
            if (marketTypeStr.ToLower().Equals("cnfutures"))
                return MarketType.CnFutures;
            if (marketTypeStr.ToLower().Equals("cnstock"))
                return MarketType.CnStock;
            return default(MarketType);
        }

        private string GetMarketTypeString(MarketType marketType)
        {
            switch (marketType)
            {
                case MarketType.CnFutures:
                    return "CnFutures";
                case MarketType.CnStock:
                    return "CnStock";
            }
            return null;
        }

        private StoreMethod ParseStoreMethod(string storeMethodStr)
        {
            if (storeMethodStr.ToLower().Equals("file"))
                return StoreMethod.File;

            return default(StoreMethod);
        }

        private string GetStoreMethodString(StoreMethod storeMethod)
        {
            switch (storeMethod)
            {
                case StoreMethod.File:
                    return "File";
            }
            return null;
        }

        public void LoadXml(XmlElement elem)
        {
            this.marketType = ParseMarketType(elem.GetAttribute(ATTRIBUTE_MARKETTYPE));
            this.id = elem.GetAttribute(ATTRIBUTE_ID);
            this.uri = elem.GetAttribute(ATTRIBUTE_URI);
            this.storeMethod = ParseStoreMethod(elem.GetAttribute(ATTRIBUTE_STOREMETHOD));

            XmlElement elemStoreTypes = (XmlElement)elem.ChildNodes[0];
            this.storeDataTypes.LoadXml(elemStoreTypes);
        }
    }
}
