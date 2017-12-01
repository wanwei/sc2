using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class Step_UpdateKLineData : IStep
    {
        private CodeInfo codeInfo;

        private ITradingDayReader tradingDayReader;
        //private int startDate;

        //private int endDate;

        private KLinePeriod period;

        private IPlugin_HistoryData historyData;

        private IKLineDataStore klineDataStore;

        private UpdatedDataInfo updatedDataInfo;

        private IUpdateInfoStore updateInfoStore;

        private bool updateFillUp;
        //public Step_UpdateKLineData(string code, int startDate, int endDate, KLinePeriod period, IPlugin_HistoryData historyData, IKLineDataStore klineDataStore)
        //{
        //    this.code = code;

        //    this.period = period;
        //    this.historyData = historyData;
        //    this.klineDataStore = klineDataStore;
        //}

        public Step_UpdateKLineData(CodeInfo codeInfo, KLinePeriod period, ITradingDayReader tradingDayReader, IPlugin_HistoryData historyData, IKLineDataStore klineDataStore, UpdatedDataInfo updatedDataInfo, IUpdateInfoStore updateInfoStore, bool updateFillUp)
        {
            this.codeInfo = codeInfo;
            this.tradingDayReader = tradingDayReader;
            this.period = period;
            this.historyData = historyData;
            this.klineDataStore = klineDataStore;
            this.updatedDataInfo = updatedDataInfo;
            this.updateInfoStore = updateInfoStore;
            this.updateFillUp = updateFillUp;
        }

        public int ProgressStep
        {
            get
            {
                if (period.PeriodType == KLineTimeType.DAY)
                    return 3;
                return 30;
                //return 3 * TimeUtils.Substract(endDate, startDate).Days;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + codeInfo.Code + "的" + period + "K线数据";
            }
        }

        public override string ToString()
        {
            return StepDesc;
        }

        public string Proceed()
        {
            int startDate;


            if (updateFillUp)
                startDate = codeInfo.Start;
            else
            {
                int lastTradingDay = klineDataStore.GetLastTradingDay(codeInfo.Code, period);
                startDate = this.tradingDayReader.GetNextTradingDay(lastTradingDay);
            }
            int endDate = this.tradingDayReader.LastTradingDay;

            IKLineData klineData = historyData.GetKLineData(codeInfo.Code, startDate, endDate, period);
            if (klineData == null || klineData.Length == 0)
                return "";

            if (updateFillUp) 
                klineDataStore.Delete(codeInfo.Code, period);
            klineDataStore.Append(codeInfo.Code, period, klineData);
            if (updatedDataInfo != null)
            {
                int realEndDate = (int)klineData.Arr_Time[klineData.Length - 1];
                updatedDataInfo.WriteUpdateInfo_KLine(codeInfo.Code, period, realEndDate);
                updateInfoStore.Save(updatedDataInfo);
            }
            return StepDesc + "完毕";
        }
    }
}