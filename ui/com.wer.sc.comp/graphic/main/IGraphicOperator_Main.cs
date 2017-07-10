using com.wer.sc.data;
using com.wer.sc.plugin;
using System;

namespace com.wer.sc.comp.graphic.main
{
    /// <summary>
    /// SC的主图操作类
    /// </summary>
    public interface IGraphicOperator_Main
    {
        void ChangeData(String code, double time, KLinePeriod period);

        void ChangeData(IKLineData klineData, double time);

        void ChangeTime(double time);

        void ChangePeriod(KLinePeriod period);

        double CurrentTime { get; }

        void Refresh();

        //void RunModel(Plugin_KLineModel model);

        //void RunModel(Plugin_KLineModel model, IKLineData data);

        //void RunModel(Plugin_KLineModel model, String code, int start, int end, KLinePeriod period);
    }
}