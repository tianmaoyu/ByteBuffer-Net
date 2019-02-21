
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace BufferWriteAndRead.Entitys
{
     public partial class Role
     {  
        public byte[] Write()
        {
            var buffer = new byte[64];
            var offset = 0;  
            foreach (var _byte in BitConverter.GetBytes(Convert.ToInt16(this.RoleId)))
            {
                buffer[offset] = _byte;
                offset += 1;
            } 
            var nameBytes = System.Text.Encoding.Unicode.GetBytes(this.RoleName);
            buffer[offset] = (byte)nameBytes.Length;
            offset += 1;
            foreach (var _byte in nameBytes)
            {
                buffer[offset] = _byte;
                offset += 1;
            }
            return new ArraySegment<byte>(buffer, 0, offset).ToArray();
        }
        public static Role Read(byte[] buffer,int offset)
        {
            var msg = new Role();
            msg.RoleId=BitConverter.ToInt16(buffer, offset);
            offset+=2;
            var strLength=buffer[offset];
            offset++;
            msg.RoleName=System.Text.Encoding.Unicode.GetString(buffer, offset,strLength);
            offset+=strLength;
            return msg;
        } 
      }
}