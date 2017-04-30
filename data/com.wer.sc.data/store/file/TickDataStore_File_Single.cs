using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    /// <summary>
    /// Tick数据存储，存储成
    /// </summary>
    public class TickDataStore_File_Single
    {
        private String path;

        public TickDataStore_File_Single(String path)
        {
            this.path = path;
        }

        public void Save(TickData data)
        {
            DirectoryInfo dir = Directory.GetParent(path);
            if (!dir.Exists)            
                dir.Create();            

            byte[] bs = GetBytes(data);
            FileStream file = new FileStream(path, FileMode.Create);
            try
            {
                file.Write(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public void Append(TickData data)
        {
            byte[] bs = GetBytes(data);
            FileStream file = new FileStream(path, FileMode.Append);
            try
            {
                file.Write(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public TickData Load()
        {
            if (!File.Exists(path))
                return null;
            FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                byte[] bs = new byte[file.Length];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(bs, 0, bs.Length);

                return FromBytes(bs, 0, bs.Length);
            }
            finally
            {
                file.Close();
            }
        }

        public static TickData FromBytes(byte[] bs)
        {
            return FromBytes(bs, 0, bs.Length);
        }

        public static TickData FromBytes(byte[] bs, int start, int len)
        {
            int size = 44;
            int dataLength = len / size;
            TickData data = new TickData(dataLength);
            for (int i = 0; i < dataLength; i++)
            {
                int offset = i * size;
                data.arr_time[i] = BitConverter.ToDouble(bs, offset);
                offset += 8;

                data.arr_price[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;
                
                data.arr_mount[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;

                data.arr_totalMount[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;

                data.arr_add[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;

                data.arr_buyPrice[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_buyMount[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;

                data.arr_sellPrice[i] = BitConverter.ToSingle(bs, offset);
                offset += 4;

                data.arr_sellMount[i] = BitConverter.ToInt32(bs, offset);
                offset += 4;

                data.arr_isBuy[i] = BitConverter.ToBoolean(bs, offset);
                offset += 4;
            }

            return data;
        }

        public static byte[] GetBytes(TickData data)
        {
            int size = 44;
            byte[] bs = new byte[size * data.Length];
            int offset = 0;
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                byte[] tmpBs = BitConverter.GetBytes(data.Time);
                Array.Copy(tmpBs, 0, bs, offset, 8);
                offset += 8;

                tmpBs = BitConverter.GetBytes(data.Price);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Mount);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.TotalMount);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.Add);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.BuyPrice);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.BuyMount);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.SellPrice);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.SellMount);
                Array.Copy(tmpBs, 0, bs, offset, 4);
                offset += 4;

                tmpBs = BitConverter.GetBytes(data.IsBuy);
                Array.Copy(tmpBs, 0, bs, offset, 1);
                offset += 4;              
            }
            return bs;
        }
    }
}