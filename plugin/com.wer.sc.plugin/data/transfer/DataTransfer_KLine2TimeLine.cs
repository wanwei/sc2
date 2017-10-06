using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    /// <summary>
    /// 将一天的k线数据转换成分时线数据
    /// </summary>
    public class DataTransfer_KLine2TimeLine
    {
        public static ITimeLineData ConvertTimeLineData(IKLineData data, float lastEndPrice)
        {
            TimeLineData r = new TimeLineData(lastEndPrice, data.Length);
            r.Code = data.Code;
            Convert2RealData(data, 0, data.Length - 1, r);
            return r;
        }

        public static List<ITimeLineData> ConvertTimeLineDataList(IKLineData data, float lastEndPrice, ITradingTimeReader_Code tradingSessionReader)
        {
            List<SplitterResult> splitResult = DaySplitter.Split(data, tradingSessionReader);

            List<ITimeLineData> realdataList = new List<ITimeLineData>(splitResult.Count);
            for (int i = 0; i < splitResult.Count; i++)
            {
                SplitterResult split = splitResult[i];
                int date = split.Date;
                int todayStartIndex = split.Index;
                int todayEndIndex;
                if (i == splitResult.Count - 1)
                    todayEndIndex = data.Length - 1;
                else
                    todayEndIndex = splitResult[i + 1].Index;
                int len = todayEndIndex - todayStartIndex + 1;
                TimeLineData r = new TimeLineData(lastEndPrice, len);
                r.Code = data.Code;
                Convert2RealData(data, todayStartIndex, todayEndIndex, r);
                realdataList.Add(r);
            }
            return realdataList;
        }

        private static void Convert2RealData(IKLineData data, int startIndex, int endIndex, TimeLineData r)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                int currentRealIndex = i - startIndex;
                r.arr_time[currentRealIndex] = data.Arr_Time[i];
                r.arr_price[currentRealIndex] = data.Arr_End[i];
                r.arr_mount[currentRealIndex] = data.Arr_Mount[i];
                r.arr_hold[currentRealIndex] = data.Arr_Hold[i];
            }
        }
    }
}
