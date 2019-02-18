
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace BufferWriteAndRead
{
     public partial class Msg1
     {  
        public byte[] Write()
        {
            var buffer = new byte[32];
            var offset = 0;  
            buffer[offset] = Convert.ToByte(this.MsgType);
            offset += 1;  
            buffer[offset] = Convert.ToByte(this.Id);
            offset += 1;  
            buffer[offset] = Convert.ToByte(this.IsSuccess);
            offset += 1;  
            buffer[offset] = Convert.ToByte(this.Name);
            offset += 1;  
            buffer[offset] = Convert.ToByte(this.dateTime);
            offset += 1;
            return buffer;
        }
        public static CreateMsg Read(byte[] buffer,int offset)
        {
            var msg = new Msg1();
            msg.MsgType=(MsgType)buffer[offset];
            offset++; 
            msg.Id=(UInt16)buffer[offset];
            offset++; 
            msg.IsSuccess=buffer[offset]==1;
            offset++; 
            msg.Name=(String)buffer[offset];
            offset++; 
            msg.dateTime=(Int64)buffer[offset];
            offset++; 
            return buffer;
        } 
      }
}