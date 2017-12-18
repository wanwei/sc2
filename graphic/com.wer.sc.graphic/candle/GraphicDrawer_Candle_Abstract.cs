using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public abstract class GraphicDrawer_Candle_Abstract : GraphicDrawer_PriceRect_Abstract
    {
        #region 初始化需要设置的属性        

        private IGraphicData_Candle dataProvider;

        public IGraphicData_Candle DataProvider
        {
            get { return dataProvider; }
            set { dataProvider = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override IGraphicData GraphicData
        {
            get { return dataProvider; }
            set { this.dataProvider = (IGraphicData_Candle)value; }
        }

        private int blockWidth = 8;

        public int BlockWidth
        {
            get
            {
                return blockWidth;
            }

            set
            {
                blockWidth = value;
            }
        }

        private int blockPadding = 2;

        public int BlockPadding
        {
            get
            {
                return blockPadding;
            }

            set
            {
                blockPadding = value;
            }
        }

        #endregion
    }

    public class DrawKLineData
    {
        private IKLineData klineData;

        private int startIndex;

        private int endIndex;

        public IKLineData KlineData
        {
            get
            {
                return klineData;
            }
        }

        public int StartIndex
        {
            get
            {
                return startIndex;
            }
        }

        public int EndIndex
        {
            get
            {
                return endIndex;
            }
        }

        public DrawKLineData(IKLineData klineData, int startIndex, int endIndex)
        {
            this.klineData = klineData;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }

        public bool KLineDataEqual(IKLineData klineData, int startIndex, int endIndex)
        {
            if (this.startIndex != startIndex || this.endIndex != endIndex)
                return false;
            return this.klineData.Time == klineData.Time;
        }
    }
}
