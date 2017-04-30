using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        private string id;

        private String name;

        private string desc;

        private MarketType marketType;

        /// <summary>
        /// 插件属性的构造函数
        /// </summary>
        /// <param name="id">插件的ID，ID最好带上命名空间，如HistoryData.CnFutures。防止重复</param>
        /// <param name="name">插件的名称</param>
        /// <param name="desc">插件的描述</param>
        /// <param name="marketType">该插件对应的市场</param>
        public PluginAttribute(string id, String name, string desc, MarketType marketType)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.marketType = marketType;
        }

        public string ID
        {
            get
            {
                return id;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public string Desc
        {
            get
            {
                return desc;
            }
        }

        public MarketType MarketType
        {
            get
            {
                return marketType;
            }
        }
    }
}
