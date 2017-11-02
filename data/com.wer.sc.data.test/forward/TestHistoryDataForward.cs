using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    [TestClass]
    public class TestHistoryDataForward
    {
        public void TestHistoryDataForward_Tick()
        {
            //string[] codes = new string[] { "RB1801", "RB0000", "MA801" };
            //ForwardReferedPeriods[] referedPeriods = new ForwardReferedPeriods[3];
            //referedPeriods[0] = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Day }, true, true);
            //referedPeriods[1] = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Day }, true, true);
            //referedPeriods[2] = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Day }, true, true);
            //ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            //IHistoryDataForward forward = DataCenter.Default.HistoryDataForwardFactory.CreateHistoryDataForward(codes, 20170601, 20170603, referedPeriods, forwardPeriod);
            //forward.OnBar += Forward_OnBar;
            //forward.OnTick += Forward_OnTick;            
            
            //while (forward.Forward())
            //{

            //}
        }

        private void Forward_OnTick(object sender, IForwardOnTickArgument argument)
        {
            Console.WriteLine(argument.TickBar);
        }

        private void Forward_OnBar(object sender, IForwardOnBarArgument arguments)
        {
            
        }

        public void TestHistoryDataForward_TickKLine()
        {
            //主做螺纹钢，跟踪螺纹钢指数15分钟线和日线
            //跟踪郑醇的分钟线
            string[] codes = new string[] { "RB1801", "RB0000", "MA801" };
            int start = 20170601;
            int end = 20170603;
            ForwardReferedPeriods[] referedPeriods = new ForwardReferedPeriods[3];
            referedPeriods[0] = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Day }, true, true);
            referedPeriods[1] = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Day }, true, true);
            referedPeriods[2] = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute }, true, true);
            ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            IDataForward forward = DataCenter.Default.HistoryDataForwardFactory.CreateHistoryDataForward(codes, start, end, referedPeriods, forwardPeriod);
            forward.OnBar += Forward_OnBar2;
            forward.OnTick += Forward_OnTick2;

            while (forward.Forward())
            {

            }
        }

        private void Forward_OnTick2(object sender, IForwardOnTickArgument argument)
        {
            Console.WriteLine(argument.TickBar);
        }

        private void Forward_OnBar2(object sender, IForwardOnBarArgument arguments)
        {

        }
    }
}
