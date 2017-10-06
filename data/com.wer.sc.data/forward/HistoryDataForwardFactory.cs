using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class HistoryDataForwardFactory : IHistoryDataForwardFactory
    {
        //public static IHistoryDataForward_Code CreateHistoryDataForward_Code(IDataReader dataReader, string code, HistoryDataForwardArguments args)
        //{
        //    IDataPackage dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, args.StartDate, args.EndDate);
        //    StrategyReferedPeriods referedPeriods = args.ReferedPeriods;
        //    ForwardPeriod forwardPeriod = new ForwardPeriod(args.IsTickForward, args.ForwardKLinePeriod);
        //    return new HistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
        //}

        //public static IHistoryDataForward_Code CreateHistoryDataForward_Code(IDataPackage dataPackage, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        //{
        //    return new HistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
        //}

        private IDataCenter dataCenter;

        public HistoryDataForwardFactory(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public IHistoryDataForward_Code CreateHistoryDataForward_Code(IDataPackage dataPackage, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            return new HistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
        }

        public IHistoryDataForward_Code CreateHistoryDataForward_Code(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            IDataPackage dataPackage = dataCenter.DataPackageFactory.CreateDataPackage(code, startDate, endDate);
            return CreateHistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
        }
    }
}