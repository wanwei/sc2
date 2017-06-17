using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 已经更新的数据信息
    /// </summary>
    public class UpdatedDataInfo
    {
        private Dictionary<string, int> dic_Id_LastUpdatedTick = new Dictionary<string, int>();

        private Dictionary<UpdatedKLineDataKey, int> dic_CodePeriod_LastUpdatedKLine = new Dictionary<UpdatedKLineDataKey, int>();

        public int GetLastUpdatedTickData(string code)
        {
            string key = code.ToUpper();
            if (dic_Id_LastUpdatedTick.ContainsKey(key))
            {
                return dic_Id_LastUpdatedTick[key];
            }
            return -1;
        }

        public int GetLastUpdatedKLineData(string code, KLinePeriod klinePeriod)
        {
            return -1;
        }
    }

    public class UpdatedKLineDataKey
    {
        public string code;

        public KLinePeriod period;
    }

    public class UpdatedKLineData
    {
        public string code;

        public KLinePeriod period;

        public int lastDate;
    }

    public class UpdatedTickData
    {
        public string code;

        public int lastDate;
    }
}