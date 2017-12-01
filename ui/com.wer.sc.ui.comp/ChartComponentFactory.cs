using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentFactory : IChartComponentFactory
    {
        public IChartComponent Create(IDataCenter dataCenter)
        {
            return null;
        }

        public IChartComponent Create(IDataCenter dataCenter, IDataPackage_Code dataPackage, double time)
        {
            throw new NotImplementedException();
        }

        public IChartComponent Create(IDataCenter dataCenter, string code, double time)
        {
            throw new NotImplementedException();
        }
    }
}
