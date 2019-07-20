using ByteBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteBufferTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            //CodeGenerate.Run(assembly);
            TestUser();
        }


        static void TestUser()
        {

            var _role = new RoleTest();
            _role.RoleId = 2;
            _role.RoleName = "admin";
            var roleBuffer = _role.Write();
            var _role1 = RoleTest.Read(roleBuffer, 0);


            var user = new UserTest();
            user.Bool = false;
            user.boolList = new List<bool>() { false, true };
            user.Char = 'A';
            user.floatList = new List<float>() { 12.1F, 12.1F };
            user.Id = 1;
            user.IdList = new List<int>() { 1, 2, 3 };
            user.StringList = new List<string>() { "SS", "ERC" };
            user.UInt16 = 12;
            user.UShort = 2342;
            var role = new RoleTest();
            role.RoleId = 2;
            role.RoleName = "admin";
            role.Id = -122334;
            role.Ids = new List<int>() { Int24.MiniValue, Int24.MaxVlaue, 0 };
            user.Role = role;
            var roleList = new List<RoleTest>();
            var role1 = new RoleTest();
            role1.RoleId = 3;
            role1.Id = 1222;
            role1.RoleName = "user1";
            roleList.Add(role1);
            var role2 = new RoleTest();
            role2.RoleId = 4;
            role2.Id = -3737;
            role2.RoleName = "user2";
            roleList.Add(role2);
            roleList.Add(role);
            user.RoleList = roleList;

            var buffer = user.Write();
            var _user = UserTest.Read(buffer, 0);
            ;

        }
    }


}
