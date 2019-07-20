using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBuffer.Entitys
{
    [BtyeContract]
    public partial class User:Writeable
    {
        [ByteMember(1, ByteType.UInt16)]
        public UInt16 UInt16 { get; set; }

        /// <summary>
        /// Char 是 二字节的
        /// </summary>
        [ByteMember(2, ByteType.Int8)]
        public char Char { get; set; }

        [ByteMember(3, ByteType.Bool)]
        public bool Bool { get; set; }

        [ByteMember(4, ByteType.Int16)]
        public Int16 Int16 { get; set; }


        [ByteMember(5, ByteType.UInt16)]
        public ushort UShort { get; set; }

        [ByteMember(6, ByteType.UInt16)]
        public int Id { get; set; }

        [ByteMember(7, ByteType.UInt16Array)]
        public List<int> IdList { get; set; }

        [ByteMember(8, ByteType.BoolArray)]
        public List<bool> boolList { get; set; }

        [ByteMember(8, ByteType.Int32Array)]
        public List<int> IntList { get; set; }

        [ByteMember(8, ByteType.Float32Array)]
        public List<float> floatList { get; set; }


        [ByteMember(8, ByteType.StringArray)]
        public List<string> StringList { get; set; }

        [ByteMember(9, ByteType.Object)]
        public Role Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ByteMember(10, ByteType.ObjectArray, "Role")]
        public List<Role> RoleList { get; set; }

      
    }

    [BtyeContract]
    public partial class Role: Writeable
    {
        [ByteMember(1, ByteType.Int16)]
        public int RoleId { get; set; }
        [ByteMember(2, ByteType.String)]
        public string RoleName { get; set; }

        [ByteMember(3, ByteType.Int24)]
        public int Id { get; set; }
        [ByteMember(4, ByteType.Int24Array)]
        public List<int> Ids { get; set; }
    }

    public class Entity
    {
        [ByteMember(0,ByteType.Int8)]
        public int EntityId { get; set; }
    }

    public abstract class Writeable
    {
        public virtual byte[] Write() { throw new NotImplementedException(); }
       
    }

}
