using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.param
{
    /// <summary>
    /// 参数集合接口
    /// </summary>
    public interface IParameters : IXmlExchange
    {
        /// <summary>
        /// 增加一个参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="caption"></param>
        /// <param name="desc"></param>
        void AddParameter(string key, string caption, string desc, ParameterType parameterType);

        /// <summary>
        /// 增加一个参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="caption"></param>
        /// <param name="desc"></param>
        /// <param name="defaultValue"></param>
        void AddParameter(string key, string caption, string desc, ParameterType parameterType, object defaultValue);

        void AddParameter(string key, string caption, string desc, ParameterType parameterType, object defaultValue, IParameterOptions options);

        void AddParameterRange(List<IParameter> parameters);

        /// <summary>
        /// 删除一个参数
        /// </summary>
        /// <param name="key"></param>
        void RemoveParameter(string key);

        /// <summary>
        /// 删除所有参数
        /// </summary>
        void ClearParameters();

        /// <summary>
        /// 得到参数描述信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IParameter GetParameter(string key);

        IParameter GetParameter(int index);

        /// <summary>
        /// 得到所有参数描述信息
        /// </summary>
        /// <returns></returns>
        List<IParameter> GetAllParameters();

        int Count { get; }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="parameters"></param>
        void SetParameterValue(Dictionary<String, Object> parameters);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parameterValue"></param>
        void SetParameterValue(String key, Object parameterValue);

        /// <summary>
        /// 得到参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Object GetParameterValue(String key);

        /// <summary>
        /// 得到所有参数值
        /// </summary>
        /// <returns></returns>
        Dictionary<String, Object> GetParameterValues();

        IParameters CloneParam();
    }
}
