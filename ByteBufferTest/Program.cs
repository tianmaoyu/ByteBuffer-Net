using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteBufferTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Test> list = new List<Test>();
            var ids= list.Select(i => i.Id);
            var idList = new List<int>();
            idList.Add(1);
            idList.RemoveAll(i => ids.Contains(i));
            Console.WriteLine("Hello World!");
        }
    }

    public class Test
    {
        public int Id { get; set; }

    }
}
