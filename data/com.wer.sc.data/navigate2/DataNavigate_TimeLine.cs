using com.wer.sc.data.cache.impl;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate2
{
    /// <summary>
    /// 分时线数据导航
    /// </summary>
    public class DataNavigate_TimeLine
    {
        private string code;

        private double currentTime;

        private DataCache_Code dataCache_Code;

        private DataReaderFactory fac;

        public DataNavigate_TimeLine(DataReaderFactory fac, DataCache_Code dataCache_Code, double currentTime)
        {
            this.fac = fac;
            this.dataCache_Code = dataCache_Code;
            this.code = dataCache_Code.Code;
            this.currentTime = currentTime;
        }

        
    }
}
