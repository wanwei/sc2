using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class KLineDataStore_File : IKLineDataStore
    {
        private DataPathUtils dataPathUtils;

        public KLineDataStore_File(DataPathUtils dataPathUtils)
        {
            this.dataPathUtils = dataPathUtils;
        }

        /// <summary>
        /// 保存K线数据，该方法会覆盖掉之前的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <param name="data"></param>
        public void Save(string code, KLinePeriod klinePeriod, IKLineData data)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            new KLineDataStore_File_Single(path).Save(data);
        }

        /// <summary>
        /// 保存新的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <param name="data"></param>
        public void Append(string code, KLinePeriod klinePeriod, IKLineData data)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            new KLineDataStore_File_Single(path).Append(data);
        }

        /// <summary>
        /// 装载K线数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public KLineData Load(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            return (KLineData)(new KLineDataStore_File_Single(path)).Load(startDate, endDate);
        }

        /// <summary>
        /// 装载所有的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public KLineData LoadAll(string code, KLinePeriod klinePeriod)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            return (KLineData)(new KLineDataStore_File_Single(path)).LoadAll();
        }

        /// <summary>
        /// 删掉某个品种一个周期的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="klinePeriod"></param>
        public void Delete(string code, KLinePeriod klinePeriod)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            File.Delete(path);

            string indexPath = path + ".index";
            File.Delete(indexPath);
        }

        public List<int> GetAllTradingDay(string code, KLinePeriod klinePeriod)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            return (new KLineDataStore_File_Single(path)).GetAllTradingDay();
        }

        public int GetFirstTradingDay(string code, KLinePeriod klinePeriod)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            return (new KLineDataStore_File_Single(path)).GetFirstTradingDay();
        }

        public int GetLastTradingDay(string code, KLinePeriod klinePeriod)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            return (new KLineDataStore_File_Single(path)).GetLastTradingDay();
        }

        public double GetLastTradingTime(string code, KLinePeriod klinePeriod)
        {
            string path = dataPathUtils.GetKLineDataPath(code, klinePeriod);
            return (new KLineDataStore_File_Single(path)).GetLastTradingTime();
        }
    }
}
