using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// 时间索引器
    /// </summary>
    public class TimeIndeier
    {
        public TimeIndeier()
        {

        }

        /// <summary>
        /// 得到timeGetter里time对应的索引号
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int IndexOf(TimeGetter timeGetter, double time)
        {
            return IndexOf(timeGetter, time, true);
        }

        /// <summary>
        /// 得到timeGetter里time对应的索引号，如果没有该时间，根据findBackward找到对应时间
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <param name="findBackward"></param>
        /// <returns></returns>
        public int IndexOf(TimeGetter timeGetter, double time, bool findBackward)
        {
            return IndexOf(timeGetter, time, findBackward, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <param name="findBackward"></param>
        /// <param name="indexIfRepeat"></param>
        /// <returns></returns>
        public int IndexOf(TimeGetter timeGetter, double time, bool findBackward, int indexIfRepeat)
        {
            int startIndex = 0;
            int endIndex = timeGetter.Count - 1;
            double ctime = timeGetter.GetTime(startIndex);
            if (time < ctime)
                return -1;
            return IndexOf(timeGetter, time, 0, endIndex, findBackward, indexIfRepeat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="findBackward"></param>
        /// <param name="indexIfRepeat"></param>
        /// <returns></returns>
        private int IndexOf(TimeGetter timeGetter, double time, int startIndex, int endIndex, bool findBackward, int indexIfRepeat)
        {
            if (endIndex - startIndex <= 1)
            {
                double startTime = timeGetter.GetTime(startIndex);
                if (startTime == time)
                    return FindRepeatIndex(timeGetter, startIndex, findBackward, indexIfRepeat);
                if (time < startTime)
                    return -1;
                double endTime = timeGetter.GetTime(endIndex);
                if (endTime == time)
                    return FindRepeatIndex(timeGetter, endIndex, findBackward, indexIfRepeat);
                if (time > endTime)
                    return -1;
                if (findBackward)
                    return FindRepeatIndex(timeGetter, startIndex, findBackward, indexIfRepeat);
                return FindRepeatIndex(timeGetter, endIndex, findBackward, indexIfRepeat);
            }
            int currentIndex = (endIndex + startIndex) / 2;
            double currentTime = timeGetter.GetTime(currentIndex);
            if (currentTime == time)
                return FindRepeatIndex(timeGetter, currentIndex, findBackward, indexIfRepeat);
            if (currentTime > time)
            {
                return IndexOf(timeGetter, time, startIndex, currentIndex, findBackward, indexIfRepeat);
            }
            else
            {
                return IndexOf(timeGetter, time, currentIndex, endIndex, findBackward, indexIfRepeat);
            }
        }

        //private int IndexOf(TimeGetter timeGetter, double time, int startIndex, int endIndex, bool findBackward, int indexIfRepeat)
        //{
        //    if (endIndex - startIndex <= 1)
        //    {
        //        double startTime = timeGetter.GetTime(startIndex);
        //        if (startTime == time)
        //            return startIndex;
        //        if (time < startTime)
        //            return -1;
        //        double endTime = timeGetter.GetTime(endIndex);
        //        if (endTime == time)
        //            return endIndex;
        //        if (time > endTime)
        //            return -1;
        //        if (findBackward)
        //            return startIndex;
        //        return endIndex;
        //    }
        //    int currentIndex = (endIndex + startIndex) / 2;
        //    double currentTime = timeGetter.GetTime(currentIndex);
        //    if (currentTime == time)
        //        return currentIndex;
        //    if (currentTime > time)
        //    {
        //        return IndexOf(timeGetter, time, startIndex, currentIndex, findBackward, indexIfRepeat);
        //    }
        //    else
        //    {
        //        return IndexOf(timeGetter, time, currentIndex, endIndex, findBackward, indexIfRepeat);
        //    }
        //}

        private int FindRepeatIndex(TimeGetter timeGetter, int index, bool findBackward, int indexIfRepeat)
        {
            //if (findBackward)
            //{
            //    int endIndex = FindEndRepeatIndex(timeGetter, index, findBackward);
            //    int firstIndex = FindFirstRepeatIndex(timeGetter, index, findBackward);
            //    int resultIndex = endIndex - indexIfRepeat;
            //    if (resultIndex > firstIndex)
            //        return resultIndex;
            //    return firstIndex;
            //}
            //else
            //{
            //    int endIndex = FindEndRepeatIndex(timeGetter, index, findBackward);
            //    int firstIndex = FindFirstRepeatIndex(timeGetter, index, findBackward);
            //    int resultIndex = firstIndex + indexIfRepeat;
            //    if (resultIndex < endIndex)
            //        return resultIndex;
            //    return endIndex;
            //}

            int endIndex = FindEndRepeatIndex(timeGetter, index, findBackward);
            int firstIndex = FindFirstRepeatIndex(timeGetter, index, findBackward);
            int resultIndex = firstIndex + indexIfRepeat;
            if (resultIndex < endIndex)
                return resultIndex;
            return endIndex;
        }

        private int FindFirstRepeatIndex(TimeGetter timeGetter, int index, bool findBackward)
        {
            double time = timeGetter.GetTime(index);
            double currentTime = time;
            while (time == currentTime)
            {
                index--;
                if (index < 0)
                    return 0;
                currentTime = timeGetter.GetTime(index);
            }
            return index + 1;
        }

        private int FindEndRepeatIndex(TimeGetter timeGetter, int index, bool findBackward)
        {
            double time = timeGetter.GetTime(index);
            double currentTime = time;
            while (time == currentTime)
            {
                index++;
                if (index >= timeGetter.Count)
                    return index - 1;
                currentTime = timeGetter.GetTime(index);
            }
            return index - 1;
        }
    }

    public interface TimeGetter
    {
        double GetTime(int index);

        int Count { get; }
    }
}
