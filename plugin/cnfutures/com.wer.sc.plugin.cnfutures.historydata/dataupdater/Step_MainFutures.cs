using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.historydata.dataupdater.generator;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_MainFutures : IStep
    {
        private DataUpdateHelper dataUpdateHelper;
        private MainFuturesScan scan;

        public Step_MainFutures(DataUpdateHelper dataUpdateHelper)
        {
            this.dataUpdateHelper = dataUpdateHelper;
            string csvPath = this.dataUpdateHelper.GetPath_Csv();
            List<CodeInfo> codes = this.dataUpdateHelper.GetAllCodes();
            this.scan = new MainFuturesScan(csvPath, codes);
        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新主力合约";
            }
        }

        public string Proceed()
        {
            IList<MainContractInfo> mainContracts = this.dataUpdateHelper.GetMainContractInfos();
            List<MainContractInfo> contracts = new List<MainContractInfo>();
            CacheUtils_MainContract cache = new CacheUtils_MainContract(mainContracts);
            ITradingDayReader reader = this.dataUpdateHelper.GetAllTradingDayReader();
            string[] varieties = this.dataUpdateHelper.GetAllVarieties();
            for (int i = 0; i < varieties.Length; i++)
            {
                string variety = varieties[i];
                Proceed(variety, cache, contracts, reader);
            }
            contracts.Sort();

            string path = dataUpdateHelper.GetPath_MainFutures();
            string[] contents = new string[contracts.Count];
            for (int i = 0; i < contracts.Count; i++)
            {
                contents[i] = contracts[i].ToString();
            }
            File.WriteAllLines(path, contents);
            return "";
        }

        private void Proceed(string variety, CacheUtils_MainContract cache, List<MainContractInfo> contracts, ITradingDayReader reader)
        {
            MainContractInfo contractInfo = cache.GetRecentMainContract(variety);
            if (contractInfo == null)
            {
                List<int> tradingDays = reader.GetAllTradingDays();
                MainFutures mf = scan.Scan(variety, tradingDays);
                contracts.AddRange(mf.mainFutures);
            }
            else
            {
                int tradingDay = contractInfo.End;
                int nextTradingDay = reader.GetNextTradingDay(tradingDay);
                if (nextTradingDay < 0)
                {
                    contracts.AddRange(cache.GetMainContractInfos(variety));
                    return;
                }
                List<int> allTradingDays = reader.GetAllTradingDays();
                IList<int> tradingDays = reader.GetTradingDays(nextTradingDay, allTradingDays[allTradingDays.Count - 1]);
                MainFutures mf = scan.Scan(variety, tradingDays);
                if (mf.mainFutures.Count > 0)
                {
                    MainContractInfo firstMf = mf.mainFutures[0];
                    if (firstMf.Code == contractInfo.Code)
                    {
                        contractInfo.End = firstMf.End;
                        mf.mainFutures.RemoveAt(0);
                    }
                    contracts.AddRange(cache.GetMainContractInfos(variety));
                    contracts.AddRange(mf.mainFutures);
                }
            }
        }
    }
}