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
        public TickDataReader(IDataStore dataStore)
        {
            this.tickDataStore = dataStore.CreateTickDataStore();
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
    }
}