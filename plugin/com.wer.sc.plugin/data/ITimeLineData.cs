using System;
using System.Collections.Generic;

namespace com.wer.sc.data
{
    public interface ITimeLineData : ITimeLineBar
    {
        float YesterdayEnd { get; }

        int Date { get; }

        int BarPos { get; set; }

        int IndexOfTime(double time);

        void SetBarPosByTime(double time);

        ITimeLineBar GetCurrentBar();

        ITimeLineBar GetBar(int index);

        #region 完整数据信息

        int Length { get; }

        IList<double> Arr_Time { get; }

        IList<float> Arr_Price { get; }

        IList<int> Arr_Mount { get; }

        IList<int> Arr_Hold { get; }

        IList<float> Arr_UpPercent { get; }

        IList<float> Arr_UpRange { get; }

        #endregion        

        String ToString(int index);
    }
}