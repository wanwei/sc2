using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    [TestClass]
    public class TestDataNavigate_ChangeTime
    {
        private IKLineData_RealTime klineData_1Minute;

        private IKLineData_RealTime klineData_5Minute;

        private IKLineData_RealTime klineData_15Minute;

        private IKLineData_RealTime klineData_1Day;

        string code = "RB1710";

        [TestMethod]
        public void TestNavigate_ChangeTime()
        {
            int start = 20170601;
            int endDate = 20170603;
            KLinePeriod[] periods = new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_5Minute,
                KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Day };

            this.klineData_1Minute = new KLineDataExtend_RealTime(DataCenter.Default.DataReader.KLineDataReader.GetData_Extend(code, start, endDate, 0, 0, KLinePeriod.KLinePeriod_1Minute));
            this.klineData_5Minute = new KLineDataExtend_RealTime(DataCenter.Default.DataReader.KLineDataReader.GetData_Extend(code, start, endDate, 0, 0, KLinePeriod.KLinePeriod_5Minute));
            this.klineData_15Minute = new KLineDataExtend_RealTime(DataCenter.Default.DataReader.KLineDataReader.GetData_Extend(code, start, endDate, 0, 0, KLinePeriod.KLinePeriod_15Minute));
            this.klineData_1Day = new KLineDataExtend_RealTime(DataCenter.Default.DataReader.KLineDataReader.GetData_Extend(code, start, endDate, 0, 0, KLinePeriod.KLinePeriod_1Day));

            IDataForward_Code dataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, start, endDate, true, true, periods);
            //dataForward.Forward();
            dataForward.OnTick += DataForward_OnTick;

            while (dataForward.Forward())
            {

            }
        }

        private double prevTime;

        private void DataForward_OnTick(object sender, IForwardOnTickArgument argument)
        {
            double time = argument.Time;
            //if (time < 20170601.205900)
            //    return;
            int tradingDay = argument.TickInfo.TickData.TradingDay;
            if (prevTime == time)
                return;
            this.prevTime = time;
            ITickData_Extend tickData = GetTickData(tradingDay);
            //Console.WriteLine(tickData);
            DataNavigate_ChangeTime.ChangeTime_TickData(tickData, time);
            Assert.AreEqual(argument.TickInfo.TickBar.ToString(), tickData.ToString());

            IRealTimeDataReader_Code realTimeData = ((IRealTimeDataReader_Code)sender);
            DataNavigate_ChangeTime.ChangeTime_KLineData(klineData_1Minute, tradingDay, time, tickData);
            Assert.AreEqual(realTimeData.GetKLineData(KLinePeriod.KLinePeriod_1Minute).ToString(), klineData_1Minute.ToString());
            DataNavigate_ChangeTime.ChangeTime_KLineData(klineData_5Minute, tradingDay, time, tickData);
            Assert.AreEqual(realTimeData.GetKLineData(KLinePeriod.KLinePeriod_5Minute).ToString(), klineData_5Minute.ToString());
            DataNavigate_ChangeTime.ChangeTime_KLineData(klineData_15Minute, tradingDay, time, tickData);
            Assert.AreEqual(realTimeData.GetKLineData(KLinePeriod.KLinePeriod_15Minute).ToString(), klineData_15Minute.ToString());
            DataNavigate_ChangeTime.ChangeTime_KLineData(klineData_1Day, tradingDay, time, tickData);
            Assert.AreEqual(realTimeData.GetKLineData(KLinePeriod.KLinePeriod_1Day).ToString(), klineData_1Day.ToString());

            ITimeLineData_RealTime timeLineData = GetTimeLineData(tradingDay);
            DataNavigate_ChangeTime.ChangeTime_TimeLineData(timeLineData, time, tickData);
            Assert.AreEqual(realTimeData.GetTimeLineData().ToString(), timeLineData.ToString());
        }

        private ITickData_Extend tickData;

        private ITickData_Extend GetTickData(int date)
        {
            if (tickData == null || tickData.TradingDay != date)
            {
                tickData = DataCenter.Default.DataReader.TickDataReader.GetTickData_Extend(code, date);
            }
            return tickData;
        }

        private ITimeLineData_RealTime timeLineData;

        private ITimeLineData_RealTime GetTimeLineData(int date)
        {
            if (timeLineData == null || timeLineData.Date != date)
            {
                timeLineData = new TimeLineDataExtend_RealTime(DataCenter.Default.DataReader.TimeLineDataReader.GetData_Extend(code, date));
            }
            return timeLineData;
        }
    }
}
