using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils.param;
using com.wer.sc.data.forward;
using com.wer.sc.data.market;
using System.Drawing;
using com.wer.sc.graphic;

namespace com.wer.sc.strategy
{
    public abstract class StrategyAbstract : IStrategy
    {
        private string name;

        public const string PARAMETER_PERIOD = "PARAMETER_PERIOD";

        private KLinePeriod defaultMainPeriod = KLinePeriod.KLinePeriod_1Minute;

        private IStrategyHelper strategyHelper;

        private IParameters parameters = ParameterFactory.CreateParameters();

        public StrategyAbstract()
        {
            //this.Parameters.AddParameter(PARAMETER_PERIOD, "计算周期", "计算周期", utils.param.ParameterType.OBJECT, KLinePeriod.KLinePeriod_1Minute);
        }

        private IKLineData mainKlineData;

        public virtual void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            if (mainKlineData == null && MainKLinePeriod != null)
            {
                mainKlineData = argument.CurrentData.GetKLineData(MainKLinePeriod);
            }
        }

        public virtual void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {

        }

        public abstract void OnBar(Object sender, IStrategyOnBarArgument currentData);

        public abstract void OnTick(Object sender, IStrategyOnTickArgument currentData);

        public virtual void OnDay(Object sender, IStrategyOnDayArgument argument)
        {

        }

        public IStrategyHelper StrategyHelper
        {
            get { return strategyHelper; }
            set { strategyHelper = value; }
        }

        public KLinePeriod MainKLinePeriod
        {
            get
            {
                return defaultMainPeriod;
            }

            set
            {
                defaultMainPeriod = value;
            }
        }

        public virtual IParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        public virtual string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        protected Dictionary<string, object> dic_Key_Data = new Dictionary<string, object>();

        protected void AddData(String key, object obj)
        {
            this.dic_Key_Data.Add(key, obj);
        }

        public Object GetData(string key)
        {
            if (dic_Key_Data.ContainsKey(key))
                return dic_Key_Data[key];
            return null;
        }

        public object GetParameter(string parameterName)
        {
            return this.parameters.GetParameterValue(parameterName);
        }

        //public virtual IStrategyQueryResult GetQueryResult()
        //{
        //    return null;
        //}

        public virtual StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        public List<string> GetStrategyReferedCodes(string code)
        {
            return null;
        }

        public virtual IList<IStrategy> GetReferedStrategies()
        {
            return null;
        }

        public void DrawAccount()
        {
            IStrategyTrader trader = this.StrategyHelper.Trader;
            if (trader == null || trader.Account == null)
                return;
            if (MainKLinePeriod == null || mainKlineData == null)
                return;
            IStrategyDrawer_PriceRect drawer = StrategyHelper.Drawer.GetDrawer_KLine(MainKLinePeriod);
            IList<TradeInfo> trades = trader.Account.CurrentTradeInfo;
            for (int i = 0; i < trades.Count; i++)
            {
                TradeInfo trade = trades[i];
                float price = (float)trade.Price;
                Color color = trade.Side == OrderSide.Sell ? Color.Green : Color.Red;
                // trade.Time;
                int barPos = FindMainBarPos(trade.Time);
                drawer.DrawPoint(new graphic.shape.PriceShape_Point(barPos, price, 8, color));
            }
        }

        private int FindMainBarPos(double time)
        {
            if (mainKlineData == null)
                return -1;
            for (int i = 1; i < mainKlineData.Arr_Time.Count; i++)
            {
                if (time > mainKlineData.Arr_Time[i - 1] && time <= mainKlineData.Arr_Time[i])
                    return i + 1;
            }
            return -1;
        }
    }
}