using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BufferWriteAndRead
{
    public class CodeGenerate
    {
        private const string projectPath = @"E:\BufferWriteAndRead\BufferWriteAndRead\BufferWriteAndRead";

        public static void Run()
        {
            var assmble = Assembly.GetExecutingAssembly();
            var modules = assmble.GetModules();
            var types = new List<Type>();
            foreach (var module in modules)
            {
                var _types = module.GetTypes().Where(i => i.GetCustomAttribute<BtyeContract>() != null).ToList();
                types.AddRange(_types);
            }
            foreach (var type in types)
            {
                // Console.WriteLine(type.Name);  

                var codeClassInfo = new CodeClassInfo();
                codeClassInfo.ClassName = type.Name;
                codeClassInfo.MemberList = new List<CodeMemberInfo>();
                var propertyes = type.GetRuntimeProperties().Where(i => i.GetCustomAttribute<ByteMember>() != null);

                foreach (var property in propertyes)
                {

                    var menberInfo = new CodeMemberInfo();
                    var byteMember = property.GetCustomAttribute<ByteMember>();
                    menberInfo.Name = property.Name;
                    menberInfo.ByteType = byteMember.ByteType;
                    menberInfo.Order = byteMember.Order;
                    menberInfo.TypeName = property.PropertyType.Name;
                    //menberInfo.TypeCode = property.MemberType.GetTypeCode();

                    codeClassInfo.MemberList.Add(menberInfo);
                }

                Console.WriteLine(codeClassInfo.ClassName);
                foreach (var info in codeClassInfo.MemberList.OrderBy(i => i.Order))
                {
                    Console.WriteLine($"name:{info.Name} type:{info.ByteType.ToString()} order:{info.Order}");
                }

                //var path = AppDomain.CurrentDomain.BaseDirectory;
                var codeStr = WriteCode(codeClassInfo);
                var fileName = @"E:\BufferWriteAndRead\BufferWriteAndRead\BufferWriteAndRead\Generate\" + codeClassInfo.ClassName + ".cs";
                var fileStream = File.Create(fileName);
                fileStream.Close();
                File.WriteAllText(fileName, codeStr, Encoding.UTF8);

            }

        }





        public static string WriteCode(CodeClassInfo codeClassInfo)
        {
            var str = WriteHeader(codeClassInfo);
            str += Write_WriteMethod(codeClassInfo);
            str += Write_ReadMethod(codeClassInfo);
            str += WriteFloor();
            return str;
        }


        private static string Get_Class_Header(CodeClassInfo codeClassInfo)
        {
            var str = String.Format(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace BufferWriteAndRead
{{
     public partial class {0}
     {{", codeClassInfo.ClassName);
            return str;
        }

        private static string Get_Class(CodeClassInfo codeClassInfo)
        {
            return null;
        }

        private static string Get_Class_Foot()
        {
            var str = String.Format(@" 
      }}
}}");
            return str;
        }

        
        private static string Get_WriteMethod_Header()
        {
            var str = string.Format(@"  
            public byte[] Write()
             {{
                var buffer = new byte[32];
                var offset = 0;");
            return str;
        }

        private static string Get_WriteMethod()
        {
            return null;
        }

        private static string Get_WriteMethod_Foot()
        {
           var str= string.Format(@"
                return buffer;
              }}");
            return str;
        }


        private static string Get_ReadMethod_Header(CodeClassInfo codeClassInfo)
        {
            var str = string.Format(@"
            public static CreateMsg Read(byte[] buffer,int offset)
              {{
                var msg = new {0}();", codeClassInfo.ClassName);
            return str;
        }

        private static string Get_ReadMethod()
        {
            return null;
        }

        private static string Get_ReadMethod_Foot()
        {

            var str= string.Format(@"
                return msg;
              }}");
            return str;
        }


        private static string Write_WriteMethod(CodeClassInfo codeClassInfo)
        {
            var str = string.Format(@"  
       public byte[] Write()
        {{
            var buffer = new byte[32];
            var offset = 0;"
    );

            foreach (var info in codeClassInfo.MemberList)
            {

                if (info.ByteType == ByteType.String)
                {

                    str += string.Format(@"

           var nameBytes = System.Text.Encoding.UTF8.GetBytes(this.{0});
            buffer[offset] = (byte)nameBytes.Length;
            offset += 1;
            foreach (var _byte in nameBytes)
            {{
                buffer[offset] = _byte;
                offset += 1;
            }}
", info.Name);

                }


                else
                {
                    str += string.Format(@"

           foreach (var _byte in BitConverter.GetBytes(this.{0}))
            {{
                buffer[offset] = _byte;
                offset += 1;
            }}
", info.Name);
                }


            }


            str += string.Format(@"
     return buffer;
        }}

");

            return str;
        }
        private static string Write_ReadMethod(CodeClassInfo codeClassInfo)
        {
            var str = string.Format(@"

        public static CreateMsg Read(byte[] buffer,int offset)
        {{
            var msg = new {0}();

", codeClassInfo.ClassName);

            foreach (var info in codeClassInfo.MemberList)
            {

                switch (info.ByteType)
                {

                    case ByteType.Int8:
                        str += CodeReadHelper.GetInt8(info);
                        break;
                    case ByteType.Uint8:
                        str += CodeReadHelper.GetUInt8(info);
                        break;
                    case ByteType.Int16:
                        str += CodeReadHelper.GetInt16(info.Name);
                        break;
                    case ByteType.Uint16:
                        str += CodeReadHelper.GetUInt16(info.Name);
                        break;
                    case ByteType.Int32:
                        str += CodeReadHelper.GetInt32(info.Name);
                        break;
                    case ByteType.Uint32:
                        str += CodeReadHelper.GetUInt32(info.Name);
                        break;
                    case ByteType.Float32:
                        str += CodeReadHelper.GetFloat32(info.Name);
                        break;
                    case ByteType.Float64:
                        str += CodeReadHelper.GetFloat64(info.Name);
                        break;

                    case ByteType.String:
                        str += CodeReadHelper.GetString(info.Name);
                        break;

                }

            }

            str += string.Format(@"
      return msg;
        }}
");

            return str;
        }



        private static string WriteHeader(CodeClassInfo codeClassInfo)
        {
            var str = String.Format(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace BufferWriteAndRead
{{
 public partial class {0}
    {{


", codeClassInfo.ClassName);

            return str;
        }

        private static string WriteFloor()
        {

            var str = String.Format(@" 
   }}
}}"
    );
            return str;
        }



    }



    public class CodeMemberInfo
    {

        public string Name { get; set; }

        public string TypeName { get; set; }

        //public TypeCode TypeCode { get; set; }

        public ByteType ByteType { get; set; }

        public int Order { get; set; }

    }

    public class CodeClassInfo
    {
        public string ClassName { get; set; }

        public List<CodeMemberInfo> MemberList { get; set; }
    }


    /// <summary>
    /// 根据 每一个 类型生成代码
    /// </summary>
    public class CodeWriteHelper
    {
        private static byte[] buffer = new byte[20];
        private static int offset = 0;

        public static string GetInt8(CodeMemberInfo memberInfo)
        {
            //buffer[offset] = Convert.ToByte(memberInfo.Name); ;
            //offset += 1;

            var str = string.Format(@"  
            buffer[offset] = Convert.ToByte(this.{0});
            offset += 1;", memberInfo.Name);
            return str;
        }
        public static string GetUInt8(CodeMemberInfo memberInfo)
        {
            //buffer[offset] = Convert.ToSByte(memberInfo.Name); ;
            //offset += 1;

            var str = string.Format(@"  
            buffer[offset] = Convert.ToSByte(this.{0});
            offset += 1;", memberInfo.Name);
            return str;
        }
        public static string GetInt16(CodeMemberInfo memberInfo)
        {
            //foreach (var _byte in BitConverter.GetBytes(Convert.ToInt16(12)))
            //{
            //    buffer[offset] = _byte;
            //    offset += 1;
            //}

            var str = string.Format(@"  
            foreach (var _byte in BitConverter.GetBytes(Convert.ToInt16(this.{0})))
            {{
                buffer[offset] = _byte;
                offset += 1;
            }} ", memberInfo.Name);
            return str;

        }
        public static string GetUInt16(CodeMemberInfo memberInfo)
        {
            //foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(12)))
            //{
            //    buffer[offset] = _byte;
            //    offset += 1;
            //}

            var str = string.Format(@"  
            foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(this.{0})))
            {{
                buffer[offset] = _byte;
                offset += 1;
            }} ", memberInfo.Name);
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string GetInt32(CodeMemberInfo memberInfo)
        {
            var str = string.Format(@"  
            foreach (var _byte in BitConverter.GetBytes(this.{0}))
            {{
                buffer[offset] = _byte;
                offset += 1;
            }} ", memberInfo.Name);
            return str;

        }
        public static string GetUInt32(CodeMemberInfo memberInfo)
        {
            //foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt32(12)))
            //{
            //    buffer[offset] = _byte;
            //    offset += 1;
            //}

            var str = string.Format(@"  
            foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt32(this.{0})))
            {{
                buffer[offset] = _byte;
                offset += 1;
            }} ", memberInfo.Name);
            return str;
        }
        public static string GetFloat32(CodeMemberInfo memberInfo)
        {

            var str = string.Format(@"  
            foreach (var _byte in BitConverter.GetBytes(this.{0}))
            {{
                buffer[offset] = _byte;
                offset += 1;
            }} ", memberInfo.Name);
            return str;
        }
        public static string GetFloat64(CodeMemberInfo memberInfo)
        {
            var str = string.Format(@"  
            foreach (var _byte in BitConverter.GetBytes(this.{0}))
            {{
                buffer[offset] = _byte;
                offset += 1;
            }} ", memberInfo.Name);
            return str;
        }
        public static string GetString(CodeMemberInfo memberInfo)
        {
            var str = string.Format(@"
            var nameBytes = System.Text.Encoding.UTF8.GetBytes(this.{0});
            buffer[offset] = (byte)nameBytes.Length;
            offset += 1;
            foreach (var _byte in nameBytes)
            {{
                buffer[offset] = _byte;
                offset += 1;
            }}", memberInfo.Name);

            return str;
        }

        public static string GetObject(CodeMemberInfo memberInfo)
        {
            return string.Empty;
        }

    }


    public class CodeReadHelper
    {

        public static string GetInt8(CodeMemberInfo info)
        {

            if (info.TypeName.Equals("Boolean"))
            {
                var str = string.Format(@"
                  msg.{0}=buffer[offset]==1;
                  offset++;
               ", info.Name, info.TypeName);

                return str;
            }
            else
            {
                var str = string.Format(@"
                  msg.{0}=({1})buffer[offset];
                  offset++;
               ", info.Name, info.TypeName);

                return str;
            }

        }
        public static string GetUInt8(CodeMemberInfo info)
        {
            var str = string.Format(@"
                msg.{0}=({1})buffer[offset];
                offset++;
               ", info.Name, info.TypeName);

            return str;
        }
        public static string GetInt16(string propertName)
        {
            var str = string.Format(@"
                msg.{0}=BitConverter.ToInt16(buffer, offset);
                offset+=2;
               ", propertName);

            return str;
        }
        public static string GetUInt16(string propertName)
        {

            var str = string.Format(@"
                msg.{0}=BitConverter.ToUInt16(buffer, offset);
                offset+=2;
               ", propertName);

            return str;
        }
        public static string GetInt32(string propertName)
        {

            var str = string.Format(@"
                 msg.{0}=BitConverter.ToInt32(buffer, offset);
                 offset+=4;
               ", propertName);

            return str;
        }
        public static string GetUInt32(string propertName)
        {
            var str = string.Format(@"
                 msg.{0}=BitConverter.ToUInt32(buffer, offset);
                 offset+=4;
               ", propertName);

            return str;
        }
        public static string GetFloat32(string propertName)
        {

            var str = string.Format(@"
                 msg.{0}=BitConverter.ToSingle(buffer, offset);
                 offset+=4;
               ", propertName);

            return str;
        }
        public static string GetFloat64(string propertName)
        {
            var str = string.Format(@"
                msg.{0}=BitConverter.ToDouble(buffer, offset);
                offset+=8;
               ", propertName);

            return str;
        }
        public static string GetString(string propertName)
        {
            var str = string.Format(@"
                 var strLength=buffer[offset];
                 offset++;
                 msg.{0}=BitConverter.ToString(buffer, offset,strLength);
                 offset+=8;
               ", propertName);
            return str;
        }
        public static string GetObject()
        {
            return string.Empty;
        }

    }
}
