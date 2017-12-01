using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentController : IChartComponentController
    {
        private ChartComponentInfo chartComponentInfo;

        private ChartComponentView chartComponentView;

        public ChartComponentController(ChartComponentInfo chartComponentInfo)
        {
            this.chartComponentInfo = chartComponentInfo;
        }

        public void Change(double time)
        {
            
        }

        public void Change(string code)
        {
            
        }

        public void Change(string code, double time)
        {
            
        }

        public void Change(IDataPackage_Code dataPackage, double time)
        {
            
        }

        public void ChangeBarWidth(double width)
        {
            throw new NotImplementedException();
        }

        public void ChangeChartType(ChartType chartType)
        {
            throw new NotImplementedException();
        }

        public void ForwardTime(ForwardPeriod forwardPeriod)
        {
            throw new NotImplementedException();
        }

        public void ForwardView(int cnt)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
