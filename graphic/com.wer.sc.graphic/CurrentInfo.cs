using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public class CurrentInfo
    {
        //股票或合约ID
        public string code;

        //当前价格
        public double currentPrice;

        //当前手数
        public double currentHand;

        //总手数
        public double totalHand;

        //总持仓
        public double totalHold;

        //日增仓
        public double dailyAdd;

        //外盘
        public double outMount;

        //外盘占比
        public double outPercent;

        //内盘
        public double inMount;

        //内盘占比
        public double inPercent;



        //上涨幅度
        public double upRange;

        //上涨百分比
        public double upPercent;

        //涨速
        public double upSpeed;

        //开盘
        public double open;

        //最高价
        public double high;

        //最低价
        public double low;

        //结算价
        public double jsPrice;

        //昨日结算价
        public double lastJsPrice;

        //做收盘价
        public double lastEndPrice;

        //涨停
        public double maxUp;

        //跌停
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
