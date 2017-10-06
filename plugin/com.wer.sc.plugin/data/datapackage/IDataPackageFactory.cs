using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public interface IDataPackageFactory
    {
        /// <summary>
        /// 创建单支股票或期货在一段时间内的数据包
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IDataPackage CreateDataPackage(string code, int startDate, int endDate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="minKlineBefore"></param>
        /// <param name="minKlineAfter"></param>
        /// <returns></returns>
        IDataPackage CreateDataPackage(string code, int startDate, int endDate, int minKlineBefore, int minKlineAfter);

        IDataPackage CreateDataPackage(string code, int openDate, int beforeDays, int afterDays);


        IDataPackage CreateDataPackage(string code, int openDate, int beforeDays, int afterDays, int minKlineBefore, int minKlineAfter);
    }
}
