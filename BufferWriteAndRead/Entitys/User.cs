using System;
using System.Collections.Generic;
using System.Text;

namespace BufferWriteAndRead.Entitys
{
    [BtyeContract]
    public partial class User
    {
        [ByteMember(1,ByteType.Uint16)]
        public UInt16 UInt16 { get; set; }

        /// <summary>
        /// Char 是 二字节的
        /// </summary>
        [ByteMember(2, ByteType.Int8)]
        public char Char { get; set; }

        [ByteMember(2, ByteType.Int8)]
        public bool Bool { get; set; }

        [ByteMember(2, ByteType.Int16)]
        public Int16 Int16 { get; set; }

        public float Float { get; set; }

        public DateTime DateTime { get; set; }

        public byte Byte { get; set; }

        public sbyte SByte { get; set; }

        [ByteMember(2, ByteType.Uint16)]
        public ushort UShort { get; set; }


        public byte[] Serialize()
        {
            var bytes = new byte[14];
            Span<byte> span = new Span<byte>(bytes);

            //BitConverter.w(UInt16);

            return new byte[2];
        }


        public User Deserialize(ArraySegment<byte> arraySegment)
        {
            var user = new User();

            return user;
        }
    }

   

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

        //byte[] ping = Encoding.UTF8.GetBytes("你的密码是什么?");
        //string str = Encoding.UTF8.GetString(ping);
        //BitConverter.ToBoolean()

        //var _str = "你好";
        //byte[] _bytes = new byte[_str.Length * sizeof(char)];
        //System.Buffer.BlockCopy(_str.ToCharArray(), 0, _bytes, 0, _bytes.Length);

        //var chars = "你好".ToCharArray();
        //var bytes= ByteHelper.CharsToBytes(chars);
        //var str= System.Text.Encoding.Default.GetString(bytes);

        //var bytes2= System.Text.Encoding.UTF8.GetBytes("你好");
        //var str2 = System.Text.Encoding.Default.GetString(bytes2);
        //Console.WriteLine(str2);

        //byte[] byteArray = System.Text.Encoding.Default.GetBytes("dddd");
        //string str = System.Text.Encoding.Default.GetString(byteArray);
        //var bytes = new byte[14];
        //Span<byte> span = new Span<byte>(bytes);
        //BitConverter.TryWriteBytes(span,12);
        //Console.WriteLine("Hello World!");
    }


}
