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
        private CodeInfo codeInfo;

        private List<int> dates;

        private DataUpdateHelper dataUpdateHelper;

        private UpdatedDataInfo updatedDataInfo;

        private bool updateFillUp;

        private bool overwrite;

        public Step_KLineData(CodeInfo codeInfo, List<int> dates, bool isOverWrite, DataUpdateHelper dataUpdateHelper)
        {
            this.codeInfo = codeInfo;
            this.dates = dates;
            this.overwrite = true;
            this.dataUpdateHelper = dataUpdateHelper;
        }

        public Step_KLineData(CodeInfo codeInfo, List<int> dates, DataUpdateHelper dataUpdateHelper, UpdatedDataInfo updatedDataInfo, bool updateFillUp)
        {
            this.codeInfo = codeInfo;
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
            return codeInfo.Code + "的K线数据：" + dates[0] + "-" + dates[dates.Count - 1];
        }

        private string Proceed_Overwrite()
        {
            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                KLineDataLastEndInfo lastEndInfo = GetLastEndInfo(date);
                Step_KLineData_OneDay step_klineData = new Step_KLineData_OneDay(dataUpdateHelper, codeInfo, date, KLinePeriod.KLinePeriod_1Minute, lastEndInfo.lastEndPrice, lastEndInfo.lastEndHold, true);
                step_klineData.Proceed();
            }
            return "更新完毕" + GetDesc();
        }

        public string Proceed()
        {
            if (overwrite)
                return Proceed_Overwrite();
            ITradingDayReader openDateReader = this.dataUpdateHelper.GetAllTradingDayReader();
            KLineDataLastEndInfo lastEndInfo;
            IKLineData lastKLineData = null;
            //string path = dataUpdateHelper.GetPath_KLineData(code, date, klinePeriod);
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
                        if (lastKLineData != null)
                        {
                            lastEndInfo.lastEndPrice = lastKLineData.Arr_End[lastKLineData.Length - 1];
                            lastEndInfo.lastEndHold = lastKLineData.Arr_Hold[lastKLineData.Length - 1];
                        }
                        else
                            lastEndInfo = GetLastEndInfo(date);
                    }
                    else
                    {
                        lastEndInfo = GetLastEndInfo(date);
                    }
                }
                Step_KLineData_OneDay step_klineData = new Step_KLineData_OneDay(dataUpdateHelper, codeInfo, date, KLinePeriod.KLinePeriod_1Minute, lastEndInfo.lastEndPrice, lastEndInfo.lastEndHold);
                step_klineData.Proceed();
                if (step_klineData.KlineData != null)
                    lastKLineData = step_klineData.KlineData;
            }
            if (!updateFillUp && updatedDataInfo != null)
            {
                updatedDataInfo.WriteUpdateInfo_KLine(codeInfo.Code, KLinePeriod.KLinePeriod_1Minute, dates[dates.Count - 1]);
                updatedDataInfo.Save();
            }
            return "更新完毕" + GetDesc();
        }

        private KLineDataLastEndInfo GetLastEndInfo(int date)
        {
            int prevDate = this.dataUpdateHelper.GetAllTradingDayReader().GetPrevTradingDay(date);
            IKLineData lastKLineData = this.dataUpdateHelper.GetUpdatedKLineData(codeInfo.Code, prevDate, KLinePeriod.KLinePeriod_1Minute);
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