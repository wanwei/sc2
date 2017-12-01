using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.account;

namespace com.wer.sc.data.store.file
{
    public class TradeFeeStore : ITradeFeeStore
    {
        public List<string> LoadAllNames()
        {
            return null;
        }

        public TradeFee Load(string name)
        {
            return null;
        }


        public void Save(string name, TradeFee tradeFee)
        {
            
        }
    }
}
