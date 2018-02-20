using com.wer.sc.data.account;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据中心接口
    /// 
    /// 数据获取的入口，每个数据中心包含K线、分时线等大量的数据信息
    /// </summary>
    public interface IDataCenter
    {
        /// <summary>
        /// 得到数据中心信息
        /// 包括数据存储的位置方式等信息
        /// </summary>
        IDataCenterInfo DataCenterInfo { get; }

        /// <summary>
        /// 得到数据读取器
        /// </summary>
        /// <returns></returns>
        IDataReader DataReader { get; }              

        /// <summary>
        /// 得到数据包创建工厂
        /// </summary>
        IDataPackageFactory DataPackageFactory { get; }

        /// <summary>
        /// 得到股票或期货代码的数据包
        /// </summary>
        ICodePeriodFactory CodePackageFactory { get; }

        /// <summary>
        /// 得到数据导航器创建工厂
        /// </summary>
        /// <returns></returns>
        IDataNavigateFactory DataNavigateFactory { get; }

        /// <summary>
        /// 得到历史数据前进器工厂
        /// </summary>
        /// <returns></returns>
        IDataForwardFactory HistoryDataForwardFactory { get; }

        /// <summary>
        /// 得到账号工厂
        /// </summary>
        IAccountManager AccountManager { get; }
    }
}