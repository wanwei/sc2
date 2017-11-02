using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.forward
{
    public class DataForwardFactory : IDataForwardFactory
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

        public DataForwardFactory(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public IDataForward_Code CreateDataForward_Code(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            if (forwardPeriod.IsTickForward)
                return new DataForward_Code_Tick(dataCenter, dataPackage, referedPeriods, forwardPeriod);
            //return new HistoryDataForward_Code_TickPeriod(dataPackage, referedPeriods.UsedKLinePeriods, forwardPeriod.KlineForwardPeriod, referedPeriods.UseTimeLineData);
            return new DataForward_Code_KLine(dataCenter, dataPackage, referedPeriods, forwardPeriod);
        }

        public IDataForward_Code CreateDataForward_Code(string code, int startDate, int endDate, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            IDataPackage_Code dataPackage = dataCenter.DataPackageFactory.CreateDataPackage_Code(code, startDate, endDate);
            return CreateDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
        }

        public IDataForward_Code CreateDataForward_Code(XmlElement xmlElem)
        {
            string forwardType = xmlElem.GetAttribute("forwardType");
            if (forwardType.Equals("kline"))
            {
                DataForward_Code_KLine dataForward = new DataForward_Code_KLine(dataCenter);
                dataForward.Load(xmlElem);
                return dataForward;
            }
            else if (forwardType.Equals("tick"))
            {
                DataForward_Code_Tick dataForward = new DataForward_Code_Tick(dataCenter);
                dataForward.Load(xmlElem);
                return dataForward;
            }
            return null;
        }

        public IDataForward CreateHistoryDataForward(IDataPackage_Code[] dataPackage, ForwardReferedPeriods[] referedPeriods, ForwardPeriod forwardPeriod)
        {
            return new DataForward(this, dataPackage, referedPeriods, forwardPeriod);
        }

        public IDataForward CreateHistoryDataForward(string[] codes, int startDate, int endDate, ForwardReferedPeriods[] referedPeriods, ForwardPeriod forwardPeriod)
        {
            IDataPackage_Code[] dataPackages = new IDataPackage_Code[codes.Length];
            for (int i = 0; i < codes.Length; i++)
            {
                string code = codes[i];
                IDataPackage_Code dataPackage = dataCenter.DataPackageFactory.CreateDataPackage_Code(code, startDate, endDate);
                dataPackages[i] = dataPackage;
            }
            return CreateHistoryDataForward(dataPackages, referedPeriods, forwardPeriod);
        }
    }
}