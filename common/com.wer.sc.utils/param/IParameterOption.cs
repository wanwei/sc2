using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.param
{
    /// <summary>
    /// 参数选项
    /// </summary>
    public interface IParameterOption : IXmlExchange
    {
        Object Value { get; }
    }
}