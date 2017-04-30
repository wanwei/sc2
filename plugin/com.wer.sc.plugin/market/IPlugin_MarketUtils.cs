using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public interface IPlugin_MarketUtils
    {
        String TransferLocalInstrumentIdToRemote(string localInstrumentId);

        String TransferRemoteInstrumentIdToLocal(string remoteInstrumentId);
    }
}
