using System.Collections.Generic;

namespace com.wer.sc.data
{
    public interface IKLineDataTradingTimeInfo
    {
        List<int> TradingDays { get; }

        List<IKLineDataTradingTimeInfo_Day> TradingDayInfos { get; }

        IKLineDataTradingTimeInfo_Day GetKLineTimeInfo_Day(int tradingDay);

        IKLineDataTradingTimeInfo_Periods GetTradingPeriodsByBarPos(int barPos);
    }

    public interface IKLineDataTradingTimeInfo_Day
    {

        int TradingDay { get; }

        int BarCount { get; }

        int StartPos { get; set; }

        int EndPos { get; set; }

        IList<IKLineDataTradingTimeInfo_Periods> TradingPeriods { get; }

        int HolidayCount { get; }

        IKLineDataTradingTimeInfo_Periods FindPeriods(int barPos);

        void AddTradingPeriods(IKLineDataTradingTimeInfo_Periods tradingPeriods);
    }

    public interface IKLineDataTradingTimeInfo_Periods
    {
        int BarCount { get; }

        int StartPos { get; }

        int EndPos { get; }

        int PeriodIndex { get; }

        IKLineDataTradingTimeInfo_Day KlineTimeInfo_Day { get; }
    }
}