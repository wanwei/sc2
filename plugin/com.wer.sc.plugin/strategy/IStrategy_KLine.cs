using com.wer.sc.ana;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// k线模型
    /// </summary>
    public abstract class IPlugin_KLineModel
    {
        private IKLineData data;

        private KLineDataMath dataMath;

        private KLineTrade trade;

        private String code;

        public IPlugin_KLineModel()
        {
        }

        public void init(String code, IKLineData data, KLineTradeFee fee, int defaultHand, float initMoney)
        {
            initTrader(data, fee, defaultHand, initMoney);
            init(code, data);
        }


        #region can be overide

        /// <summary>
        /// 初始化交易器
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fee"></param>
        /// <param name="defaultHand"></param>
        /// <param name="initMoney"></param>
        public virtual void initTrader(IKLineData data, KLineTradeFee fee, int defaultHand, float initMoney)
        {
            this.trade = new KLineTrade(data, fee, defaultHand, initMoney);
        }

        /**
         * 初始化模型
         * 该方法用于注入的和组合模型
         * @param code
         * @param data
         */
        public virtual void init(String code, IKLineData data)
        {
            /**
             * compoundmodel不需要支持交易
             */
            this.code = code;
            this.data = data;
            this.dataMath = new KLineDataMath(data);
            List<IPlugin_KLineModel> models = GetCompoundModels();
            if (models != null)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    models[i].init(code, data);
                }
            }
        }

        /// <summary>
        /// 该方法用于子类重载，在模型开始运行前会运行该方法
        /// </summary>
        public virtual void ModelStart()
        {

        }

        /// <summary>
        /// 该方法用于子类重载，在模型开始运行后会运行该方法
        /// </summary>
        public virtual void ModelEnd()
        {

        }

        /// <summary>
        /// 该方法用于子类重载，可以得到注入的模型
        /// 注入模型和组合模型(CompoundModels)不同之处在于
        /// 注入模型可以引用不同周期的数据，比如现在基于1分钟数据做的模型，也可以将基于15分钟线的模型拿来进行分析
        /// 组合模型仅仅是为了多模型复用，让一个模型使用其它模型里面的逻辑。
        /// </summary>
        /// <returns></returns>
        public virtual List<KLineModelImport> GetModelImports()
        {
            return null;
        }

        /// <summary>
        /// 该方法用于子类重载，主要用于多模型间复用
        /// </summary>
        /// <returns></returns>
        public virtual List<IPlugin_KLineModel> GetCompoundModels()
        {
            return null;
        }


        #endregion

        public void ModelLoop()
        {
            List<IPlugin_KLineModel> models = GetCompoundModels();
            if (models != null)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    models[i].ModelLoop();
                }
            }
            Loop();
        }

        public abstract void Loop();

        public int Length
        {
            get
            {
                return data.Length;
            }
        }

        #region 交易

        public bool AutoFilter
        {
            get
            {
                return trade.AutoFilter;
            }
            set
            {
                if (this.trade == null)
                    return;
                trade.AutoFilter = value;
            }
        }

        public void bk()
        {
            if (this.trade == null)
                return;
            trade.bk();
        }

        public void bk(int cnt)
        {
            if (this.trade == null)
                return;
            trade.bk(cnt);
        }

        public void bp()
        {
            if (this.trade == null)
                return;
            trade.bp();
        }

        public void bp(int cnt)
        {
            if (this.trade == null)
                return;
            trade.bp(cnt);
        }

        public void sk()
        {
            if (this.trade == null)
                return;
            trade.sk();
        }

        public void sk(int cnt)
        {
            if (this.trade == null)
                return;
            trade.sk(cnt);
        }

        public void sp()
        {
            if (this.trade == null)
                return;
            trade.sp();
        }

        public void sp(int cnt)
        {
            if (this.trade == null)
                return;
            trade.sp(cnt);
        }

        #endregion

        #region 数据属性

        /// <summary>
        /// 得到当前正在交易的股票或期货代码
        /// </summary>
        /// <returns></returns>
        public String Code
        {
            get
            {
                return code;
            }
        }

        /// <summary>
        /// 得到当前
        /// </summary>
        public int BarPos
        {
            get
            {
                return data.BarPos;
            }
        }

        public void NextBarPos()
        {
            this.data.BarPos++;
        }


        /// <summary>
        /// 得到当前对应的Chart
        /// </summary>
        /// <returns></returns>
        public IKLineBar Chart
        {
            get
            {
                return this.data.GetCurrentBar();
            }
        }

        public IKLineData KLineData
        {
            get { return this.data; }
        }

        public float Start
        {
            get
            {
                return Arr_Start[BarPos];
            }
        }

        public float High
        {
            get
            {
                return Arr_High[BarPos];
            }
        }

        public float Low
        {
            get
            {
                return Arr_Low[BarPos];
            }
        }

        public float End
        {
            get
            {
                return Arr_End[BarPos];
            }
        }

        public int Mount
        {
            get
            {
                return Arr_Mount[BarPos];
            }
        }

        public float Hold
        {
            get
            {
                return Arr_Hold[BarPos];
            }
        }

        public double FullTime
        {
            get
            {
                return Arr_Time[BarPos];
            }
        }

        public float Time
        {
            get
            {
                return (float)(Arr_Time[BarPos] - Date);
            }
        }

        public int Date
        {
            get
            {
                return (int)Arr_Time[BarPos];
            }
        }

        /// <summary>
        /// 得到当前k线block的高价位
        /// </summary>
        public float BlockHigh
        {
            get
            {
                return Arr_BlockHigh[BarPos];
            }
        }

        /// <summary>
        /// 得到当前k线block的低价位
        /// </summary>
        public float BlockLow
        {
            get
            {
                return Arr_BlockLow[BarPos];
            }
        }

        #region 完整数据

        public IList<double> Arr_Time { get { return data.Arr_Time; } }

        public IList<float> Arr_Start { get { return data.Arr_Start; } }

        public IList<float> Arr_High { get { return data.Arr_High; } }

        public IList<float> Arr_Low { get { return data.Arr_Low; } }

        public IList<float> Arr_End { get { return data.Arr_End; } }

        public IList<int> Arr_Mount { get { return data.Arr_Mount; } }

        public IList<int> Arr_Hold { get { return data.Arr_Hold; } }

        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        public IList<float> Arr_Height { get { return data.Arr_Height; } }

        /// <summary>
        /// 得到每个k线的振幅百分比数组
        /// </summary>
        public IList<float> Arr_HeightPercent { get { return data.Arr_HeightPercent; } }

        public IList<float> Arr_BlockHigh { get { return data.Arr_BlockHigh; } }

        public IList<float> Arr_BlockLow { get { return data.Arr_BlockLow; } }

        public IList<float> Arr_BlockHeight { get { return data.Arr_BlockHeight; } }

        public IList<float> Arr_BlockHeightPercent { get { return data.Arr_BlockHeightPercent; } }

        public IList<float> Arr_UpPercent { get { return data.Arr_UpPercent; } }

        #endregion

        #endregion

        #region 计算函数

        public float Height(int len)
        {
            return dataMath.Height(len);
        }

        public float Height(IList<float> values, int len)
        {
            return dataMath.Height(values, len);
        }

        public float Ma(IList<float> values, int len)
        {
            return dataMath.Ma(values, len);
        }

        public float Max(IList<float> values, int len)
        {
            return dataMath.Max(values, len);
        }

        public int MaxBars(IList<float> values, int len)
        {
            return dataMath.MaxBars(values, len);
        }

        public float Min(IList<float> values, int len)
        {
            return dataMath.Min(values, len);
        }

        public int MinBars(IList<float> values, int len)
        {
            return dataMath.MinBars(values, len);
        }

        public float Lowest(IList<float> values, int len)
        {
            return dataMath.Lowest(values, len);
        }

        public float Highest(IList<float> values, int len)
        {
            return dataMath.Highest(values, len);
        }

        public int LowestBars(IList<float> values, int len)
        {
            return dataMath.LowestBars(values, len);
        }

        public int HighestBars(IList<float> values, int len)
        {
            return dataMath.HighestBars(values, len);
        }

        public float AveragePrice(IList<float> values, int len)
        {
            return dataMath.AveragePrice(values, len);
        }

        /**
         * 数组1是否和数组2相交
         * @param values1
         * @param values2
         * @return 0未相交；1数组1向上穿过数组2；2数组1向下穿过数组2
         */
        public int Cross(IList<float> values1, IList<float> values2)
        {
            return dataMath.Cross(values1, values2);
        }
        public int Cross(float[] values1, float[] values2)
        {
            return dataMath.Cross(values1, values2);
        }
        #endregion

        #region 数据操作及获取

        public void AddData(float[] objs, float value)
        {
            objs[BarPos] = value;
        }

        public void AddData(List<float> objs, float value)
        {
            objs.Add(value);
        }

        public void AddData(List<int> objs, int value)
        {
            objs.Add(value);
        }

        public float RefData(float[] objs, int len)
        {
            return objs[BarPos - len];
        }

        public float RefData(IList<float> objs, int len)
        {
            return objs[objs.Count - 1 - len];
        }

        public int RefData(IList<int> objs, int len)
        {
            return objs[objs.Count - 1 - len];
        }

        public int RefData(int len)
        {
            return (int)Arr_Time[BarPos - len];
        }

        #endregion

        #region 画图

        public List<PolyLineArray> polyLines = new List<PolyLineArray>();

        public void AddPolyLine(PolyLineArray polyLine)
        {
            polyLines.Add(polyLine);
        }

        public List<PolyLineList> polyLineList = new List<PolyLineList>();

        public void AddPolyLine(PolyLineList polyLine)
        {
            polyLineList.Add(polyLine);
        }

        public void ClearPolyLine()
        {
            polyLines.Clear();
            polyLineList.Clear();
        }

        public List<PointArray> points = new List<PointArray>();

        public void AddPoint(PointArray polyLine)
        {
            points.Add(polyLine);
        }

        public List<PointList> pointLists = new List<PointList>();

        public void AddPoint(PointList polyLine)
        {
            pointLists.Add(polyLine);
        }

        public void ClearPoints()
        {
            points.Clear();
            pointLists.Clear();
        }

        #endregion
    }
}
