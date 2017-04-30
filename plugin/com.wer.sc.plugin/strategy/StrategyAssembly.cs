using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyAssembly : IStrategyAssembly
    {
        private string assemblyName;

        private string fullPath;

        private IList<String> rootPaths = new List<String>();

        private Dictionary<string, IList<String>> dic_Parent_SubPath = new Dictionary<string, IList<string>>();

        private Dictionary<string, IList<StrategyInfo>> dic_Parent_SubStrategies = new Dictionary<string, IList<StrategyInfo>>();

        private List<StrategyInfo> strategyInfos = new List<StrategyInfo>();

        private Dictionary<Type, StrategyInfo> dic_Type_StrategyInfo = new Dictionary<Type, StrategyInfo>();

        private Dictionary<String, StrategyInfo> dic_Id_StrategyInfo = new Dictionary<string, StrategyInfo>();

        /// <summary>
        /// 得到策略包的名称
        /// </summary>
        public string AssemblyName
        {
            get
            {
                return assemblyName;
            }
        }

        /// <summary>
        /// 得到策略包的完整路径
        /// </summary>
        public string FullPath { get { return fullPath; } }

        /// <summary>
        /// 得到所有策略信息
        /// </summary>
        /// <returns></returns>
        public List<StrategyInfo> GetAllStrategies()
        {
            return strategyInfos;
        }

        public StrategyInfo GetStrategy(String strategyId)
        {
            StrategyInfo value;
            bool b = dic_Id_StrategyInfo.TryGetValue(strategyId, out value);
            return b ? value : null;
        }

        private void AddStrategyInfo(StrategyInfo strategyInfo)
        {
            strategyInfos.Add(strategyInfo);
            dic_Id_StrategyInfo.Add(strategyInfo.StrategyID, strategyInfo);
            dic_Type_StrategyInfo.Add(strategyInfo.StrategyClassType, strategyInfo);
        }

        private void InitPaths()
        {
            List<string> roots = new List<string>();
            for (int i = 0; i < strategyInfos.Count; i++)
            {
                StrategyInfo strategyInfo = strategyInfos[i];
                string path = strategyInfo.StrategyClassType.Namespace;
                AddSubStrategies(strategyInfo, path);
                string root = AddPathLoop(path);
                if (!roots.Contains(root))
                    roots.Add(root);
            }
            for (int i = 0; i < roots.Count; i++)
            {
                rootPaths.Add(GetRootPath(roots[i]));
            }
        }

        private string GetRootPath(String root)
        {
            string rootPath = root;

            IList<String> subPaths = dic_Parent_SubPath[rootPath];
            int cnt = subPaths.Count;
            while (cnt == 1)
            {
                rootPath = subPaths[0];
                if (!dic_Parent_SubPath.ContainsKey(rootPath))
                    break;
                subPaths = dic_Parent_SubPath[rootPath];
                cnt = subPaths.Count;
            }            
            return rootPath;
        }

        private string AddPathLoop(string path)
        {
            string parent = GetParent(path);
            while (parent != null)
            {
                AddSubPath(path, parent);
                path = parent;
                parent = GetParent(path);
            }
            return path;
        }

        private void AddSubStrategies(StrategyInfo strategyInfo, string parentPath)
        {
            if (dic_Parent_SubStrategies.ContainsKey(parentPath))
            {
                dic_Parent_SubStrategies[parentPath].Add(strategyInfo);
                return;
            }
            List<StrategyInfo> strategies = new List<StrategyInfo>();
            strategies.Add(strategyInfo);
            dic_Parent_SubStrategies.Add(parentPath, strategies);
        }

        private void AddSubPath(string path, string parent)
        {
            if (dic_Parent_SubPath.ContainsKey(parent))
            {
                IList<string> currentPaths = dic_Parent_SubPath[parent];
                if (!currentPaths.Contains(path))
                    currentPaths.Add(path);
                return;
            }
            List<string> paths = new List<string>();
            paths.Add(path);
            dic_Parent_SubPath.Add(parent, paths);
        }

        private string GetParent(string path)
        {
            int index = path.LastIndexOf('.');
            if (index < 0)
                return null;
            return path.Substring(0, index);
        }

        private void CheckRoot(string path, IList<String> currentRootPaths)
        {
            for (int i = 0; i < currentRootPaths.Count; i++)
            {
                string rootPath = currentRootPaths[i];
                if (path == rootPath || IsParent(path, rootPath))
                    return;
                if (IsParent(rootPath, path))
                {
                    currentRootPaths.RemoveAt(i);
                    currentRootPaths.Add(path);
                    return;
                }
            }
            currentRootPaths.Add(path);
        }

        private bool IsParent(string path, string parentPath)
        {
            if (path == parentPath)
                return false;
            return parentPath.IndexOf(path) == 0;
        }

        /// <summary>
        /// 得到所有的顶级目录
        /// 对于C#插件，目录就是命名空间
        /// 对于python插件，目录是现实中的目录
        /// </summary>
        /// <returns></returns>
        public IList<string> GetRootPath()
        {
            return rootPaths;
        }

        /// <summary>
        /// 得到所有的子命名空间，如果传入空或空字符串，则返回第一层的命名空间
        /// </summary>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        public IList<string> GetSubPath(string parentPath)
        {
            IList<string> value;
            bool b = dic_Parent_SubPath.TryGetValue(parentPath, out value);
            return b ? value : null;
        }

        /// <summary>
        /// 得到所有的子策略
        /// </summary>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        public IList<StrategyInfo> GetSubStrategies(string parentPath)
        {
            IList<StrategyInfo> value;
            bool b = dic_Parent_SubStrategies.TryGetValue(parentPath, out value);
            return b ? value : null;
        }

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <param name="strategyInfo"></param>
        /// <returns></returns>
        public IStrategy CreateStrategyObject(StrategyInfo strategyInfo)
        {
            if (strategyInfo == null)
                return null;
            return (IStrategy)Activator.CreateInstance(strategyInfo.StrategyClassType);
        }

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IStrategy CreateStrategyObject(string strategyId)
        {
            return CreateStrategyObject(GetStrategy(strategyId));
        }

        ///// <summary>
        ///// 得到一个默认的插件对象实例
        ///// </summary>
        ///// <param name="strategyInfo"></param>
        ///// <returns></returns>
        //public IStrategy GetStrategyObject(StrategyInfo strategyInfo)
        //{
        //    if (strategyInfo == null)
        //        return null;
        //    return null;
        //}

        ///// <summary>
        ///// 得到一个默认的插件对象实例
        ///// </summary>
        ///// <param name="strategyId"></param>
        ///// <returns></returns>
        //public IStrategy GetStrategyObject(string strategyId)
        //{
        //    return GetStrategyObject(GetStrategy(strategyId));
        //}

        /// <summary>
        /// 根据策略名称查找策略，模糊查找
        /// </summary>
        /// <param name="strategyName"></param>
        /// <returns></returns>
        public IList<IStrategyAssembly> SearchStrategyInfo(String strategyName)
        {
            return null;
        }

        internal static StrategyAssembly Create(string path)
        {
            //try
            //{
            StrategyAssembly strategyAssembly = new StrategyAssembly();
            Assembly ass = Assembly.LoadFrom(path);
            if (ass == null)
                return null;

            strategyAssembly.fullPath = path;
            strategyAssembly.assemblyName = ass.GetName().Name;            
            Type[] types = ass.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type classType = types[i];
                //类上定义了特性才行
                Type attributeType = typeof(StrategyAttribute);
                if (!classType.IsDefined(attributeType, false))
                    continue;

                Type inheritType = classType.GetInterface(typeof(IStrategy).FullName);
                if (inheritType == null)
                    continue;

                var attributes = classType.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType() == attributeType)
                    {
                        string id = (String)attribute.GetType().GetProperty("ID").GetValue(attribute);
                        String name = (String)attribute.GetType().GetProperty("Name").GetValue(attribute);
                        string desc = (String)attribute.GetType().GetProperty("Desc").GetValue(attribute);
                        StrategyInfo strategyInfo = new StrategyInfo(strategyAssembly, classType, id, name, desc);
                        strategyAssembly.AddStrategyInfo(strategyInfo);
                    }
                }
            }
            strategyAssembly.InitPaths();
            return strategyAssembly;
            //}
            //catch (Exception e)
            //{
            //    //Console.WriteLine(e.Data);
            //    return null;
            //}
        }
    }
}
