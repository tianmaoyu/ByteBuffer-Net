using System;
using System.Collections.Generic;
using System.Text;

namespace BufferWriteAndRead
{

    /// <summary>
    /// 契约
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BtyeContract : Attribute
    {

    }

    /// <summary>
    /// 成员
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ByteMember : Attribute
    {
        public int Order { get; set; }

        public ByteType ByteType { get; set; }
        public ByteMember(int order, ByteType tyteType)
        {
            this.Order = order;
            this.ByteType = tyteType;
        }
    }


    /// <summary>
    /// 类型  参考 TypeCode
    /// </summary>
    public enum ByteType
    {
        /// <summary>
        /// byte 无符号 8 位整数  0 到 255
        /// </summary>
        Int8 = 1,

        /// <summary>
        /// sbyte :-128 到 127  有符号 8 位整数
        /// </summary>
        Uint8 = 2,

        /// <summary>
        /// short 有符号 16 位整数  -32,768 到 32,767
        /// </summary>
        Int16 = 3,
        /// <summary>
        /// ushort 无符号 16 位整数  0 到 65,535
        /// </summary>
        Uint16 = 4,

        /// <summary>
        ///int 有符号 32 位整数  -2,147,483,648 到 2,147,483,647
        /// </summary>
        Int32 = 5,

        /// <summary>
        ///uint 无符号 32 位整数  0 到 4,294,967,295
        /// </summary>
        Uint32 = 6,

        /// <summary>
        /// 32 为 带小数类型 float
        /// </summary>
        Float32 = 7,
        /// <summary>
        /// 64 为 带 小数 类型  double
        /// </summary>
        Float64 = 8,

        String = 9,
        Object = 10,

        //数组
        Int8Array = 10,

        UInt8Array = 11,

        Int16Array = 13,

        Uint16Array = 14,

        Int32Array = 15,

        Uint32Array = 16,

        Float32Array = 17,

        Float64Array = 18,

        StringArray = 19,

        ObjectArray = 20,
    }

}
