using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 插件管理器接口
    /// </summary>
    public interface IPluginMgr
    {
        /// <summary>
        /// 得到所有插件
        /// </summary>
        /// <returns></returns>
        List<PluginInfo> GetAllPlugins();

        /// <summary>
        /// 根据插件类型得到对应的插件
        /// </summary>
        /// <param name="type">对应PluginInfo里的类型</param>
        /// <returns></returns>
        List<PluginInfo> GetPlugins(Type type);

        /// <summary>
        /// 根据插件类型得到对应的插件
        /// </summary>
        /// <param name="type">对应PluginInfo里的类型</param>
        /// <returns></returns>
        List<PluginInfo> GetPlugins(MarketType market, Type type);

        /// <summary>
        /// 得到指定ID的插件信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PluginInfo GetPlugin(string id);

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T CreatePluginObject<T>(string pluginId);

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <param name="pluginInfo"></param>
        /// <returns></returns>
        T CreatePluginObject<T>(PluginInfo pluginInfo);

        /// <summary>
        /// 得到一个默认的插件对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        T GetPluginObject<T>(string pluginId);

        /// <summary>
        /// 得到一个默认的插件对象实例
        /// </summary>
        /// <param name="pluginInfo"></param>
        /// <returns></returns>
        T GetPluginObject<T>(PluginInfo pluginInfo);
    }
}