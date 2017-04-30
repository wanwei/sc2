using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.wer.sc.utils
{
    public class ByteUtils
    {
        /**
	     * 将int转化为byte
	     * 
	     * @param b byte数组
	     * @param num 数字
	     * @param length 转化后占byte数组的长度
	     * @param offset 偏移量
	     */
        public static void int2bytes(byte[] b, int num, int length, int offset)
        {
            int max = offset + length;
            for (int i = offset; i < max; i++)
            {
                // TODO 测试一下(length - 1) * 8和直接用数字的效率
                b[i] = (byte)(num >> ((length - 1) * 8 - i * 8));
            }
        }

        public static byte[] int2bytes(int num, int length)
        {
            // TODO 负数有问题
            byte[] b = new byte[length];
            for (int i = 0; i < length; i++)
            {
                // TODO 测试一下(length - 1) * 8和直接用数字的效率
                b[i] = (byte)(num >> ((length - 1) * 8 - i * 8));
            }
            return b;
        }

        public static byte[] int2bytes(int num)
        {
            return int2bytes(num, 4);
        }

        public static int bytes2int(byte[] b, int length, int offset)
        {
            int mask = 0xff;
            int temp = 0;
            int res = 0;
            int max = offset + length;
            for (int i = offset; i < max; i++)
            {
                res <<= 8;
                temp = b[i] & mask;
                res |= temp;
            }
            return res;
        }

        public static int bytes2int(byte[] b, int length, int offset,
                bool isEndFirst)
        {
            int mask = 0xff;
            int temp = 0;
            int res = 0;
            int max = offset + length;
            // TODO 重构一下
            if (isEndFirst)
            {
                for (int i = max - 1; i >= offset; i--)
                {
                    res <<= 8;
                    temp = b[i] & mask;
                    res |= temp;
                }
            }
            else
            {
                for (int i = offset; i < max; i++)
                {
                    res <<= 8;
                    temp = b[i] & mask;
                    res |= temp;
                }
            }
            return res;
        }

        public static void long2Byte(byte[] b, long l, int offset)
        {
            int length = 8;
            int max = offset + length;
            for (int i = offset; i < max; i++)
            {
                b[i] = (byte)(l >> ((length - 1) * 8 - i * 8));
            }
        }

        public static long byte2Long(byte[] b, int offset)
        {
            int length = 8;
            int mask = 0xff;
            long temp = 0;
            long res = 0;
            int max = offset + length;
            for (int i = offset; i < max; i++)
            {
                res <<= 8;
                temp = b[i] & mask;
                res |= temp;
            }
            return res;
        }

        public static bool isBytesNull(byte[] bs, int offset, int length)
        {
            int count = 0;
            while (count < length)
            {
                if (bs[offset + count] != 0)
                    return false;
                count++;
            }
            return true;
        }

        public static String byte2FullTime(byte[] bs, int offset)
        {
            //StringBuffer sb = new StringBuffer();
            //boolean hasDate = isBytesNull(bs, offset, 4);
            //if (hasDate)
            //    sb.Append(byte2Date(bs, offset));
            //boolean hasTime = isBytesNull(bs, offset + 4, 4);
            //if (hasTime)
            //    sb.Append(byte2Time(bs, offset + 4));
            //return sb.toString();
            DateTime time = new DateTime();

            return null;
        }

        public static void fullTime2Byte(String time, byte[] bs, int offset)
        {
            // boolean hasDate =
        }
        
        public static void price2Bytes(byte[] bs, double price, int offset)
        {
            int intPrice = (int)(price * 1000);
            int2bytes(bs, intPrice, 4, offset);
        }

        public static double bytes2Price(byte[] bs, int offset)
        {
            int price = bytes2int(bs, 4, offset);
            return ((double)price) / 1000;
        }
    }
}