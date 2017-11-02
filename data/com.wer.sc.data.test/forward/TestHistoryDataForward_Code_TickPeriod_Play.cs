using com.wer.sc.data.datapackage;
using com.wer.sc.data.realtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    [TestClass]
    public class TestHistoryDataForward_Code_TickPeriod_Play
    {
        //[TestMethod]
        public void TestKLineData_Play()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170601;

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnTick += KlineDataForward_OnTick;
            Console.WriteLine(klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            klineDataForward.Play();
            while (klineDataForward.Time < 20170531.210005)
            {

            }
            klineDataForward.Pause();
        }

        //[TestMethod]
        public void TestKLineData_Play2()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170601;

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnTick += KlineDataForward_OnTick;
            klineDataForward.NavigateTo(20170531.210011);
            Console.WriteLine(klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            klineDataForward.Play();
            while (klineDataForward.Time < 20170531.210015)
            {

            }
            klineDataForward.Pause();
        }

        private void KlineDataForward_OnTick(object sender, IForwardOnTickArgument argument)
        {
            IKLineData klineData = ((IDataForward_Code)sender).GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            //Console.WriteLine(tickData);
            Console.WriteLine(klineData);
        }

        private static IDataForward_Code GetKLineDataForward(string code, int start, int endDate)
        {
            //KLineData_RealTime klineData_1Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Minute);
            //KLineData_RealTime klineData_5Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_5Minute);
            //KLineData_RealTime klineData_15Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_15Minute);
            //KLineData_RealTime klineData_1Day = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Day);
            //Dictionary<KLinePeriod, KLineData_RealTime> dic = new Dictionary<KLinePeriod, KLineData_RealTime>();

            //IList<int> tradingDays = CommonData.GetDataReader().TradingDayReader.GetTradingDays(start, endDate);
            //dic.Add(KLinePeriod.KLinePeriod_1Minute, klineData_1Minute);
            //dic.Add(KLinePeriod.KLinePeriod_5Minute, klineData_5Minute);
            //dic.Add(KLinePeriod.KLinePeriod_15Minute, klineData_15Minute);
            //dic.Add(KLinePeriod.KLinePeriod_1Day, klineData_1Day);

            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, endDate);
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            referedPeriods.UseTickData = true;

            ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            IDataForward_Code klineDataForward = DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
            //new HistoryDataForward_Code_TickPeriod(, code, periods, KLinePeriod.KLinePeriod_1Minute);
            return klineDataForward;
        }

    }
}
