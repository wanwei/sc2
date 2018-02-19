using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;

namespace com.wer.sc.data
{
    public class DataNavigateMgr
    {
        private DataReaderFactory dataReaderFactory;

        private Dictionary<string, IDataNavigate> dicDataNavigate = new Dictionary<string, IDataNavigate>();

        public DataNavigateMgr(DataReaderFactory dataReaderFactory)
        {
            this.dataReaderFactory = dataReaderFactory;
        }

        public IDataNavigate CreateDataNavigate(string code, double time)
        {
            return null;
        }

        public IDataNavigate CreateDataNavigate(string code, double time, int startDate, int endDate)
        {
            return null;
        }

        public IDataNavigate GetDataNavigate(string code, double time)
        {
            if (dicDataNavigate.ContainsKey(code))
                return dicDataNavigate[code];
            IDataNavigate nav = CreateDataNavigate(code, time);
            dicDataNavigate.Add(code, nav);
            return nav;
        }

        public IDataNavigate GetDataNavigate(string code, double time, int startDate, int endDate)
        {
            return null;
        }

        public IDataNavigate2 CreateNavigate_Old(String code, double time)
        {
            return new DataNavigate2(dataReaderFactory, code, time);
        }

        public IDataNavigate2 CreateNavigate_Old(String code, double time, int startDate, int endDate)
        {
            return new DataNavigate2(dataReaderFactory, code, time, startDate, endDate);
        }
    }
}
