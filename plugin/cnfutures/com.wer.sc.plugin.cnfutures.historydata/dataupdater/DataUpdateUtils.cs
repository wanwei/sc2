using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class DataUpdateUtils
    {
        private DataUpdateHelper dataUpdateHelper;

        //private IDataProvider dataProvider;

        public DataUpdateUtils(DataUpdateHelper dataUpdateHelper, IDataProvider dataProvider)
        {
            this.dataUpdateHelper = dataUpdateHelper;
           // this.dataProvider = dataProvider;
        }


    }
}
