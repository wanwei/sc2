using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.param
{
    /// <summary>
    /// 参数选项集合
    /// </summary>
    public interface IParameterOptions : IXmlExchange
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        ParameterType OptionType { get; }

        void AddOption(object value);
      
        /// <summary>
        /// 清空所有options
        /// </summary>
        void ClearOptions();

        /// <summary>
        /// 参数的所有options
        /// </summary>
        List<IParameterOption> Options { get; }
    }
}
