using com.wer.sc.comp.graphic.timeline;
using com.wer.sc.data;
using com.wer.sc.comp.graphic.info;
using System;
using com.wer.sc.data.navigate;

namespace com.wer.sc.comp.graphic.main
{
    public class GraphicDataProvider //: IGraphicDrawer_Chart
    {
        private GraphicType graphicType;

        //private DataReaderFactory dataReaderFac;
        //private IDataNavigate_Code dataNavigate;

        //private IGraphicDataProvider_Candle dataProvider_Candle;
        //private IGraphicDataProvider_TimeLine dataProvider_Real;
        //private IGraphicDataProvider_CurrentInfo dataProvider_CurrentInfo;
        //private IGraphicOperator_Main operator_Main;

        //public GraphicDataProvider_Main(DataReaderFactory dataReaderFac)
        //{
        //    this.dataReaderFac = dataReaderFac;
        //    this.dataNavigate = new DataNavigate(dataReaderFac);

        //    this.dataProvider_Candle = new GraphicDataProvider_CandleNav(dataReaderFac, dataNavigate);
        //    this.dataProvider_Real = new GraphicDataProvider_RealNav(dataReaderFac, dataNavigate);
        //    this.dataProvider_CurrentInfo = new GraphicDataProvider_CurrentInfo_Nav(dataNavigate);
        //    this.operator_Main = new GraphicOperator_Main(dataReaderFac, dataNavigate);
        //}

        //public IGraphicDataProvider_Candle DataProvider_Candle
        //{
        //    get { return dataProvider_Candle; }
        //}

        //public IGraphicDataProvider_TimeLine DataProvider_Real
        //{
        //    get { return dataProvider_Real; }
        //}

        //public IGraphicDataProvider_CurrentInfo DataProvider_Info
        //{
        //    get { return dataProvider_CurrentInfo; }
        //}

        //public IDataNavigate_Code DataNavigate
        //{
        //    get
        //    {
        //        return dataNavigate;
        //    }

        //    set
        //    {
        //        dataNavigate = value;
        //    }
        //}


        //public IGraphicDataProvider_Candle GetProvider_Candle()
        //{
        //    return dataProvider_Candle;
        //}

        //public IGraphicDataProvider_TimeLine GetProvider_Real()
        //{
        //    return dataProvider_Real;
        //}

        //public IGraphicDataProvider_CurrentInfo GetProvider_Info()
        //{
        //    return dataProvider_CurrentInfo;
        //}

        //public IGraphicOperator_Main GetOperator()
        //{
        //    return operator_Main;
        //}
        public void ShowGraphic_Candle(IKLineData klineData)
        {
            throw new NotImplementedException();
        }

        public void ShowGraphic_TimeLine(ITimeLineData timeLineData)
        {
            throw new NotImplementedException();
        }

        public GraphicType GetGraphicType()
        {
            return graphicType;            
        }

        public void ShowGraphic_Tick(ITickData tickData)
        {
            throw new NotImplementedException();
        }
    }
}
