using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.param
{
    /// <summary>
    /// 单个参数的元数据信息
    /// </summary>
    public interface IParameter : IXmlExchange
    {
        string Key { get; }

        string Caption { get; }

        ParameterType ParameterType { get; }

        object DefaultValue { get; }

        object Value { get; set; }

        IParameterOptions Options { get; }
    }
}