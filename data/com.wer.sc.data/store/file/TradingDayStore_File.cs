using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    /// <summary>
    /// 开盘日数据存储，存储成CSV格式
    /// </summary>
    public class TradingDayStore_File : ITradingDayStore
    {
        private String path;

        public TradingDayStore_File(String path)
        {
            this.path = path;
        }

        public void Save(List<int> tradingDays)
        {
            CsvUtils_TradingDay.Save(path, tradingDays);
        }

        public List<int> Load()
        {
            return CsvUtils_TradingDay.Load(path);
        }

        public void Delete()
        {
            File.Delete(path);
        }
    }
}
