﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 数据中心接口
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// 创建股票或期货信息保存接口
        /// </summary>
        /// <returns></returns>
        IInstrumentStore CreateInstrumentStore();

        /// <summary>
        /// 创建交易日保存接口
        /// </summary>
        /// <returns></returns>
        ITradingDayStore CreateTradingDayStore();

        /// <summary>
        /// 创建K线保存接口
        /// </summary>
        /// <returns></returns>
        IKLineDataStore CreateKLineDataStore();

        /// <summary>
        /// 创建tick保存接口
        /// </summary>
        /// <returns></returns>
        ITickDataStore CreateTickDataStore();

        /// <summary>
        /// 创建交易时间保存接口
        /// </summary>
        /// <returns></returns>
        ITradingSessionStore CreateTradingSessionStore();
    }
}