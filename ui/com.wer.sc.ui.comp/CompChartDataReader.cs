using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public class CompChartDataReader
    {
        private CompChartData compChartData;

        private IDataCenter dataCenter;

        private IDataPackage_Code dataPackage;

        private IDataNavigate_Code dataNavigate_Code;

        private IDataForward_Code historyDataForward_CodePlaying;

        public IDataPackage_Code DataPackage
        {
            get { return dataPackage; }
        }

        public IDataForward_Code GetHistoryDataForward_Playing()
        {
            if (historyDataForward_CodePlaying == null)
            {
                ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
                referedPeriods.UseTickData = true;
                referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
                referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);

                ForwardPeriod fp = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);

                this.historyDataForward_CodePlaying = dataCenter.HistoryDataForwardFactory.CreateDataForward_Code(this.dataPackage, referedPeriods, fp);

                //HistoryDataForwardFactory.CreateHistoryDataForward_Code(this.DataPackage, referedPeriods, fp);
                //HistoryDataForwardArguments args = new HistoryDataForwardArguments();
                //int date = dataReader.CreateTradingSessionReader(code).GetTradingDay(time);
                //args.StartDate = date;
                //args.EndDate = date;
                //args.IsTickForward = true;
                //args.ReferedPeriods = new strategy.StrategyReferedPeriods();
                //args.ReferedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
                //args.ReferedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);

                //this.historyDataForward_CodePlaying = HistoryDataForwardFactory.CreateHistoryDataForward_Code(dataReader, code, args);
                this.historyDataForward_CodePlaying.NavigateTo(compChartData.Time);
                this.historyDataForward_CodePlaying.OnTick += HistoryDataForward_Code_OnTick;
            }
            else
            {
                if (!isPlaying)
                    this.historyDataForward_CodePlaying.NavigateTo(compChartData.Time);
            }
            return historyDataForward_CodePlaying;
        }

        private void HistoryDataForward_Code_OnTick(object sender, IForwardOnTickArgument argument)
        {
            //if (argument.TickInfo.TickBar != null)
            //    this.time = argument.TickInfo.TickBar.Time;
        }
    }
}
