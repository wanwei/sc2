using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    /// <summary>
    /// 用户需要将数据保存成以下方式：
    /// 数据目录：
    ///     --opendate
    ///     --codes
    ///     --maincontracts
    ///     --m01
    ///         --tick
    ///             --M01_20040102.tick
    ///             --M01_20040105.tick
    ///             --......
    ///         --M01_1minute.kline
    ///         --M01_1hour.kline
    ///         --......
    ///         --M01_dayopentime
    ///     --m03
    ///     --......
    /// </summary>
    public class DataPathUtils
    {
        private String dataPath;

        public DataPathUtils(String dataPath)
        {
            this.dataPath = RealPath(dataPath);
        }
        public String GetUpdateInfoPath()
        {
            return dataPath + "\\updateinfo";
        }

        public String GetInstrumentPath()
        {
            return dataPath + "\\instruments";
        }

        public String GetTradingDayPath()
        {
            return dataPath + "\\tradingday";
        }

        public string GetMainContractsPath()
        {
            return dataPath + "\\maincontracts";
        }

        public string GetTickPath(string code)
        {
            return dataPath + "\\" + code + "\\tick\\";
        }

        public string GetTickPath(string code, int date)
        {
            String realPath = GetTickPath(code) + code + "_" + date + ".tick";
            return realPath;
        }

        public string GetTradingSessionPath(string code)
        {
            String realPath = dataPath + "\\" + code + "\\" + code + "_dayopentime";
            return realPath;
        }

        public string GetTradingTimePath(string code)
        {
            String realPath = dataPath + "\\" + code + "\\" + code + "_tradingtime";
            return realPath;
        }

        public String GetKLineDataPath(String code, KLinePeriod period)
        {
            String realPath = dataPath + "\\" + code + "\\" + code + "_" + period.Period + GetPeriodTypeName(period.PeriodType) + ".kline";
            return realPath;
        }

        public String GetAccountPath()
        {
            return dataPath + "\\TRADE\\ACCOUNT\\";
        }

        public String GetAccountPath(string accountID)
        {
            return dataPath + "\\TRADE\\ACCOUNT\\" + accountID + ".account";
        }

        public String GetAccountPath_Fee()
        {
            return dataPath + "\\TRADE\\account.fee";
        }

        private String GetPeriodTypeName(KLineTimeType type)
        {
            switch (type)
            {
                case KLineTimeType.SECOND:
                    return "second";
                case KLineTimeType.MINUTE:
                    return "minute";
                case KLineTimeType.HOUR:
                    return "hour";
                case KLineTimeType.DAY:
                    return "day";
                case KLineTimeType.WEEK:
                    return "week";
            }
            return "";
        }

        private String RealPath(String path2)
        {
            String path = path2;
            if (!path.EndsWith("\\") || !path.EndsWith("/"))
                path += "\\";
            return path;
        }
    }
}