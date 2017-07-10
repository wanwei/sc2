using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public abstract class GraphicDrawer_Candle_Abstract : GraphicDrawer_Abstract
    {
        #region 初始化需要设置的属性        

        private IGraphicDrawer_Chart_Candle dataProvider;

        public IGraphicDrawer_Chart_Candle DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
            }
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

        private int blockPadding = 1;

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
}
