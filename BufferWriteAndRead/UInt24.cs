using System;
using System.Collections.Generic;
using System.Text;

namespace BufferWriteAndRead
{

    /// <summary>
    /// 默认小端模式
    /// </summary>
    public class UInt24
    {

        public const int MaxVlaue = 0xFFFFFF;//16777215
        public const int MiniVlaue = 0;
        /// <summary>
        /// 默认小端
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ReadUInt24(byte[] buffer, int offset)
        {
            return buffer[offset + 0] | buffer[offset + 1] << 8 | buffer[offset + 2] << 16;
        }
        public static Byte[] GetBytes(int value)
        {
            var bytes = new byte[3];
            bytes[0] = (byte)(value & 0xFF);
            bytes[1] = (byte)(value >> 8);
            bytes[2] = (byte)(value >> 16);
            return bytes;
        }

    }
}
