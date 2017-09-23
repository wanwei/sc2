using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CacheUtils_CodeInfo
    {
        private List<CodeInfo> codes = new List<CodeInfo>();

        private Dictionary<String, CodeInfo> dic_Code_Codes = new Dictionary<string, CodeInfo>();

        private List<String> catelogs = new List<string>();

        private Dictionary<String, List<CodeInfo>> dic_Catelog_Codes = new Dictionary<string, List<CodeInfo>>();

        private Dictionary<string, List<CodeInfo>> dic_Exhcange_Codes = new Dictionary<string, List<CodeInfo>>();

        public CacheUtils_CodeInfo(List<CodeInfo> codes)
        {
            this.codes = codes;
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                dic_Code_Codes.Add(code.Code.ToUpper(), code);

                string catelog = code.Catelog.ToUpper();
                if (!catelogs.Contains(catelog))
                    catelogs.Add(catelog);

                if (dic_Catelog_Codes.ContainsKey(catelog))
                {
                    dic_Catelog_Codes[catelog].Add(code);
                }
                else
                {
                    List<CodeInfo> catelogCodes = new List<CodeInfo>();
                    catelogCodes.Add(code);
                    dic_Catelog_Codes.Add(catelog, catelogCodes);
                }

                string exchange = code.Exchange.ToUpper();
                if(dic_Exhcange_Codes.ContainsKey(exchange))
                {
                    dic_Exhcange_Codes[exchange].Add(code);
                }
                else
                {
                    List<CodeInfo> exchangeCodes = new List<CodeInfo>();
                    exchangeCodes.Add(code);
                    dic_Exhcange_Codes.Add(exchange, exchangeCodes);
                }
            }
        }

        public List<CodeInfo> GetAllCodes()
        {
            return codes;
        }

        public bool Contain(String code)
        {
            return dic_Code_Codes.ContainsKey(code.ToUpper());
        }

        public CodeInfo GetCode(String code)
        {
            string key = code.ToUpper();
            if (dic_Code_Codes.ContainsKey(key))
                return dic_Code_Codes[key];
            return null;
        }

        public List<String> GetAllCatelogs()
        {
            return catelogs;
        }

        public List<CodeInfo> GetCodesByCatelog(String catelog)
        {
            string key = catelog.ToUpper();
            if (dic_Catelog_Codes.ContainsKey(catelog))
                return dic_Catelog_Codes[key];
            return null;
        }

        public List<CodeInfo> GetCodesByExchange(string exchange)
        {
            string key = exchange.ToUpper();
            if (dic_Exhcange_Codes.ContainsKey(exchange))
                return dic_Exhcange_Codes[key];
            return null;
        }

        public List<CodeInfo> GetCodesByCatelog(String catelog, int day)
        {
            List<CodeInfo> codes = GetCodesByCatelog(catelog);
            List<CodeInfo> dayCodes = new List<CodeInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                if (code.Start <= day && code.End >= day)
                    dayCodes.Add(code);
            }
            return dayCodes;
        }
    }
}