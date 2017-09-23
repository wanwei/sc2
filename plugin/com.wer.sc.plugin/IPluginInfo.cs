using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public interface IPluginInfo
    {  /// <summary>
       /// 得到插件的实现类对应的类型
       /// </summary>
        Type PluginClassType { get; }

        /// <summary>
        /// 得到插件接口类型
        /// </summary>
        Type PluginType { get; }

        /// <summary>
        /// 插件ID
        /// </summary>
        string PluginID { get; }

        /// <summary>
        /// 插件名称
        /// </summary>
        string PluginName { get; }


        string PluginDesc { get; }

        MarketType MarketType { get; }
    }
}
