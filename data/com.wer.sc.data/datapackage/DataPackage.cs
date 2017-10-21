using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class DataPackage : IDataPackage
    {
        private Dictionary<string, IDataPackage_Code> dic_Code_DataPackage = new Dictionary<string, IDataPackage_Code>();

        private IDataReader dataReader;

        private IList<string> codes;

        private int startDate;

        private int endDate;

        private int minBefore;

        private int minAfter;

        private IList<int> allTradingDays;

        public DataPackage(IDataReader dataReader, string[] codes, int startDate, int endDate, int minBefore, int minAfter)
        {
            this.dataReader = dataReader;
            this.codes = codes;

            if (dataReader.TradingDayReader.IsTrade(startDate))
                this.startDate = startDate;
            else
                this.startDate = dataReader.TradingDayReader.GetNextTradingDay(startDate);
            if (this.dataReader.TradingDayReader.IsTrade(endDate))
                this.endDate = endDate;
            else
                this.endDate = dataReader.TradingDayReader.GetPrevTradingDay(endDate);

            this.minBefore = minBefore;
            this.minAfter = minAfter;

            this.allTradingDays = dataReader.TradingDayReader.GetTradingDays(startDate, endDate);
        }

        public int EndDate
        {
            get
            {
                return endDate;
            }
        }

        public int StartDate
        {
            get
            {
                return startDate;
            }
        }

        public IList<string> GetCodes()
        {
            return codes;
        }

        public IDataPackage_Code GetDataPackage(string code)
        {
            if (dic_Code_DataPackage.ContainsKey(code))
                return dic_Code_DataPackage[code];
            //if(codes.Contains(code))
            //    IDataPackage_Code dataPackage = 
            return null;
        }

        public IList<int> GetTradingDays()
        {
            return allTradingDays;
        }
    }
}
