using com.wer.sc.plugin;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.loader
{
    /// <summary>
    /// 策略信息类
    /// </summary>
    public class StrategyInfo : IStrategyInfo
    {
        private IStrategyAssembly strategyAssembly;

        private Type strategyClassType;

        private string strategyID;

        private string strategyName;

        private string strategyDesc;

        private string strategyPath;

        //private IParameters parameters;

        private bool isError;

        private string errorInfo;   

        public StrategyInfo(IStrategyAssembly pluginAssembly, Type pluginClassType, string pluginID, string pluginName, string pluginDesc, string strategyPath) : this(pluginAssembly, pluginClassType, pluginID, pluginName, pluginDesc, strategyPath, false, "")
        {
        }

        public StrategyInfo(IStrategyAssembly pluginAssembly, Type pluginClassType, string pluginID, string pluginName, string pluginDesc, string strategyPath, bool isError, string errorInfo)
        {
            this.strategyAssembly = pluginAssembly;
            this.strategyClassType = pluginClassType;
            this.strategyID = pluginID;
            this.strategyName = pluginName;
            this.strategyDesc = pluginDesc;
            this.strategyPath = strategyPath;
            this.isError = isError;
            this.errorInfo = errorInfo;
        }

        /// <summary>
        /// 得到插件所在的Assembly
        /// </summary>
        public IStrategyAssembly StrategyAssembly
        {
            get
            {
                return strategyAssembly;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type StrategyClassType
        {
            get { return strategyClassType; }
        }

        /// <summary>
        /// 插件ID
        /// </summary>
        public string StrategyID
        {
            get
            {
                return strategyID;
            }
        }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name
        {
            get
            {
                return strategyName;
            }
        }

        public string Description
        {
            get
            {
                return strategyDesc;
            }
        }

        public string StrategyPath
        {
            get { return strategyPath; }
        }

        public IStrategy CreateStrategy()
        {
            //Assembly.Load("com.wer.sc.strategy.common");
            //Assembly assembly = Assembly.LoadFile(@"D:\SCWORK\DEV\SC2\bin\Debug\strategy\com.wer.sc.strategy.common.dll");
            //assembly.CreateInstance("com.wer.sc.strategy.common.Strategy_Zigzag");
            //assembly = Assembly.LoadFile(@"D:\SCWORK\DEV\SC2\bin\Debug\strategy\com.wer.sc.strategy.daily.dll");
            //Object obj = assembly.CreateInstance("com.wer.sc.strategy.daily.20171213.Strategy_20171213");
            //return this.strategyAssembly.CreateStrategyObject(this);
            Object obj = Activator.CreateInstance(strategyClassType);
            return (IStrategy)obj;
        }

        public IStrategyData CreateStrategyData()
        {
            StrategyData strategyData = new StrategyData(this, CreateStrategy());
            return strategyData;
        }

        public static IStrategy CreateNewStrategyWithParameters(IStrategy strategy)
        {
            IParameters parameters = strategy.Parameters;
            IStrategy newStrategy = (IStrategy)Activator.CreateInstance(strategy.GetType());
            newStrategy.Parameters.SetParameterValue(parameters.GetParameterValues());
            //newStrategy.Parameters.AddParameterRange(parameters.GetAllParameters());
            return newStrategy;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(strategyClassType.FullName).Append(",");
            sb.Append(StrategyID).Append(",");
            sb.Append(Name).Append(",");
            sb.Append(Description).Append(",");
            sb.Append(StrategyPath).Append(",");
            sb.Append(isError).Append(",");
            sb.Append(errorInfo);
            return sb.ToString();
        }

        //public IParameters Parameters
        //{
        //    get
        //    {
        //        return parameters;
        //    }
        //}

        /// <summary>
        /// 是否是错误的策略
        /// </summary>
        public bool IsError { get { return false; } }

        /// <summary>
        /// 策略的错误信息
        /// </summary>
        public string ErrorInfo { get { return null; } }
    }
}