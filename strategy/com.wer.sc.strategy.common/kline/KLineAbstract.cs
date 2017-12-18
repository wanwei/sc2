using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.kline
{
    public abstract class KLineAbstract
    {
        private IKLineData klineData;

        private int length;

        private int startIndex;

        private int endIndex;

        public KLineAbstract(IKLineData klineData, int endIndex) : this(klineData, endIndex, 1)
        {

        }

        public KLineAbstract(IKLineData klineData, int endIndex, int length)
        {
            this.klineData = klineData;
            this.length = length;
            this.startIndex = endIndex - length + 1;
            this.endIndex = endIndex;
        }

        public int Length
        {
            get { return this.length; }
        }

        public int StartIndex
        {
            get { return endIndex - length + 1; }
        }

        public int EndIndex
        {
            get { return endIndex; }
        }

        public IKLineBar GetBar()
        {
            return GetBar(endIndex);
        }

        public IKLineBar GetBar(int index)
        {
            return klineData.GetBar(index + startIndex);
        }

        /// <summary>
        /// 得到K线模式的类型
        /// </summary>
        /// <returns></returns>
        public abstract KLineType GetKLineType();

        /// <summary>
        /// 检查K线是否符合该模式
        /// </summary>
        /// <param name="klineData"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract bool Check(IKLineData klineData, int index);
    }
}