using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class MainContractReader : IMainContractReader
    {
        private Dictionary<string, List<MainContractInfo>> dic_Variety_Contracts = new Dictionary<string, List<MainContractInfo>>();

        public MainContractReader(IDataStore dataStore)
        {
            IList<MainContractInfo> mainContractInfos = dataStore.CreateMainContractStore().Load();
            for (int i = 0; i < mainContractInfos.Count; i++)
            {
                MainContractInfo mainContract = mainContractInfos[i];
                if (dic_Variety_Contracts.ContainsKey(mainContract.Variety))
                {
                    dic_Variety_Contracts[mainContract.Variety].Add(mainContract);
                }
                else
                {
                    List<MainContractInfo> mainContracts = new List<MainContractInfo>();
                    mainContracts.Add(mainContract);
                    dic_Variety_Contracts.Add(mainContract.Variety, mainContracts);
                }
            }
        }

        public MainContractInfo GetMainContractInfo(string variety, int date)
        {
            if (!dic_Variety_Contracts.ContainsKey(variety))
                return null;
            List<MainContractInfo> mainContracts = dic_Variety_Contracts[variety];
            for (int i = 0; i < mainContracts.Count; i++)
            {
                MainContractInfo mainContract = mainContracts[i];
                if (mainContract.Start <= date && mainContract.End >= date)
                    return mainContract;
            }
            return null;
        }

        public List<MainContractInfo> GetMainContractInfos(string variety)
        {
            if (!dic_Variety_Contracts.ContainsKey(variety))
                return null;
            return dic_Variety_Contracts[variety];
        }

        public List<MainContractInfo> GetMainContractInfos(string variety, int startDate, int endDate)
        {
            List<MainContractInfo> mainContractInfos = new List<MainContractInfo>();
            List<MainContractInfo> allContracts = GetMainContractInfos(variety);
            bool isFirst = true;
            for (int i = 0; i < allContracts.Count; i++)
            {
                MainContractInfo currentContract = allContracts[i];
                if (currentContract.End < startDate)
                    continue;
                if (currentContract.Start > endDate)
                    continue;
                MainContractInfo contract = new MainContractInfo();
                contract.CopyFrom(currentContract);
                if (isFirst)
                {                    
                    contract.Start = startDate;                    
                    isFirst = false;
                }
                if (contract.End > endDate)
                    contract.End = endDate;
                mainContractInfos.Add(contract);
            }
            return mainContractInfos;
        }

        public MainContractInfo GetNextMainContractInfo(string variety, int date)
        {
            if (!dic_Variety_Contracts.ContainsKey(variety))
                return null;
            List<MainContractInfo> mainContracts = dic_Variety_Contracts[variety];
            for (int i = 0; i < mainContracts.Count; i++)
            {
                MainContractInfo mainContract = mainContracts[i];
                if (mainContract.Start <= date && mainContract.End >= date)
                {
                    if (i == mainContracts.Count - 1)
                        return null;
                    return mainContracts[i + 1];
                }
            }
            return null;
        }

        public MainContractInfo GetPrevMainContractInfo(string variety, int date)
        {
            if (!dic_Variety_Contracts.ContainsKey(variety))
                return null;
            List<MainContractInfo> mainContracts = dic_Variety_Contracts[variety];
            for (int i = 0; i < mainContracts.Count; i++)
            {
                MainContractInfo mainContract = mainContracts[i];
                if (mainContract.Start <= date && mainContract.End >= date)
                {
                    if (i == 0)
                        return null;
                    return mainContracts[i - 1];
                }
            }
            return null;
        }

        public MainContractInfo GetRecentMainContract(string variety)
        {
            return null;
        }
    }
}
