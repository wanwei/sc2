using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.daily;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.appoint
{

    public class DataProvider_Appoint : IDataProvider
    {
        public const string SRCPATH1 = @"E:\FUTURES\CSV\TICK\";
        public const string SRCPATH2 = @"E:\FUTURES\CSV\TICK_JSY\";
        public const string SRCPATH3 = @"E:\FUTURES\CSV\TICK_DAILY\";

        private IDataProvider dataProvider1;
        private IDataProvider dataProvider2;
        private IDataProvider dataProvider3;

        private List<AppointUpdate> appointUpdates = new List<AppointUpdate>();

        private List<CodeInfo> allCodes = new List<CodeInfo>();

        private List<int> tradingDays = new List<int>();

        public DataProvider_Appoint(string pluginPath)
        {
            dataProvider1 = new DataProvider_BiaoPuYongHua(SRCPATH1, pluginPath);
            dataProvider2 = new DataProvider_JinShuYuan(SRCPATH2, pluginPath);
            dataProvider3 = new DataProvider_Daily(SRCPATH3, pluginPath);

            InitAllCodes();
            InitAllDates();

            string path = pluginPath + "\\config\\appoint";
            string[] appoints = File.ReadAllLines(path);
            for (int i = 0; i < appoints.Length; i++)
            {
                string str = appoints[i];
                if (str == "")
                    continue;
                string[] arr = str.Split(',');
                //没有K线的情况暂时不处理
                if (arr[2] == "0" || arr[2] == "2")
                    continue;
                AppointUpdate ap = new AppointUpdate();
                ap.Code = arr[0];
                ap.Date = int.Parse(arr[1]);
                this.appointUpdates.Add(ap);
            }
        }

        private void InitAllCodes()
        {
            AddCodes(dataProvider1.GetNewCodes());
            AddCodes(dataProvider2.GetNewCodes());
            AddCodes(dataProvider3.GetNewCodes());
        }

        private void InitAllDates()
        {
            AddDates(dataProvider1.GetNewTradingDays());
            AddDates(dataProvider2.GetNewTradingDays());
            AddDates(dataProvider3.GetNewTradingDays());
        }

        private HashSet<string> codeSet = new HashSet<string>();

        private void AddCodes(List<CodeInfo> codes)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo codeInfo = codes[i];
                if (codeSet.Contains(codeInfo.Code))
                    continue;

                codeSet.Add(codeInfo.Code);
                this.allCodes.Add(codeInfo);
            }
        }

        private HashSet<int> dateSet = new HashSet<int>();

        private void AddDates(List<int> dates)
        {
            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                if (dateSet.Contains(date))
                    continue;

                dateSet.Add(date);
                this.tradingDays.Add(date);
            }
        }

        public List<AppointUpdate> GetAppointUpdate()
        {
            return appointUpdates;
        }

        public List<CodeInfo> GetNewCodes()
        {
            return allCodes;
        }

        public List<int> GetNewTradingDays()
        {
            return tradingDays;
        }

        public ITickData LoadTickData(string code, int date)
        {
            if (date <= 20160429)
                return dataProvider1.LoadTickData(code, date);
            else if (date <= 20170331)
                return dataProvider2.LoadTickData(code, date);
            return dataProvider3.LoadTickData(code, date);
        }
    }
}
