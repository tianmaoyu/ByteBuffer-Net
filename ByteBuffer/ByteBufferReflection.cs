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

    [BtyeContract]
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

    /// <summary>
    ///  读写
    /// </summary>
    public partial class CreateMsg
    {
        public byte[] Write1()
        {
            var buffer = new byte[32];
            var offset = 0;

           

            foreach (var _byte in BitConverter.GetBytes(this.UInt16))
            {
                buffer[offset] = _byte;
                offset += 1;
            }

            buffer[offset] = (byte)this.Char;
            offset += 1;


            buffer[offset] = (byte)(this.Bool?1:0);
            offset += 1;

            foreach (var _byte in BitConverter.GetBytes(this.Char))
            {
                buffer[offset] = _byte;
                offset += 1;
            }

            foreach (var _byte in BitConverter.GetBytes(this.Int16))
            {
                buffer[offset] = _byte;
                offset += 1;
            }

            foreach (var _byte in BitConverter.GetBytes(this.Float))
            {
                buffer[offset] = _byte;
                offset += 1;
            }

            foreach (var _byte in BitConverter.GetBytes(this.Float))
            {
                buffer[offset] = _byte;
                offset += 1;
            }

            Program.Clientbuffer[offset] = Byte;
            offset += 1;


            var nameBytes = System.Text.Encoding.UTF8.GetBytes(this.Name);
            Program.Clientbuffer[offset] = (byte)nameBytes.Length;
            offset += 1;
            foreach (var _byte in nameBytes)
            {
                buffer[offset] = _byte;
                offset += 1;
            }

            return buffer;
        }


        public static CreateMsg Read1(byte[] buffer,int offset)
        {
            var msg = new CreateMsg();
            //var




            return msg;
        }

    }





}
