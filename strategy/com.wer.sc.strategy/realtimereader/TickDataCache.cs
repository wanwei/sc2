using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.realtimereader
{
    /// <summary>
    /// tick的数据装载
    /// </summary>
    public class TickDataCache
    {
        private ITickData tickData;

        private ITickData lastTickData;

        public TickDataCache(IDataReader dataReader)
        {
            
        }

        public ITickData CurrentTickData
        {
            get { return tickData; }
        }
    }
}
