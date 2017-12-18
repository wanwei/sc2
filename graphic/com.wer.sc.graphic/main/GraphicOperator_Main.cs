using com.wer.sc.ana;
using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.main
{
    /// <summary>
    /// 主图的操作类
    /// </summary>
    public class GraphicOperator_Main //: IGraphicOperator_Main
    {
        private double currentTime;

        private IKLineData klineData;

        private GraphicDataProvider dataProvider;

        //private IDataNavigate dataNavigate;

        //private KLineModelRunner runner;

        //public GraphicOperator_Main(DataReaderFactory dataReaderFac, IDataNavigate navigate)
        //{
        //    this.dataNavigate = navigate;
        //}

        //public double CurrentTime
        //{
        //    get
        //    {
        //        return currentTime;
        //    }
        //}

        //public void ChangeData(IKLineData klineData, double time)
        //{
        //    this.klineData = klineData;
        //    dataNavigate.Change(klineData, time);
        //    this.currentTime = time;
        //}

        //public void ChangeData(string code, double time, KLinePeriod period)
        //{
        //    dataNavigate.Change(code, time, period);
        //    this.currentTime = time;
        //}

        //public void ChangePeriod(KLinePeriod period)
        //{
        //    dataNavigate.ChangePeriod(period);
        //}

        //public void ChangeTime(double time)
        //{
        //    dataNavigate.ChangeTime(time);
        //    this.currentTime = time;
        //}

        //public void Refresh()
        //{
        //    dataNavigate.ChangeTime(currentTime);
        //}

        //public void RunModel(Plugin_KLineModel model)
        //{

        //}

        //public void RunModel(Plugin_KLineModel model, IKLineData data)
        //{
        //    this.klineData = data;
        //    runner.Code = data.Code;
        //    runner.StartDate = (int)data.Arr_Time[0];
        //    runner.EndDate = (int)data.Arr_Time[data.Length - 1];
        //    runner.Period = data.Period;
        //    runner.Model = model;
        //    runner.Data = data;
        //    if (ModelRunHandler != null)
        //        runner.RunAsync(ModelRunHandler);
        //    else
        //        runner.run();
        //}

        //public KLineModelRunHandler ModelRunHandler;

        //public void RunModel(Plugin_KLineModel model, string code, int start, int end, KLinePeriod period)
        //{
            
        //}

        //private void ModelRunedHandler(KLineModelRunArgs args)
        //{
        //    KLineModelRunner runner = args.Runner;
        //    //dataProvider.ChangeData(data);
        //    //dataProvider.EndIndex = data.Length - 1;

        //    Plugin_KLineModel model = runner.Model;

        //    //drawer.drawer_chart.ClearPoints();
        //    //drawer.drawer_chart.ClearPolyLine();
        //    //drawer.drawer_chart.AddPolyLines(model.polyLines);
        //    //drawer.drawer_chart.AddPolyLines(model.polyLineList);
        //    //drawer.drawer_chart.AddPoints(model.points);
        //    //drawer.drawer_chart.AddPoints(model.pointLists);
        //    //drawer.DrawGraph();
        //}
    }
}
