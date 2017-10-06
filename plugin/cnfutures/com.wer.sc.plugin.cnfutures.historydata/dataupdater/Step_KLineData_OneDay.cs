using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
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
        private CodeInfo codeInfo;

        private int date;

        private KLinePeriod klinePeriod;

        private float lastEndPrice;

        private int lastEndHold;

        private DataUpdateHelper dataUpdateHelper;

        private IKLineData klineData;

        private bool overwrite;
        //private KLineTimeListGetter timeListGetter;

        public Step_KLineData_OneDay(DataUpdateHelper dataUpdateHelper, CodeInfo codeInfo, int date, KLinePeriod klinePeriod, float lastEndPrice, int lastEndHold)
        {
            this.codeInfo = codeInfo;
            this.date = date;
            this.klinePeriod = klinePeriod;
            this.dataUpdateHelper = dataUpdateHelper;
            this.lastEndPrice = lastEndPrice;
            this.lastEndHold = lastEndHold;

            //ITradingDayReader openDateReader = dataUpdateHelper.GetAllTradingDayReader();
            //ITradingTimeReader openTimeReader = dataUpdateHelper.GetTradingSessionDetailReader();
            //this.timeListGetter = new KLineTimeListGetter(openDateReader, openTimeReader);
        }

        public Step_KLineData_OneDay(DataUpdateHelper dataUpdateHelper, CodeInfo codeInfo, int date, KLinePeriod klinePeriod, float lastEndPrice, int lastEndHold, bool overwrite) : this(dataUpdateHelper, codeInfo, date, klinePeriod, lastEndPrice, lastEndHold)
        {
            this.overwrite = overwrite;
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
                return "更新" + codeInfo.Code + "在" + date + "的K线数据";
                //return "更新K线数据" + codeInfo.Code + "-" + date;
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
            string path = dataUpdateHelper.GetPath_KLineData(codeInfo.Code, date, klinePeriod);
            //if (!(codeInfo.Code.EndsWith("0000") || codeInfo.Code.EndsWith("MI")))
            //if (!overwrite && File.Exists(path))
            //    return codeInfo.Code + "-" + date + "的K线数据已存在";

            TickData tickData = (TickData)dataUpdateHelper.GetUpdatedTickData(codeInfo.Code, date);
            if (tickData == null)
                tickData = (TickData)dataUpdateHelper.GetNewTickData(codeInfo.ServerCode, date);
            //tick数据没有，则不生成对应K线数据
            if (tickData == null)
            {
                string msg = codeInfo.Code + "-" + date + "的tick数据不存在";
                LogHelper.Warn(GetType(), msg);
                return msg;
            }

            List<double[]> tradingPeriod = dataUpdateHelper.GetTradingTime(codeInfo.Code, date).TradingPeriods;
            //List<double[]> klineTimes = TradingTimeUtils.GetKLineTimeList_Full(tradingPeriod, KLinePeriod.KLinePeriod_1Minute);
            //timeListGetter.GetKLineTimeList(code, date, klinePeriod);
            this.klineData = DataTransfer_Tick2KLine.Transfer(tickData, tradingPeriod, KLinePeriod.KLinePeriod_1Minute, lastEndPrice, lastEndHold);
            CsvUtils_KLineData.Save(path, klineData);
            return "更新" + codeInfo.Code + "-" + date + "的" + klinePeriod + "K线完成";
        }
    }
}