using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public class CompChartInfo
    {
        private string code;

        private double time;

        private ChartType chartType;

        private float kLineBlockWidth;

        private KLinePeriod klinePeriod;

        public bool CheckData()
        {
            if (code == null)
                return false;
            if (time <= 0)
                return false;
            if (kLineBlockWidth <= 0)
                return false;
            if (klinePeriod == null)
                return false;
            return true;
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public ChartType ChartType
        {
            get
            {
                return chartType;
            }

            set
            {
                chartType = value;
            }
        }

        public float KLineBlockWidth
        {
            get
            {
                return kLineBlockWidth;
            }

            set
            {
                kLineBlockWidth = value;
            }
        }
    }
}
