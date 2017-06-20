using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.update;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_KLineData : IStep
    {
        private string code;

        private List<int> dates;

        private DataUpdateHelper dataUpdateHelper;

        private UpdatedDataInfo updatedDataInfo;

        private bool updateFillUp;

        public Step_KLineData(string code, List<int> dates, DataUpdateHelper dataUpdateHelper, UpdatedDataInfo updatedDataInfo, bool updateFillUp)
        {
            this.code = code;
            this.dates = dates;
            this.dataUpdateHelper = dataUpdateHelper;
            this.updatedDataInfo = updatedDataInfo;
            this.updateFillUp = updateFillUp;
        }

        public int ProgressStep
        {
            get
            {
                return dates.Count * 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + GetDesc();
            }
        }

        private string GetDesc()
        {
            return code + "的K线数据：" + dates[0] + "-" + dates[dates.Count - 1];
        }

        public string Proceed()
        {
            ITradingDayReader openDateReader = this.dataUpdateHelper.GetAllTradingDayReader();
            KLineDataLastEndInfo lastEndInfo;
            IKLineData lastKLineData = null;

            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                if (i == 0)
                {
                    lastEndInfo = GetLastEndInfo(date);
                }
                else
                {
                    if (openDateReader.GetTradingDayIndex(date) - openDateReader.GetTradingDayIndex(dates[i - 1]) == 1)
                    {
                        lastEndInfo.lastEndPrice = lastKLineData.Arr_End[lastKLineData.Length - 1];
                        lastEndInfo.lastEndHold = lastKLineData.Arr_Hold[lastKLineData.Length - 1];
                    }
                    else
                    {
                        lastEndInfo = GetLastEndInfo(date);
                    }
                }
                Step_KLineData_OneDay step_klineData = new Step_KLineData_OneDay(dataUpdateHelper, code, date, KLinePeriod.KLinePeriod_1Minute, lastEndInfo.lastEndPrice, lastEndInfo.lastEndHold);
                step_klineData.Proceed();
                lastKLineData = step_klineData.KlineData;
            }
            if (!updateFillUp && updatedDataInfo != null)
            {
                updatedDataInfo.WriteUpdateInfo_KLine(code, KLinePeriod.KLinePeriod_1Minute, dates[dates.Count - 1]);
                updatedDataInfo.Save();
            }
            return "更新完毕" + GetDesc();
        }

        private KLineDataLastEndInfo GetLastEndInfo(int date)
        {
            int prevDate = this.dataUpdateHelper.GetAllTradingDayReader().GetPrevTradingDay(date);
            IKLineData lastKLineData = this.dataUpdateHelper.GetUpdatedKLineData(code, prevDate, KLinePeriod.KLinePeriod_1Minute);
            float lastEndPrice = lastKLineData != null ? lastKLineData.Arr_End[lastKLineData.Length - 1] : -1;
            int lastEndHold = lastKLineData != null ? lastKLineData.Arr_Hold[lastKLineData.Length - 1] : -1;
            return new KLineDataLastEndInfo(lastEndPrice, lastEndHold);
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }

    struct KLineDataLastEndInfo
    {
        public float lastEndPrice;

        public int lastEndHold;

        public KLineDataLastEndInfo(float lastEndPrice, int lastEndHold)
        {
            this.lastEndPrice = lastEndPrice;
            this.lastEndHold = lastEndHold;
        }
    }
}