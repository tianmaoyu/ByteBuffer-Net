
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
            var countIdList = this.IdList == null ? 0 : this.IdList.Count;
            buffer[offset] = (byte)countIdList;
            offset++;
            foreach (var item in this.IdList)
            {
                foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(item)))
                {
                    buffer[offset] = _byte;
                    offset++;
                }
            }
            var countIntList = this.IntList == null ? 0 : this.IntList.Count;
            buffer[offset] = (byte)countIntList;
            offset++;
            foreach (var item in this.IntList)
            {
                foreach (var _byte in BitConverter.GetBytes(item))
                {
                    buffer[offset] = _byte;
                    offset++;
                }
            }
            var countfloatList = this.floatList == null ? 0 : this.floatList.Count;
            buffer[offset] = (byte)countfloatList;
            offset++;
            foreach (var item in this.floatList)
            {
                foreach (var _byte in BitConverter.GetBytes(item))
                {
                    buffer[offset] = _byte;
                    offset++;
                }
            }
            var countStringList = this.StringList == null ? 0 : this.StringList.Count;
            buffer[offset] = (byte)countStringList;
            offset++;
            foreach (var item in this.StringList)
            {
                var nameBytes = System.Text.Encoding.UTF8.GetBytes(item);
                buffer[offset] = (byte)nameBytes.Length;
                offset += 1;
                foreach (var _byte in nameBytes)
                {
                    buffer[offset] = _byte;
                    offset += 1;
                }
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
            msg.Int16 = BitConverter.ToInt16(buffer, offset);
            offset += 2;
            msg.UShort = BitConverter.ToUInt16(buffer, offset);
            offset += 2;
            msg.Id = BitConverter.ToUInt16(buffer, offset);
            offset += 2;
            var countIdList = buffer[offset];
            offset++;
            var listIdList = new List<int>();
            for (var i = 0; i < countIdList; i++)
            {
                var item = BitConverter.ToUInt16(buffer, offset);
                offset += 2;
                listIdList.Add(item);
            }
            msg.IdList = listIdList;
            var countIntList = buffer[offset];
            offset++;
            var listIntList = new List<int>();
            for (var i = 0; i < countIntList; i++)
            {
                var item = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                listIntList.Add(item);
            }
            msg.IntList = listIntList;
            var countfloatList = buffer[offset];
            offset++;
            var listfloatList = new List<float>();
            for (var i = 0; i < countfloatList; i++)
            {
                var item = BitConverter.ToSingle(buffer, offset);
                offset += 4;
                listfloatList.Add(item);
            }
            msg.floatList = listfloatList;
            var countStringList = buffer[offset];
            offset++;
            var listStringList = new List<string>();
            for (var i = 0; i < countStringList; i++)
            {
                var strLength = buffer[offset];
                offset++;
                var item = BitConverter.ToString(buffer, offset, strLength);
                offset += strLength;
                listStringList.Add(item);
            }
            msg.StringList = listStringList;
            return msg;
        }
    }
}