using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.download.sina
{
    /// <summary>
    /// 新浪数据保存
    /// --sz000002
    ///   --sz000002_kline_day.csv
    ///   --sz000002_kline_day_fuquan.csv
    ///   --tick
    ///     --sz000002_20160102.csv
    ///     --......
    /// --sh000001
    /// --codes.csv
    /// --tradingdays.csv
    /// </summary>
    public class DownloadPathUtils
    {
        private string path;

        public DownloadPathUtils(string path)
        {
            this.path = path;
        }

        public string GetPath_Codes()
        {
            return path + "\\codes.csv";
        }

        public string GetPath_Tradingday()
        {
            return path + "\\tradingdays.csv";
        }

        public string GetKLinePath(string code)
        {
            return path + "\\" + code + "\\" + code + "_kline_day.csv";
        }

        public string GetKLinePath_Fuquan(string code)
        {
            return path + "\\" + code + "\\" + code + "_kline_day_fuquan.csv";
        }

        public string GetTickPath(string code)
        {
            return path + "\\" + code + "\\tick\\";
        }

        public string GetTickPath(string code, int date)
        {
            //sz000002_20160102.csv
            return GetTickPath(code) + code + "_" + date + ".csv";
        }
    }
}
