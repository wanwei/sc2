using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate_Code_Tick
    {
        private IDataPackage dataPackage;

        private string code;

        private double time;

        private int date = -1;

        private TickData tickData;

        private ITradingTimeReader_Code sessionReader;

        public DataNavigate_Code_Tick(IDataPackage dataPackage, double time)
        {
            this.dataPackage = dataPackage;
            this.sessionReader = dataPackage.GetTradingTimeReader();
            this.ChangeTime(time);
        }

        public double Time
        {
            get { return time; }
        }

        public void ChangeTime(double time)
        {
            if (this.time == time)
                return;
            this.time = time;
            int date = this.sessionReader.GetTradingDay(time);
            if (date < 0)
                date = this.sessionReader.GetRecentTradingDay(time);
            if (this.date != date)
            {
                this.date = date;
                this.tickData = (TickData)dataPackage.GetTickData(date);
            }
            int index = TimeIndeierUtils.IndexOfTime_Tick(tickData, time, true);
            tickData.BarPos = index < 0 ? 0 : index;
        }

        public ITickData GetTickData()
        {
            return tickData;
        }
    }
}