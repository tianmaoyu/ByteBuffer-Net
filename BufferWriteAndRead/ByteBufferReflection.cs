using System;
using System.Collections.Generic;
using System.Text;

namespace BufferWriteAndRead
{
    public class ByteBufferReflection
    {


    }

    [BtyeContract]
    public class CreateMsg
    {
        [ByteMember(1)]
        public UInt16 UInt16 { get; set; }
        [ByteMember(1)]
        public char Char { get; set; }
        [ByteMember(1)]
        public bool Bool { get; set; }
        [ByteMember(1)]
        public Int16 Int16 { get; set; }
        [ByteMember(1)]
        public float Float { get; set; }

        [ByteMember(1)]
        public byte Byte { get; set; }
        [ByteMember(1)]
        public sbyte SByte { get; set; }
        [ByteMember(1)]
        public ushort UShort { get; set; }
    }


}
