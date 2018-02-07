using com.wer.sc.strategy;
using com.wer.sc.strategy.loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.loader
{
    public class StrategyAssembly : StrategyAssemblyConfig, IStrategyAssembly
    {
        private Assembly assembly;

        private bool isError;

        private string errorInfo;

        public StrategyAssembly()
        {
        }

        public bool IsError
        {
            get
            {
                return isError;
            }
        }

        public string ErrorInfo
        {
            get
            {
                return errorInfo;
            }
        }

        public static StrategyAssembly Create(string file)
        {
            StrategyAssembly strategyAssembly = new StrategyAssembly();
            strategyAssembly.Load(file);

            if (!File.Exists(strategyAssembly.FullPath))
            {
                strategyAssembly.isError = true;
                strategyAssembly.errorInfo = "无法找到Assembly" + strategyAssembly.AssemblyName;
                return strategyAssembly;
            }
            byte[] buffer = File.ReadAllBytes(strategyAssembly.FullPath);
            Assembly ass = Assembly.Load(buffer);
            if (ass == null)
            {
                strategyAssembly.isError = true;
                strategyAssembly.errorInfo = strategyAssembly.AssemblyName + "装载失败";
                return strategyAssembly;
            }
            strategyAssembly.assembly = ass;

            List<IStrategyInfo> strategies = strategyAssembly.GetAllStrategies();
            foreach (StrategyInfo config in strategies)
            {
                string clsName = config.ClassName;
                Type classType = ass.GetType(clsName, false, true);

                if (classType == null)
                {
                    config.IsError = true;
                    config.ErrorInfo = "类型" + clsName + "不存在";
                    continue;
                }
                config.strategyType = classType;
                Type inheritType = classType.GetInterface(typeof(IStrategy).FullName);
                if (inheritType == null)
                {
                    config.IsError = true;
                    config.ErrorInfo = "类型" + clsName + "没有实现IStrategy";
                    continue;
                }                
            }
            return strategyAssembly;
        }

        public IStrategy CreateStrategy(string strategyClsName)
        {
            if (assembly == null)
                InitAssembly();
            object obj = assembly.CreateInstance(strategyClsName);
            //object obj = Activator.CreateInstance(strategyClsName);
            return (IStrategy)obj;
        }

        private void InitAssembly()
        {
            byte[] buffer = System.IO.File.ReadAllBytes(FullPath);
            this.assembly = Assembly.Load(buffer);
        }

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <param name="strategyInfo"></param>
        /// <returns></returns>
        public IStrategy CreateStrategyObject(IStrategyInfo strategyInfo)
        {
            if (strategyInfo == null)
                return null;
            object obj = Activator.CreateInstance(strategyInfo.StrategyClassType);
            return (IStrategy)obj;
            //assembly.CreateInstance("", false, BindingFlags.CreateInstance, null, null, null, null);
            //return (IStrategy)assembly.CreateInstance(strategyInfo.StrategyClassType.ToString());
        }

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IStrategy CreateStrategyObject(string strategyClassName)
        {
            return CreateStrategyObject(GetStrategyInfo(strategyClassName));
        }

        public IStrategyData CreateStrategyData(string strategyClsName)
        {
            return new StrategyData(GetStrategyInfo(strategyClsName));
        }
    }
}
