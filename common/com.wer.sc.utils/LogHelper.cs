using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
//[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = @"Config\Log4net.config")]
namespace com.wer.sc.utils
{
    public class LogHelper
    {
        public const int LEVEL_INFO = 0;

        public const int LEVEL_WARN = 1;

        public const int LEVEL_ERROR = 2;

        public const int LEVEL_FATAL = 3;

        private static bool logScope = false;

        public static bool LogScope
        {
            get
            {
                return logScope;
            }

            set
            {
                logScope = value;
            }
        }

        private static List<String> scopes = new List<string>();

        public static List<string> Scopes
        {
            get
            {
                return scopes;
            }
        }

        public static void AddScope(String nameSpace)
        {
            scopes.Add(nameSpace);
        }

        public static void RemoveScope(String nameSpace)
        {
            scopes.Remove(nameSpace);
        }

        private static bool IsLog(Type t)
        {
            if (!logScope)
                return true;
            if (scopes.Count == 0)
                return false;
            String fullName = t.FullName;
            for (int i = 0; i < scopes.Count; i++)
            {
                if (fullName.StartsWith(scopes[i]))
                    return true;
            }
            return false;
        }

        public static void Fatal(Type t, Exception e)
        {
            if (!IsLog(t))
                return;
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Fatal("Fatal", e);
        }

        public static void Fatal(Type t, string msg)
        {
            if (!IsLog(t))
                return;
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Fatal(msg);
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void Error(Type t, Exception ex)
        {
            if (!IsLog(t))
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void Error(Type t, string msg)
        {
            if (!IsLog(t))
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        public static void Warn(Type t, Exception e)
        {
            if (!IsLog(t))
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn("Warn", e);
        }

        public static void Warn(Type t, string msg)
        {
            if (!IsLog(t))
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn(msg);
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void Info(Type t, Exception ex)
        {
            if (!IsLog(t))
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info("Info", ex);
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void Info(Type t, string msg)
        {
            if (!IsLog(t))
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);            
            log.Info(msg);
        }
    }
}
