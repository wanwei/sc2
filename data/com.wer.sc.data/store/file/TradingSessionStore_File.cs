using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class TradingSessionStore_File : ITradingSessionStore
    {
        private DataPathUtils dataPathUtils;

        public TradingSessionStore_File(DataPathUtils dataPathUtils)
        {
            this.dataPathUtils = dataPathUtils;
        }

        /// <summary>
        /// 保存一个品种的所有交易时间数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="tradingSessions"></param>
        public void Save(string code, List<TradingSession> tradingSessions)
        {
            string path = dataPathUtils.GetTradingSessionPath(code);
            CsvUtils_TradingSession.Save(path, tradingSessions);
        }

        /// <summary>
        /// 装载一个品种的交易时间数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<TradingSession> Load(string code)
        {
            string path = dataPathUtils.GetTradingSessionPath(code);
            return CsvUtils_TradingSession.Load(path);
        }

        /// <summary>
        /// 删除一个品种的交易时间数据
        /// </summary>
        /// <param name="code"></param>
        public void Delete(string code)
        {
            string path = dataPathUtils.GetTradingSessionPath(code);
            File.Delete(path);
        }
    }
}
