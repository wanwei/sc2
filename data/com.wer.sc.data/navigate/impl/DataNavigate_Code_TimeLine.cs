using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate.impl
{
    public class DataNavigate_Code_TimeLine
    {
        private IDataReader dataReader;

        private string code;

        private double time;

        public DataNavigate_Code_TimeLine(IDataReader dataReader, string code, double time)
        {

        }

        public double Time
        {
            get { return 0; }
        }

        public void ChangeTime(double time)
        {

        }

        public ITimeLineData GetTimeLineData()
        {
            return null;
        }
    }
}
