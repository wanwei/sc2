using com.wer.sc.data.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTraderSetting : AccountSetting
    {
        private double initMoney = 100000;

        public double InitMoney
        {
            get
            {
                return initMoney;
            }

            set
            {
                initMoney = value;
            }
        }
    }
}
