
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace ByteBuffer.Entitys
{
     public partial class Role
     {  
        public override byte[] Write()
        {
            var buffer = new byte[64];
            var offset = 0;  
            foreach (var _byte in BitConverter.GetBytes(Convert.ToInt16(this.RoleId)))
            {
                buffer[offset] = _byte;
                offset += 1;
            } 
            var RoleNameBytes = StringEncoding.GetBytes(this.RoleName);
            buffer[offset] = (byte)RoleNameBytes.Length;
            offset += 1;
            foreach (var _byte in RoleNameBytes)
            {
                buffer[offset] = _byte;
                offset += 1;
            }
            var IdBytes = Int24.GetBytes(this.Id);
            foreach (var _byte in IdBytes)
            {
                buffer[offset] = _byte;
                offset += 1;
            }  
            var countIds = this.Ids == null ? 0 : this.Ids.Count;
            buffer[offset] = (byte)countIds;
            offset++;
            if (countIds > 0)  
            { 
            foreach (var item in this.Ids)
            {
                foreach (var _byte in  Int24.GetBytes(item))
                {
                    buffer[offset] = _byte;
                    offset ++;
                }
            }}
            return new ArraySegment<byte>(buffer, 0, offset).ToArray();
        }
        public static Role Read(byte[] buffer,int offset)
        {
            var msg = new Role();
            msg.RoleId=BitConverter.ToInt16(buffer, offset);
            offset+=2;
            var RoleNameLength=buffer[offset];
            offset++;
            msg.RoleName=StringEncoding.GetString(buffer, offset,RoleNameLength);
            offset+=RoleNameLength;
            msg.Id=Int24.ReadInt24(buffer, offset);
            offset+=3;
            var countIds = buffer[offset];
            offset++;
            var listIds = new List<int>();
            for (var i = 0; i < countIds; i++)
            {
                var item = Int24.ReadInt24(buffer, offset);
                offset += 3;
                listIds.Add(item);
            }
            msg.Ids = listIds;
            return msg;
        } 
      }
}