using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    public class KLineHoldInfo
    {
        public String code;

        public bool isMoreOrLess;

        public int mount;

        public float cost;

        public void addHold(bool isMoreOrLess, float price, int cnt)
        {
            if (mount == 0)
            {
                this.isMoreOrLess = isMoreOrLess;
                this.cost = price;
                this.mount = cnt;
            }
            else
            {
                this.cost = (cost * mount + price * cnt) / (mount + cnt);
                this.mount += cnt;
            }
        }

        public void removeHold(float price, int cnt)
        {
            if (mount == cnt)
            {
                this.cost = 0;
                this.mount = 0;
            }
            else
            {
                this.cost = (cost * mount - price * cnt) / (mount - cnt);
                this.mount -= cnt;
            }
        }
    }
}