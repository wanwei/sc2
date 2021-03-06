﻿using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan
{
    /// <summary>
    /// Tick数据的原始数据加载
    /// </summary>
    public class DataProvider_JinShuYuan_TickData
    {
        private string srcDataPath;

        //private CodeInfoGenerator dataLoader_InstrumentInfo;
        private DataLoader_Variety dataLoader_Variety;

        private DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        public DataProvider_JinShuYuan_TickData(string srcDataPath, string pluginPath)
        {
            this.srcDataPath = srcDataPath;
            this.dataLoader_Variety = new DataLoader_Variety(pluginPath);
            //this.dataLoader_InstrumentInfo = new CodeInfoGenerator(pluginPath);
            this.dataLoader_TradingSessionDetail = new DataLoader_TradingSessionDetail(pluginPath, dataLoader_Variety);
        }

        public TickData GetOriginalTickData(string code, int date)
        {
            String path = GetTickDataPath(code, date);
            if (!File.Exists(path))
                return null;
            return GetTickData(path);
        }

        public static TickData GetTickData(string path)
        {
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return ReadLinesToTickData(lines);
        }

        public ITickData GetTickData(string code, int date)
        {
            return GetOriginalTickData(code, date);
        }

        private static TickData ReadLinesToTickData(string[] lines)
        {
            int totalmount = 0;
            int cnt = GetEmptyLines(lines);
            TickData data = new TickData(lines.Length - 1 - cnt);
            // lines[0].Split(',');
            for (int i = 0; i < lines.Length - 1 - cnt; i++)
            {
                String line = lines[i + 1];
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                if (dataArr.Length > 30)
                    return ReadLinesToTickData_Full(lines);
                if (dataArr.Length < 5)
                    continue;

                //String[] dateArr = dataArr[2].Split('-');
                //double date = double.Parse(dateArr[0] + Fill(dateArr[1]) + Fill(dateArr[2]));
                //String[] timeArr = dataArr[1].Split(':');
                //double time = double.Parse(timeArr[0] + timeArr[1] + timeArr[2]);
                //double fulltime = date + time / 1000000;


                double fulltime = GetFullTime(dataArr[2]);

                data.arr_time[i] = fulltime;
                data.arr_price[i] = float.Parse(dataArr[3]);
                data.arr_mount[i] = (int)float.Parse(dataArr[7]);
                data.Arr_Hold[i] = int.Parse(dataArr[4]);
                data.arr_add[i] = int.Parse(dataArr[5]);
                if (i == 0)
                    data.arr_add[i] += data.Arr_Hold[i];
                //data.arr_ = int.Parse(dataArr[6]);
                totalmount += data.arr_mount[i];
                data.arr_totalMount[i] = totalmount;
                data.arr_buyPrice[i] = float.Parse(dataArr[12]);
                data.arr_buyMount[i] = int.Parse(dataArr[14]);
                data.arr_sellPrice[i] = float.Parse(dataArr[13]);
                data.arr_sellMount[i] = int.Parse(dataArr[15]);
                data.arr_isBuy[i] = dataArr[11].Equals("B");
            }
            return data;
        }

        private static TickData ReadLinesToTickData_Full(string[] lines)
        {
            int totalmount = 0;
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

                double day = int.Parse(dataArr[43]);
                string second = dataArr[20];
                string secondStr = second.Substring(0, 2) + second.Substring(3, 2) + second.Substring(6, 2);
                int secondInt = int.Parse(secondStr);
                double fulltime = day + Math.Round((double)secondInt / 1000000, 6);

                data.arr_time[i] = fulltime;
                data.arr_price[i] = float.Parse(dataArr[4]);
                if (i == 0)
                    data.arr_mount[i] = (int)float.Parse(dataArr[11]);
                else
                    data.arr_mount[i] = (int)float.Parse(dataArr[11]) - data.arr_totalMount[i - 1];
                data.Arr_Hold[i] = int.Parse(dataArr[13]);
                if (i == 0)
                    data.arr_add[i] += data.Arr_Hold[i];
                else
                    data.arr_add[i] = data.Arr_Hold[i] - data.Arr_Hold[i - 1];
                //data.arr_ = int.Parse(dataArr[6]);
                totalmount += data.arr_mount[i];
                data.arr_totalMount[i] = totalmount;
                data.arr_buyPrice[i] = float.Parse(dataArr[22]);
                data.arr_buyMount[i] = int.Parse(dataArr[23]);
                data.arr_sellPrice[i] = float.Parse(dataArr[24]);
                data.arr_sellMount[i] = int.Parse(dataArr[25]);
                data.arr_isBuy[i] = data.Arr_SellPrice[i] == data.Arr_Price[i];
            }
            return data;
        }

        private static double GetFullTime(String timeStr)
        {
            //2017-04-05 08:59:00.294
            string dayStr = timeStr.Substring(0, 4) + timeStr.Substring(5, 2) + timeStr.Substring(8, 2);
            string secondStr = timeStr.Substring(11, 2) + timeStr.Substring(14, 2) + timeStr.Substring(17, 2);
            int day = int.Parse(dayStr);
            int second = int.Parse(secondStr);
            return day + Math.Round((double)second / 1000000, 6);
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

        private String GetTickDataPath(String code, int date)
        {
            int month = date / 100;

            CodeIdParser parser = new CodeIdParser(code);
            VarietyInfo varietyInfo = dataLoader_Variety.GetVariety(parser.VarietyId);
            if (varietyInfo == null)
                return null;
            string market = varietyInfo.Exchange;
            if (market.Equals("DL"))
                market = "dc";
            else if (market.Equals("SQ"))
                market = "sc";
            else if (market.Equals("ZZ"))
                market = "zc";

            string path = srcDataPath + "\\" + month + "\\" + market + "\\" + GetFullCode(code, parser, market == "zc") + "_" + date + ".csv";
            return path;

            //return srcDataPath + "\\" + dataLoader_InstrumentInfo.GetBelongMarket(code) + "\\" + date + "\\" + code + "_" + date + ".csv";
        }

        private string GetFullCode(String code, CodeIdParser parser, bool isZZ)
        {
            if (isZZ)
            {
                string suffix = parser.Suffix;
                return parser.VarietyId + suffix.Substring(1, 3);
            }
            return code;
        }
    }
}