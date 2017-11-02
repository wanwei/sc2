using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.forward
{
    public interface IDataForwardFactory
    {
        IDataForward_Code CreateDataForward_Code(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod);

        IDataForward_Code CreateDataForward_Code(string code, int startDate, int endDate, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod);

        IDataForward_Code CreateDataForward_Code(XmlElement xmlElem);

        IDataForward CreateHistoryDataForward(IDataPackage_Code[] dataPackage, ForwardReferedPeriods[] referedPeriods, ForwardPeriod forwardPeriod);

        IDataForward CreateHistoryDataForward(string[] codes, int startDate, int endDate, ForwardReferedPeriods[] referedPeriods, ForwardPeriod forwardPeriod);
    }
}
