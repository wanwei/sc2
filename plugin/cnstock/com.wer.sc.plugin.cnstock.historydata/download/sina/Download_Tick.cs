using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.download.sina
{
    public class Download_Tick
    {
        /// <summary>
        /// 数据格式：
        /// 
        /// 成交时间,成交价,价格变动,成交量(手),成交额(元),性质
        //15:00:03,31.27,0.02,8232,25741870,买盘
        //14:57:00,31.25,-0.01,119,371875,卖盘
        //14:56:57,31.26,-0.01,250,781500,卖盘
        //14:56:54,31.27,0.01,151,472177,买盘
        //14:56:51,31.26,--,47,146922,卖盘
        //14:56:48,31.26,--,245,765870,卖盘
        //14:56:45,31.26,0.01,164,512664,中性盘
        //14:56:42,31.25,--,283,884375,卖盘
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<string[]> Request(string code, int date)
        {
            string _address = "http://market.finance.sina.com.cn/downxls.php?date=" + GetDate(date) + "&symbol=" + code;
            string str = HttpUtils.HttpGetUrl(_address, "gbk");
            string[] strArr = str.Split('\n');
            List<string[]> ticks = new List<string[]>();
            for (int i = 0; i < strArr.Length; i++)
            {
                string strLine = strArr[i];
                string[] arr = strLine.Split('\t');
                ticks.Add(arr);
            }
            return ticks;
        }

        private static string GetDate(int date)
        {
            string str = date.ToString();
            return str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
        }
    }
}
