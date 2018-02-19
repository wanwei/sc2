using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataprepare
{
    public class DataPrepare_TickData
    {
        private string GetDate(int date)
        {
            string str = date.ToString();
            return str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
        }

        public static void Main(string[] args)
        {
            DataPrepare_TickData req = new DataPrepare_TickData();
            //symbol = sh600516 & date = 2018 - 02 - 09
            req.Request("sh600516", 20180209);
        }

        public void Request(string code, int date)
        {
            string _address = "http://market.finance.sina.com.cn/downxls.php?date=" + GetDate(date) + "&symbol=" + code;
            string str = HttpGetUrl(_address);
            string[] strArr = str.Split('\n');
            for (int i = 0; i < strArr.Length; i++)
            {
                string strLine = strArr[i];
                string[] arr = strLine.Split('\t');
                for (int j = 0; j < arr.Length; j++) { 
                    Console.Write(arr[j]);
                    Console.Write(",");
                }
                Console.WriteLine();
            }
            //Console.WriteLine(str);
            //HttpClient client = new HttpClient();

            ////远程获取数据  
            //var task = client.GetAsync(_address);
            //var rep = task.Result;//在这里会等待task返回。  

            ////读取响应内容  
            //var task2 = rep.Content.ReadAsStreamAsync();
            ////.ReadAsStringAsync();
            //Stream ret = task2.Result;//在这里会等待task返回。  



            //Console.WriteLine("Hit ENTER to exit...");
            //Console.ReadLine();
        }

        public static String HttpGetUrl(String url)
        {
            string strReturn = string.Empty;
            String urlEsc = url;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(urlEsc);
            req.Method = "GET";
            try
            {
                using (WebResponse wr = req.GetResponse())
                {
                    Stream stream = wr.GetResponseStream();
                    byte[] buf = new byte[1024];
                    while (true)
                    {
                        int len = stream.Read(buf, 0, buf.Length);
                        if (len <= 0)//len <= 0 跳出循环
                            break;
                        strReturn += System.Text.Encoding.GetEncoding("gbk").GetString(buf, 0, len);//指定编码格式
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strReturn;
        }
    }
}
