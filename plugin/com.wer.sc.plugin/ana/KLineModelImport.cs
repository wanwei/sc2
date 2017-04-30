using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    /// <summary>
    /// import模型
    /// 用于模型间跨周期互相引用
    /// </summary>
    public class KLineModelImport
    {
        /**
         * 要引用的模型
         */
        public IPlugin_KLineModel Model;

        /**
         * 所用的周期
         */
        public KLinePeriod KLinePeriod;

        /**
         * 是否
         */
        public bool extendPeriod = false;

        /**
         * 引用的其它合约，如果为空，表示引用本合约其它周期
         */
        public String Contract;

        public KLineModelImport()
        {
        }

        public KLineModelImport(IPlugin_KLineModel model, KLinePeriod period)
        {
            this.Model = model;
            this.KLinePeriod = period;
        }

        public KLineModelImport(IPlugin_KLineModel model, KLinePeriod period, bool extendPeriod)
        {
            this.Model = model;
            this.KLinePeriod = period;
            this.extendPeriod = extendPeriod;
        }

        public KLineModelImport(IPlugin_KLineModel model, String contract)
        {
            this.Model = model;
            this.Contract = contract;
        }
    }
}
