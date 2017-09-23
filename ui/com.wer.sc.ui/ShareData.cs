using com.wer.sc.data.datacenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui
{
    public class ShareData
    {
        private string dataCenterUrl = @"E:\scdata\cnfutures\";

        public string DataCenterUrl
        {
            get
            {
                return dataCenterUrl;
            }

            set
            {
                dataCenterUrl = value;
            }
        }

        private DataCenter dataCenter;

        private static ShareData instance = new ShareData();

        public static ShareData Instance
        {
            get { return instance; }
        }

        private object lockObj = new object();

        public DataCenter DataCenter
        {
            get
            {
                lock (lockObj)
                {
                    if (dataCenter == null)
                        dataCenter = DataCenterManager.Instance.GetDataCenter(dataCenterUrl);
                    return dataCenter;
                }
            }
        }
    }
}
