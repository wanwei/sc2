using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.wer.sc.data
{
    /// <summary>
    /// k线数据实现类，该类描述了一套完整的k线数据
    /// 
    /// 当K线数据从数据中心获取后，会初始化成该类
    /// </summary>
    public class KLineData : KLineData_Abstract
    {
        public double[] arr_time;

        public float[] arr_start;

        public float[] arr_high;

        public float[] arr_low;

        public float[] arr_end;

        public int[] arr_mount;

        public float[] arr_money;

        public int[] arr_hold;        

        public KLineData(int length)
        {
            arr_time = new double[length];
            arr_start = new float[length];
            arr_high = new float[length];
            arr_low = new float[length];
            arr_end = new float[length];
            arr_mount = new int[length];
            arr_money = new float[length];
            arr_hold = new int[length];
        }

        #region 得到完整数据        

        public override IList<double> Arr_Time
        {
            get
            {
                return arr_time;
            }
        }

        public override IList<float> Arr_Start
        {
            get
            {
                return arr_start;
            }
        }

        public override IList<float> Arr_High
        {
            get
            {
                return arr_high;
            }
        }

        public override IList<float> Arr_Low
        {
            get
            {
                return arr_low;
            }
        }

        public override IList<float> Arr_End
        {
            get
            {
                return arr_end;
            }
        }

        public override IList<int> Arr_Mount
        {
            get
            {
                return arr_mount;
            }
        }

        public override IList<float> Arr_Money
        {
            get
            {
                return arr_money;
            }
        }

        public override IList<int> Arr_Hold
        {
            get
            {
                return arr_hold;
            }
        }

        #endregion
    }
}