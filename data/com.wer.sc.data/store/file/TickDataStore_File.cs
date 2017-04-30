using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class TickDataStore_File : ITickDataStore
    {
        private DataPathUtils dataPathUtils;

        public TickDataStore_File(DataPathUtils dataPathUtils)
        {
            this.dataPathUtils = dataPathUtils;
        }

        public void Save(string code, int date, TickData data)
        {
            string path = dataPathUtils.GetTickPath(code, date);
            new TickDataStore_File_Single(path).Save(data);
        }

        /// <summary>
        /// 保存tick数据，该方法会写入新的tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="data"></param>
        public void Append(string code, int date, TickData data)
        {
            string path = dataPathUtils.GetTickPath(code, date);
            new TickDataStore_File_Single(path).Append(data);
        }

        /// <summary>
        /// 装载tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public TickData Load(string code, int date)
        {
            string path = dataPathUtils.GetTickPath(code, date);
            TickData tickData = new TickDataStore_File_Single(path).Load();
            tickData.Code = code;
            return tickData;
        }

        /// <summary>
        /// 删掉指定的tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        public void Delete(string code, int date)
        {
            string path = dataPathUtils.GetTickPath(code, date);
            File.Delete(path);
        }

        public List<int> GetAllDays(string code)
        {
            string tickCodePath = dataPathUtils.GetTickPath(code);
            if (!Directory.Exists(tickCodePath))
                return new List<int>();
            String[] files = Directory.GetFiles(tickCodePath);
            List<int> openDates = new List<int>();
            foreach (String file in files)
            {
                int openDate;
                int index = file.LastIndexOf('_');
                bool isInt = int.TryParse(file.Substring(index + 1, 8), out openDate);
                if (isInt)
                    openDates.Add(openDate);
            }
            openDates.Sort();            
            return openDates;
        }
    }
}
