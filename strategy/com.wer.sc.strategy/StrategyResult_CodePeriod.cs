using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.graphic.shape;
using com.wer.sc.strategy;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    public class StrategyResult_CodePeriod : IStrategyResult_CodePeriod
    {
        private ICodePeriod codePeriod;

        private StrategyForwardPeriod forwardPeriod;

        private StrategyReferedPeriods referedPeriods;

        private IStrategyDrawer strategyDrawer;

        private IStrategyTrader strategyTrader;

        public StrategyResult_CodePeriod()
        {

        }

        public StrategyResult_CodePeriod(ICodePeriod codePeriod, StrategyForwardPeriod forwardPeriod, StrategyReferedPeriods referedPeriods)
        {
            this.codePeriod = codePeriod;
            this.forwardPeriod = forwardPeriod;
            this.referedPeriods = referedPeriods;
        }

        public StrategyResult_CodePeriod(ICodePeriod codePeriod, StrategyForwardPeriod forwardPeriod, StrategyReferedPeriods referedPeriods, IStrategyDrawer strategyDrawer, IStrategyTrader strategyTrader)
        {
            this.codePeriod = codePeriod;
            this.forwardPeriod = forwardPeriod;
            this.referedPeriods = referedPeriods;
            this.strategyDrawer = strategyDrawer;
            this.strategyTrader = strategyTrader;
        }

        /// <summary>
        /// 单个代码周期
        /// </summary>
        public ICodePeriod CodePeriod
        {
            get
            {
                return codePeriod;
            }
        }

        /// <summary>
        /// 回测前进的周期
        /// </summary>
        public StrategyForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
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
        }

        /// <summary>
        /// 策略在该代码周期内画的所有形状
        /// </summary>
        public IStrategyDrawer StrategyDrawer
        {
            get
            {
                return strategyDrawer;
            }
        }

        /// <summary>
        /// 策略交易部分
        /// </summary>
        public IStrategyTrader StrategyTrader
        {
            get
            {
                return strategyTrader;
            }
        }

        public int XmlElementCount
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlElems"></param>
        public void Save(IList<XmlElement> xmlElems)
        {
            SaveMain(xmlElems[0]);
            if (this.strategyDrawer != null && xmlElems[1] != null)
                this.strategyDrawer.Save(xmlElems[1]);
            if (this.strategyTrader != null && xmlElems[2] != null)
                this.strategyTrader.Save(xmlElems[2]);
        }

        private void SaveMain(XmlElement xmlElem)
        {
            XmlElement elemCodePeriod = xmlElem.OwnerDocument.CreateElement("codeperiod");
            xmlElem.AppendChild(elemCodePeriod);
            codePeriod.Save(elemCodePeriod);

            XmlElement elemForwardPeriod = xmlElem.OwnerDocument.CreateElement("forwardperiod");
            xmlElem.AppendChild(elemForwardPeriod);
            forwardPeriod.Save(elemForwardPeriod);

            XmlElement elemReferedPeriods = xmlElem.OwnerDocument.CreateElement("referedperiods");
            xmlElem.AppendChild(elemReferedPeriods);
            referedPeriods.Save(elemReferedPeriods);
        }

        public void Load(IList<XmlElement> xmlElems)
        {
            LoadMain(xmlElems[0]);
            if (xmlElems.Count > 1)
            {
                XmlElement elemShapes = xmlElems[1];
                if (elemShapes != null)
                {
                    this.strategyDrawer = new StrategyDrawer();
                    this.strategyDrawer.Load(elemShapes);
                }
            }
            if (xmlElems.Count > 2)
            {
                XmlElement elemTrader = xmlElems[2];
                if (elemTrader != null)
                {
                    this.strategyTrader = new StrategyTrader_History();
                    this.StrategyTrader.Load(elemTrader);
                }
            }
        }

        private void LoadMain(XmlElement xmlElem)
        {
            XmlElement elemCodePeriod = (XmlElement)xmlElem.GetElementsByTagName("codeperiod")[0];
            codePeriod = new CodePeriod();
            codePeriod.Load(elemCodePeriod);

            XmlElement elemForwardPeriod = (XmlElement)xmlElem.GetElementsByTagName("forwardperiod")[0];
            forwardPeriod = new StrategyForwardPeriod();
            forwardPeriod.Load(elemForwardPeriod);

            XmlElement elemReferedPeriods = (XmlElement)xmlElem.GetElementsByTagName("referedperiods")[0];
            referedPeriods = new StrategyReferedPeriods();
            referedPeriods.Load(elemReferedPeriods);
        }

        public void Save(string path)
        {
            XmlElement[] elemArr = new XmlElement[3];
            elemArr[0] = GetRoot("strategyresult_code");
            if (this.strategyDrawer != null)
                elemArr[1] = GetRoot("strategyshape");
            if (this.strategyTrader != null)
                elemArr[2] = GetRoot("strategytrader");
            Save(elemArr);

            elemArr[0].OwnerDocument.Save(path);
            if (elemArr[1] != null)
                elemArr[1].OwnerDocument.Save(path + ".shape");
            if (elemArr[2] != null)
                elemArr[2].OwnerDocument.Save(path + ".trader");
        }

        private XmlElement GetRoot(string rootTag)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(rootTag);
            doc.AppendChild(root);
            return root;
        }

        public void Load(string path)
        {
            if (!File.Exists(path))
                return;
            XmlElement[] elemArr = new XmlElement[3];
            XmlDocument docResult = new XmlDocument();
            docResult.Load(path);
            elemArr[0] = docResult.DocumentElement;

            string shapePath = path + ".shape";
            if (File.Exists(shapePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(shapePath);
                elemArr[1] = doc.DocumentElement;
            }

            string traderPath = path + ".trader";
            if (File.Exists(traderPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(traderPath);
                elemArr[2] = doc.DocumentElement;
            }

            this.Load(elemArr);
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }
    }
}