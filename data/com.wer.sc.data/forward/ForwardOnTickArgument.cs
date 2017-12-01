using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class ForwardOnTickArgument : IForwardOnTickArgument
    {
        private IForwardTickInfo tickInfo;

        public IForwardTickInfo TickInfo
        {
            get { return tickInfo; }
        }

        public string Code
        {
            get { return tickInfo.TickData.Code; }
        }

        public double Time
        {
            get { return tickInfo.TickBar.Time; }
        }

        private IDataForward_Code dataForward_Code;

        public IDataForward_Code DataForward_Code
        {
            get { return dataForward_Code; }
        }
        public ForwardOnTickArgument(ITickData_Extend tickData, int index, IDataForward_Code dataForward_Code)
        {
            this.tickInfo = new ForwardTickInfo(tickData, index);
            this.dataForward_Code = dataForward_Code;
        }

        public IRealTimeDataReader_Code CurrentData
        {
            get
            {
                return dataForward_Code;
            }
        }

        public IRealTimeDataReader_Code GetOtherData(string code)
        {
            return dataForward_Code.GetAttachedDataReader(code);
        }
    }
}