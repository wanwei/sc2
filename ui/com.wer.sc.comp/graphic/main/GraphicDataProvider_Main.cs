using com.wer.sc.comp.graphic.real;
using com.wer.sc.data;
using com.wer.sc.comp.graphic.info;
using System;
using com.wer.sc.data.navigate;

namespace com.wer.sc.comp.graphic.main
{
    public class GraphicDataProvider_Main : IGraphicDataProvider_Main
    {
        private DataReaderFactory dataReaderFac;
        private IDataNavigate3 dataNavigate;

        private IGraphicDataProvider_Candle dataProvider_Candle;
        private IGraphicDataProvider_Real dataProvider_Real;
        private IGraphicDataProvider_CurrentInfo dataProvider_CurrentInfo;
        private IGraphicOperator_Main operator_Main;

        public GraphicDataProvider_Main(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
            this.dataNavigate = new DataNavigate3(dataReaderFac);

            this.dataProvider_Candle = new GraphicDataProvider_CandleNav(dataReaderFac, dataNavigate);
            this.dataProvider_Real = new GraphicDataProvider_RealNav(dataReaderFac, dataNavigate);
            this.dataProvider_CurrentInfo = new GraphicDataProvider_CurrentInfo_Nav(dataNavigate);
            this.operator_Main = new GraphicOperator_Main(dataReaderFac, dataNavigate);
        }

        public IGraphicDataProvider_Candle DataProvider_Candle
        {
            get { return dataProvider_Candle; }
        }

        public IGraphicDataProvider_Real DataProvider_Real
        {
            get { return dataProvider_Real; }
        }

        public IGraphicDataProvider_CurrentInfo DataProvider_Info
        {
            get { return dataProvider_CurrentInfo; }
        }

        public IDataNavigate3 DataNavigate
        {
            get
            {
                return dataNavigate;
            }

            set
            {
                dataNavigate = value;
            }
        }


        public IGraphicDataProvider_Candle GetProvider_Candle()
        {
            return dataProvider_Candle;
        }

        public IGraphicDataProvider_Real GetProvider_Real()
        {
            return dataProvider_Real;
        }

        public IGraphicDataProvider_CurrentInfo GetProvider_Info()
        {
            return dataProvider_CurrentInfo;
        }

        public IGraphicOperator_Main GetOperator()
        {
            return operator_Main;
        }
    }
}
