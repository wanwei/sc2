﻿using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.real
{
    public interface IGraphicDataProvider_Real
    {
        String Code { get; }

        /// <summary>
        /// 获得当前数据
        /// </summary>
        /// <returns></returns>
        ITimeLineData GetRealData();

        /// <summary>
        /// 得到当前的Charts
        /// </summary>
        /// <returns></returns>
        ITimeLineBar GetCurrentChart();

        /// <summary>
        /// 得到或设置当前的index
        /// </summary>
        int CurrentIndex { get; }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        double CurrentTime { get; }

        //event DataChange
    }

    public delegate void DataChangeEventHandler(Object source, DataChangeEventArgs e);

    public class DataChangeEventArgs : System.EventArgs
    {
        public DataChangeEventArgs()
        {
            
        }        
    }
}