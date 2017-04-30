﻿using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.generator
{
    /// <summary>
    /// 期货指数的更新
    /// </summary>
    public class DataGenerator_TickData_Index
    {
        //private String csvDataPath;
        private IDataLoader dataLoader;

        public DataGenerator_TickData_Index(IDataLoader dataLoader)
        {
            //this.csvDataPath = dataLoader.CsvDataPath;
            this.dataLoader = dataLoader;
        }

        private ITickData GetAdjustedTickData(string code, int date)
        {
            return dataLoader.LoadUpdatedTickData(code, date);
        }

        public ITickData Generate(String variety, int date)
        {
            String indexCode = variety + "13";
            ITickData indexdata = GetAdjustedTickData(indexCode, date);
            if (indexdata != null)
                return indexdata;

            List<CodeInfo> codes = dataLoader.LoadInstruments(variety);
            List<ITickData> tickData = new List<ITickData>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                String upperCode = code.Code.ToUpper();
                if (upperCode.EndsWith("MI") || upperCode.EndsWith("13"))
                    continue;
                ITickData data = GetAdjustedTickData(code.Code, date);
                if (data != null)
                    tickData.Add(data);
            }
            if (tickData.Count == 0)
                return null;
            List<double[]> openTime = this.dataLoader.LoadTradingSessionDetail(codes[0].Code, date);
            return Generate(tickData, openTime);
        }

        public ITickData Generate(List<ITickData> tickData, List<double[]> openTime)
        {
            List<double> times = GetTimeArr(tickData, openTime);

            TickData data = new TickData(times.Count);
            int[] currentIndeies = new int[tickData.Count];
            int[] lastIndeies = new int[tickData.Count];

            int[] holds = new int[tickData.Count];
            int[] mounts = new int[tickData.Count];
            for (int i = 0; i < times.Count; i++)
            {
                double time = times[i];
                data.arr_time[i] = time;
                CalcIndeies(tickData, time, currentIndeies, lastIndeies);
                CalcMount(tickData, mounts, lastIndeies, currentIndeies);
                CalcCurrent(data, i, tickData, mounts, lastIndeies);
            }

            return data;
        }

        //private void CalcCurrentHold(List<TickData> tickData, int[] holds, int[] lastIndeies, int[] currentIndeies)
        //{
        //    for (int i = 0; i < holds.Length; i++)
        //    {
        //        int hold = holds[i];
        //        holds[i] = hold + CalcCurrentHold(tickGetValue(i], lastIndeies[i], currentIndeies[i]);
        //    }
        //}

        //private int CalcCurrentHold(TickData data, int lastIndex, int index)
        //{
        //    int hold = 0;
        //    for (int i = lastIndex + 1; i <= index; i++)
        //    {
        //        data.BarPos = i;
        //        hold += data.Add;
        //    }
        //    return hold;
        //}

        private void CalcMount(List<ITickData> tickData, int[] mounts, int[] lastIndeies, int[] currentIndeies)
        {
            for (int i = 0; i < mounts.Length; i++)
            {
                mounts[i] = CalcMount(tickData[i], lastIndeies[i], currentIndeies[i]);
            }
        }

        private int CalcMount(ITickData data, int lastIndex, int index)
        {
            int mount = 0;
            for (int i = lastIndex + 1; i <= index; i++)
            {
                data.BarPos = i;
                mount += data.Mount;
            }
            return mount;
        }

        private List<double> GetTimeArr(List<ITickData> tickData, List<double[]> openTime)
        {
            ITickData mainTick = GetMainTickData(tickData);
            int tradingDay = mainTick.TradingDay;
            int prevTradingDay = dataLoader.LoadTradingDayReader().GetPrevTradingDay(tradingDay);
            List<double> times = KLineTimeListUtils.GetKLineTimeList(tradingDay, prevTradingDay, openTime, new KLinePeriod(KLinePeriod.TYPE_SECOND, 1));
            List<double> timeArr = new List<double>(times.Count);

            int dateStart = (int)mainTick.Arr_Time[0];
            int dateEnd = (int)mainTick.Arr_Time[mainTick.Length - 1];
            if (dateStart == dateEnd)
            {
                for (int i = 0; i < times.Count; i++)
                {
                    //times[i] = dateStart + times[i];
                    timeArr.Add(dateStart + times[i]);
                }
            }
            else
            {
                bool isNextDay = false;
                for (int i = 0; i < times.Count; i++)
                {
                    if (i != 0 && !isNextDay)
                        isNextDay = times[i - 1] > times[i];
                    int date = isNextDay ? dateEnd : dateStart;
                    //times[i] = date + times[i];
                    timeArr.Add(date + times[i]);
                }
            }

            return timeArr;
        }

        private ITickData GetMainTickData(List<ITickData> tickData)
        {
            ITickData mainTick = tickData[0];
            for (int i = 1; i < tickData.Count; i++)
            {
                ITickData tick = tickData[i];
                if (tick == null)
                    continue;
                if (mainTick == null || tick.Length > mainTick.Length)
                    mainTick = tick;
            }
            return mainTick;
        }

        private void CalcIndeies(List<ITickData> data, double currentTime, int[] currentIndeies, int[] lastIndeies)
        {
            for (int i = 0; i < data.Count; i++)
            {
                ITickData tickdata = data[i];
                if (tickdata == null)
                    continue;
                lastIndeies[i] = currentIndeies[i];
                int nextIndex = calcNextIndex(tickdata, currentIndeies[i], currentTime);
                currentIndeies[i] = nextIndex;
                tickdata.BarPos = nextIndex;
            }
        }

        private int calcNextIndex(ITickData data, int currentTickIndex, double currentTime)
        {
            if (currentTickIndex + 1 >= data.Length)
                return currentTickIndex;

            double nextTickTime = data.Arr_Time[currentTickIndex + 1];
            if (nextTickTime <= currentTime)
                return currentTickIndex + 1;
            return currentTickIndex;
        }

        private bool isNextOverTime(ITickData data, int currentTickIndex, double time)
        {
            if (currentTickIndex + 1 >= data.Length)
                return false;
            double nextTickTime = data.Arr_Time[currentTickIndex + 1];
            return nextTickTime >= time;
        }

        private void CalcCurrent(ITickData data, int currentTickIndex, List<ITickData> currentData, int[] mounts, int[] lastIndeies)
        {
            float price = 0;
            int mount = 0;
            int totalMount = 0;
            int hold = 0;
            int add = 0;
            for (int i = 0; i < currentData.Count; i++)
            {
                ITickData tickdata = currentData[i];
                if (tickdata == null)
                    continue;
                price += tickdata.Price * tickdata.Hold;
                mount += mounts[i];
                totalMount += tickdata.TotalMount;
                hold += tickdata.Hold;
                if (currentTickIndex == 0)
                    add += tickdata.Hold;
                else
                    add += tickdata.Hold - tickdata.Arr_Hold[lastIndeies[i]];
            }
            price = (float)Math.Round((float)(price / hold), 2);
            data.Arr_Price[currentTickIndex] = price;
            data.Arr_Mount[currentTickIndex] = mount;
            data.Arr_TotalMount[currentTickIndex] = totalMount;
            data.Arr_Add[currentTickIndex] = add;
        }
    }
}
