using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana.test.model
{
    public class KLineModel_ImportContract : IPlugin_KLineModel
    {

        //日模型
        private KLineModel_Simple2 model_M01;

        public KLineModel_ImportContract()
        {
            model_M01 = new KLineModel_Simple2();
        }

        public override List<KLineModelImport> GetModelImports()
        {
            List<KLineModelImport> models = new List<KLineModelImport>();
            models.Add(new KLineModelImport(model_M01, "m01"));
            return models;
        }


        public override void ModelEnd()
        {
            //System.out.println("earn:" + earn);
        }

        //持仓类型，0不持仓，1多仓，-1空仓
        private int holdType = 0;

        private float holdPrice;

        private float earn = 0;

        private int openDate;

        public override void Loop()
        {
            //如果今天开过仓，当日不平仓
            int date = Date;
            if (date == openDate)
                return;

            /**
             * 该算法和KLineModel_Compound里面基本一样，但此处用的是import方式导入
             * 
             * 买入开仓：40日线上扬+5日线上穿10日线开仓+当前高于上一个底部
             * 平仓：5日线反穿10日线
             * 
             * 卖出开仓：40日线下跌+5日线下穿10日线+当前低于上一个顶部
             * 
             * 过滤条件：
             * 成交量大于5万
             * 
             */
            //int crossType = model_Day.cross(model_Day.ma5, model_Day.ma10);
            int crossType = model_M01.Cross(model_M01.ma5, model_M01.ma10);
            bool isMa40Up = model_M01.RefData(model_M01.ma40, 0) > model_M01.RefData(model_M01.ma40, 1);
            //boolean isMa40Up = model_15Minute.ref(model_15Minute.ma40, 0) > model_Day.ref(model_15Minute.ma40, 1);

            if (holdType == 1)
            {
                if (crossType < 0)
                {
                    float ff = End - holdPrice;
                    Debug.WriteLine(Date + ":bp-" + End + "|earn:" + ff);
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
                    Debug.WriteLine(Date + ":sp-" + End + "|earn:" + ff);
                    earn += ff;
                    holdType = 0;
                }
                return;
            }

            Boolean conBk = isMa40Up && crossType > 0;
            if (conBk)
            {
                Debug.WriteLine(Date + ":bk-" + End);
                holdPrice = End;
                holdType = 1;
                openDate = date;
                return;
            }

            Boolean conSk = !isMa40Up && crossType < 0;
            if (conSk)
            {
                Debug.WriteLine(Date + ":sk-" + End);
                holdPrice = End;
                holdType = -1;
                openDate = date;
                return;
            }
        }
    }
}
