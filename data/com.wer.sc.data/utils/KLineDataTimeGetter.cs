using com.wer.sc.data.utils;

namespace com.wer.sc.data.utils
{

    public class KLineDataTimeGetter : TimeGetter
    {
        private IKLineData data;
        public KLineDataTimeGetter(IKLineData data)
        {
            this.data = data;
        }

        public int Count
        {
            get { return data.Length; }
        }

        public double GetTime(int index)
        {
            return data.Arr_Time[index];
        }
    }
}
