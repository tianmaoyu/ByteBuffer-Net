
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace BufferWriteAndRead
{
    public partial class CreateMsg
    {



        public byte[] Write()
        {
            var buffer = new byte[32];
            var offset = 0;

            foreach (var _byte in BitConverter.GetBytes(this.Bool))
            {
                buffer[offset] = _byte;
                offset += 1;
            }


            foreach (var _byte in BitConverter.GetBytes(this.Byte))
            {
                buffer[offset] = _byte;
                offset += 1;
            }

            return buffer;
        }


    }
}