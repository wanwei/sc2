using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace com.wer.sc.data.store.file
{
    /// <summary>
    /// k线数据存储，存储成二进制格式
    /// </summary>
    public class KLineDataStore_File_Single
    {
        public const int LEN_EVERYKLINE = 36;

        private String path;

        private KLineDataIndexer indexer;

        public KLineDataStore_File_Single(String path)
        {
            this.path = path;
            this.indexer = new KLineDataIndexer(path);
        }

        public void Save(IKLineData data)
        {
            DirectoryInfo dir = Directory.GetParent(path);
            if (!dir.Exists)
                dir.Create();

            byte[] bs = GetBytes(data);
            FileStream file = new FileStream(path, FileMode.Create);
            try
            {
                file.Write(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public void Append(IKLineData data)
        {
            byte[] bs = GetBytes(data);
            FileStream file = new FileStream(path, FileMode.Append);
            try
            {
                file.Write(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public KLineData LoadAll()
        {
            if (!File.Exists(path))
                return null;
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                byte[] bs = new byte[file.Length];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);

                return FromBytes(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public IKLineData LoadByIndex(int startIndex, int endIndex)
        {
            if (!File.Exists(path))
                return null;
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                byte[] bs = new byte[file.Length];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);

                return FromBytes(bs, startIndex * LEN_EVERYKLINE, (endIndex - startIndex + 1) * LEN_EVERYKLINE);
            }
            finally
            {
                file.Close();
            }
        }

        public int Length()
        {
            FileInfo f = new FileInfo(path);
            return (int)(f.Length / LEN_EVERYKLINE);
        }

        public KLineData FromBytes(byte[] bs)
        {
            return FromBytes(bs, 0, bs.Length);
        }

        public static KLineData FromBytes(byte[] bs, int start, int len)
        {
            int size = LEN_EVERYKLINE;
            int dataLength = len / size;
            KLineData data = new KLineData(dataLength);
            for (int i = 0; i < dataLength; i++)
            {
                int offset = i * size + start;
                data.arr_time[i] = BitConverter.ToDouble(bs, offset);
                offset += 8;

                data.arr_start[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_high[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_low[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_end[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_mount[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;

                data.arr_money[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_hold[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;
            }

            return data;
        }

        public static byte[] GetBytes(IKLineData data)
        {
            int size = LEN_EVERYKLINE;
            byte[] bs = new byte[size * data.Length];
            int offset = 0;
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                byte[] tmpBs = BitConverter.GetBytes(data.Time);
                Array.Copy(tmpBs, 0, bs, offset, 8);
                offset += 8;

                tmpBs = BitConverter.GetBytes(data.Start);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.High);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Low);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.End);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Mount);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Money);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Hold);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;
            }
            return bs;
        }

        public double GetFirstTime()
        {
            if (!File.Exists(path))
                return -1;
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                byte[] bs = new byte[8];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);
                return BitConverter.ToDouble(bs, 0);
            }
            finally
            {
                file.Close();
            }
        }

        public double GetLastTime()
        {
            if (!File.Exists(path))
                return -1;
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                long index = file.Length - LEN_EVERYKLINE;

                byte[] bs = new byte[8];
                file.Seek(index, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);
                return BitConverter.ToDouble(bs, 0);
            }
            finally
            {
                file.Close();
            }
        }
        
        public List<int> GetAllTradingDay()
        {
            if (!File.Exists(path))
                return null;
            KLineDataIndexResult result = LoadIndex();
            return result.DateList;
        }

        public IKLineData Load(int startDate, int endDate)
        {
            if (!File.Exists(path))
                return null;
            KLineDataIndexResult result = LoadIndex();
            return Load(startDate, endDate, result);
        }

        public IKLineData Load(int date, int beforeDayCount, int afterDayCount)
        {
            if (!File.Exists(path))
                return null;
            KLineDataIndexResult result = LoadIndex();
            int index = FindIndex(result, date, true);
            if (index < 0)
                return null;
            int startIndex = index - beforeDayCount;
            if (startIndex < 0)
                startIndex = 0;

            int endIndex = index + afterDayCount;
            if (endIndex >= result.DateList.Count)
                endIndex = result.DateList.Count - 1;

            return LoadByIndex(startIndex, endIndex);
        }

        internal IKLineData Load(int startDate, int endDate, KLineDataIndexResult result)
        {
            int startIndex = GetStartIndex(startDate, result);
            int endIndex = GetEndIndex(endDate, result);
            if (startIndex < 0 || endIndex < 0 || startIndex > endIndex)
                return null;
            return LoadByIndex(startIndex, endIndex);
        }

        internal int GetStartIndex(int startDate, KLineDataIndexResult result)
        {
            int startIndex;
            if (result.IndexDic.Keys.Contains(startDate))
                startIndex = result.IndexDic[startDate];
            else
            {
                int realStartDate = FindDate(result, startDate, true);
                startIndex = result.GetDateDataIndex(realStartDate);
            }

            return startIndex;
        }

        internal int GetEndIndex(int endDate, KLineDataIndexResult result)
        {
            //最后一个index是结束日的后一天对应的index-1
            int endIndex;
            if (result.IndexDic.Keys.Contains(endDate))
            {
                endIndex = result.NextDateIndex(endDate) - 1;
                if (endIndex < 0)
                    endIndex = Length() - 1;
            }
            else
            {
                int realEndDateNext = FindDate(result, endDate, true);
                if (realEndDateNext < 0)
                    endIndex = Length() - 1;
                else
                {
                    endIndex = result.IndexDic[realEndDateNext] - 1;
                }
            }

            return endIndex;
        }

        private int FindDate(KLineDataIndexResult result, int date, bool forward)
        {
            List<int> dateList = result.DateList;
            if (forward)
            {
                int lastDate = dateList[0];
                if (lastDate > date)
                    return lastDate;
                for (int i = 1; i < dateList.Count; i++)
                {
                    if (dateList[i] > date && dateList[i - 1] < date)
                        return dateList[i];
                }
            }
            else
            {
                int lastDate = dateList[dateList.Count - 1];
                if (lastDate < date)
                    return lastDate;
                for (int i = dateList.Count - 2; i >= 0; i--)
                {
                    if (dateList[i] < date && dateList[i + 1] > date)
                        return dateList[i];
                }
            }
            return -1;
        }

        private int FindIndex(KLineDataIndexResult result, int date, bool forward)
        {
            List<int> dateList = result.DateList;
            if (forward)
            {
                int lastDate = dateList[0];
                if (lastDate > date)
                    return 0;
                for (int i = 1; i < dateList.Count; i++)
                {
                    if (dateList[i] > date && dateList[i - 1] < date)
                        return result.IndexDic[dateList[i]];
                }
            }
            else
            {
                int lastDate = dateList[dateList.Count - 1];
                if (lastDate < date)
                    return 0;
                for (int i = dateList.Count - 2; i >= 0; i--)
                {
                    if (dateList[i] < date && dateList[i + 1] > date)
                        return result.IndexDic[dateList[i]];
                }
            }
            return -1;
        }

        internal KLineDataIndexResult LoadIndex()
        {
            KLineDataIndexResult result = LoadIndexInternal();
            if (result == null)
            {
                DoIndex();
                return LoadIndexInternal();
            }
            else
            {
                int lastDate = (int)GetLastTime();
                if (result.LastDate != lastDate)
                {
                    DoIndex();
                    return LoadIndexInternal();
                }
                else
                    return result;
            }
        }

        private KLineDataIndexResult LoadIndexInternal()
        {
            return indexer.GetIndexResult();
        }

        /// <summary>
        /// 生成索引
        /// </summary>
        private void DoIndex()
        {
            indexer.DoIndex();
        }

        public int GetFirstTradingDay()
        {
            KLineDataIndexResult indexResult = LoadIndex();
            if (indexResult == null)
                return -1;
            return indexResult.FirstDate;
        }

        public int GetLastTradingDay()
        {
            KLineDataIndexResult indexResult = LoadIndex();
            if (indexResult == null)
                return -1;
            return indexResult.LastDate;
        }

        public double GetLastTradingTime()
        {
            return GetLastTime();
        }
    }

    public class KLineDataIndexResult
    {
        private Dictionary<int, int> indexDic = new Dictionary<int, int>();
        private List<int> dateList = new List<int>();
        //private Dictionary<int, int> dateDic = new Dictionary<int, int>();

        public void AddIndex(int date, int index)
        {
            //TODO 此处应该是不会有重复KEY的
            //以前加上判断原因是m03数据生成有误，20151222生成的时间错位
            //但是m03数据基本不可用，所以注释掉
            //if (!IndexDic.Keys.Contains(date))
            //{
            IndexDic.Add(date, index);
            dateList.Add(date);
            //}
        }

        public int GetDateDataIndex(int date)
        {
            if (indexDic.Keys.Contains(date))
                return indexDic[date];
            return -1;
        }

        public Dictionary<int, int> IndexDic
        {
            get
            {
                return indexDic;
            }
        }

        public List<int> DateList
        {
            get
            {
                return dateList;
            }
        }

        public int GetDateIndex(int date)
        {
            return dateList.IndexOf(date);
        }

        public int NextDate(int date)
        {
            int index = GetDateIndex(date) + 1;
            if (index < 0 || index >= dateList.Count)
                return -1;
            return dateList[index];
        }

        public int NextDateIndex(int date)
        {
            int nextdate = NextDate(date);
            if (nextdate > 0)
                return indexDic[nextdate];
            return -1;
        }

        public int PrevDate(int date)
        {
            int index = GetDateIndex(date) - 1;
            if (index < 0)
                return -1;
            return dateList[index];
        }

        public int PrevDateIndex(int date)
        {
            int prevdate = PrevDate(date);
            if (prevdate > 0)
                return indexDic[prevdate];
            return -1;
        }

        public int FirstDate
        {
            get
            {
                return dateList[0];
            }
        }

        public int LastDate
        {
            get
            {
                return dateList[dateList.Count - 1];
            }
        }
    }
}