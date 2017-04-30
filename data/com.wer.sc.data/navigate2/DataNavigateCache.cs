using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{

    public class DataNavigateCache
    {
        private Dictionary<string, DataNavigate3> dicNavigate = new Dictionary<string, DataNavigate3>();        

        public IKLineData GetData(String code, KLinePeriod period, float time)
        {            
            return null;
        }
    }
}
