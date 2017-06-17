using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    /// <summary>
    /// 期货品种信息获取类
    /// </summary>
    public class DataLoader_Variety
    {
        private List<VarietyInfo> allVarietyInfos;
        private Dictionary<string, List<VarietyInfo>> dic_Exchange_Varieties = new Dictionary<string, List<VarietyInfo>>();
        private Dictionary<String, VarietyInfo> dic_Id_VarietyInfo;

        public DataLoader_Variety(string pluginPath)
        {
            PathUtils pathUtils = new PathUtils(pluginPath);
            string path = pathUtils.CatelogPath;
            string[] lines = File.ReadAllLines(path);

            this.allVarietyInfos = new List<VarietyInfo>(lines.Length);
            this.dic_Id_VarietyInfo = new Dictionary<string, VarietyInfo>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                VarietyInfo variety = GetVarietyInfo(lines[i]);
                if (variety == null)
                    continue;
                this.allVarietyInfos.Add(variety);
                this.dic_Id_VarietyInfo.Add(variety.Code, variety);
                if (this.dic_Exchange_Varieties.ContainsKey(variety.Exchange))
                {
                    this.dic_Exchange_Varieties[variety.Exchange].Add(variety);
                }
                else
                {
                    List<VarietyInfo> varieties = new List<VarietyInfo>();
                    varieties.Add(variety);
                    this.dic_Exchange_Varieties.Add(variety.Exchange, varieties);
                }
            }
        }

        private VarietyInfo GetVarietyInfo(string content)
        {
            string[] arr = content.Split(',');
            if (arr.Length < 3)
                return null;
            VarietyInfo variety = new VarietyInfo();
            variety.Code = arr[0].ToUpper();
            variety.Name = arr[1];
            variety.Exchange = arr[2].ToUpper();
            return variety;
        }

        public List<VarietyInfo> GetAllVarieties()
        {
            return allVarietyInfos;
        }

        public List<VarietyInfo> GetVarieties(string exchange)
        {
            if (exchange == null)
                return null;
            string key = exchange.ToUpper();
            if (dic_Exchange_Varieties.ContainsKey(key))
                return dic_Exchange_Varieties[key];
            return null;
        }

        public VarietyInfo GetVariety(string varietyId)
        {
            if (varietyId == null)
                return null;
            string key = varietyId.ToUpper();
            if (dic_Id_VarietyInfo.ContainsKey(key))
                return dic_Id_VarietyInfo[key];
            return null;
        }
    }
}