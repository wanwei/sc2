using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward.impl;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class HistoryDataForwardFactory
    {
        //public static IHistoryDataForward CreateHistoryDataForward(IDataReader dataReader, IList<string> codes, HistoryDataForwardArguments args)
        //{
        //    return new HistoryDataForward(dataReader, codes, args);
        //}

        //public static IHistoryDataForward_Code CreateHistoryDataForward_Code(IDataPackage dataPackage,)
        //{
        //    return new 
        //}

        public static IHistoryDataForward_Code CreateHistoryDataForward_Code(IDataReader dataReader, string code, HistoryDataForwardArguments args)
        {
            IDataPackage dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, args.StartDate, args.EndDate);
            StrategyReferedPeriods referedPeriods = args.ReferedPeriods;
            ForwardPeriod forwardPeriod = new ForwardPeriod(args.IsTickForward, args.ForwardKLinePeriod);
            return new HistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
        }

        public static IHistoryDataForward_Code CreateHistoryDataForward_Code(IDataPackage dataPackage, StrategyReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            return new HistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
        }
    }
}