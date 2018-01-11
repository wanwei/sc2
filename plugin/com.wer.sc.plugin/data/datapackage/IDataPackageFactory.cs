using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.datapackage
{
    public interface IDataPackageFactory
    {
        /// <summary>
        /// 创建数据包
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IDataPackage CreateDataPackage(string[] codes, int startDate, int endDate);

        /// <summary>
        /// 创建数据包
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="minKlineBefore"></param>
        /// <param name="minKlineAfter"></param>
        /// <returns></returns>
        IDataPackage CreateDataPackage(string[] codes, int startDate, int endDate, int minKlineBefore, int minKlineAfter);

        /// <summary>
        /// 创建单支股票或期货在一段时间内的数据包
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IDataPackage_Code CreateDataPackage_Code(string code, int startDate, int endDate);

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
        IDataPackage_Code CreateDataPackage_Code(string code, int startDate, int endDate, int minKlineBefore, int minKlineAfter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="openDate"></param>
        /// <param name="beforeDays"></param>
        /// <param name="afterDays"></param>
        /// <returns></returns>
        IDataPackage_Code CreateDataPackage_Code(string code, int openDate, int beforeDays, int afterDays);

        /// <summary>
        /// 创建数据包
        /// </summary>
        /// <param name="code"></param>
        /// <param name="openDate"></param>
        /// <param name="beforeDays"></param>
        /// <param name="afterDays"></param>
        /// <param name="minKlineBefore"></param>
        /// <param name="minKlineAfter"></param>
        /// <returns></returns>
        IDataPackage_Code CreateDataPackage_Code(string code, int openDate, int beforeDays, int afterDays, int minKlineBefore, int minKlineAfter);

        /// <summary>
        /// 根据xml创建数据包
        /// </summary>
        /// <param name="xmlElem"></param>
        /// <returns></returns>
        IDataPackage_Code CreateDataPackage_Code(XmlElement xmlElem);

        /// <summary>
        /// 创建单支股票或期货的实时数据包
        /// </summary>        
        /// <param name="code"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        //IRealTimeDataPackage_Code CreateRealTimeDataPackage_Code(string code, double time);
    }
}
