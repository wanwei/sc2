using com.wer.sc.utils.param.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.param
{
    /// <summary>
    /// 参数工厂
    /// </summary>
    public class ParameterFactory
    {
        public static IParameters CreateParameters()
        {
            return new Parameters();
        }

        public static IParameterOptions CreateParameterOptions(ParameterType optionType, object[] values)
        {
            return new ParameterOptions(optionType, values);
        }
    }
}
