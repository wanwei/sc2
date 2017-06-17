using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    /// <summary>
    /// k线模型的运行器
    /// </summary>
    public class KLineModelRunner
    {
        private IKLineDataReader reader;

        //code
        private String code;

        //数据开始时间
        private int modelStartDate = -1;

        //开始日期
        private int startDate;

        //结束日期
        private int endDate;

        private KLinePeriod period;

        private int defaultHand;

        //测试所用的金额
        private float initMoney;

        private KLineTradeFee fee;

        private IPlugin_KLineModel model;

        public void SetData(String code, int startDate, int endDate, KLinePeriod period)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.period = period;
            this.data = null;
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public int StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }
        public int EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }

            set
            {
                period = value;
            }
        }

        public int ModelStartDate
        {
            get
            {
                return modelStartDate;
            }

            set
            {
                modelStartDate = value;
            }
        }

        public int DefaultHand
        {
            get
            {
                return defaultHand;
            }

            set
            {
                defaultHand = value;
            }
        }

        public float InitMoney
        {
            get
            {
                return initMoney;
            }

            set
            {
                initMoney = value;
            }
        }

        public IPlugin_KLineModel Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        private IKLineData data;

        public IKLineData Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public KLineModelRunner(String datapath)
        {
            this.reader = new KLineDataReader(datapath);
        }

        public KLineModelRunner(DataReaderFactory2 fac)
        {
            this.reader = fac.KLineDataReader;
        }

        private Object lockRunObject = new object();

        public void run()
        {
            lock (lockRunObject)
            {                
                if (Model.GetModelImports() == null)
                {
                    executeNoImport();
                }
                else
                {
                    executeImport();
                }
                DealModelDraw(model);
            }
        }

        private KLineModelRunHandler handler;

        public void RunAsync(KLineModelRunHandler callback)
        {
            this.handler = callback;
            Thread t = new Thread(new ThreadStart(RunInternal));
            t.Start();
        }

        private void RunInternal()
        {
            this.run();
            if (this.handler != null)
                this.handler(new KLineModelRunArgs(this));
        }

        private void executeNoImport()
        {
            int realModelStartDate = modelStartDate < 0 ? startDate : modelStartDate;
            if (data == null)
                data = this.reader.GetData(Code, StartDate, EndDate, Period);
            Model.init(Code, data, fee, DefaultHand, InitMoney);
            Model.ModelStart();
            while (true)
            {
                try
                {
                    int date = Model.Date;
                    if (date >= realModelStartDate)
                        Model.ModelLoop();
                }
                catch (Exception e)
                {
                    //e.StackTrace();
                }
                if (Model.BarPos == data.Length - 1)
                    break;
                else
                    Model.NextBarPos();
            }
            Model.ModelEnd();            
        }

        private void DealModelDraw(IPlugin_KLineModel model)
        {
            model.ClearPoints();
            model.ClearPolyLine();
            Type type = Model.GetType();
            var fields = type.GetFields();

            foreach (var field in fields)
            {
                bool isDefinedLines = field.IsDefined(typeof(ModelLinesAttribute), false);
                bool isDefinedPoints = field.IsDefined(typeof(ModelPointsAttribute), false);
                if (!isDefinedLines && !isDefinedPoints)
                    continue;

                var attributes = field.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    //获取属性的值
                    var fieldValue = field.GetValue(Model);
                    if (fieldValue == null)
                        continue;

                    Color color = (Color)attribute.GetType().GetProperty("Color").GetValue(attribute);
                    int width = (int)attribute.GetType().GetProperty("Width").GetValue(attribute);

                    if (fieldValue is List<PricePoint>)
                    {
                        if (isDefinedPoints)
                            model.AddPoint(new PointList((List<PricePoint>)fieldValue, color, width));
                        if (isDefinedLines)
                            model.AddPolyLine(new PolyLineList((List<PricePoint>)fieldValue, color, width));
                    }
                    else if (fieldValue is Array)
                    {
                        if (isDefinedPoints)
                            model.AddPoint(new PointArray((float[])fieldValue, color, width));
                        if (isDefinedLines)
                            model.AddPolyLine(new PolyLineArray((float[])fieldValue, color, width));
                    }
                }
            }
        }

        private void executeImport()
        {
            int realModelStartDate = modelStartDate < 0 ? startDate : modelStartDate;
            if (data == null)
                data = this.reader.GetData(Code, StartDate, EndDate, Period);

            //准备数据
            List<KLineModelImportWarp> importModels = importDataPrepare();
            Model.init(Code, data, fee, DefaultHand, InitMoney);
            Model.ModelStart();
            while (true)
            {
                int date = Model.Date;
                bool realStart = date >= realModelStartDate;

                //首先将import进来的模型loop一遍			
                loopImportWarps(importModels, Model.BarPos, realStart);
                try
                {
                    if (realStart)
                        Model.ModelLoop();
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                }
                if (Model.BarPos == data.Length - 1)
                    break;
                else
                    Model.NextBarPos();
            }
            Model.ModelEnd();
        }

        private void loopImportWarps(List<KLineModelImportWarp> importModels, int mainBarPos, bool realStart)
        {
            for (int i = 0; i < importModels.Count; i++)
            {
                loopImportWarp(importModels[i], mainBarPos, realStart);
            }
        }

        private void loopImportWarp(KLineModelImportWarp importModelWarp, int mainBarPos, bool realStart)
        {
            /**
             * 修改
             * 
             */
            IPlugin_KLineModel importModel = importModelWarp.model.Model;
            //int barPos = importModel.getBarPos();
            //确定是否要跳转到下一个bar
            if (isNextPos(importModel))
            {
                //barPos++;
                importModel.NextBarPos();
                if (realStart)
                {
                    int barPos = importModel.BarPos;
                    importModel.Arr_Start[barPos] = Model.Start;
                    importModel.Arr_High[barPos] = Model.High;
                    importModel.Arr_Low[barPos] = Model.Low;
                    importModel.Arr_End[barPos] = Model.End;
                    importModel.Arr_Mount[barPos] = Model.Mount;
                }
                //importModel.arr_hold[barPos] = model.hold();
                //importModel.arr_time[barPos] = model.fullTime();			
            }
            else
            {
                if (realStart)
                {
                    int barPos = importModel.BarPos;
                    if (Model.High > importModel.High)
                        importModel.Arr_High[barPos] = Model.High;
                    if (Model.Low < importModel.Low)
                        importModel.Arr_Low[barPos] = Model.Low;
                    importModel.Arr_End[barPos] = Model.End;
                    importModel.Arr_Mount[barPos] += Model.Mount;
                    //importModel.arr_hold[barPos] = model.hold();
                }
            }
            if (realStart)
                importModel.ModelLoop();
        }

        private bool isNextPos(IPlugin_KLineModel importModel)
        {
            int nextBarPos = importModel.BarPos + 1;
            if (nextBarPos >= importModel.Length)
                return false;
            double importTime = importModel.Arr_Time[nextBarPos];
            //String importTime = importModel.fullTime();
            double time = Model.FullTime;
            if ((time > 0 && importTime > 0) || (time < 0 && importTime < 0))
            {
                return time >= importTime;
            }
            else
            {
                return Model.Date > importModel.Date;
            }
        }

        private List<KLineModelImportWarp> importDataPrepare()
        {
            List<KLineModelImportWarp> modelWarps = new List<KLineModelImportWarp>();
            List<KLineModelImport> importModels = Model.GetModelImports();
            for (int i = 0; i < importModels.Count; i++)
            {
                KLineModelImport klineModel = importModels[i];
                String importModelCode = StringUtils.IsEmpty(klineModel.Contract) ? Code : klineModel.Contract;
                int realStart = ModelStartDate < 0 ? StartDate : ModelStartDate;
                IKLineData data = this.reader.GetData(importModelCode, realStart, EndDate, klineModel.KLinePeriod);
                klineModel.Model.init(importModelCode, data);
                //klineModel.Model.setBarPos(-1);
                modelWarps.Add(new KLineModelImportWarp(klineModel, data));
            }
            return modelWarps;
        }
    }

    public delegate void KLineModelRunHandler(KLineModelRunArgs args);

    public class KLineModelRunArgs
    {
        private KLineModelRunner runner;

        private bool isCancel;

        public KLineModelRunArgs(KLineModelRunner runner)
        {
            this.runner = runner;
        }

        public KLineModelRunner Runner
        {
            get
            {
                return runner;
            }
        }

        public bool IsCancel
        {
            get
            {
                return isCancel;
            }

            set
            {
                isCancel = value;
            }
        }
    }

    internal class KLineModelImportWarp
    {
        public KLineModelImport model;

        public IKLineData data;

        public KLineModelImportWarp(KLineModelImport model, IKLineData data)
        {
            this.model = model;
            this.data = data;
        }
    }
}