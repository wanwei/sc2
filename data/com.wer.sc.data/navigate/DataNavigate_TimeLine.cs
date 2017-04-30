using com.wer.sc.data.cache.impl;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 分时线数据导航
    /// </summary>
    public class DataNavigate_TimeLine
    {
        private DataCache_Code code;

        private double currentTime;

        private DataReaderFactory fac;

        public DataNavigate_TimeLine(DataReaderFactory fac, DataCache_Code code, double currentTime)
        {
            this.fac = fac;
            this.code = code;
            this.currentTime = currentTime;
        }
    }
}
