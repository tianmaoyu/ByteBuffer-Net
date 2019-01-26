using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BufferWriteAndRead
{
    public class ByteBufferReflection
    {

        public static void test()
        {
            var msg = new CreateMsg();

            msg.UInt16 = 65532;
            msg.UShort = 12333;
            msg.SByte = -1;
            msg.Float = 0.3322345f;
            msg.Char = 'A';
            msg.Bool = false;
            msg.Name = "eric";
          
            var type = msg.GetType();
            var byteContract = type.GetCustomAttribute<BtyeContract>();
            Console.WriteLine(byteContract == null);

            foreach (var property in type.GetProperties())
            {
                var byteMember = property.GetCustomAttribute<ByteMember>();
                Console.WriteLine($"属性:{property.Name}  值：{property.GetValue(msg)}  :类型{property.PropertyType}  顺序：{byteMember.order}");
            }

        }


    }

    [BtyeContract]
    public partial class CreateMsg
    {
        [ByteMember(1)]
        public UInt16 UInt16 { get; set; }
        [ByteMember(2)]
        public char Char { get; set; }
        [ByteMember(3)]
        public bool Bool { get; set; }
        [ByteMember(4)]
        public Int16 Int16 { get; set; }
        [ByteMember(5)]
        public float Float { get; set; }

        [ByteMember(6)]
        public byte Byte { get; set; }
        [ByteMember(7)]
        public sbyte SByte { get; set; }
        [ByteMember(8)]
        public ushort UShort { get; set; }
        [ByteMember(9)]
        public String Name { get; set; }
      
    }

    /// <summary>
    ///  读写
    /// </summary>
    public partial class CreateMsg
    {
        public byte[] Write()
        {
            var buffer = new byte[32];
            var offset = 0;

            return buffer;
        }

        
        public static CreateMsg Read(byte[] buffer)
        {
            var msg = new CreateMsg();


            return msg;
        }

    }


    /// <summary>
    /// 类型  参考 TypeCode
    /// </summary>
    public enum ByteType
    {
        /// <summary>
        /// sbyte :-128 到 127  有符号 8 位整数
        /// </summary>
        Int8 = 1,

        /// <summary>
        /// byte 无符号 8 位整数  0 到 255
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
