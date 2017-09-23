using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 数据更新插件
    /// 该插件用于系统内的数据更新
    /// 比如下载股票数据、更新期货数据到数据中心等。
    /// 该插件是由com.wer.sc.data.updater项目装载并执行
    /// </summary>
    public interface IPlugin_DataUpdater
    {
        /// <summary>
        /// 得到所有更新步骤
        /// </summary>
        IUpdateHelper PluginHelper { get; }

        /// <summary>
        /// 得到更新时用到的所有参数及其描述
        /// </summary>
        /// <returns></returns>
        List<PreparerArgument> GetAllArguments();

        /// <summary>
        /// 设置参数的值
        /// 该方法会在更新前调用，系统会将所有参数预先设置到更新器里
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetValue(string key, object value);
    }

    public class PreparerArgument
    {
        private string id;

        private string name;

        private string value;

        public PreparerArgument()
        {

        }

        public PreparerArgument(string id, string name, string value)
        {
            this.id = id;
            this.name = name;
            this.value = value;
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}