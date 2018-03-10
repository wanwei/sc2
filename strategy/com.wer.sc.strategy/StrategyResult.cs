using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.utils;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略结果
    /// </summary>
    public class StrategyResult : IStrategyResult
    {
        private string name;

        private List<ICodePeriod> codePeriods = new List<ICodePeriod>();

        private int startDate;

        private int endDate;

        private ForwardPeriod forwardPeriod;

        private StrategyReferedPeriods referedPeriods;

        //private IStrategy strategy;

        private IParameters parameters;

        private IStrategyQueryResultManager queryResultManager;

        private List<IStrategyResult_CodePeriod> strategyResult_CodePeriods = new List<IStrategyResult_CodePeriod>();

        private Dictionary<ICodePeriod, IStrategyResult_CodePeriod> dic_CodePeriod_StrategyResult_CodePeriods = new Dictionary<ICodePeriod, IStrategyResult_CodePeriod>();

        /// <summary>
        /// 策略里使用的
        /// </summary>
        public IList<ICodePeriod> CodePeriods
        {
            get
            {
                return codePeriods;
            }
        }

        /// <summary>
        /// 策略执行开始时间
        /// </summary>
        public int StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                this.startDate = value;
            }
        }

        /// <summary>
        /// 策略执行结束时间
        /// </summary>
        public int EndDate
        {
            get
            {
                return endDate;
            }
            set { endDate = value; }
        }

        /// <summary>
        /// 回测前进的周期
        /// </summary>
        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
            set { forwardPeriod = value; }
        }

        /// <summary>
        /// 回测引用的周期
        /// </summary>
        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return referedPeriods;
            }
            set { referedPeriods = value; }
        }

        /// <summary>
        /// 回测使用的参数
        /// </summary>
        public IParameters Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        /// <summary>
        /// 回测找到的总结果集
        /// </summary>
        public IStrategyQueryResultManager StrategyQueryResultManager
        {
            get
            {
                if (queryResultManager == null)
                    this.queryResultManager = new StrategyQueryResultManager();
                return queryResultManager;
            }
            set { queryResultManager = value; }
        }

        /// <summary>
        /// 得到代码周期的单独运行结果
        /// 有绘图或者交易的策略才会有单独的代码周期结果，否则没有
        /// </summary>
        public IList<IStrategyResult_CodePeriod> StrategyResult_Codes
        {
            get { return strategyResult_CodePeriods; }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        public StrategyResult()
        {
        }

        public void AddStrategyResult_Code(IStrategyResult_CodePeriod strategyResult_CodePeriod)
        {
            if (strategyResult_CodePeriod == null)
                return;
            this.codePeriods.Add(strategyResult_CodePeriod.CodePeriod);
            strategyResult_CodePeriods.Add(strategyResult_CodePeriod);
            dic_CodePeriod_StrategyResult_CodePeriods.Add(strategyResult_CodePeriod.CodePeriod, strategyResult_CodePeriod);
        }

        /// <summary>
        /// 得到指定代码周期的运行结果
        /// </summary>
        /// <param name="codePeriod"></param>
        /// <returns></returns>
        public IStrategyResult_CodePeriod GetStrategyResult_Code(ICodePeriod codePeriod)
        {
            if (dic_CodePeriod_StrategyResult_CodePeriods.ContainsKey(codePeriod))
                return dic_CodePeriod_StrategyResult_CodePeriods[codePeriod];
            return null;
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("start", startDate.ToString());
            xmlElem.SetAttribute("end", endDate.ToString());

            XmlElement elemPeriods = xmlElem.OwnerDocument.CreateElement("referperiods");
            xmlElem.AppendChild(elemPeriods);
            this.referedPeriods.Save(elemPeriods);

            XmlElement elemForward = xmlElem.OwnerDocument.CreateElement("forwardperiod");
            xmlElem.AppendChild(elemForward);
            this.forwardPeriod.Save(elemForward);
        }

        public void Load(XmlElement xmlElem)
        {
            this.startDate = int.Parse(xmlElem.GetAttribute("start"));
            this.endDate = int.Parse(xmlElem.GetAttribute("end"));

            XmlElement elemPeriods = (XmlElement)xmlElem.GetElementsByTagName("referperiods")[0];
            this.referedPeriods = new StrategyReferedPeriods();
            this.referedPeriods.Load(elemPeriods);

            XmlElement elemForward = (XmlElement)xmlElem.GetElementsByTagName("forwardperiod")[0];
            this.forwardPeriod = new ForwardPeriod();
            this.forwardPeriod.Load(elemForward);
        }

        /// <summary>
        /// 策略结果保存方式：
        /// --20180307
        ///     --双顶_20170101_20171001
        ///         --双顶_20170101_20171001.strategyresult
        ///         --双顶_20170101_20171001.结果集1.queryresult
        ///         --双顶_20170101_20171001.结果集2.queryresult
        ///             --rb
        ///                 双顶_rb_20170101_20171001.strategyresult_code
        ///                 双顶_rb_20170101_20171001.strategyresult_code.shape
        ///                 双顶_rb_20170101_20171001.strategyresult_code.trader
        ///             --ma
        ///             --m
        /// --20180308
        ///     --双顶_20170101_20171001
        ///     --双顶_20170101_20171001
        /// </summary>
        public void Save(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("strategyresult");
            doc.AppendChild(root);
            Save(root);
            doc.Save(path);
            //doc.Save(GetPath(path));

            //SaveQueryResult(path);
        }

        //public void SaveQueryResult(string path)
        //{
        //    FileInfo f = new FileInfo(path);
        //    string name = f.Name;
        //    string title = name.Substring(0, name.LastIndexOf('.'));

        //    IList<IStrategyQueryResult> queryResults = this.StrategyQueryResultManager.GetQueryResults();
        //    for (int i = 0; i < queryResults.Count; i++)
        //    {
        //        IStrategyQueryResult queryResult = queryResults[i];
        //        string querypath = f.Directory.FullName + "\\" + title + "." + queryResult.Name + ".queryresult";
        //        queryResult.Save(querypath);
        //    }
        //}

        private string GetPath(string path)
        {
            return path;
            //return path + "\\" + this.Name + ".strategyresult";
        }

        public void Load(string path)
        {
            string resultPath = GetPath(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(resultPath);
            this.Load(doc.DocumentElement);

            FileInfo f = new FileInfo(path);
            FileInfo[] queryFiles = f.Directory.GetFiles("*.queryresult");

            for (int i = 0; i < queryFiles.Length; i++)
            {
                StrategyQueryResult queryResult = new StrategyQueryResult();
                queryResult.Load(queryFiles[i].FullName);
                this.StrategyQueryResultManager.AddQueryResult(queryResult);
            }
        }
    }
}