using System;
using System.Collections.Generic;
using System.Text;

namespace BufferWriteAndRead
{

    /// <summary>
    /// 契约
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BtyeContract : Attribute
    {

    }

    /// <summary>
    /// 成员
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ByteMember : Attribute
    {
        public int Order { get; set; }

        public ByteType ByteType { get; set; }
        public ByteMember(int order, ByteType tyteType)
        {
            this.Order = order;
            this.ByteType = tyteType;
        }
    }


  

}
