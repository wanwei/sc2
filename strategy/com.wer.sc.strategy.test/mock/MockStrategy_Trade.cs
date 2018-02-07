using com.wer.sc.data.market;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.mock
{
    /// <summary>
    /// 15分钟线，MA10上穿买入，下穿卖出
    /// </summary>
    public class MockStrategy_Trade : StrategyAbstract
    {
        private MockStrategy_Ma referedStrategy_MA10;

        private MockStrategy_Ma referedStrategy_MA20;

        private List<IStrategy> referedStrategies = new List<IStrategy>();

        public MockStrategy_Trade()
        {
            this.referedStrategy_MA10 = new MockStrategy_Ma();
            this.referedStrategy_MA20 = new MockStrategy_Ma();
            this.referedStrategy_MA10.Parameters.SetParameterValue(MockStrategy_Ma.PARAMKEY_MA, 10);
            this.referedStrategy_MA20.Parameters.SetParameterValue(MockStrategy_Ma.PARAMKEY_MA, 20);
            this.referedStrategies.Add(referedStrategy_MA10);
            this.referedStrategies.Add(referedStrategy_MA20);
        }

        public override IList<IStrategy> GetReferedStrategies()
        {
            return referedStrategies;
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            List<float> ma10 = referedStrategy_MA10.MAList;
            List<float> ma20 = referedStrategy_MA20.MAList;
            if (ma10.Count < 2)
                return;
            if (ma10[ma10.Count - 2] > ma20[ma20.Count - 2] && ma10[ma10.Count - 1] > ma20[ma20.Count - 1])
            {
                this.StrategyOperator.Trader.CloseAll();
                return;
            }
            if (ma10[ma10.Count - 2] < ma20[ma20.Count - 2] && ma10[ma10.Count - 1] < ma20[ma20.Count - 1])
            {
                this.StrategyOperator.Trader.Open(currentData.Code, OrderSide.Buy, currentData.CurrentData.Price, 5);
                return;
            }
        }

        public override void OnEnd(object sender, IStrategyOnEndArgument argument)
        {

        }

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {

        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }
    }
}