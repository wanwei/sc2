using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.info
{
    public class CurrentInfo
    {
        public string code;

        public double currentPrice;
        public double currentHand;
        public double totalHand;
        public double totalHold;
        public double dailyAdd;
        public double outMount;
        public double outPercent;
        public double inMount;
        public double inPercent;

        public double upRange;
        public double upPercent;
        public double upSpeed;
        public double open;
        public double high;
        public double low;
        public double jsPrice;
        public double lastJsPrice;
        public double maxUp;
        public double maxDown;

        public String GetUpStr()
        {
            return upRange + "/" + StringUtils.GetPercent(upPercent);
        }

        public String GetUpSpeedStr()
        {
            return StringUtils.GetPercent(upSpeed);
        }
    }
}
