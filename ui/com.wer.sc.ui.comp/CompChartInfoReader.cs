using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public class CompChartInfoReader
    {
        private IDataCenter dataCenter;

        private CompChartInfo compChartInfo;

        //数据包
        private IDataPackage_Code currentDataPackage;

        //数据导航
        private IDataNavigate_Code dataNavigate_Code;

        //数据前进
        private IDataForward_Code historyDataForward_CodePlaying;

        public CompChartInfoReader(IDataCenter dataCenter, CompChartInfo compChartInfo)
        {
            this.dataCenter = dataCenter;
            this.compChartInfo = compChartInfo;
        }

        public IDataPackage_Code DataPackage_Code
        {
            get
            {
                return currentDataPackage;
            }
        }

        public IRealTimeDataReader_Code CurrentRealTimeDataReader
        {
            get
            {
                if (!compChartInfo.CheckData())
                    return null;
                if (dataNavigate_Code == null)
                    dataNavigate_Code = this.dataCenter.DataNavigateFactory.CreateDataNavigate_Code(compChartInfo.Code, compChartInfo.Time);
                else
                {
                    if (dataNavigate_Code.Code != compChartInfo.Code)
                        dataNavigate_Code = this.dataCenter.DataNavigateFactory.CreateDataNavigate_Code(compChartInfo.Code, compChartInfo.Time);
                }
                dataNavigate_Code.NavigateTo(compChartInfo.Time);
                this.currentDataPackage = dataNavigate_Code.DataPackage;
                return dataNavigate_Code;
            }
        }

        private IDataForward_Code GetHistoryDataForward_Playing()
        {
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UseTickData = true;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);

            ForwardPeriod fp = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);

            this.historyDataForward_CodePlaying = dataCenter.HistoryDataForwardFactory.CreateDataForward_Code(this.currentDataPackage, referedPeriods, fp);
            this.historyDataForward_CodePlaying.NavigateTo(compChartInfo.Time);
            this.historyDataForward_CodePlaying.OnTick += HistoryDataForward_Code_OnTick;
            return historyDataForward_CodePlaying;
        }

        private void HistoryDataForward_Code_OnTick(object sender, IForwardOnTickArgument argument)
        {

        }
    }
}