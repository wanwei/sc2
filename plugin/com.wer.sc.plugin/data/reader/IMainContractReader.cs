using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public interface IMainContractReader
    {
        /// <summary>
        /// 获得一张合约的所有主力合约信息
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        List<MainContractInfo> GetMainContractInfos(string variety);

        /// <summary>
        /// 获得一张合约一段时间内的主力合约
        /// </summary>
        /// <param name="variety"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<MainContractInfo> GetMainContractInfos(string variety, int startDate, int endDate);

        /// <summary>
        /// 获得主力合约
        /// </summary>
        /// <param name="variety"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        MainContractInfo GetMainContractInfo(string variety, int date);

        /// <summary>
        /// 得到下一个主合约
        /// </summary>
        /// <param name="variety"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        MainContractInfo GetNextMainContractInfo(string variety, int date);

        /// <summary>
        /// 得到上一个主合约
        /// </summary>
        /// <param name="variety"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        MainContractInfo GetPrevMainContractInfo(string variety, int date);

        /// <summary>
        /// 得到最新的主力合约
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        MainContractInfo GetRecentMainContract(string variety);
    }
}
