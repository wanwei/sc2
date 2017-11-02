using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 前进周期
    /// </summary>
    public class ForwardPeriod : IXmlExchange
    {
        private bool isTickForward;

        private KLinePeriod klineForwardPeriod;

        public ForwardPeriod()
        {

        }

        public ForwardPeriod(bool isTickForward, KLinePeriod klineForwardPeriod)
        {
            this.isTickForward = isTickForward;
            this.klineForwardPeriod = klineForwardPeriod;
        }

        /// <summary>
        /// 是否是以tick为周期前进
        /// </summary>
        public bool IsTickForward
        {
            get
            {
                return isTickForward;
            }
        }

        /// <summary>
        /// 得到K线前进周期
        /// 如果是以K线为周期前进，则这是其前进周期
        /// 如果是以tick为前进周期，则OnBar的触发是该属性
        /// </summary>
        public KLinePeriod KlineForwardPeriod
        {
            get
            {
                return klineForwardPeriod;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is ForwardPeriod))
                return false;

            ForwardPeriod fp = (ForwardPeriod)obj;
            return this.isTickForward == fp.isTickForward && this.klineForwardPeriod.Equals(fp.klineForwardPeriod);
        }

        public override int GetHashCode()
        {
            return this.klineForwardPeriod.GetHashCode() * 10 + isTickForward.GetHashCode();
        }

        public void Load(XmlElement xmlElem)
        {
            this.klineForwardPeriod = new KLinePeriod();
            this.klineForwardPeriod.Load(xmlElem);
            this.isTickForward = bool.Parse(xmlElem.GetAttribute("isTickForward"));
        }

        public void Save(XmlElement xmlElem)
        {
            this.klineForwardPeriod.Save(xmlElem);
            xmlElem.SetAttribute("isTickForward", this.isTickForward.ToString());
        }
    }
}