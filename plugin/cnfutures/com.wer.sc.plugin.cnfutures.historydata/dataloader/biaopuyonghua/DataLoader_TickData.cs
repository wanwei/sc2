using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.adjust;
using com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.biaopuyonghua
{
    /// <summary>
    /// Tick数据的原始数据加载
    /// </summary>
    public class DataLoader_TickData
    {
        private string srcDataPath;

        private IDataLoader dataLoader;

        private DataLoader_InstrumentInfo dataLoader_InstrumentInfo;

        public DataLoader_TickData(string srcDataPath, IDataLoader dataLoader)
        {
            this.srcDataPath = srcDataPath;
            this.dataLoader = dataLoader;            
            this.dataLoader_InstrumentInfo = ((DataLoader_Abstract)dataLoader).DataLoader_InstrumentInfo;
        }

        public TickData GetOriginalTickData(string code, int date)
        {
            String path = GetCodePath(code, date);
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return ReadLinesToTickData(lines);
        }

        public ITickData GetTickData(string code, int date)
        {
            if (code.EndsWith("13"))
            {
                DataGenerator_TickData_Index gen = new DataGenerator_TickData_Index(dataLoader);
                return gen.Generate(code.Substring(0, code.Length - 2), date);
            }
            if (code.EndsWith("MI"))
            {
                DataGenerator_TickData_Main gen = new DataGenerator_TickData_Main(dataLoader);
                return gen.Generate(code.Substring(0, code.Length - 2), date);
            }
            TickDataAdjuster tickDataAdjuster = new TickDataAdjuster();
            TickData tickData = GetOriginalTickData(code, date);
            if (tickData == null)
                return null;
            tickDataAdjuster.Adjust(tickData, dataLoader.LoadTradingSessionDetail(code, date));
            return tickData;
        }

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
            return srcDataPath + "\\" + dataLoader_InstrumentInfo.GetBelongMarket(code) + "\\" + date + "\\" + code + "_" + date + ".csv";
        }
    }
}
