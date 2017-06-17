using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana.test.model
{
    /// <summary>
    /// 组合策略，可以将各种策略组合起来形成一个新策略
    /// </summary>
    public class KLineModel_Compound : IPlugin_KLineModel
    {

        private KLineModel_Simple model_Simple;

        private KLineModel_Simple2 model_Simple2;

        private List<IPlugin_KLineModel> models = new List<IPlugin_KLineModel>();

        public KLineModel_Compound()
        {
            model_Simple = new KLineModel_Simple();
            model_Simple2 = new KLineModel_Simple2();
            models.Add(model_Simple);
            models.Add(model_Simple2);
        }

        public override void ModelEnd()
        {
            Debug.WriteLine("earn:" + earn);
        }

        public override List<IPlugin_KLineModel> GetCompoundModels()
        {
            return models;
        }

        //持仓类型，0不持仓，1多仓，-1空仓
        private int holdType = 0;

        private float holdPrice;

        public float earn = 0;     

        public override void Loop()
        {

            /**
             * 买入开仓：40日线上扬+5日线上穿10日线开仓+当前高于上一个底部
             * 平仓：5日线反穿10日线
             * 
             * 卖出开仓：40日线下跌+5日线下穿10日线+当前低于上一个顶部
             * 
             * 过滤条件：
             * 成交量大于5万
             * 
             */
            float gd = RefData(model_Simple.Arr_RealGD, 0);
            float dd = RefData(model_Simple.Arr_RealDD, 0);
            int crossType = Cross(model_Simple2.ma5, model_Simple2.ma10);
            bool isMa40Up = RefData(model_Simple2.ma40, 0) > RefData(model_Simple2.ma40, 1);

            if (holdType == 1)
            {
                if (crossType < 0)
                {
                    float ff = End - holdPrice;
                    Console.WriteLine(Date + ":bp-" + End + "|earn:" + ff);
                    earn += ff;
                    holdType = 0;
                }
                return;
            }
            else if (holdType == -1)
            {
                if (crossType > 0)
                {
                    float ff = holdPrice - End;
                    Console.WriteLine(Date + ":sp-" + End + "|earn:" + ff);
                    earn += ff;
                    holdType = 0;
                }
                return;
            }

            bool conBk = isMa40Up && crossType > 0 && End > dd;
            if (conBk)
            {
                Console.WriteLine(Date + ":bk-" + End);
                holdPrice = End;
                holdType = 1;
                return;
            }

            bool conSk = !isMa40Up && crossType < 0 && End < gd;
            if (conSk)
            {
                Console.WriteLine(Date + ":sk-" + End);
                holdPrice = End;
                holdType = -1;
                return;
            }
        }
    }
}