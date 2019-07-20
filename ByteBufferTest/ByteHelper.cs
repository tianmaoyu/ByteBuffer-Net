using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBufferTest
{

    public class ByteHelper
    {
        public static byte[] Int32ToBytes(int value)
        {
            byte[] src = new byte[4];
            src[3] = (byte)((value >> 24) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }

        public static int bytesToInt32(byte[] src, int offset)
        {

            int value = (src[offset] & 0xFF)
                         | ((src[offset + 1] & 0xFF) << 8)
                         | ((src[offset + 2] & 0xFF) << 16)
                         | ((src[offset + 3] & 0xFF) << 24);
            return value;
        }


        /** char转byte */
        public static byte[] CharsToBytes(char[] chars)
        {
            int len = chars.Length;
            byte[] bytes = new byte[len];

            for (int i = 0; i < len; i++)
            {
                bytes[i] = (byte)(chars[i]);
            }
            return bytes;
        }

        /** byte转char */
        public static char[] BytesToChars(byte[] bytes)
        {

            int len = bytes.Length;
            char[] chars = new char[len];

            for (int i = 0; i < len; i++)
            {
                chars[i] = (char)(bytes[i] & 0xff);
            }
            return chars;


        }


        // Date -> byte[2] 
        public static byte[] DateToByte(DateTime date)
        {
            int year = date.Year - 2000;
            if (year < 0 || year > 127)
                return new byte[4];
            int month = date.Month;
            int day = date.Day;
            int date10 = year * 512 + month * 32 + day;
            return BitConverter.GetBytes((ushort)date10);
        }
        // byte[2] -> Date  直接  long tickts
        public static DateTime ByteToDate(byte[] b)
        {
            int date10 = (int)BitConverter.ToUInt16(b, 0);
            int year = date10 / 512 + 2000;
            int month = date10 % 512 / 32;
            int day = date10 % 512 % 32;
            return new DateTime(year, month, day);
        }

    }
}
