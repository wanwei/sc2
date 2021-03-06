﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using System.Drawing;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy.cnfutures.import
{
    /// <summary>
    /// 
    /// </summary>
    [Strategy("STRATEGY.MA", "MA指标", "MA指标", "指标")]
    public class Strategy_MaList : StrategyAbstract
    {
        private int length;

        private KLinePeriod period;

        private List<float> maPrice = new List<float>();        

        public List<float> MaPrice
        {
            get
            {
                return maPrice;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
        }

        public Strategy_MaList()
        {
            this.period = KLinePeriod.KLinePeriod_1Minute;
            this.length = 5;
        }

        public Strategy_MaList(KLinePeriod klinePeriod, int length)
        {
            this.period = klinePeriod;
            this.length = length;
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            IKLineData klineData = currentData.CurrentData.GetKLineData(period);
            int barPos = klineData.BarPos;
            int startPos = barPos - length;
            startPos = startPos < 0 ? 0 : startPos;

            float total = 0;
            for (int i = startPos; i <= barPos; i++)
            {
                total += klineData.Arr_End[i];
            }
            this.maPrice.Add(total / (barPos - startPos + 1));
        }

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {

        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            StrategyOperator.DrawOperator.GetDrawer_KLine(MainKLinePeriod).DrawPolyLine(maPrice, Color.Red);

            //StrategyHelper.DrawPoint()
        }

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }
    }

    class MaPeriod
    {
        KLinePeriod klinePeriod;

        int length;
    }
}
