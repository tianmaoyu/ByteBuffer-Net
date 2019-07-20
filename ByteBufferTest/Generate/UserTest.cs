
using ByteBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace ByteBufferTest
{
     public partial class UserTest
     {  
        public override byte[] Write()
        {
            var buffer = new byte[256];
            var offset = 0;  
            foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(this.UInt16)))
            {
                buffer[offset] = _byte;
                offset += 1;
            }   
            buffer[offset] = Convert.ToByte(this.Char);
            offset += 1;  
            buffer[offset] =(byte)(this.Bool? 1 : 0);
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
            if (countIdList > 0)  
            { 
            foreach (var item in this.IdList)
            {
                foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(item)))
                {
                    buffer[offset] = _byte;
                    offset ++;
                }
            }}  
            var countboolList = this.boolList == null ? 0 : this.boolList.Count;
            buffer[offset] = (byte)countboolList;
            offset++;
            if (countboolList > 0)  
            { 
                foreach (var item in this.boolList)
                {
                     buffer[offset] = (byte)(item?1:0);
                     offset++;
                }
             }  
            var countIntList = this.IntList == null ? 0 : this.IntList.Count;
            buffer[offset] = (byte)countIntList;
            offset++;
            if (countIntList > 0)  
            { 
            foreach (var item in this.IntList)
            {
                foreach (var _byte in BitConverter.GetBytes(item))
                {
                    buffer[offset] = _byte;
                    offset ++;
                }
            }}  
            var countfloatList = this.floatList == null ? 0 : this.floatList.Count;
            buffer[offset] = (byte)countfloatList;
            offset++;
            if (countfloatList > 0)  
            { 
            foreach (var item in this.floatList)
            {
                foreach (var _byte in BitConverter.GetBytes(item))
                {
                    buffer[offset] = _byte;
                    offset ++;
                }
            }}  
            var countStringList = this.StringList == null ? 0 : this.StringList.Count;
            buffer[offset] = (byte)countStringList;
            offset++;
            if (countStringList > 0)  
            { 
            foreach (var item in this.StringList)
            {
                 var nameBytes = StringEncoding.GetBytes(item);
                 buffer[offset] = (byte)nameBytes.Length;
                 offset += 1;
                 foreach (var _byte in nameBytes)
                 {
                    buffer[offset] = _byte;
                    offset += 1;
                 }
            }}
            if (this.Role == null)
            {
                buffer[offset] = 0;
                offset += 1;
            }
            else
            {
                var _buffer = this.Role.Write();
                buffer[offset] = (byte)_buffer.Length;
                offset++;
                for (var i = 0; i < _buffer.Length; i++)
                {
                    buffer[offset] = _buffer[i];
                    offset++;
                }
            }  
            var countRoleList = this.RoleList == null ? 0 : this.RoleList.Count;
            buffer[offset] = (byte)countRoleList;
            offset++;
            if (countRoleList > 0)
            {
                foreach (var item in this.RoleList)
                {
                    if (item == null)
                    {
                        buffer[offset] = 0;
                        offset ++;
                    }
                    else
                    {
                        var _buffer = item.Write();
                        buffer[offset] = (byte)_buffer.Length;
                        offset++;
                        for (var i = 0; i < _buffer.Length; i++)
                        {
                            buffer[offset] = _buffer[i];
                            offset++;
                        }
                    }
                }
            }
            return new ArraySegment<byte>(buffer, 0, offset).ToArray();
        }
        public static UserTest Read(byte[] buffer,int offset)
        {
            var msg = new UserTest();
            msg.UInt16=BitConverter.ToUInt16(buffer, offset);
            offset+=2;
            msg.Char=(Char)buffer[offset];
            offset++; 
            msg.Bool=buffer[offset]==1;
            offset++; 
            msg.Int16=BitConverter.ToInt16(buffer, offset);
            offset+=2;
            msg.UShort=BitConverter.ToUInt16(buffer, offset);
            offset+=2;
            msg.Id=BitConverter.ToUInt16(buffer, offset);
            offset+=2;
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
            var countboolList = buffer[offset];
            offset++;
            var listboolList = new List<bool>();
            for (var i = 0; i < countboolList; i++)
            {
                var item = buffer[offset]==1;
                offset++;
                listboolList.Add(item);
            }
            msg.boolList = listboolList; 
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
                var strLength=buffer[offset];
                offset++;
                var item=StringEncoding.GetString(buffer, offset,strLength);
                offset+=strLength;
                listStringList.Add(item);
            }
            msg.StringList = listStringList;
            var RoleLength = buffer[offset];
            offset++;
            if (RoleLength == 0)
            {
                msg.Role = null;
            }
            else
            {
                var _buffer = new ArraySegment<byte>(buffer, offset, RoleLength).ToArray();
                msg.Role = RoleTest.Read(_buffer, 0);
                offset += RoleLength;
            }
            var countRoleList = buffer[offset];
            offset++;
            var listRoleTestList = new List<RoleTest>();
            for (var i = 0; i < countRoleList; i++)
            {
                var _RoleTestLength = buffer[offset];
                offset++;
                if (_RoleTestLength == 0)
                {
                    listRoleTestList.Add(null);
                }
                else
                {
                    var _buffer = new ArraySegment<byte>(buffer, offset, _RoleTestLength).ToArray();
                    listRoleTestList.Add(RoleTest.Read(_buffer, 0));
                    offset += _RoleTestLength;
                }
            }
            msg.RoleList = listRoleTestList;
            return msg;
        } 
      }
}