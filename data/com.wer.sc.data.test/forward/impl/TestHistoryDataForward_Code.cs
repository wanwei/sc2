using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.data.forward
{
    [TestClass]
    public class TestHistoryDataForward_Code
    {
        [TestMethod]
        public void TestRealTimeReader()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170610;
            IHistoryDataForward_Code realTimeReader = GetRealTimeReader(code, start, endDate, false);
            while (!realTimeReader.IsEnd)
            {
                realTimeReader.Forward();
                IKLineData klineData = realTimeReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
                Console.WriteLine(klineData);
            }
        }

        [TestMethod]
        public void TestRealTimeReader_OnTick()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170610;
            IHistoryDataForward_Code realTimeReader = GetRealTimeReader(code, start, endDate, true);
            realTimeReader.OnTick += RealTimeReader_OnTick;
            realTimeReader.OnBar += RealTimeReader_OnBar;
            while (!realTimeReader.IsEnd)
            {
                realTimeReader.Forward();
                Console.WriteLine("timeline:" + realTimeReader.GetTimeLineData());
            }
        }

        private void RealTimeReader_OnBar(object sender, ForwardOnBarArgument argument)
        {
            ForwardOnbar_Info onBarInfo = argument.ForwardOnBar_Infos[0];
            int barPos = onBarInfo.FinishedBarPos;
            if (barPos == 0)
                return;
            Console.WriteLine("kline:" + onBarInfo.KlineData.GetBar(barPos - 1));
        }

        private void RealTimeReader_OnTick(object sender, ITickData tickData, int index)
        {
            Console.WriteLine("tick:" + tickData);
        }

        private static IHistoryDataForward_Code GetRealTimeReader(string code, int start, int endDate, bool useTickData)
        {
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.isReferTimeLineData = false;
            referedPeriods.UseTickData = useTickData;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);

            //HistoryDataForwardArguments args = new HistoryDataForwardArguments();
            //args.StartDate = start;
            //args.EndDate = endDate;
            //args.ReferedPeriods = referedPeriods;
            //args.IsTickForward = useTickData;
            //args.ForwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;

            IDataPackage_Code dataPackage = CommonData.GetDataPackage(code, start, endDate);
            //HistoryDataForward_Code realTimeReader = new HistoryDataForward_Code(CommonData.GetDataReader(), code, args);
            IHistoryDataForward_Code realTimeReader = DataCenter.Default.HistoryDataForwardFactory.CreateHistoryDataForward_Code(dataPackage, referedPeriods, new ForwardPeriod(useTickData, KLinePeriod.KLinePeriod_1Minute));
            return realTimeReader;
        }
    }
}
