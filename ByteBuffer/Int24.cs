using System;
using System.Runtime.InteropServices;

namespace ByteBuffer
{

    /// <summary>
    /// 默认小端模式
    /// </summary>
    public class Int24
    {
        public const int MaxVlaue = 0x7FFFFF;//,8388607
        public const int MiniValue = -(0x7FFFFF+1);// -8388608
        /// <summary>
        /// 默认小端
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ReadInt24(byte[] buffer, int offset)
        {
             return buffer[offset + 0] | buffer[offset + 1] << 8 | (sbyte)buffer[offset + 2] << 16;
        }
        public static Byte[] GetBytes(int value)
        {
            var bytes = new byte[3];
            bytes[0] = (byte) value;
            bytes[1] = (byte)(value >> 8);
            bytes[2] = (byte)(value >> 16) ;
            return bytes;
            //var bytes = new byte[3];
            //bytes[0] = (byte)(value & 0xFF);
            //bytes[1] = (byte)((value >> 8) & 0xFF);
            //bytes[2] = (byte)((value >> 16) & 0xFF);
            //return bytes;
        }

    }
}