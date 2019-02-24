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
            var projectPath = Path.GetFullPath("../../..");

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
                var codeClassInfo = new CodeClassInfo();
                codeClassInfo.ClassName = type.Name;
                codeClassInfo.NameSpace = type.Namespace;
                codeClassInfo.MemberList = new List<CodeMemberInfo>();
                var propertyes = type.GetRuntimeProperties().Where(i => i.GetCustomAttribute<ByteMember>() != null);

                foreach (var property in propertyes)
                {
                    var menberInfo = new CodeMemberInfo();
                    var byteMember = property.GetCustomAttribute<ByteMember>();
                    menberInfo.Name = property.Name;
                    menberInfo.ByteType = byteMember.ByteType;
                    menberInfo.Order = byteMember.Order;
                    if (byteMember.TypeName == null)
                    {
                        menberInfo.TypeName = property.PropertyType.Name;
                    }
                    else
                    {
                        menberInfo.TypeName = byteMember.TypeName;
                    }
                    
                    codeClassInfo.MemberList.Add(menberInfo);
                }
                codeClassInfo.MemberList = codeClassInfo.MemberList.OrderBy(i => i.Order).ToList();


                var nameSpaces = codeClassInfo.NameSpace.Split(".");
                var inerPath = string.Empty;
                for (var i = 1; i < nameSpaces.Length; i++)
                {
                    inerPath += "\\" + nameSpaces[i];
                }

                var fileDir = projectPath + inerPath + @"\Generate\";
                if (!Directory.Exists(fileDir)) Directory.CreateDirectory(fileDir);
                var fileName = projectPath + inerPath + @"\Generate\" + codeClassInfo.ClassName + ".cs";
                if (File.Exists(fileName)) continue;
                var fileStream = File.Create(fileName);
                fileStream.Close();
                var codeStr = Get_Class(codeClassInfo);
                File.WriteAllText(fileName, codeStr, Encoding.UTF8);
            }

        }


        private static string Get_Class(CodeClassInfo codeClassInfo)
        {
            var str = Get_Class_Header(codeClassInfo);
            str += Get_WriteMethod(codeClassInfo);
            str += Get_ReadMethod(codeClassInfo);
            str += Get_Class_Foot();
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
namespace {1}
{{
     public partial class {0}
     {{", codeClassInfo.ClassName, codeClassInfo.NameSpace);
            return str;
        }

        private static string Get_Class_Foot()
        {
            var str = String.Format(@" 
      }}
}}");
            return str;
        }

        private static string Get_WriteMethod(CodeClassInfo codeClassInfo)
        {
            var str = CodeWriteHelper.Get_Method_Header();
            foreach (var info in codeClassInfo.MemberList)
            {
                switch (info.ByteType)
                {
                    case ByteType.Int8:
                        str += CodeWriteHelper.GetInt8(info);
                        break;
                    case ByteType.UInt8:
                        str += CodeWriteHelper.GetUInt8(info);
                        break;
                    case ByteType.Int16:
                        str += CodeWriteHelper.GetInt16(info);
                        break;
                    case ByteType.Uint16:
                        str += CodeWriteHelper.GetUInt16(info);
                        break;
                    case ByteType.Int32:
                        str += CodeWriteHelper.GetInt32(info);
                        break;
                    case ByteType.Uint32:
                        str += CodeWriteHelper.GetUInt32(info);
                        break;
                    case ByteType.Float32:
                        str += CodeWriteHelper.GetFloat32(info);
                        break;
                    case ByteType.Float64:
                        str += CodeWriteHelper.GetFloat64(info);
                        break;
                    case ByteType.String:
                        str += CodeWriteHelper.GetString(info);
                        break;

                    case ByteType.Object:
                        str += CodeWriteHelper.GetObject(info);
                        break;

                    // array
                    case ByteType.BoolArray:
                        str += CodeWriteHelper.GetArrayBool(info);
                        break;
                    case ByteType.Int8Array:
                        str += CodeWriteHelper.GetArrayInt8(info);
                        break;
                    case ByteType.UInt8Array:
                        str += CodeWriteHelper.GetArrayUInt8(info);
                        break;
                    case ByteType.Int16Array:
                        str += CodeWriteHelper.GetArrayInt16(info);
                        break;
                    case ByteType.Uint16Array:
                        str += CodeWriteHelper.GetArrayUInt16(info);
                        break;
                    case ByteType.Int32Array:
                        str += CodeWriteHelper.GetArrayInt32(info);
                        break;
                    case ByteType.Uint32Array:
                        str += CodeWriteHelper.GetArrayUInt32(info);
                        break;
                    case ByteType.Float32Array:
                        str += CodeWriteHelper.GetArrayFloat32(info);
                        break;
                    case ByteType.Float64Array:
                        str += CodeWriteHelper.GetArrayFloat64(info);
                        break;
                    case ByteType.StringArray:
                        str += CodeWriteHelper.GetArrayString(info);
                        break;
                    case ByteType.ObjectArray:
                        str += CodeWriteHelper.GetArrayObject(info);
                        break;
                }
            }
            str += CodeWriteHelper.Get_Method_Foot();
            return str;
        }

        private static string Get_ReadMethod(CodeClassInfo codeClassInfo)
        {

            var str = CodeReadHelper.Get_Method_Header(codeClassInfo);
            foreach (var info in codeClassInfo.MemberList)
            {
                switch (info.ByteType)
                {
                    case ByteType.Int8:
                        str += CodeReadHelper.GetInt8(info);
                        break;
                    case ByteType.UInt8:
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
                    case ByteType.Object:
                        str += CodeReadHelper.GetObject(info);
                        break;

                    //arrary
                    case ByteType.BoolArray:
                        str += CodeReadHelper.GetArrayBool(info);
                        break;
                    case ByteType.Int8Array:
                        str += CodeReadHelper.GetArrayInt8(info);
                        break;
                    case ByteType.UInt8Array:
                        str += CodeReadHelper.GetArrayUInt8(info);
                        break;
                    case ByteType.Int16Array:
                        str += CodeReadHelper.GetArrayInt16(info.Name);
                        break;
                    case ByteType.Uint16Array:
                        str += CodeReadHelper.GetArrayUInt16(info.Name);
                        break;
                    case ByteType.Int32Array:
                        str += CodeReadHelper.GetArrayInt32(info.Name);
                        break;
                    case ByteType.Uint32Array:
                        str += CodeReadHelper.GetArrayUInt32(info.Name);
                        break;
                    case ByteType.Float32Array:
                        str += CodeReadHelper.GetArrayFloat32(info.Name);
                        break;
                    case ByteType.Float64Array:
                        str += CodeReadHelper.GetArrayFloat64(info.Name);
                        break;
                    case ByteType.StringArray:
                        str += CodeReadHelper.GetArrayString(info.Name);
                        break;
                    case ByteType.ObjectArray:
                        str += CodeReadHelper.GetArrayObject(info);
                        break;
                }
            }
            str += CodeReadHelper.Get_Method_Foot();
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

        public string NameSpace { get; set; }

        public List<CodeMemberInfo> MemberList { get; set; }
    }


    /// <summary>
    /// 根据 每一个 类型生成代码
    /// </summary>
    public class CodeWriteHelper
    {
        private static byte[] buffer = new byte[20];
        private static int offset = 0;

        public static string Get_Method_Header()
        {
            var str = string.Format(@"  
        public byte[] Write()
        {{
            var buffer = new byte[64];
            var offset = 0;");
            return str;
        }

        public static string Get_Method_Foot()
        {
            var str = string.Format(@"
            return new ArraySegment<byte>(buffer, 0, offset).ToArray();
        }}");
            return str;
        }

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
            buffer[offset] = Convert.ToByte(this.{0});
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
            var nameBytes = System.Text.Encoding.Unicode.GetBytes(this.{0});
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
            //if (this.{0} == null)
            //{
            //    buffer[offset] = 0;
            //    offset += 1;
            //}
            //else
            //{
            //    var _buffer = this.{0}.Write();
            //    buffer[offset] = (byte)_buffer.Length;
            //    buffer.Concat(_buffer);
            //    offset += _buffer.Length;
            //}

            var str = string.Format(@"
            if (this.{0} == null)
            {{
                buffer[offset] = 0;
                offset += 1;
            }}
            else
            {{
                var _buffer = this.{0}.Write();
                buffer[offset] = (byte)_buffer.Length;
                buffer.Concat(_buffer);
                offset += _buffer.Length;
            }}", memberInfo.Name);
            return str;
        }



        #region array
        public static string GetArrayBool(CodeMemberInfo memberInfo)
        {
           
            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
                foreach (var item in this.{0})
                {{
                     buffer[offset] = (byte)(item?1:0);
                     offset++;
                }}
             }}", memberInfo.Name);
            return str;
        }
        public static string GetArrayInt8(CodeMemberInfo memberInfo)
        {
            //var length = this.{ 0} == null ? 0 : this.{0}.Count;
            //buffer[offset] = (byte)length;
            //offset++;
            //foreach (var id in this.{0})
            //{
            //    buffer[offset] = (byte)id;
            //    offset++;
            //}

            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
                foreach (var id in this.{0})
                {{
                    buffer[offset] = (byte)id;
                    offset++;
            }}}}", memberInfo.Name);
            return str;
        }
        public static string GetArrayUInt8(CodeMemberInfo memberInfo)
        {
            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var id in this.{0})
            {{
                buffer[offset] = (byte)id;
                offset++;
            }}}}", memberInfo.Name);
            return str;
        }
        public static string GetArrayInt16(CodeMemberInfo memberInfo)
        {
            //var count = this.{0} == null ? 0 : this.{0}.Count;
            //buffer[offset] = (byte)count;
            //offset++;
            //foreach (var item in this.{0})
            //{
            //    foreach (var _byte in BitConverter.GetBytes(Convert.ToInt16(item)))
            //    {
            //        buffer[offset] = _byte;
            //        offset += 1;
            //    }
            //}

            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var item in this.{0})
            {{
                foreach (var _byte in BitConverter.GetBytes(Convert.ToInt16(item)))
                {{
                    buffer[offset] = _byte;
                    offset ++;
                }}
            }}}}", memberInfo.Name);
            return str;

        }
        public static string GetArrayUInt16(CodeMemberInfo memberInfo)
        {
            //foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(12)))
            //{
            //    buffer[offset] = _byte;
            //    offset += 1;
            //}

            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var item in this.{0})
            {{
                foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt16(item)))
                {{
                    buffer[offset] = _byte;
                    offset ++;
                }}
            }}}}", memberInfo.Name);
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string GetArrayInt32(CodeMemberInfo memberInfo)
        {
            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var item in this.{0})
            {{
                foreach (var _byte in BitConverter.GetBytes(item))
                {{
                    buffer[offset] = _byte;
                    offset ++;
                }}
            }}}}", memberInfo.Name);
            return str;
        }
        public static string GetArrayUInt32(CodeMemberInfo memberInfo)
        {
            //foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt32(12)))
            //{
            //    buffer[offset] = _byte;
            //    offset += 1;
            //}

            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var item in this.{0})
            {{
                foreach (var _byte in BitConverter.GetBytes(Convert.ToUInt32(item))))
                {{
                    buffer[offset] = _byte;
                    offset ++;
                }}
            }}}}", memberInfo.Name);
            return str;

        }
        public static string GetArrayFloat32(CodeMemberInfo memberInfo)
        {
            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var item in this.{0})
            {{
                foreach (var _byte in BitConverter.GetBytes(item))
                {{
                    buffer[offset] = _byte;
                    offset ++;
                }}
            }}}}", memberInfo.Name);
            return str;
        }
        public static string GetArrayFloat64(CodeMemberInfo memberInfo)
        {
            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var item in this.{0})
            {{
                foreach (var _byte in BitConverter.GetBytes(item))
                {{
                    buffer[offset] = _byte;
                    offset ++;
                }}
            }}}}", memberInfo.Name);
            return str;
        }
        public static string GetArrayString(CodeMemberInfo memberInfo)
        {


            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)  
            {{ 
            foreach (var item in this.{0})
            {{
                 var nameBytes = System.Text.Encoding.Unicode.GetBytes(item);
                 buffer[offset] = (byte)nameBytes.Length;
                 offset += 1;
                 foreach (var _byte in nameBytes)
                 {{
                    buffer[offset] = _byte;
                    offset += 1;
                 }}
            }}}}", memberInfo.Name);
            return str;
        }

        public static string GetArrayObject(CodeMemberInfo memberInfo)
        {
            //对象数组
            //var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            //buffer[offset] = (byte)count{0};
            //offset++;
            //if (count{0} > 0)
            //{
            //    foreach (var item in this.{0})
            //    {
            //        if (item == null)
            //        {
            //            buffer[offset] = 0;
            //            offset += 1;
            //        }
            //        else
            //        {
            //            var _buffer = item.Write();
            //            buffer[offset] = (byte)_buffer.Length;
            //            buffer.Concat(_buffer);
            //            offset += _buffer.Length;
            //        }
            //    }
            //}

            var str = string.Format(@"  
            var count{0} = this.{0} == null ? 0 : this.{0}.Count;
            buffer[offset] = (byte)count{0};
            offset++;
            if (count{0} > 0)
            {{
                foreach (var item in this.{0})
                {{
                    if (item == null)
                    {{
                        buffer[offset] = 0;
                        offset += 1;
                    }}
                    else
                    {{
                        var _buffer = item.Write();
                        buffer[offset] = (byte)_buffer.Length;
                        buffer.Concat(_buffer);
                        offset += _buffer.Length;
                    }}
                }}
            }}", memberInfo.Name);
            return str;
        }
        #endregion

    }


    public class CodeReadHelper
    {


        public static string Get_Method_Header(CodeClassInfo codeClassInfo)
        {
            var str = string.Format(@"
        public static {0} Read(byte[] buffer,int offset)
        {{
            var msg = new {0}();", codeClassInfo.ClassName);
            return str;
        }

        public static string Get_Method_Foot()
        {
            var str = string.Format(@"
            return msg;
        }}");
            return str;
        }




        public static string GetBool(CodeMemberInfo info)
        {
            var str = string.Format(@"
            msg.{0}=buffer[offset]==1;
            offset++; ", info.Name, info.TypeName);
            return str;
        }
        public static string GetInt8(CodeMemberInfo info)
        {
            var str = string.Format(@"
            msg.{0}=({1})buffer[offset];
            offset++; ", info.Name, info.TypeName);
            return str;
        }
        public static string GetUInt8(CodeMemberInfo info)
        {
            var str = string.Format(@"
            msg.{0}=({1})buffer[offset];
            offset++;", info.Name, info.TypeName);
            return str;
        }
        public static string GetInt16(string propertName)
        {
            var str = string.Format(@"
            msg.{0}=BitConverter.ToInt16(buffer, offset);
            offset+=2;", propertName);
            return str;
        }
        public static string GetUInt16(string propertName)
        {

            var str = string.Format(@"
            msg.{0}=BitConverter.ToUInt16(buffer, offset);
            offset+=2;", propertName);
            return str;
        }
        public static string GetInt32(string propertName)
        {

            var str = string.Format(@"
            msg.{0}=BitConverter.ToInt32(buffer, offset);
            offset+=4;", propertName);
            return str;
        }
        public static string GetUInt32(string propertName)
        {
            var str = string.Format(@"
            msg.{0}=BitConverter.ToUInt32(buffer, offset);
            offset+=4;", propertName);
            return str;
        }
        public static string GetFloat32(string propertName)
        {

            var str = string.Format(@"
            msg.{0}=BitConverter.ToSingle(buffer, offset);
            offset+=4;", propertName);
            return str;
        }
        public static string GetFloat64(string propertName)
        {
            var str = string.Format(@"
            msg.{0}=BitConverter.ToDouble(buffer, offset);
            offset+=8;", propertName);
            return str;
        }
        public static string GetString(string propertName)
        {
            var str = string.Format(@"
            var strLength=buffer[offset];
            offset++;
            msg.{0}=System.Text.Encoding.Unicode.GetString(buffer, offset,strLength);
            offset+=strLength;", propertName);
            return str;
        }
        public static string GetObject(CodeMemberInfo info)
        {
            var str = string.Format(@"
            var {0}IsNull = buffer[offset];
            offset++;
            if ({0}IsNull == 0)
            {{
                msg.{0} = null;
            }}
            else
            {{
                var roleLength = buffer[offset];
                offset++;
                var _buffer = new ArraySegment<byte>(buffer, offset, roleLength).ToArray();
                msg.{0} = {1}.Read(_buffer, 0);
                offset += roleLength;
            }}", info.Name,info.TypeName);
            return str;
        }

        #region Array

        public static string GetArrayBool(CodeMemberInfo info)
        {
            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<bool>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = buffer[offset]==1;
                offset++;
                list{0}.Add(item);
            }}
            msg.{0} = list{0}; ", info.Name, info.TypeName);
            return str;

        }

        public static string GetArrayInt8(CodeMemberInfo info)
        {
            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<byte>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = buffer[offset];
                offset++;
                list{0}.Add(item);
            }}
            msg.{0} = list{0}; ", info.Name, info.TypeName);
            return str;
        }

        public static string GetArrayUInt8(CodeMemberInfo info)
        {
            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<byte>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = buffer[offset];
                offset++;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", info.Name);
            return str;
        }
        public static string GetArrayInt16(string propertName)
        {

            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<int>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = BitConverter.ToInt16(buffer, offset);
                offset += 2;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", propertName);
            return str;
        }
        public static string GetArrayUInt16(string propertName)
        {
            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<int>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = BitConverter.ToUInt16(buffer, offset);
                offset += 2;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", propertName);
            return str;

            //var str = string.Format(@"
            //msg.{0}=BitConverter.ToUInt16(buffer, offset);
            //offset+=2;", propertName);
            //return str;
        }
        public static string GetArrayInt32(string propertName)
        {

            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<int>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", propertName);
            return str;

            //var str = string.Format(@"
            //msg.{0}=BitConverter.ToInt32(buffer, offset);
            //offset+=4;", propertName);
            //return str;
        }
        public static string GetArrayUInt32(string propertName)
        {
            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<UInt32>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = BitConverter.ToUInt32(buffer, offset);
                offset += 4;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", propertName);
            return str;

            //var str = string.Format(@"
            //msg.{0}=BitConverter.ToUInt32(buffer, offset);
            //offset+=4;", propertName);
            //return str;
        }
        public static string GetArrayFloat32(string propertName)
        {
            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<float>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = BitConverter.ToSingle(buffer, offset);
                offset += 4;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", propertName);
            return str;

            //var str = string.Format(@"
            //msg.{0}=BitConverter.ToSingle(buffer, offset);
            //offset+=4;", propertName);
            //return str;
        }
        public static string GetArrayFloat64(string propertName)
        {

            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<double>();
            for (var i = 0; i < count{0}; i++)
            {{
                var item = BitConverter.ToDouble(buffer, offset);
                offset += 8;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", propertName);
            return str;

            //var str = string.Format(@"
            //msg.{0}=BitConverter.ToDouble(buffer, offset);
            //offset+=8;", propertName);
            //return str;
        }
        public static string GetArrayString(string propertName)
        {

            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{0} = new List<string>();
            for (var i = 0; i < count{0}; i++)
            {{
                var strLength=buffer[offset];
                offset++;
                var item=System.Text.Encoding.Unicode.GetString(buffer, offset,strLength);
                offset+=strLength;
                list{0}.Add(item);
            }}
            msg.{0} = list{0};", propertName);
            return str;

            //var str = string.Format(@"
            //var strLength=buffer[offset];
            //offset++;
            //msg.{0}=BitConverter.ToString(buffer, offset,strLength);
            //offset+=strLength;", propertName);
            //return str;
        }


        public static string GetArrayObject(CodeMemberInfo memberInfo)
        {
            //var count{0} = buffer[offset];
            //offset++;
            //var list{1}List = new List<{1}>();
            //for (var i = 0; i < count{0}; i++)
            //{
            //    var {1}IsNull = buffer[offset];
            //    offset++;
            //    if ({1}IsNull == 0)
            //    {
            //        list{1}List.Add(null);
            //    }
            //    else
            //    {
            //        var {0}Length = buffer[offset];
            //        offset++;
            //        var _buffer = new ArraySegment<byte>(buffer, offset, {0}Length).ToArray();
            //        list{1}List.Add({0}.Read(_buffer, 0));
            //        offset += {0}Length;
            //    }
            //}
            //msg.RoleList = list{1}List;

            var str = string.Format(@"
            var count{0} = buffer[offset];
            offset++;
            var list{1}List = new List<{1}>();
            for (var i = 0; i < count{0}; i++)
            {{
                var _{1}IsNull = buffer[offset];
                offset++;
                if (_{1}IsNull == 0)
                {{
                    list{1}List.Add(null);
                }}
                else
                {{
                    var {0}Length = buffer[offset];
                    offset++;
                    var _buffer = new ArraySegment<byte>(buffer, offset, {0}Length).ToArray();
                    list{1}List.Add({1}.Read(_buffer, 0));
                    offset += {0}Length;
                }}
            }}
            msg.{0} = list{1}List;", memberInfo.Name, memberInfo.TypeName);

            return str;
        }

        #endregion

    }
}
