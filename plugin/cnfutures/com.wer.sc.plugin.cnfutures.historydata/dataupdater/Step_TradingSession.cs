using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    /// <summary>
    /// 开盘时间更新
    /// </summary>
    public class Step_TradingSession : IStep
    {
        private string code;

        private DataUpdateHelper dataUpdateHelper;

        //private DataLoader_TradingSessionDetail dataLoader_TradingSessionDetail;

        public Step_TradingSession(string code, DataUpdateHelper dateUpdateHelper)
        {
            this.code = code;
            this.dataUpdateHelper = dateUpdateHelper;
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
                return "更新" + code + "的开盘时间";
            }
        }

        public string Proceed()
        {
            List<TradingSession> result = GetAllTradingSession();
            if (result == null)
                return code + "的开盘时间已经是最新的，不需要更新";
            string path = dataUpdateHelper.GetPath_TradingSession(code);
            CsvUtils_TradingSession.Save(path, result);
            return "更新完成" + code + "的开盘时间";
        }

        /// <summary>
        /// 得到该合约的所有开盘时间，如果返回空，则表示现在数据已经是最新的
        /// </summary>
        /// <returns></returns>
        public List<TradingSession> GetAllTradingSession()
        {
            List<TradingSession> updatedTradingSessionList = this.dataUpdateHelper.GetUpdatedTradingSessions(code);
            //List<TradingSession> updatedTradingSessionList = new List<TradingSession>();

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
            List<TradingSession> updateStartTimes = CalcDayOpenTime(code, openDates, firstCodeTradingDayIndex, lastCodeTradingDayIndex);
            if (updateStartTimes == null || updateStartTimes.Count == 0)
                return null;

            List<TradingSession> result = new List<TradingSession>();
            if (updatedTradingSessionList != null)
                result.AddRange(updatedTradingSessionList);
            result.AddRange(updateStartTimes);
            return result;
        }

        private List<TradingSession> CalcDayOpenTime(string code, List<int> openDates, int startIndex, int endIndex)
        {
            List<TradingSession> dayStartTimes = new List<TradingSession>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                int date = openDates[i];

                List<double[]> openTime = this.LoadTradingSessionDetail(code, date);
                double startTime = openTime[0][0];
                double endTime = openTime[openTime.Count - 1][1];
                if (startTime > 0.18)
                {
                    if (i == 0)
                        throw new ArgumentException("传入的" + date + "有夜盘，必须传入其之前的日期");
                    dayStartTimes.Add(new TradingSession(date, openDates[i - 1] + startTime, openDates[i] + endTime));
                }
                else
                {
                    dayStartTimes.Add(new TradingSession(date, date + startTime, date + endTime));
                }
            }
            return dayStartTimes;
        }

        public List<double[]> LoadTradingSessionDetail(string code, int date)
        {
            return dataUpdateHelper.GetTradingSessionDetailReader().GetTradingTime(code, date);
        }

        private int GetLastUpdatedIndex(List<TradingSession> dayStartTimes)
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