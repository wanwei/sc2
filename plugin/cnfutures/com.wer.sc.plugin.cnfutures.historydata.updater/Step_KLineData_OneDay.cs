using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils.update;
using com.wer.sc.utils.ui.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.datapreparer
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

        private IDataLoader dataLoader;

        private IKLineData klineData;

        private KLineTimeListGetter timeListGetter;

        public Step_KLineData_OneDay(string code, int date, KLinePeriod klinePeriod, IDataLoader dataLoader, float lastEndPrice, int lastEndHold)
        {
            this.code = code;
            this.date = date;
            this.klinePeriod = klinePeriod;
            this.dataLoader = dataLoader;
            this.lastEndPrice = lastEndPrice;
            this.lastEndHold = lastEndHold;

            ITradingDayReader openDateReader = dataLoader.LoadTradingDayReader();
            ITradingTimeReader openTimeReader = dataLoader.LoadTradingSessionDetailReader();
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
            TickData tickData = (TickData)dataLoader.LoadUpdatedTickData(code, date);
            /*
             * 此处不处理tickData为空的情况
             * 在DataTransfer_Tick2KLine.Transfer里处理tickData为空的情况
             */
            List<double> klineTimes = timeListGetter.GetKLineTimeList(code, date, klinePeriod);
            this.klineData = DataTransfer_Tick2KLine.Transfer(tickData, klineTimes, lastEndPrice, lastEndHold);
            string path = CsvHistoryData_PathUtils.GetKLineDataPath(dataLoader.CsvDataPath, code, date, klinePeriod);
            CsvUtils_KLineData.Save(path, klineData);
            return "更新" + code + "-" + date + "的" + klinePeriod + "K线完成";
        }
    }
}
