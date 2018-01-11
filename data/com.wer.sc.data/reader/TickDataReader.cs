using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class TickDataReader : ITickDataReader
    {
        private ITickDataStore tickDataStore;

        private IDataReader dataReader;

        public TickDataReader(IDataStore dataStore, IDataReader dataReader)
        {
            this.tickDataStore = dataStore.CreateTickDataStore();
            this.dataReader = dataReader;
        }

        //public List<int> GetTickDates(String code)
        //{            
        //    string path = utils.GetTickPath(code);
        //    if (!Directory.Exists(path))
        //        return new List<int>();
        //    String[] files = Directory.GetFiles(path);                        
        //    List<int> ticks = new List<int>();
        //    foreach (String file in files)
        //    {
        //        int startIndex = file.IndexOf('_') + 1;
        //        ticks.Add(int.Parse(file.Substring(startIndex, 8)));
        //    }
        //    ticks.Sort();
        //    return ticks;
        //}

        public TickData GetTickData(String code, int date)
        {
            return tickDataStore.Load(code, date);
        }

        public ITickData_Extend GetTickData_Extend(string code, int date)
        {
            ITickData tickData = GetTickData(code, date);
            if (tickData == null)
                return null;
            ITradingTime tradingTime = this.dataReader.CreateTradingTimeReader(code).GetTradingTime(date);
            return new TickData_Extend(tickData, tradingTime);
        }
    }
}