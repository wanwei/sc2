using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 分时线的数据点实现类
    /// </summary>
    public class TimeLineBar : TimeLineBar_Abstract
    {
        private string code;

        private double time;

        private float price;

        private int mount;

        private int hold;

        private float upRange;

        private float upPercent;

        public override string Code
        {
            get
            {
                return code;
            }

            set
            {
                this.code = value;
            }
        }

        public override int Hold
        {
            get
            {
                return hold;
            }

            set
            {
                this.hold = value;
            }
        }

        public override int Mount
        {
            get
            {
                return mount;
            }

            set
            {
                this.mount = value;
            }
        }

        public override float Price
        {
            get
            {
                return price;
            }

            set
            {
                this.price = value;
            }
        }

        public override double Time
        {
            get
            {
                return time;
            }

            set
            {
                this.time = value;
            }
        }

        public override float UpPercent
        {
            get
            {
                return upPercent;
            }

            set
            {
                this.upPercent = value;
            }
        }

        public override float UpRange
        {
            get
            {
                return upRange;
            }

            set
            {
                this.upRange = value;
            }
        }
    }
}