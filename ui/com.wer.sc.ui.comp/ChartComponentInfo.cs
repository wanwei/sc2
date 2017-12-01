using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentInfo : IChartComponentInfo
    {
        //默认显示K线
        private ChartType chartType = ChartType.KLine;

        //默认不显示十字线选中的index
        private int crossSelectedIndex = -1;

        private double currentTime;

        private IDataPackage_Code dataPackage_Code;

        private KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;

        private int currentIndex;

        private int showStartIndex;

        private int showEndIndex;

        public ChartComponentInfo(IDataPackage_Code dataPackage)
        {
            this.dataPackage_Code = dataPackage;
        }

        public ChartType ChartType
        {
            get
            {
                return chartType;
            }
            set
            {
                this.chartType = value;
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

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
            set
            {
                this.currentIndex = value;
            }
        }

        public double CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                this.currentTime = value;
            }
        }


        public IDataPackage_Code DataPackage
        {
            get
            {
                return dataPackage_Code;
            }
            set
            {
                this.dataPackage_Code = value;
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
                this.klinePeriod = value;
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
                this.showEndIndex = value;
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
                this.showStartIndex = value;
            }
        }

        public bool CheckData()
        {
            if (this.dataPackage_Code == null)
                return false;
            if (currentTime <= 0)
                return false;
            if (klinePeriod == null)
                return false;
            return true;
        }
    }
}