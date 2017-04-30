using com.wer.sc.utils;
using System;
using System.IO;
using System.Text;

namespace com.wer.sc.mockdata
{
    /// <summary>
    /// 测试用例装载器
    /// 所有SC的测试用例必须放在
    /// </summary>
    public class TestCaseManager
    {
        public static void DeleteTestCaseFile(Type type, string fileName)
        {
            DeleteTestCaseFile(type.Namespace, fileName);
        }

        public static void DeleteTestCaseFile(string nameSpace, string fileName)
        {
            string path = GetTestCasePath(nameSpace, fileName);
            File.Delete(path);
        }

        public static void SaveTestCaseFile(Type type, string fileName, string content)
        {
            SaveTestCaseFile(type.Namespace, fileName, content);
        }

        public static void SaveTestCaseFile(string nameSpace, string fileName, string content)
        {
            string path = GetTestCasePath(nameSpace, fileName);
            File.WriteAllText(path, content);
        }

        public static string LoadTestCaseFile(Type type, string fileName)
        {
            return LoadTestCaseFile(type.Namespace, fileName);
        }

        public static string LoadTestCaseFile(string nameSpace, string fileName)
        {
            string path = GetTestCasePath(nameSpace, fileName);
            return File.ReadAllText(path);
        }

        public static string LoadTestCaseFile(Type type, string fileName, Encoding encoding)
        {
            return LoadTestCaseFile(type.Namespace, fileName, encoding);
        }

        public static string LoadTestCaseFile(string nameSpace, string fileName, Encoding encoding)
        {
            string path = GetTestCasePath(nameSpace, fileName);
            return File.ReadAllText(path, encoding);
        }

        public static string GetTestCasePath(Type type, string fileName)
        {
            return GetTestCasePath(type.Namespace, fileName);
        }

        public static string GetTestCasePath(string nameSpace, string fileName)
        {
            string testcasePath = nameSpace.Replace('.', '\\');
            string path = ScConfig.Instance.ScPath + "\\TestCase\\" + testcasePath + "\\" + fileName;
            return path;
        }
    }
}
