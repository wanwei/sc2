using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2
{
    public class InstrumentWriter
    {
        private string path;

        public InstrumentWriter(string path)
        {
            this.path = path;
        }

        public void Writer(List<plugin.market.InstrumentInfo> instruments)
        {
            //JsonUtils_Instrument.Save()
        }

        public List<plugin.market.InstrumentInfo> Load()
        {
            return null;
        }
    }
}
