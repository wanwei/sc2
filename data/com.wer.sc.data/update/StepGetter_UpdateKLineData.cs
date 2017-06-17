using com.wer.sc.data.reader.cache;
using com.wer.sc.data.store;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 更新K线数据
    /// 
    /// </summary>
    public class StepGetter_UpdateKLineData
    {
        private const int DAYS_EVERYKLINESTEP = 50;

        private IPlugin_HistoryData historyData;

        private IKLineDataStore klineDataStore;

        private List<KLinePeriod> updatePeriods;

        private bool isFillUp;

        public StepGetter_UpdateKLineData(IPlugin_HistoryData historyData, IDataStore dataStore, List<KLinePeriod> updatePeriods, bool isFillUp)
        {
            this.historyData = historyData;
            this.klineDataStore = dataStore.CreateKLineDataStore();
            this.updatePeriods = updatePeriods;
            this.isFillUp = isFillUp;
        }

        public List<IStep> GetSteps()
        {
            List<IStep> steps = new List<IStep>();
            AddSteps_KLineData(steps);
            return steps;
        }

        private void AddSteps_KLineData(List<IStep> steps)
        {
            List<CodeInfo> instruments = historyData.GetInstruments();
            List<int> tradingDays = historyData.GetTradingDays();

            TradingDayCache cache = new TradingDayCache(tradingDays);

            for (int i = 0; i < instruments.Count; i++)
            {
                CodeInfo instrument = instruments[i];
                AddSteps_KLineData_Instrument(steps, instrument, cache);
            }
        }

        private void AddSteps_KLineData_Instrument(List<IStep> steps, CodeInfo codeInfo, TradingDayCache tradingDaysCache)
        {
            string code = codeInfo.Code;
            for (int i = 0; i < updatePeriods.Count; i++)
            {
                //TODO 暂时没处理FillUp的情况，考虑使用全覆盖的方式实现
                KLinePeriod period = updatePeriods[i];
                int lastTradingDay = klineDataStore.GetLastTradingDay(code, period);

                int startDate;
                if (lastTradingDay < 0)
                {
                    if (codeInfo.Start != 0)
                        startDate = codeInfo.Start;
                    else
                        startDate = tradingDaysCache.GetAllTradingDays()[0];
                }
                else
                {
                    int lastTradingDayIndex = tradingDaysCache.GetTradingDayIndex(lastTradingDay);
                    if (lastTradingDayIndex < 0)
                    {
                        //TODO 不应该出现这种情况
                    }
                    int startIndex = lastTradingDayIndex + 1;
                    if (startIndex >= tradingDaysCache.GetAllTradingDays().Count)
                        continue;
                    startDate = tradingDaysCache.GetTradingDay(startIndex);
                }

                int endIndex;
                if (codeInfo.End <= 0)
                    endIndex = tradingDaysCache.GetAllTradingDays().Count - 1;
                else
                    endIndex = tradingDaysCache.GetTradingDayIndex(codeInfo.End, true);

                int endDate = tradingDaysCache.GetTradingDay(endIndex);
                if (endDate < startDate)
                    return;
                Step_UpdateKLineData step = new Step_UpdateKLineData(code, startDate, endDate, period, historyData, klineDataStore);
                steps.Add(step);
            }
        }

        private void Add2WaitForUpdateInfoDic(Dictionary<string, List<KLineNewDataInfo>> dic, KLinePeriod klinePeriod, List<InstrumentDatesInfo> dataInfoList)
        {
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                InstrumentDatesInfo updateInfo = dataInfoList[i];
                if (dic.ContainsKey(updateInfo.instrument))
                {
                    dic[updateInfo.instrument].Add(new KLineNewDataInfo(updateInfo, klinePeriod));
                }
                else
                {
                    List<KLineNewDataInfo> updateinfos = new List<KLineNewDataInfo>();
                    updateinfos.Add(new KLineNewDataInfo(updateInfo, klinePeriod));
                    dic.Add(updateInfo.instrument, updateinfos);
                }
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, List<KLineNewDataInfo> updateDataInfos)
        {
            for (int i = 0; i < updateDataInfos.Count; i++)
            {
                KLineNewDataInfo klineUpdateDataInfo = updateDataInfos[i];
                InstrumentDatesInfo updateDataInfo = klineUpdateDataInfo.NewDataInfo;
                if (updateDataInfo.dates.Count == 0)
                    continue;
                string code = updateDataInfo.instrument;
                int startDate = updateDataInfo.dates[0];
                int endDate = updateDataInfo.dates[updateDataInfo.dates.Count - 1];
                Step_UpdateKLineData step = new Step_UpdateKLineData(code, startDate, endDate, klineUpdateDataInfo.KLinePeriod, historyData, klineDataStore);
                steps.Add(step);
            }
        }
    }


    class KLineNewDataInfo
    {
        public InstrumentDatesInfo NewDataInfo;

        public KLinePeriod KLinePeriod;

        public KLineNewDataInfo(InstrumentDatesInfo newDataInfo, KLinePeriod KLinePeriod)
        {
            this.NewDataInfo = newDataInfo;
            this.KLinePeriod = KLinePeriod;
        }
    }
}
