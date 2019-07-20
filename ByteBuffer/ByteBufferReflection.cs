using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ByteBuffer
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
                Console.WriteLine($"属性:{property.Name}  值：{property.GetValue(msg)}  :类型{property.PropertyType}  顺序：{byteMember.Order}");
            }

        }


    }

    //[BtyeContract]
    public partial class CreateMsg
    {
        [ByteMember(1,ByteType.UInt16)]
        public UInt16 UInt16 { get; set; }

        [ByteMember(2, ByteType.UInt8)]
        public char Char { get; set; }

        [ByteMember(3, ByteType.Int8)]
        public bool Bool { get; set; }

        [ByteMember(4, ByteType.Int16)]
        public Int16 Int16 { get; set; }

        [ByteMember(5, ByteType.Float32)]
        public float Float { get; set; }

        [ByteMember(6, ByteType.Int8)]
        public byte Byte { get; set; }


        [ByteMember(7, ByteType.UInt8)]
        public sbyte SByte { get; set; }

        [ByteMember(8, ByteType.UInt16)]
        public ushort UShort { get; set; }

        [ByteMember(9, ByteType.String)]
        public String Name { get; set; }

    }
}
