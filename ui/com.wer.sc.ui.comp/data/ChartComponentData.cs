using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentData_ //: IChartComponentData
    {
        private string code;

        private double time;

        private ChartType chartType;

        private KLinePeriod klinePeriod;

        private int showStartIndex;

        private int showEndIndex;

        private int crossSelectedIndex;

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

        public KLinePeriod KLinePeriod
        {
            get
            {
                return klinePeriod;
            }

            set
            {
                klinePeriod = value;
            }
        }

        public int ShowStartIndex
        {
            get
            {
                return showStartIndex;
            }

            set
            {
                showStartIndex = value;
            }
        }

        public int ShowEndIndex
        {
            get
            {
                return showEndIndex;
            }

            set
            {
                showEndIndex = value;
            }
        }

        public int CrossSelectedIndex
        {
            get
            {
                return crossSelectedIndex;
            }

            set
            {
                crossSelectedIndex = value;
            }
        }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <returns></returns>
        public bool CheckData()
        {
            return false;
        }
    }
}
