using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_TradingTime : IStep
    {
        private string code;

        private DataUpdateHelper dataUpdateHelper;

        private bool forceUpdate;

        public Step_TradingTime(string code, DataUpdateHelper dateUpdateHelper)
        {
            this.code = code;
            this.dataUpdateHelper = dateUpdateHelper;
        }

        public Step_TradingTime(string code, DataUpdateHelper dateUpdateHelper, bool forceUpdate) : this(code, dateUpdateHelper)
        {
            this.forceUpdate = forceUpdate;
        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的所有交易时间";
            }
        }

        public string Proceed()
        {
            List<TradingTime> result = GetAllTradingTime();
            if (result == null)
            {
                return code + "的交易时间已经是最新的，不需要更新";
            }
            string path = dataUpdateHelper.GetPath_TradingTime(code);
            CsvUtils_TradingTime.Save(path, result);
            return "更新完成" + code + "的交易时间";
        }

        /// <summary>
        /// 得到该合约的所有开盘时间，如果返回空，则表示现在数据已经是最新的
        /// </summary>
        /// <returns></returns>
        public List<TradingTime> GetAllTradingTime()
        {
            IList<TradingTime> updatedTradingSessionList;
            if (forceUpdate)
                updatedTradingSessionList = new List<TradingTime>();
            else
                updatedTradingSessionList = this.dataUpdateHelper.GetUpdatedTradingTime(code);
            CodeInfo codeInfo = this.dataUpdateHelper.GetCodeInfo(code);

            ITradingDayReader openDateReader = this.dataUpdateHelper.GetAllTradingDayReader();

            int firstCodeTradingDayIndex;
            int lastCodeTradingDayIndex;
            int lastUpdateTradingDayIndex = GetLastUpdatedIndex(updatedTradingSessionList);
            if (lastUpdateTradingDayIndex < 0)
            {
                firstCodeTradingDayIndex = GetFirstTradingDayIndex(codeInfo, openDateReader);
                lastCodeTradingDayIndex = GetLastTradingDayIndex(codeInfo, openDateReader);
            }
            else
            {
                firstCodeTradingDayIndex = lastUpdateTradingDayIndex + 1;
                if (firstCodeTradingDayIndex >= openDateReader.GetAllTradingDays().Count)
                    return null;
                lastCodeTradingDayIndex = GetLastTradingDayIndex(codeInfo, openDateReader);
            }
            if (firstCodeTradingDayIndex < 0 || lastCodeTradingDayIndex < 0)
                return null;

            List<int> openDates = openDateReader.GetAllTradingDays();
            List<TradingTime> updateStartTimes = CalcDayOpenTime(code, openDates, firstCodeTradingDayIndex, lastCodeTradingDayIndex);
            if (updateStartTimes == null || updateStartTimes.Count == 0)
                return null;

            return GetTradingSessionDataResult(updatedTradingSessionList, updateStartTimes);
        }

        private List<TradingTime> GetTradingSessionDataResult(IList<TradingTime> updatedTradingSessionList, List<TradingTime> updateTradingSessionList)
        {
            HashSet<int> set_TradingDay = new HashSet<int>();
            List<TradingTime> result = new List<TradingTime>();
            if (updatedTradingSessionList != null)
            {
                for (int i = 0; i < updatedTradingSessionList.Count; i++)
                {
                    TradingTime session = updatedTradingSessionList[i];
                    if (set_TradingDay.Contains(session.TradingDay))
                        continue;
                    set_TradingDay.Add(session.TradingDay);
                    result.Add(session);
                }
            }
            if (updateTradingSessionList == null)
                return result;
            for (int i = 0; i < updateTradingSessionList.Count; i++)
            {
                TradingTime session = updateTradingSessionList[i];
                if (set_TradingDay.Contains(session.TradingDay))
                    continue;
                set_TradingDay.Add(session.TradingDay);
                result.Add(session);
            }
            return result;
        }

        private List<TradingTime> CalcDayOpenTime(string code, List<int> openDates, int startIndex, int endIndex)
        {
            List<TradingTime> dayTradingTimeArr = new List<TradingTime>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                int date = openDates[i];

                List<double[]> openTime = this.LoadTradingSessionDetail(code, date);
                double startTime = openTime[0][0];
                double endTime = openTime[0][0];
                //double endTime = openTime[openTime.Count - 1][1];
                if (startTime > 0.18)
                {
                    if (i == 0)
                        throw new ArgumentException("传入的" + date + "有夜盘，必须传入其之前的日期");
                    dayTradingTimeArr.Add(new TradingTime(date, GetFullOpenTime(openTime, openDates[i - 1], openDates[i])));
                }
                else
                {
                    dayTradingTimeArr.Add(new TradingTime(date, GetFullOpenTime(openTime, -1, openDates[i])));
                }
            }
            return dayTradingTimeArr;
        }

        private List<double[]> GetFullOpenTime(List<double[]> tradingTimeArr, int lastTradingDay, int tradingDay)
        {
            double startTime = tradingTimeArr[0][0];
            if (startTime > 0.18)
            {
                List<double[]> fullTradingTimeArr = new List<double[]>();
                for (int i = 0; i < tradingTimeArr.Count; i++)
                {
                    double[] tradingTime = tradingTimeArr[i];
                    double[] fullTradingTime = GetFullTradingTime(tradingTime, lastTradingDay, tradingDay);
                    fullTradingTimeArr.Add(fullTradingTime);
                }
                return fullTradingTimeArr;
            }
            else
            {
                List<double[]> fullTradingTimeArr = new List<double[]>();
                for (int i = 0; i < tradingTimeArr.Count; i++)
                {
                    double[] tradingTime = tradingTimeArr[i];
                    double[] fullTradingTime = GetFullTradingTime(tradingTime, lastTradingDay, tradingDay);  //new double[] { tradingDay + tradingTime[0], tradingDay + tradingTime[1] };
                    fullTradingTimeArr.Add(fullTradingTime);
                }
                return fullTradingTimeArr;
            }
        }

        private double[] GetFullTradingTime(double[] tradingTime, int lastTradingDay, int tradingDay)
        {
            double startTime = tradingTime[0];
            double endTime = tradingTime[1];
            int startDay = startTime > 0.18 ? lastTradingDay : tradingDay;
            if (endTime < startTime)
            {
                int secondDay = (int)Math.Round(TimeUtils.AddDays(startDay, 1));
                return new double[] { startDay + tradingTime[0], secondDay + tradingTime[1] };
            }
            else
                return new double[] { startDay + tradingTime[0], startDay + tradingTime[1] };
        }

        public List<double[]> LoadTradingSessionDetail(string code, int date)
        {
            return dataUpdateHelper.GetTradingSessionDetailReader().GetTradingTime(code, date);
        }

        private int GetLastUpdatedIndex(IList<TradingTime> dayStartTimes)
        {
            if (dayStartTimes == null || dayStartTimes.Count == 0)
                return -1;
            int lastDate = dayStartTimes[dayStartTimes.Count - 1].TradingDay;
            ITradingDayReader openDateReader = this.dataUpdateHelper.GetAllTradingDayReader();
            int lastIndex = openDateReader.GetTradingDayIndex(lastDate);
            return lastIndex;
        }

        private int GetFirstTradingDayIndex(CodeInfo codeInfo, ITradingDayReader openDateReader)
        {
            int codeFirstDate = codeInfo.Start;
            if (codeFirstDate < 0)
                return 0;
            int firstTradingDay = openDateReader.FirstTradingDay;
            firstTradingDay = codeFirstDate > firstTradingDay ? codeFirstDate : firstTradingDay;

            int index = openDateReader.GetTradingDayIndex(firstTradingDay);
            if (index >= 0)
                return index;
            int date = openDateReader.GetNextTradingDay(firstTradingDay);
            return openDateReader.GetTradingDayIndex(date);
        }

        private int GetLastTradingDayIndex(CodeInfo codeInfo, ITradingDayReader openDateReader)
        {
            int codeLastDate = codeInfo.End;
            if (codeLastDate <= 0)
                return openDateReader.GetAllTradingDays().Count - 1;
            int lastTradingDay = openDateReader.LastTradingDay;
            lastTradingDay = codeLastDate > lastTradingDay ? lastTradingDay : codeLastDate;

            int index = openDateReader.GetTradingDayIndex(lastTradingDay);
            if (index >= 0)
                return index;
            int date = openDateReader.GetPrevTradingDay(lastTradingDay);
            return openDateReader.GetTradingDayIndex(date);
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}
