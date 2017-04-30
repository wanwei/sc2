using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataUpdate
    {
        public const int PROGRESS_PERIOD = 10;

        private bool isCancel;

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
        private DataUpdate_Code update_Code;

        private DataUpdate_OpenDate update_OpenDate;

        private DataUpdate_KLine update_KLine;

        private DataUpdate_Tick update_Tick;

        private DataProviderWrap dataProviderWrap;

        private DataReaderFactory dataReaderFactory;

        public DataUpdate(DataProviderWrap dataProviderWrap)
        {
            this.dataProviderWrap = dataProviderWrap;
            Plugin_DataProvider dataProvider = dataProviderWrap.GetProvider();
            update_Code = new DataUpdate_Code(dataProvider);
            update_OpenDate = new DataUpdate_OpenDate(dataProvider);
            update_KLine = new DataUpdate_KLine(dataProvider);
            update_Tick = new DataUpdate_Tick(dataProvider);
            this.dataReaderFactory = new DataReaderFactory(dataProviderWrap.GetDataPath());
        }

        public void Update()
        {
            Thread thread1 = new Thread(new ThreadStart(GenerateInternal));
            thread1.Start();
        }

        private void GenerateInternal()
        {
            update_Code.Update();
            update_OpenDate.Update();

            UpdateInfo generates = DataPrepare();
            for (int i = 0; i < generates.generates.Count; i++)
            {
                UpdateInfo_Code generate_Code = generates.generates[i];
                UpdateTick(generate_Code.generate_Tick);
                UpdateKLine(generate_Code.generate_KLine);
            }
        }

        private void UpdateTick(UpdateInfo_Tick generate_Tick)
        {
            String code = generate_Tick.code;
            List<int> openDates = generate_Tick.dates;
            for (int i = 0; i < openDates.Count; i++)
            {
                if (IsCancel)
                    return;
                int date = openDates[i];
                update_Tick.DoUpdate(code, openDates[i]);

                int nextIndex = i + 1;
                if (nextIndex >= openDates.Count)
                {
                    return;
                }

                if ((nextIndex) % PROGRESS_PERIOD == 0)
                {
                    if (AfterGeneratedPeriod != null)
                    {
                        UpdatedPeriodArgs args = new UpdatedPeriodArgs();
                        args.nextStartDate = date;
                        int endIndex = i + PROGRESS_PERIOD;
                        endIndex = endIndex >= openDates.Count ? openDates.Count - 1 : endIndex;
                        args.nextEndDate = openDates[endIndex];
                        args.code = generate_Tick.code;
                        args.Description = "正在生成tick数据：" + args.nextStartDate + "-" + args.nextEndDate;
                        AfterGeneratedPeriod(args);
                    }
                }
            }
        }

        private void UpdateKLine(UpdateInfo_KLine generate_KLine)
        {
            String code = generate_KLine.code;
            //KLinePeriod period = new KLinePeriod(KLineTimeType.SECOND, 5);
            //DoAfterGenerateKLinePeriod(generate_KLine.dates_5second, period);
            //KLineData data = UpdateKLineByTick(code, generate_KLine.dates_5second, period);

            KLinePeriod period = null;
            IKLineData data = null;
            if (generate_KLine.dates_1min.Count != 0)
            {
                period = new KLinePeriod(KLineTimeType.MINUTE, 1);
                DoAfterGenerateKLinePeriod(generate_KLine.dates_1min, period);
                data = UpdateKLineByTick(code, generate_KLine.dates_1min, period);
            }

            if (generate_KLine.dates_15min.Count != 0)
            {
                period = new KLinePeriod(KLineTimeType.MINUTE, 15);
                DoAfterGenerateKLinePeriod(generate_KLine.dates_15min, period);
                if (IsOpenDateEquals(generate_KLine.dates_1min, generate_KLine.dates_15min))
                    UpdateKLineByKLine(code, period, data);
                else
                {
                    List<int> days = generate_KLine.dates_15min;
                    IKLineData dataFor15min = dataReaderFactory.KLineDataReader.GetData(code, days[0], days[days.Count - 1], new KLinePeriod(KLineTimeType.MINUTE, 1));
                    UpdateKLineByKLine(code, period, dataFor15min);
                }
            }

            if (generate_KLine.dates_1hour.Count != 0)
            {
                period = new KLinePeriod(KLineTimeType.HOUR, 1);
                DoAfterGenerateKLinePeriod(generate_KLine.dates_1hour, period);
                if (IsOpenDateEquals(generate_KLine.dates_1min, generate_KLine.dates_1hour))
                    UpdateKLineByKLine(code, period, data);
                else
                {
                    List<int> days = generate_KLine.dates_1hour;
                    IKLineData dataFor1Hour = dataReaderFactory.KLineDataReader.GetData(code, days[0], days[days.Count - 1], new KLinePeriod(KLineTimeType.MINUTE, 1));
                    UpdateKLineByKLine(code, period, dataFor1Hour);
                }
            }

            if (generate_KLine.dates_Day.Count != 0)
            {
                period = new KLinePeriod(KLineTimeType.DAY, 1);
                DoAfterGenerateKLinePeriod(generate_KLine.dates_Day, period);
                if (IsOpenDateEquals(generate_KLine.dates_1min, generate_KLine.dates_Day))
                    UpdateKLineByKLine(code, period, data);
                else
                {
                    List<int> days = generate_KLine.dates_Day;
                    IKLineData dataForDay = dataReaderFactory.KLineDataReader.GetData(code, days[0], days[days.Count - 1], new KLinePeriod(KLineTimeType.MINUTE, 1));
                    UpdateKLineByKLine(code, period, dataForDay);
                }
            }
        }

        private void DoAfterGenerateKLinePeriod(List<int> opendates, KLinePeriod period)
        {
            if (AfterGeneratedPeriod != null && opendates.Count != 0)
            {
                UpdatedPeriodArgs args = new UpdatedPeriodArgs();
                args.Description = "正在生成" + period + "kline数据：" + opendates[0] + "-" + opendates[opendates.Count - 1];
                AfterGeneratedPeriod(args);
            }
        }

        private bool IsOpenDateEquals(List<int> opendates1, List<int> opendates2)
        {
            if (opendates1.Count != opendates2.Count)
                return false;
            if (opendates1[0] != opendates2[0])
                return false;
            return true;
        }

        private IKLineData UpdateKLineByTick(String code, List<int> dates, KLinePeriod period)
        {
            return update_KLine.UpdateByTick(code, dataReaderFactory, period, dates);
        }

        private IKLineData UpdateKLineByKLine(String code, KLinePeriod period, IKLineData data)
        {
            return update_KLine.UpdateByKLine(code, dataReaderFactory, period, data);
        }

        public UpdateInfo DataPrepare()
        {
            UpdateInfo generate = GetUpdateInfo();

            if (AfterPrepared != null)
                AfterPrepared(generate);
            return generate;
        }

        private UpdateInfo GetUpdateInfo()
        {
            List<InstrumentInfo> codes = dataProviderWrap.GetCurrentCodes();

            UpdateInfo generateInfo = new UpdateInfo();
            for (int i = 0; i < codes.Count; i++)
            {
                generateInfo.generates.Add(GetUpdateInfo_Code(codes[i].Code));
            }
            return generateInfo;
        }

        public UpdateInfo_Code GetUpdateInfo_Code(String code)
        {
            UpdateInfo_Code generate_Code = new UpdateInfo_Code();
            generate_Code.generate_Tick = GetGenerateTick(code);
            generate_Code.generate_KLine = GetGenerateKLine(code);
            return generate_Code;
        }

        private UpdateInfo_Tick GetGenerateTick(String code)
        {
            UpdateInfo_Tick generate_Tick = new UpdateInfo_Tick();
            generate_Tick.code = code;
            generate_Tick.dates = update_Tick.GetUpdateDates(code, dataReaderFactory);
            return generate_Tick;
        }

        private UpdateInfo_KLine GetGenerateKLine(String code)
        {
            UpdateInfo_KLine generate_KLine = new UpdateInfo_KLine();
            generate_KLine.code = code;
            generate_KLine.dates_5second = update_KLine.GetUpdateDates(code, dataReaderFactory, new KLinePeriod(KLineTimeType.SECOND, 5));
            generate_KLine.dates_1min = update_KLine.GetUpdateDates(code, dataReaderFactory, new KLinePeriod(KLineTimeType.MINUTE, 1));
            generate_KLine.dates_15min = update_KLine.GetUpdateDates(code, dataReaderFactory, new KLinePeriod(KLineTimeType.MINUTE, 15));
            generate_KLine.dates_1hour = update_KLine.GetUpdateDates(code, dataReaderFactory, new KLinePeriod(KLineTimeType.HOUR, 1));
            generate_KLine.dates_Day = update_KLine.GetUpdateDates(code, dataReaderFactory, new KLinePeriod(KLineTimeType.DAY, 1));
            return generate_KLine;
        }

        public AfterPreparedHandler AfterPrepared;

        public AfterGeneratedPeriodHandler AfterGeneratedPeriod;

        public AfterGeneratedHandler AfterGenerated;

    }

    public delegate void AfterPreparedHandler(UpdateInfo generateInfo);
    public delegate void AfterGeneratedPeriodHandler(UpdatedPeriodArgs args);
    public delegate void AfterGeneratedHandler(UpdatedArgs args);

    public class UpdateInfo
    {
        public List<UpdateInfo_Code> generates = new List<UpdateInfo_Code>();

        public int GetPeriodCount()
        {
            int max = 0;
            for (int i = 0; i < generates.Count; i++)
            {
                max += generates[i].GetCalcPeriodCount();
            }
            return max;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < generates.Count; i++)
            {
                if (i != 0)
                    sb.Append("\r\n");
                sb.Append(generates[i]);
            }
            return sb.ToString();
        }

        public static void ListToString(StringBuilder sb, string prefix, List<int> dates)
        {
            sb.Append(prefix).Append(":");
            for (int i = 0; i < dates.Count; i++)
            {
                if (i != 0)
                    sb.Append(',');
                sb.Append(dates[i]);
            }
        }

        public static void ListToString(StringBuilder sb, string prefix, List<int> dates, string suffix)
        {
            sb.Append(prefix).Append(":");
            for (int i = 0; i < dates.Count; i++)
            {
                if (i != 0)
                    sb.Append(',');
                sb.Append(dates[i]);
            }
            sb.Append(suffix);
        }

        public static String ListToString(string prefix, List<int> dates)
        {
            StringBuilder sb = new StringBuilder();
            ListToString(sb, prefix, dates);
            return sb.ToString();
        }
    }

    public class UpdateInfo_Code
    {
        public UpdateInfo_Tick generate_Tick;

        public UpdateInfo_KLine generate_KLine;

        public int GetCalcPeriodCount()
        {
            return generate_Tick.GetCalcPeriodCount() + generate_KLine.GetCalcPeriodCount();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(generate_Tick).Append("\r\n");
            sb.Append(generate_KLine);
            return sb.ToString();
        }
    }

    public class UpdateInfo_Tick
    {
        public String code;

        public List<int> dates;

        public int GetCalcPeriodCount()
        {
            int progress = DataUpdate.PROGRESS_PERIOD;
            if (dates.Count % progress == 0)
                return (dates.Count / progress);
            return (dates.Count / progress) + 1;
        }

        public override string ToString()
        {
            return UpdateInfo.ListToString(code + "_tick", dates);
        }
    }

    public class UpdateInfo_KLine
    {
        public String code;

        public List<int> dates_5second;

        public List<int> dates_1min;

        public List<int> dates_15min;

        public List<int> dates_1hour;

        public List<int> dates_Day;

        public int GetCalcPeriodCount()
        {
            int progress = DataUpdate.PROGRESS_PERIOD;
            int p1 = GetCalcPeriodCount(dates_5second.Count, progress);
            p1 += GetCalcPeriodCount(dates_1min.Count, progress * 5);
            p1 += GetCalcPeriodCount(dates_15min.Count, progress * 60);
            return p1;
        }

        private int GetCalcPeriodCount(int cnt, int progress)
        {
            if (cnt % progress == 0)
                return (cnt / progress);
            return (cnt / progress) + 1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            UpdateInfo.ListToString(sb, code + "_kline_5second", dates_5second, "\r\n");
            UpdateInfo.ListToString(sb, code + "_kline_1min", dates_1min, "\r\n");
            UpdateInfo.ListToString(sb, code + "_kline_15min", dates_15min, "\r\n");
            UpdateInfo.ListToString(sb, code + "_kline_1hour", dates_1hour, "\r\n");
            UpdateInfo.ListToString(sb, code + "_kline_day", dates_Day);
            return sb.ToString();
        }
    }

    public class UpdatedArgs
    {
    }

    public class UpdatedPeriodArgs
    {
        public String code;

        public int generatedStartDate;

        public int generatedEndDate;

        public int nextStartDate;

        public int nextEndDate;

        public String Description;
    }
}