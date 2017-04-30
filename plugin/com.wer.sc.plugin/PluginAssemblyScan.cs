using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 检索
    /// </summary>
    public class PluginAssemblyScan
    {
        private static string[] ignoreDll = GetIgnoreDll();

        private static string[] GetIgnoreDll()
        {
            string[] strs = new string[] {
                "com.wer.sc.plugin.dll",
                "com.wer.sc.utils.dll",
                "log4net.dll" };
            //,
            //  "XAPI_CSharp.dll"};
            return strs;
        }

        public List<PluginAssembly> Scan(String path)
        {
            //Console.WriteLine(path);
            if (!Directory.Exists(path))
                return new List<PluginAssembly>();
            String[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            List<PluginAssembly> plugins = new List<PluginAssembly>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                //if (IsIgnoreDll(file))
                //    continue;
                PluginAssembly plugin = PluginAssembly.Create(file);
                if (plugin != null)
                    plugins.Add(plugin);
            }
            string[] dirs = Directory.GetDirectories(path);
            for (int i = 0; i < dirs.Length; i++)
            {
                //Console.WriteLine(dirs[i]);
                plugins.AddRange(Scan(dirs[i]));
            }
            for (int i = 0; i < plugins.Count; i++)
                Console.WriteLine(plugins[i]);
            return plugins;
        }

        //private static bool IsIgnoreDll(string file)
        //{
        //    for (int i = 0; i < ignoreDll.Length; i++)
        //    {
        //        if (file.EndsWith(ignoreDll[i]))
        //            return true;
        //    }
        //    return false;
        //}

        private void LoadPluginFile(string configFile, List<PluginAssembly> pluginAssemblies)
        {
            if (!File.Exists(configFile))
                return;
            DirectoryInfo dir = new FileInfo(configFile).Directory;

            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);
            XmlNodeList nodes = doc.GetElementsByTagName("Assembly");
            foreach (XmlNode node in nodes)
            {
                if (!(node is XmlElement))
                    continue;
                XmlElement elem = (XmlElement)node;
                string str = elem.GetAttribute("file");
                string dllPath = dir.FullName + "\\" + str;
                if (!File.Exists(dllPath))
                    continue;
                //PluginAssembly assembly = new PluginAssembly();
            }
        }

        private IList<PluginInfo> ScanStrategies(string path)
        {
            return null;
        }
    }
}
