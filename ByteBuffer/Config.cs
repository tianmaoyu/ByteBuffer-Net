using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBuffer
{
    public class Config
    {
        /// <summary>
        /// 一个消息 最多的字节数，如果消息大，会报超出数组
        /// </summary>
        public static int Message_Buffer_Max = 256;

        ///// <summary>
        ///// 字符串的编码方式
        ///// </summary>
        //public static StringEncodingType String_Encoding_Type = StringEncodingType.UTF16;

        ///// <summary>
        ///// 是否小端，否则大端模式
        ///// </summary>
        //public static bool Is_Little_Endian = true;


    }

    public enum StringEncodingType
    {
        /**
         * 一个字符 2个字节来存储,
         * 也叫 Unicode, 
         */
        UTF16 = 1,
        /**
         * 一个字符 使用 1-4 个字节来存储  
         */
        UTF8 = 2,
    }

}
