using com.wer.sc.data.store.file;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class DataReaderFactory2
    {
        private String dataPath;

        private DataPathUtils pathUtils;
        private InstrumentReader codeReader;
        private ITradingDayReader openDateReader;
        private TradingSessionReader_CodeMgr openDateReaderMgr;
        private TradingSessionReader_Code openTimeReader;
        private HistoryDataReader_Tick tickDataReader;
        private KLineDataReader klineDataReader;
        private TimeLineDataReader realDataReader;

        public DataReaderFactory2(String dataPath)
        {
            this.dataPath = dataPath;
            this.pathUtils = new DataPathUtils(dataPath);
            //this.codeReader = new InstrumentReader(PathUtils.GetCodePath());
            this.openDateReader = new TradingDayReader(PathUtils.GetTradingDayPath());
            this.openTimeReader = new TradingSessionReader_Code(dataPath);
            this.tickDataReader = new HistoryDataReader_Tick(dataPath);
            this.klineDataReader = new KLineDataReader(dataPath);
            this.realDataReader = new TimeLineDataReader(this);
            //this.dataNavigate = new DataNavigate3(this);
            //this.cacheFactory = new DataCacheFactory(this);
            //this.dataNavigateMgr = new DataNavigateMgr(this);
            this.openDateReaderMgr = new TradingSessionReader_CodeMgr(this);
        }

        public IInstrumentReader CodeReader
        {
            get { return codeReader; }
        }

        public ITradingDayReader OpenDateReader
        {
            get { return openDateReader; }
        }

        public ITradingDayReader GetOpenDateReader(string code)
        {
            return openDateReaderMgr.GetOpenDateReader(code);
        }

        public ITradingSessionReader_Instrument OpenTimeReader
        {
            get
            {
                return openTimeReader;
            }
        }

        public IKLineDataReader KLineDataReader
        {
            get { return klineDataReader; }
        }

        public TimeLineDataReader TimeLineDataReader
        {
            get { return realDataReader; }
        }

        public HistoryDataReader_Tick TickDataReader
        {
            get { return tickDataReader; }
        }


        public DataPathUtils PathUtils
        {
            get { return pathUtils; }
        }

    }
}
