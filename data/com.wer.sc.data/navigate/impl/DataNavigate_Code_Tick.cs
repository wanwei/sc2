using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate.impl
{
    public class DataNavigate_Code_Tick
    {
        private IDataPackage dataPackage;

        //private IDataReader dataReader;

        private string code;

        private double time;

        private int date = -1;

        private TickData tickData;

        private ITradingSessionReader_Instrument sessionReader;

        public DataNavigate_Code_Tick(IDataPackage dataPackage, double time)
        {
            this.dataPackage = dataPackage;
            this.sessionReader = dataPackage.GetTradingSessionReader();
            this.ChangeTime(time);
        }

        public DataNavigate_Code_Tick(IDataReader dataReader, string code, double time)
        {
            //this.dataReader = dataReader;
            //this.code = code;
            //this.sessionReader = dataReader.CreateTradingSessionReader(code);
            //this.ChangeTime(time);
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