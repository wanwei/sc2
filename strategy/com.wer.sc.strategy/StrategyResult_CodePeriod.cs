using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.graphic.shape;
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

        private IPriceShapeContainer priceShapeContainer;

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

        public StrategyResult_CodePeriod(ICodePeriod codePeriod, StrategyForwardPeriod forwardPeriod, StrategyReferedPeriods referedPeriods, IPriceShapeContainer shapeContainer, IStrategyTrader strategyTrader)
        {
            this.codePeriod = codePeriod;
            this.forwardPeriod = forwardPeriod;
            this.referedPeriods = referedPeriods;
            this.priceShapeContainer = shapeContainer;
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
        public IPriceShapeContainer PriceShapes
        {
            get
            {
                return priceShapeContainer;
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

        public void Save(string file, XmlElement xmlElem)
        {
            XmlElement elemCodePeriod = xmlElem.OwnerDocument.CreateElement("codeperiod");
            xmlElem.AppendChild(elemCodePeriod);
            codePeriod.Save(elemCodePeriod);

            XmlElement elemForwardPeriod = xmlElem.OwnerDocument.CreateElement("forwardperiod");
            xmlElem.AppendChild(elemCodePeriod);
            forwardPeriod.Save(elemCodePeriod);

            XmlElement elemReferedPeriods = xmlElem.OwnerDocument.CreateElement("referedperiods");
            xmlElem.AppendChild(elemReferedPeriods);
            referedPeriods.Save(elemReferedPeriods);

            SaveShapes(file);
            SaveTrader(file);
        }

        private void SaveShapes(string file)
        {
            string shapeFile = file + ".shapes";
            
        }

        private void SaveTrader(string file)
        {

        }

        public void Load(string file, XmlElement xmlElem)
        {
        }
    }
}