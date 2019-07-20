using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBuffer
{
    public class StringEncoding
    {
        public static byte[] GetBytes(string str)
        {
            return System.Text.Encoding.Unicode.GetBytes(str);
            //return System.Text.Encoding.BigEndianUnicode.GetBytes(str);
        }

        public static string GetString(byte[] buffer, int offset, int strLength)
        {
            return System.Text.Encoding.Unicode.GetString(buffer, offset, strLength);
            //return System.Text.Encoding.BigEndianUnicode.GetString(buffer, offset, strLength);
        }
    }
}
