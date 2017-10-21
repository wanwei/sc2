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
    public interface IDataCenter
    {
        /// <summary>
        /// 得到数据中心信息
        /// </summary>
        IDataCenterInfo DataCenterInfo { get; }

        /// <summary>
        /// 得到数据读取器
        /// </summary>
        /// <returns></returns>
        IDataReader DataReader { get; }              

        /// <summary>
        /// 得到数据包工厂
        /// </summary>
        IDataPackageFactory DataPackageFactory { get; }

        /// <summary>
        /// 得到数据导航器创建工厂
        /// </summary>
        /// <returns></returns>
        IDataNavigateFactory DataNavigateFactory { get; }

        /// <summary>
        /// 得到数据前进器工厂
        /// </summary>
        /// <returns></returns>
        IDataForwardFactory HistoryDataForwardFactory { get; }
    }
}
