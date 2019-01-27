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
        public static void Run()
        {
            var assmble = Assembly.GetExecutingAssembly();
            var modules= assmble.GetModules();
            var types =new List<Type>();
            foreach(var module in modules)
            {
                var _types = module.GetTypes().Where(i=>i.GetCustomAttribute<BtyeContract>()!=null).ToList();
                types.AddRange(_types);
            }
            foreach (var type in types)
            {
                // Console.WriteLine(type.Name);  

                var codeClassInfo = new CodeClassInfo();
                codeClassInfo.ClassName = type.Name;
                codeClassInfo.MemberList = new List<CodeMemberInfo>();
                var propertyes = type.GetRuntimeProperties().Where(i=>i.GetCustomAttribute<ByteMember>()!=null);

                foreach(var property in propertyes)
                {

                    var menberInfo = new CodeMemberInfo();
                    var byteMember=  property.GetCustomAttribute<ByteMember>();
                    menberInfo.Name = property.Name;
                    menberInfo.ByteType = byteMember.ByteType;
                    menberInfo.Order = byteMember.Order;
                    codeClassInfo.MemberList.Add(menberInfo);
                }

                Console.WriteLine(codeClassInfo.ClassName);
                foreach(var info in codeClassInfo.MemberList.OrderBy(i=>i.Order))
                {
                    Console.WriteLine($"name:{info.Name} type:{info.ByteType.ToString()} order:{info.Order}");
                }


                var codeStr = WriteCode(codeClassInfo);
                var fileName = @"E:\BufferWriteAndRead\BufferWriteAndRead\BufferWriteAndRead\Generate\" + codeClassInfo.ClassName + ".cs";
                var fileStream= File.Create(fileName);
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




        private static string Write_WriteMethod(CodeClassInfo codeClassInfo)
        {
            var str = string.Format(@"  
       public byte[] Write()
        {{
            var buffer = new byte[32];
            var offset = 0;"
);
           
            foreach(var info in codeClassInfo.MemberList)
            {
                if (info.ByteType == ByteType.Int8)
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
            var str = string.Format(@"");
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


        public class CodeMemberInfo
        {

            public string Name { get; set; }

            public ByteType ByteType { get; set; }

            public int Order { get; set; }

        }

        public class CodeClassInfo
        {
            public string ClassName { get; set; }
           
            public List<CodeMemberInfo> MemberList { get; set; }
        }

    }
}
