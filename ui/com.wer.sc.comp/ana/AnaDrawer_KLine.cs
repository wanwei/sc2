using com.wer.sc.ana;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.ana
{
    public class AnaDrawer_KLine
    {
        private DataReaderFactory fac;
        private KLineModelRunner runner;
        private Control control;
        private GraphicDrawer_Candle drawer = new GraphicDrawer_Candle();
        private IGraphicDataProvider_Candle dataProvider;

        private IKLineData data;

        public AnaDrawer_KLine(DataReaderFactory fac, IGraphicDataProvider_Candle dataProvider)
        {
            this.fac = fac;
            this.runner = new KLineModelRunner(fac);
            this.dataProvider = dataProvider;
            this.drawer.DataProvider = dataProvider;
        }

        public void Bind(Control control)
        {
            this.control = control;
            this.drawer.BindControl(control);
        }

        public void UnBind()
        {
            this.drawer.UnBindControl();
            this.control = null;
        }

        public void Show(String code, int start, int end, KLinePeriod period)
        {
            this.data = fac.KLineDataReader.GetData(code, start, end, period);
            dataProvider.ChangeData(data);
            dataProvider.EndIndex = data.Length - 1;
        }

        public void Show(String code, int start, int end, KLinePeriod period, float time)
        {
            this.data = fac.KLineDataReader.GetData(code, start, end, period);
            dataProvider.ChangeData(data);
            //dataProvider.currentTime = time;
            //dataProvider.ch
        }

        public void Run(Plugin_KLineModel model)
        {
            Run(this.data, model);
        }

        public void Run(IKLineData data, Plugin_KLineModel model)
        {
            this.data = data;
            runner.Code = data.Code;
            runner.StartDate = (int)data.Arr_Time[0];
            runner.EndDate = (int)data.Arr_Time[data.Length - 1];
            runner.Period = data.Period;
            runner.Model = model;
            runner.Data = data;
            runner.RunAsync(new KLineModelRunHandler(handler));
        }

        public void Run(String code, int start, int end, KLinePeriod period, Plugin_KLineModel model)
        {
            runner.Code = code;
            runner.StartDate = start;
            runner.EndDate = end;
            runner.Period = period;
            runner.Model = model;
            runner.Data = null;
            runner.RunAsync(new KLineModelRunHandler(handler));
        }

        private void handler(KLineModelRunArgs args)
        {
            KLineModelRunner runner = args.Runner;
            this.data = runner.Data;
            dataProvider.ChangeData(data);
            dataProvider.EndIndex = data.Length - 1;

            Plugin_KLineModel model = runner.Model;

            drawer.drawer_chart.ClearPoints();
            drawer.drawer_chart.ClearPolyLine();
            drawer.drawer_chart.AddPolyLines(model.polyLines);
            drawer.drawer_chart.AddPolyLines(model.polyLineList);
            drawer.drawer_chart.AddPoints(model.points);
            drawer.drawer_chart.AddPoints(model.pointLists);
            drawer.DrawGraph();
        }
    }
}
