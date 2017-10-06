using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.data.cnfutures;
using System.IO;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua.adjust;
using com.wer.sc.data.utils;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua
{
    public class DataProvider_BiaoPuYongHua : IDataProvider
    {
        private string srcDataPath;

        private string pluginPath;

        //private CodeInfoGenerator codeInfoGenerator;

        private DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        private DataProvider_BiaoPuYongHua_TradingDay dataprovider_TradingDay;

        private DataProvider_BiaoPuYongHua_CodeInfo dataprovider_CodeInfo;

        private DataLoader_Variety dataLoader_Variety;

        //private Dictionary<string, int> dic_OldCodeId_IpoDate = new Dictionary<string, int>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="srcDataPath">数据源目录</param>
        /// <param name="pluginPath">插件所在目录</param>
        public DataProvider_BiaoPuYongHua(string srcDataPath, string pluginPath)
        {
            this.srcDataPath = srcDataPath;
            this.pluginPath = pluginPath;
            this.dataLoader_Variety = new DataLoader_Variety(pluginPath);
            this.dataprovider_CodeInfo = new DataProvider_BiaoPuYongHua_CodeInfo(srcDataPath, pluginPath);
            this.dataprovider_TradingDay = new DataProvider_BiaoPuYongHua_TradingDay(srcDataPath, pluginPath);
            this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(pluginPath, dataLoader_Variety);
        }

        private void Init()
        {
            //if (this.codeInfoGenerator != null)
            //    return;
            //this.codeInfoGenerator = new CodeInfoGenerator(pluginPath);
            //this.dataprovider_TradingDay = new DataProvider_BiaoPuYongHua_TradingDay(srcDataPath);
            //this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(pluginPath, codeInfoGenerator);
            //this.initIpoDates();
        }

        //private void initIpoDates()
        //{
        //    List<CodeInfo> codes = this.codeInfoGenerator.GetAllOldCodes();
        //    for (int i = 0; i < codes.Count; i++)
        //    {
        //        string code = codes[i].Code;
        //        dic_OldCodeId_IpoDate.Add(code, GetIpoDateInternal(code));
        //    }
        //}

        //private int GetIpoDateInternal(string oldCodeId)
        //{
        //    ITradingDayReader reader = this.dataprovider_TradingDay.GetTradingDayReader();
        //    List<int> tradingDays = reader.GetAllTradingDays();
        //    for (int i = 0; i < tradingDays.Count; i++)
        //    {
        //        int date = tradingDays[i];
        //        string path = GetOldCodePath(oldCodeId, date);
        //        if (File.Exists(path))
        //            return date;
        //    }
        //    return -1;
        //}

        //private String GetOldCodePath(String code, int date)
        //{
        //    return srcDataPath + "\\" + codeInfoGenerator.DataLoader_CodeInfo.GetBelongMarket(code) + "\\" + date + "\\" + code + "_" + date + ".csv";
        //}

        //private int GetIpoDate(String oldCodeId)
        //{
        //    Init();
        //    if (dic_OldCodeId_IpoDate.ContainsKey(oldCodeId))
        //        return dic_OldCodeId_IpoDate[oldCodeId];
        //    return -1;
        //}

        #region 获得tickdata

        public ITickData LoadTickData(string code, int date)
        {
            TickDataAdjuster tickDataAdjuster = new TickDataAdjuster();
            TickData tickData = LoadOriginalTickData(code, date);
            if (tickData == null)
                return null;
            tickDataAdjuster.Adjust(tickData, dataLoader_TradingSessionDetail.GetTradingTime(code, date));
            return tickData;
        }

        private TickData LoadOriginalTickData(String code, int date)
        {
            String path = GetCodePath(code, date);
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            TickData tickData = ReadLinesToTickData(lines);
            //I1601_20150807 错位4个小时
            //I1609_20160104 错位4个小时
            //MA1601_20150807 错位4小时
            if (IsErrorTick(code, date))
                AdjustTick(tickData, code);
            return tickData;
        }

        private void AdjustTick(TickData tickData, string code)
        {
            for (int i = 0; i < tickData.Length; i++)
            {
                double time = tickData.arr_time[i];
                time = TimeUtils.AddHours(time, 4);
                if (code == "I1609")
                {
                    int date = (int)Math.Round(time);
                    if (date == 20160102)
                    {
                        time = Math.Round(time + 2, 6);
                    }
                }
                tickData.arr_time[i] = time;
            }
        }

        private bool IsErrorTick(string code, int date)
        {
            if (code.Equals("I1601"))
                return date == 20150807;
            if (code.Equals("I1609"))
                return date == 20160104;
            if (code.Equals("MA601"))
                return date == 20150807;
            return false;
        }
        // ErrorCodeDate
        //private string[] errCodes;
        //private int[] errDates;

        //string[] errTick = new string[]
        //{ "I1601_20150807", "I1609_20160104","MA1601_20150807" };

        private static TickData ReadLinesToTickData(string[] lines)
        {
            int cnt = GetEmptyLines(lines);
            TickData data = new TickData(lines.Length - 1 - cnt);
            for (int i = 0; i < lines.Length - 1 - cnt; i++)
            {
                String line = lines[i + 1];
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                if (dataArr.Length < 5)
                    continue;

                String[] dateArr = dataArr[0].Split('-');
                double date = double.Parse(dateArr[0] + Fill(dateArr[1]) + Fill(dateArr[2]));
                String[] timeArr = dataArr[1].Split(':');
                double time = double.Parse(timeArr[0] + timeArr[1] + timeArr[2]);
                double fulltime = date + time / 1000000;

                data.arr_time[i] = fulltime;
                data.arr_price[i] = float.Parse(dataArr[2]);
                data.arr_mount[i] = int.Parse(dataArr[3]);
                data.arr_totalMount[i] = int.Parse(dataArr[4]);
                data.arr_add[i] = int.Parse(dataArr[5]);
                data.arr_buyPrice[i] = (int)float.Parse(dataArr[6]);
                data.arr_buyMount[i] = int.Parse(dataArr[7]);
                data.arr_sellPrice[i] = (int)float.Parse(dataArr[12]);
                data.arr_sellMount[i] = int.Parse(dataArr[13]);
                data.arr_isBuy[i] = dataArr[18].Equals("B");
            }
            return data;
        }

        private static int GetEmptyLines(string[] lines)
        {
            int cnt = 0;
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (lines[i].Trim().Equals(""))
                    cnt++;
                else
                    break;
            }
            return cnt;
        }

        private static String Fill(String s)
        {
            if (s.Length == 1)
                return "0" + s;
            return s;
        }

        private String GetCodePath(String code, int date)
        {
            CodeIdParser parser = new CodeIdParser(code);
            VarietyInfo varietyInfo = dataLoader_Variety.GetVariety(parser.VarietyId);
            return srcDataPath + "\\" + varietyInfo.Exchange + "\\" + date + "\\"
                + CodeInfoUtils.GetSimpleCodeId(code) + "_" + date + ".csv";
        }

        #endregion

        public List<int> GetNewTradingDays()
        {
            return dataprovider_TradingDay.GetTradingDays();
        }

        public List<CodeInfo> GetNewCodes()
        {
            return dataprovider_CodeInfo.GetNewCodes();
        }

        public List<AppointUpdate> GetAppointUpdate()
        {
            return null;
        }
    }

    class ErrorCodeDate
    {
        string code;
        int date;
    }
}