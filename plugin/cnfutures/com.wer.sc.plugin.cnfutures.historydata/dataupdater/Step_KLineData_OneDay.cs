using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.transfer;
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
    /// 一天的K线数据生成器
    /// </summary>
    public class Step_KLineData_OneDay : IStep
    {
        private string code;

        private int date;

        private KLinePeriod klinePeriod;

        private float lastEndPrice;

        private int lastEndHold;

        private DataUpdateHelper dataUpdateHelper;

        private IKLineData klineData;

        private KLineTimeListGetter timeListGetter;

        public Step_KLineData_OneDay(DataUpdateHelper dataUpdateHelper, string code, int date, KLinePeriod klinePeriod, float lastEndPrice, int lastEndHold)
        {
            this.code = code;
            this.date = date;
            this.klinePeriod = klinePeriod;
            this.dataUpdateHelper = dataUpdateHelper;
            this.lastEndPrice = lastEndPrice;
            this.lastEndHold = lastEndHold;

            ITradingDayReader openDateReader = dataUpdateHelper.GetAllTradingDayReader();
            ITradingTimeReader openTimeReader = dataUpdateHelper.GetTradingSessionDetailReader();
            this.timeListGetter = new KLineTimeListGetter(openDateReader, openTimeReader);
        }

        public int ProgressStep
        {
            get
            {
                return 3;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新";
            }
        }

        public IKLineData KlineData
        {
            get
            {
                return klineData;
            }
        }

        public string Proceed()
        {
            TickData tickData = (TickData)dataUpdateHelper.GetNewTickData(code, date);
            /*
             * 此处不处理tickData为空的情况
             * 在DataTransfer_Tick2KLine.Transfer里处理tickData为空的情况
             */
            List<double> klineTimes = timeListGetter.GetKLineTimeList(code, date, klinePeriod);
            this.klineData = DataTransfer_Tick2KLine.Transfer(tickData, klineTimes, lastEndPrice, lastEndHold);
            string path = dataUpdateHelper.GetPath_KLineData(code, date, klinePeriod);
            CsvUtils_KLineData.Save(path, klineData);
            return "更新" + code + "-" + date + "的" + klinePeriod + "K线完成";
        }
    }
}
