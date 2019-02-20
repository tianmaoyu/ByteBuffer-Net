
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace BufferWriteAndRead.Entitys
{
    public partial class User
    {
        public byte[] Write()
        {
            var buffer = new byte[32];
            var offset = 0;
            foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(this.UInt16)))
            {
                buffer[offset] = _byte;
                offset += 1;
            }
            buffer[offset] = Convert.ToByte(this.Char);
            offset += 1;
            buffer[offset] = Convert.ToByte(this.Bool);
            offset += 1;
            foreach (var _byte in BitConverter.GetBytes(Convert.ToInt16(this.Int16)))
            {
                buffer[offset] = _byte;
                offset += 1;
            }
            foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(this.UShort)))
            {
                buffer[offset] = _byte;
                offset += 1;
            }
            foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(this.Id)))
            {
                buffer[offset] = _byte;
                offset += 1;
            }
            return buffer;
        }
        public static User Read(byte[] buffer, int offset)
        {
            var msg = new User();
            msg.UInt16 = BitConverter.ToUInt16(buffer, offset);
            offset += 2;
            msg.Char = (Char)buffer[offset];
            offset++;
            msg.Bool = buffer[offset] == 1;
            offset++;
            msg.Int16 = BitConverter.ToInt16(buffer, offset);
            offset += 2;
            msg.UShort = BitConverter.ToUInt16(buffer, offset);
            offset += 2;
            msg.Id = BitConverter.ToUInt16(buffer, offset);
            offset += 2;

            var count = buffer[offset];
            offset++;
            var list = new List<int>();
            for (var i = 0; i < count; i++)
            {
                var item = buffer[offset];
                offset++;
                list.Add(item);
            }
            msg.IdList = list;

            return msg;
        }
    }
}