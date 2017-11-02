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
        private ITickData tickData;

        public ITickData TickData
        {
            get { return tickData; }
        }

        private int index;

        public int Index
        {
            get { return index; }
        }

        public ITickBar TickBar
        {
            get { return TickData.GetBar(Index); }
        }

        private IDataForward_Code dataForward_Code;

        public ForwardOnTickArgument(ITickData tickData, int index, IDataForward_Code dataForward_Code)
        {
            this.tickData = tickData;
            this.index = index;
            this.dataForward_Code = dataForward_Code;
        }

        public double Time
        {
            get { return TickData.Time; }
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