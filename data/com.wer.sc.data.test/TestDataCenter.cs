using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class TestDataCenter
    {
        public static string URI = "file:/E:/SCDATA/CNFUTURES/";

        private static DataCenter _instance = Init();

        private static DataCenter Init()
        {
            return DataCenterManager.Instance.GetDataCenterByUri(URI);
        }

        public static DataCenter Instance
        {
            get
            {
                return _instance;
            }            
        }
    }
}
