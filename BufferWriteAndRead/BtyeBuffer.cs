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
        public int order { get; set; }
        public ByteMember(int order)
        {
            this.order = order;
        }
    }


  

}
