using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataProviderWrap
    {
        private Plugin_DataProvider provider;

        private IInstrumentReader codeReader;

        private IKLineDataReader klineDataReader;

        private ITickDataReader tickDataReader;

        private DataReaderFactory factory;

        public DataProviderWrap(Plugin_DataProvider provider)
        {
            this.factory = new DataReaderFactory(provider.GetDataPath());
            this.provider = provider;
            this.codeReader = factory.CodeReader;
            this.klineDataReader = factory.KLineDataReader;
            this.tickDataReader = factory.TickDataReader;
        }

        public DataReaderFactory GetFactory()
        {
            return factory;
        }

        public Plugin_DataProvider GetProvider()
        {
            return provider;
        }

        public String GetName()
        {
            return provider.GetName();
        }

        public String GetDescription()
        {
            return provider.GetDescription();
        }

        public String GetDataPath()
        {
            return provider.GetDataPath();
        }

        public List<InstrumentInfo> GetCurrentCodes()
        {
            return codeReader.GetAllCodes();
        }

        public List<InstrumentInfo> GetUpdateCodes()
        {
            List<InstrumentInfo> codes = provider.GetCodes();
            List<InstrumentInfo> updateCodes = new List<InstrumentInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                InstrumentInfo c = codes[i];
                if (!codeReader.Contain(c.Code))
                    updateCodes.Add(c);
            }
            return updateCodes;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="type">
        ///// 0:1分钟
        ///// 1:15分钟
        ///// 2:1小时
        ///// 3:1日
        ///// </param>
        ///// <returns></returns>
        //public int GetCurrentKLineUpdateDate(String code, int type)
        //{
        //    if (type == 0)
        //        return klineDataReader.GetLastDate(code, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
        //    if (type == 1)
        //        return klineDataReader.GetLastDate(code, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15));
        //    if (type == 2)
        //        return klineDataReader.GetLastDate(code, new KLinePeriod(KLinePeriod.TYPE_HOUR, 1));
        //    if (type == 3)
        //        return klineDataReader.GetLastDate(code, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
        //    return -1;
        //}

        //public int GetCurrentTickUpdateDate(String code)
        //{
        //    List<int> dates = tickDataReader.GetTickDates(code);
        //    return dates[dates.Count - 1];
        //}
    }
}
