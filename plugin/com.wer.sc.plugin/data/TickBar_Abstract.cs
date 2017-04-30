using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public abstract class TickBar_Abstract : ITickBar
    {
        public abstract string Code { get; set; }

        public abstract int Add { get; set; }

        public abstract int BuyMount { get; set; }

        public abstract float BuyPrice { get; set; }

        public abstract int Hold { get; set; }

        public abstract bool IsBuy { get; set; }

        public abstract int Mount { get; set; }

        public abstract float Price { get; set; }

        public abstract int SellMount { get; set; }

        public abstract float SellPrice { get; set; }

        public abstract double Time { get; set; }

        public abstract int TotalMount { get; set; }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Time).Append(",");
            sb.Append(Price).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(TotalMount).Append(",");
            sb.Append(Add).Append(",");
            sb.Append(BuyPrice).Append(",");
            sb.Append(BuyMount).Append(",");
            sb.Append(SellPrice).Append(",");
            sb.Append(SellMount).Append(",");
            sb.Append(IsBuy ? 1 : 0);
            return sb.ToString();
        }
    }
}
