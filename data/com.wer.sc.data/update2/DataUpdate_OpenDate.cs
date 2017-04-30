using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataUpdate_OpenDate
    {
        private Plugin_DataProvider dataProvider;

        private DataPathUtils pathUtils;

        public DataUpdate_OpenDate(Plugin_DataProvider dataProvider)
        {
            this.pathUtils = new DataPathUtils(dataProvider.GetDataPath());
            this.dataProvider = dataProvider;
        }

        public void Update()
        {
            List<int> openDates = dataProvider.GetOpenDates();
            String[] openDateStr = new String[openDates.Count];
            for (int i = 0; i < openDates.Count; i++)
            {
                openDateStr[i] = openDates[i].ToString(); ;
            }
            File.WriteAllLines(pathUtils.GetOpenDatePath(), openDateStr);
        }
    }
}
