using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.param
{
    public interface ParameterObject : IXmlExchange
    {
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="parameters"></param>
        void SetParameter(Dictionary<String, Object> parameters);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parameterValue"></param>
        void SetParameter(String key, Object parameterValue);

        /// <summary>
        /// 得到参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Object GetParameter(String key);

        /// <summary>
        /// 得到所有参数值
        /// </summary>
        /// <returns></returns>
        Dictionary<String, Object> GetParameterValues();

        /// <summary>
        /// 清空所有参数
        /// </summary>
        void ClearParameter();

        /// <summary>
        /// 获得描述信息
        /// </summary>
        String Description { get; }

        /// <summary>
        /// 得到所有参数的元信息
        /// 该方法是用来提供对象的所有参数内容
        /// </summary>
        /// <returns></returns>
        List<ParameterMeta> GetParameterMetas();

        /// <summary>
        /// 克隆参数对象
        /// </summary>
        /// <returns></returns>
        ParameterObject cloneParam();
    }
}