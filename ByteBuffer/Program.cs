﻿using ByteBuffer.Entitys;
using Newtonsoft.Json;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBuffer
{
    class Program
    {

        public static byte[] Clientbuffer = new byte[1024 * 10];

        public static Action<string> onTest;
        public static class BitTools
        {
            public static int SignExtend32(int value, int bits)
            {
                int shift = 8 * sizeof(int) - bits;
                return (value << shift) >> shift;
            }
        }
        static void Main(string[] args)
        {
            var bytes0= System.Text.Encoding.Unicode.GetBytes("sssssssssss 你好");

            var bytes = UInt24.GetBytes(UInt24.MaxVlaue);
            var value = UInt24.ReadUInt24(bytes, 0);

            var bytes4 = Int24.GetBytes(Int24.MiniValue);
            var bytes2 = Int24.GetBytes(-12);
            var value2 = Int24.ReadInt24(bytes4, 0);

            CodeGenerate.Run();


            return;




            //var client = new Client();
            //client.ClientId = 0;
            //client.StarThreadRun();

            //var client1 = new Client();
            //client1.ClientId = 1;
            //client1.StarThreadRun();
            ////10 个 线程进行读写，1 线程进行处理


            //var server = new Server();
            //server.StarThreadRun();
            ///// <summary>
            ///// 读写
            ///// </summary>
            //Thread.Sleep(1000000);

        }


        public static void TestUser()
        {

            var _role = new Role();
            _role.RoleId = 2;
            _role.RoleName = "admin";

            var roleBuffer = _role.Write();
            var _role1 = Role.Read(roleBuffer, 0);


            var user = new User();
            user.Bool = false;
            user.boolList = new List<bool>() { false, true };
            //user.Byte = 1;
            user.Char = 'A';
            //user.Float = 33.43F;
            user.floatList = new List<float>() { 12.1F, 12.1F };
            user.Id = 1;
            user.IdList = new List<int>() { 1, 2, 3 };
            user.StringList = new List<string>() { "SS", "ERC" };
            user.UInt16 = 12;
            //user.SByte = 1;
            user.UShort = 2342;
            var role = new Role();
            role.RoleId = 2;
            role.RoleName = "admin";
            user.Role = role;
            var roleList = new List<Role>();
            var role1 = new Role();
            role1.RoleId = 3;
            role1.RoleName = "user1";
            roleList.Add(role1);
            var role2 = new Role();
            role2.RoleId = 4;
            role2.RoleName = "user2";
            roleList.Add(role2);
            user.RoleList = roleList;

            //var buffer = user.Write();
            //var user1 = User.Read(buffer, 0);

            //性能对比
            //var wathc = new Stopwatch();
            //wathc.Start();
            //byte[] buffer=null;
            //for (var i = 0; i < 100000; i++)
            //{
            //    buffer = user.Write();
            //    var user1 = User.Read(buffer, 0);

            //}
            //wathc.Stop();
            //Console.WriteLine(buffer.Length);
            //Console.WriteLine("json:" + wathc.ElapsedMilliseconds);
            //wathc.Reset();
            //wathc.Restart();
            //var json = string.Empty;
            //for (var i = 0; i < 100000; i++)
            //{
            //    json = JsonConvert.SerializeObject(user);
            //    JsonConvert.DeserializeObject(json);
            //}
            //wathc.Stop();
            //Console.WriteLine(Encoding.Unicode.GetBytes(json).Length);
            //Console.WriteLine("btye:" + wathc.ElapsedMilliseconds);
        }





    }

    //public abstract class EntityBase
    //{
    //    public static virtual void Say()
    //    {
    //        Console.WriteLine("EntityBase");
    //    }

    //}

    //public class Etity1 : EntityBase
    //{
    //    public override void Say()
    //    {
    //        Console.WriteLine("Etity1");
    //    }
    //}

    //public class Server
    //{
    //    public void StarThreadRun()
    //    {
    //        Task.Factory.StartNew(() =>
    //        {
    //            this.ReadBuffer();
    //        });
    //    }

    //    private void ReadBuffer()
    //    {

    //        while (true)
    //        {
    //            Thread.Sleep(20);//ms
    //            var index = 0;
    //            for (int i = 0; i < 1024 * 10; i = i + 32)
    //            {

    //                var rw = (byte)Program.Clientbuffer[index];

    //                if (rw == (byte)1) break;
    //                index += 32;
    //            }
    //            if (index > 1024 * 10 - 1) continue;
    //            var type = Program.Clientbuffer[index + 1];

    //            if (type == (byte)MsgType.type1)
    //            {
    //                var msg1 = new Msg1();
    //                msg1.ReadMsg(index);
    //                var jsonStr = JsonConvert.SerializeObject(msg1, Formatting.Indented);
    //                Console.WriteLine(jsonStr);
    //            }
    //        }

    //    }

    //}


    //public class Client
    //{
    //    public int ClientId { get; set; }

    //    public void StarThreadRun()
    //    {
    //        Task.Factory.StartNew(() =>
    //        {
    //            this.WriteBuffer();
    //        });
    //    }




    //    /// <summary>
    //    /// 写
    //    /// </summary>
    //    private void WriteBuffer()
    //    {
    //        while (true)
    //        {

    //            Thread.Sleep(50);//MS
    //            var index = this.ClientId * 1024;

    //            for (int i = 0; i < 1024; i = i + 32)
    //            {
    //                var rw = (byte)Program.Clientbuffer[index];

    //                if (rw == (byte)0) break;
    //                index += 32;
    //            }
    //            if (index > this.ClientId * 1024 + 1024) continue;

    //            var msg1 = new Msg1();
    //            msg1.WriteMsg(index);


    //        }
    //    }



    //}


    public partial class Msg1 : IMsg
    {
        [ByteMember(1, ByteType.Int8)]
        public MsgType MsgType { get; set; } = MsgType.type1;
        [ByteMember(2, ByteType.Int8)]
        public ushort Id { get; set; } = 12;
        [ByteMember(3, ByteType.Int8)]
        public bool IsSuccess { get; set; } = true;
        [ByteMember(4, ByteType.Int8)]
        public string Name { get; set; } = "你好";
        [ByteMember(5, ByteType.Int8)]
        public long dateTime { get; set; } = DateTime.Now.Ticks;



        public override void ReadMsg(int index)
        {
            var _index = index;
            index += 2;//不读 type



            var IdBytes = Program.Clientbuffer.AsSpan(index, 2);
            this.Id = BitConverter.ToUInt16(IdBytes);
            index += 2;

            var isSuccessBytes = Program.Clientbuffer.AsSpan(index, 1);
            this.IsSuccess = BitConverter.ToBoolean(isSuccessBytes);
            index += 1;

            var nameLength = (int)Program.Clientbuffer[index];
            index += 1;
            this.Name = System.Text.Encoding.UTF8.GetString(Program.Clientbuffer, index, nameLength);
            index += nameLength;

            var timeBytes = Program.Clientbuffer.AsSpan(index, 8);
            this.dateTime = BitConverter.ToInt64(timeBytes);
            index += 8;

            Program.Clientbuffer[_index] = (byte)0; //0 已经写完 待写
        }

        public override void WriteMsg(int index)
        {
            var _index = index;

            index += 1;
            Program.Clientbuffer[index] = (byte)this.MsgType; //1 位： type

            var idBytes = BitConverter.GetBytes(this.Id);
            foreach (var _byte in idBytes)
            {
                index += 1;
                Program.Clientbuffer[index] = _byte;
            }

            var isSuccessBytes = BitConverter.GetBytes(this.IsSuccess);
            foreach (var _byte in isSuccessBytes)
            {
                index += 1;
                Program.Clientbuffer[index] = _byte;
            }

            var nameBytes = System.Text.Encoding.UTF8.GetBytes(this.Name);

            index += 1;
            Program.Clientbuffer[index] = (byte)nameBytes.Length;

            foreach (var _byte in nameBytes)
            {
                index += 1;
                Program.Clientbuffer[index] = _byte;
            }

            var timeBytes = BitConverter.GetBytes(this.dateTime);
            foreach (var _byte in timeBytes)
            {
                index += 1;
                Program.Clientbuffer[index] = _byte;
            }

            Program.Clientbuffer[_index] = 1;//1 已经写完 待读状态





        }
    }


    public class Msg2 : IMsg
    {
        public MsgType MsgType { get; set; } = MsgType.type2;

        public float Px { get; set; } = 23.34f;

        public float Py { get; set; } = 5534f;

        public override void ReadMsg(int index)
        {
            var _index = index;
            index += 2;//不读 type

            var PxBytes = Program.Clientbuffer.AsSpan(index, 4);
            this.Px = BitConverter.ToSingle(PxBytes);
            index += 4;

            var PyBytes = Program.Clientbuffer.AsSpan(index, 4);
            this.Py = BitConverter.ToSingle(PyBytes);
            index += 4;
            Program.Clientbuffer[_index] = (byte)0; //0 已经写完 待写
        }

        public override void WriteMsg(int index)
        {
            var _index = index;

            index += 1;
            Program.Clientbuffer[index] = (byte)this.MsgType; //1 位： type

            var pxBytes = BitConverter.GetBytes(this.Px);
            foreach (var _byte in pxBytes)
            {
                index += 1;
                Program.Clientbuffer[index] = _byte;
            }

            var pyBytes = BitConverter.GetBytes(this.Py);
            foreach (var _byte in pyBytes)
            {
                index += 1;
                Program.Clientbuffer[index] = _byte;
            }
            Program.Clientbuffer[_index] = 1;//1 已经写完 待读状态
        }
    }

    public enum MsgType
    {
        type1 = 1,
        type2 = 2,
    }


    public abstract class IMsg
    {
        public abstract void WriteMsg(int index);

        public abstract void ReadMsg(int index);

    }
}
