using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 已经更新的数据信息
    /// </summary>
    public class UpdatedDataInfo
    {
        private string path;

        private List<string> codes_tick = new List<string>();

        private List<string> codes_kline = new List<string>();

        private List<KLinePeriod> storedKLinePeriods = new List<KLinePeriod>();

        //保存的tick信息
        private Dictionary<string, int> dic_Id_LastUpdatedTick = new Dictionary<string, int>();

        //保存的k线信息
        private Dictionary<UpdatedKLineDataKey, int> dic_CodePeriod_LastUpdatedKLine = new Dictionary<UpdatedKLineDataKey, int>();

        public UpdatedDataInfo(string path)
        {
            this.path = path;
            FileUtils.EnsureDirExist(path);
            this.Load();
        }

        public void WriteUpdateInfo_Tick(string code, int lastUpdatedDate)
        {
            code = code.ToUpper();
            if (!codes_tick.Contains(code))
                codes_tick.Add(code);
            if (dic_Id_LastUpdatedTick.ContainsKey(code))
            {
                dic_Id_LastUpdatedTick.Remove(code);
            }
            dic_Id_LastUpdatedTick.Add(code, lastUpdatedDate);
        }

        public void WriteUpdateInfo_KLine(string code, KLinePeriod klinePeriod, int lastUpdatedDate)
        {
            code = code.ToUpper();
            if (!codes_kline.Contains(code))
                codes_kline.Add(code);
            UpdatedKLineDataKey key = new UpdatedKLineDataKey();
            key.code = code;
            key.period = klinePeriod;
            if (!storedKLinePeriods.Contains(klinePeriod))
                storedKLinePeriods.Add(klinePeriod);

            if (dic_CodePeriod_LastUpdatedKLine.ContainsKey(key))
            {
                dic_CodePeriod_LastUpdatedKLine.Remove(key);
            }
            dic_CodePeriod_LastUpdatedKLine.Add(key, lastUpdatedDate);
        }

        public int GetLastUpdatedTickData(string code)
        {
            string key = code.ToUpper();
            if (dic_Id_LastUpdatedTick.ContainsKey(key))
            {
                return dic_Id_LastUpdatedTick[key];
            }
            return -1;
        }

        public int GetLastUpdatedKLineData(string code, KLinePeriod klinePeriod)
        {
            UpdatedKLineDataKey key = new UpdatedKLineDataKey();
            key.code = code.ToUpper();
            key.period = klinePeriod;

            if (dic_CodePeriod_LastUpdatedKLine.ContainsKey(key))
                return dic_CodePeriod_LastUpdatedKLine[key];
            return -1;
        }

        /// <summary>
        /// 保存更新信息
        /// </summary>
        /// <param name="path"></param>
        public void Save()
        {
            SaveTick();
            SaveKLine();
        }

        private void SaveTick()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < codes_tick.Count; i++)
            {
                string code = codes_tick[i];
                if (dic_Id_LastUpdatedTick.ContainsKey(code))
                {
                    sb.Append(code).Append(",")
                        .Append(dic_Id_LastUpdatedTick[code]).Append("\r\n");
                }
            }
            File.WriteAllText(GetPath_UpdatedTickDataInfo(), sb.ToString());
        }

        private void SaveKLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < codes_kline.Count; i++)
            {
                string code = codes_kline[i];
                SaveKLine_Code(code, sb);
            }
            File.WriteAllText(GetPath_UpdatedKLineDataInfo(), sb.ToString());
        }

        private void SaveKLine_Code(string code, StringBuilder sb)
        {
            for (int i = 0; i < storedKLinePeriods.Count; i++)
            {
                KLinePeriod klinePeriod = storedKLinePeriods[i];
                UpdatedKLineDataKey key = new UpdatedKLineDataKey();
                key.code = code;
                key.period = klinePeriod;

                if (dic_CodePeriod_LastUpdatedKLine.ContainsKey(key))
                {
                    int lastUpdateDate = dic_CodePeriod_LastUpdatedKLine[key];
                    sb.Append(code).Append(",").Append((int)klinePeriod.PeriodType)
                        .Append(",").Append(klinePeriod.Period)
                        .Append(",").Append(lastUpdateDate).Append("\r\n");
                }
            }
        }

        /// <summary>
        /// 装载更新信息
        /// </summary>
        /// <param name="path"></param>
        public void Load()
        {
            LoadTickUpdateInfo();
            LoadKLineUpdateInfo();
        }

        private void LoadTickUpdateInfo()
        {
            this.dic_Id_LastUpdatedTick.Clear();
            this.codes_tick.Clear();
            string path_tick = GetPath_UpdatedTickDataInfo();
            if (!File.Exists(path_tick))
                return;
            string[] lines_tick = File.ReadAllLines(path_tick);
            for (int i = 0; i < lines_tick.Length; i++)
            {
                string line = lines_tick[i];
                if (StringUtils.IsEmpty(line))
                    continue;
                string[] content = line.Split(',');
                string code = content[0].Trim();
                this.dic_Id_LastUpdatedTick.Add(code, int.Parse(content[1]));
                if (!this.codes_tick.Contains(code))
                    this.codes_tick.Add(code);
            }
        }

        private void LoadKLineUpdateInfo()
        {
            this.codes_kline.Clear();
            this.storedKLinePeriods.Clear();
            //this.dic_Id_KLinePeriod_LastUpdatedKLine.Clear();
            this.dic_CodePeriod_LastUpdatedKLine.Clear();
            string path_kline = GetPath_UpdatedKLineDataInfo();
            if (!File.Exists(path_kline))
                return;
            string[] lines_kline = File.ReadAllLines(path_kline);
            for (int i = 0; i < lines_kline.Length; i++)
            {
                string line = lines_kline[i];
                if (StringUtils.IsEmpty(line))
                    continue;
                string[] content = line.Split(',');
                string code = content[0].Trim();
                UpdatedKLineDataKey key = new UpdatedKLineDataKey();
                key.code = code;
                key.period = new KLinePeriod((KLineTimeType)int.Parse(content[1]), int.Parse(content[2]));
                this.dic_CodePeriod_LastUpdatedKLine.Add(key, int.Parse(content[3]));
                if (!this.storedKLinePeriods.Contains(key.period))
                    this.storedKLinePeriods.Add(key.period);
                if (!this.codes_kline.Contains(code))
                    this.codes_kline.Add(code);
            }
        }
        private string GetPath_UpdatedTickDataInfo()
        {
            /*
             * 更新
             * M1401,20160401
             * M1405,20160401
             */
            return path + "\\updatedtickdata";
        }

        private string GetPath_UpdatedKLineDataInfo()
        {
            /*
             * 更新，code,klineperiod.type,period,date
             * M1401,1,5,20160401
             * M1401,1,15,20160401
             */
            return path + "\\updatedklinedata";
        }
    }

    public class UpdatedKLineDataKey
    {
        public string code;

        public KLinePeriod period;

        public override bool Equals(object obj)
        {
            if (!(obj is UpdatedKLineDataKey))
                return false;
            UpdatedKLineDataKey key = (UpdatedKLineDataKey)obj;
            if (this.period.Equals(key.period) && this.code == key.code)
                return true;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return code.GetHashCode() * 10 + period.GetHashCode();
        }
    }

    public class UpdatedKLineData
    {
        public string code;

        public KLinePeriod period;

        public int lastDate;
    }

    public class UpdatedTickData
    {
        public string code;

        public int lastDate;
    }
}