using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate : IDataNavigate
    {
        private IDataNavigate_Code currentNavigate_Code;

        private IDataNavigateFactory fac;

        private IDataReader dataReader;

        private CodeInfo codeInfo;

        public DataNavigate(IDataNavigateFactory fac, IDataReader dataReader, IDataNavigate_Code currentNavigate_Code)
        {
            this.currentNavigate_Code = currentNavigate_Code;
            this.fac = fac;
            this.dataReader = dataReader;
            this.codeInfo = this.dataReader.CodeReader.GetCodeInfo(currentNavigate_Code.Code);
        }

        public string Code
        {
            get
            {
                return currentNavigate_Code.Code;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return currentNavigate_Code.DataPackage;
            }
        }

        public double Time
        {
            get
            {
                return currentNavigate_Code.Time;
            }
        }

        public event DelegateOnNavigateTo OnNavigateTo;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public void Change(string code)
        {
            this.currentNavigate_Code = fac.CreateDataNavigate_Code(code, this.Time);
            this.codeInfo = dataReader.CodeReader.GetCodeInfo(code);
        }

        public void Change(string code, double time)
        {
            this.currentNavigate_Code = fac.CreateDataNavigate_Code(code, time);
        }

        public bool Backward(KLinePeriod forwardPeriod)
        {
            bool canBackWard = currentNavigate_Code.Backward(forwardPeriod);
            if (!canBackWard)
            {
                int startDate = this.DataPackage.StartDate;
                if (startDate > codeInfo.Start)
                {
                    currentNavigate_Code = fac.CreateDataNavigate_Code(Code, Time);
                    return currentNavigate_Code.Backward(forwardPeriod);
                }
            }
            return canBackWard;
        }

        public bool Forward(KLinePeriod forwardPeriod)
        {
            bool canForward = this.currentNavigate_Code.Forward(forwardPeriod);
            if (!canForward)
            {
                int endDate = this.DataPackage.EndDate;
                int codeEndDate = codeInfo.End;
                if (codeEndDate == 0)
                    codeEndDate = this.dataReader.TradingDayReader.FirstTradingDay;
                if (endDate < codeEndDate)
                {
                    currentNavigate_Code = fac.CreateDataNavigate_Code(Code, Time);
                    return currentNavigate_Code.Forward(forwardPeriod);
                }
            }
            return canForward;
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return this.currentNavigate_Code.GetKLineData(period);
        }

        public ITickData GetTickData()
        {
            return this.currentNavigate_Code.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            return this.currentNavigate_Code.GetTimeLineData();
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return this.currentNavigate_Code.IsPeriodEnd(period);
        }

        public bool NavigateTo(double time)
        {
            if (this.currentNavigate_Code.Time == time)
                return true;
            bool canNav = this.currentNavigate_Code.NavigateTo(time);
            if (!canNav)
            {
                int tradingDay = this.dataReader.CreateTradingTimeReader(Code).GetTradingDay(time);
                if (!IsInTradingDay(codeInfo, tradingDay))
                    return false;
                this.currentNavigate_Code = fac.CreateDataNavigate_Code(Code, time);
            }
            return canNav;
        }

        private bool IsInTradingDay(CodeInfo codeInfo, int tradingDay)
        {
            if (codeInfo.Start != 0 && tradingDay < codeInfo.Start)
                return false;
            if (codeInfo.End != 0 && tradingDay > codeInfo.End)
                return false;
            return true;
        }

        public void Play()
        {

        }

        public void Pause()
        {

        }
    }
}