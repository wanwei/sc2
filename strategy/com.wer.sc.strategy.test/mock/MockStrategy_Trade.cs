using com.wer.sc.data.market;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{
    public class MockStrategy_Trade : StrategyAbstract
    {
        private MockStrategy_Ma referedStrategy_MA5;

        private MockStrategy_Ma referedStrategy_MA20;

        private List<IStrategy> referedStrategies = new List<IStrategy>();

        public MockStrategy_Trade()
        {
            this.referedStrategy_MA5 = new MockStrategy_Ma();
            this.referedStrategy_MA20 = new MockStrategy_Ma();
            this.referedStrategy_MA5.Parameters.SetParameterValue(MockStrategy_Ma.PARAMKEY_MA, 5);
            this.referedStrategy_MA20.Parameters.SetParameterValue(MockStrategy_Ma.PARAMKEY_MA, 20);
            this.referedStrategies.Add(referedStrategy_MA5);
            this.referedStrategies.Add(referedStrategy_MA20);
        }

        public override IList<IStrategy> GetReferedStrategies()
        {
            return referedStrategies;
        }

        public override void OnBar(object sender, StrategyOnBarArgument currentData)
        {
            List<float> ma5 = referedStrategy_MA5.MAList;
            List<float> ma20 = referedStrategy_MA20.MAList;
            if (ma5.Count < 2)
                return;
            if (ma5[ma5.Count - 2] < ma20[ma20.Count - 2] && ma5[ma5.Count - 1] > ma20[ma20.Count - 1])
            {
                this.StrategyOperator.Trader.Open(OrderSide.Buy, currentData.Price, 5);
                return;
            }
            if (ma5[ma5.Count - 2] > ma20[ma20.Count - 2] && ma5[ma5.Count - 1] < ma20[ma20.Count - 1])
            {
                this.StrategyOperator.Trader.CloseAll();
                return;
            }
        }

        public override void OnStrategyEnd(object sender, StrategyOnEndArgument argument)
        {

        }

        public override void OnStrategyStart(object sender, StrategyOnStartArgument argument)
        {

        }

        public override void OnTick(object sender, StrategyOnTickArgument currentData)
        {

        }
    }
}