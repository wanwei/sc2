using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    public class TimeLineData_Present : TimeLineData_Abstract
    { /// <summary>
      /// 全日时间
      /// </summary>
        public double[] arr_time;

        /// <summary>
        /// 全日价格
        /// </summary>
        public float[] arr_price;

        /// <summary>
        /// 全日成交
        /// </summary>
        public int[] arr_mount;

        /// <summary>
        /// 全天持仓数据
        /// </summary>
        public int[] arr_hold;

        private float[] arr_UpPercent;

        private float[] arr_UpRange;

        public TimeLineData_Present(float yesterdayEnd, int length)
        {
            this.YesterdayEnd = yesterdayEnd;
            arr_time = new double[length];
            arr_price = new float[length];
            arr_mount = new int[length];
            arr_hold = new int[length];
        }

        public void Receive(ITickBar tickBar)
        {

        }

        public override IList<double> Arr_Time
        {
            get
            {
                return arr_time;
            }
        }

        public override IList<float> Arr_Price
        {
            get
            {
                return arr_price;
            }
        }

        public override IList<int> Arr_Mount
        {
            get
            {
                return arr_mount;
            }
        }

        public override IList<int> Arr_Hold
        {
            get
            {
                return arr_hold;
            }
        }

        public override IList<float> Arr_UpPercent
        {
            get
            {
                if (arr_UpPercent == null)
                {
                    arr_UpPercent = new float[Length];
                    for (int i = 0; i < Length; i++)
                    {
                        float p = arr_price[i];
                        arr_UpPercent[i] = (float)Math.Round((p - YesterdayEnd) / p * 100, 2);
                    }
                }
                return arr_UpPercent;
            }
        }

        public override IList<float> Arr_UpRange
        {
            get
            {
                if (arr_UpRange == null)
                {
                    arr_UpRange = new float[Length];
                    for (int i = 0; i < Length; i++)
                    {
                        float p = arr_price[i];
                        arr_UpRange[i] = (float)Math.Round(p - YesterdayEnd, 2);
                    }
                }
                return arr_UpRange;
            }
        }
    }
}
