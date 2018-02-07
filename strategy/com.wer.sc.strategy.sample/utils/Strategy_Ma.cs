using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.param;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy.utils
{

    public class Strategy_Ma : StrategyAbstract
    {
        private int maPeriod;

        public const string PARAMKEY_MA = "MAPeriod";

        //均线
        private List<float> maList = new List<float>();

        private List<float> endList = new List<float>();

        /// <summary>
        /// 得到整体均线
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public List<float> MAList
        {
            get
            {
                return maList;
            }
        }

        public float MA
        {
            get { return maList[maList.Count - 1]; }
        }

        public float End
        {
            get { return endList[endList.Count - 1]; }
        }

        public Strategy_Ma()
        {
            this.Parameters.AddParameter(PARAMKEY_MA, "MA周期", "MA周期", ParameterType.INTEGER, 5);
        }

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
            this.maPeriod = (int)this.Parameters.GetParameter(PARAMKEY_MA).Value;
        }

        public override void OnEnd(object sender, IStrategyOnEndArgument argument)
        {

        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            IStrategyOnBarInfo barInfo = currentData.MainBar;
            GenMa(barInfo.KLineData, barInfo.KLineData.BarPos, maList, maPeriod);
        }

        private void GenMa(IKLineData klineData, int barPos, List<float> maList, int length)
        {
            int startPos = barPos - length + 1;
            startPos = startPos < 0 ? 0 : startPos;

            float total = 0;
            for (int i = startPos; i <= barPos; i++)
            {
                total += klineData.Arr_End[i];
            }
            maList.Add(total / (barPos - startPos + 1));
            endList.Add(klineData.Arr_End[barPos]);
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override IParameters Parameters
        {
            get
            {
                return base.Parameters;
            }
        }
    }
}
