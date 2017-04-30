using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.receiver
{
    public class TickData_RealReader
    {
        private string path;

        private int date;

        public TickData_RealReader(string path, int date)
        {
            this.path = path;
            this.date = date;            
        }

        public ITickData ReadTickData(string code)
        {
            string tickPath = TickData_RealWriter.GetTickBarPath(path, code, date);
            return CsvUtils_TickData.Load(tickPath);
        }
    }
}