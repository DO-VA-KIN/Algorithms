using System;
using System.IO;
using System.Numerics;
using System.Xml.Serialization;

namespace Lab12_Buff
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Start!");

            //Generator
            uint[] uints = new uint[1000];
            Generator gen = new();
            for (int i = 0; i < 1000; i++)
                uints[i] = gen.Next(101);
            //Array.Sort(uints);
            foreach (uint val in uints)
                Console.WriteLine(val);

            XmlSerializer xmlSerializer = new(typeof(uint[]));
            File.Delete(@"B:\Projects\university\алгоритмы c#\lab12Generator\lab12Generator\bin\Debug\res.xml");
            using (FileStream fs = new(@"B:\Projects\university\алгоритмы c#\lab12Generator\lab12Generator\bin\Debug\res.xml", FileMode.OpenOrCreate))
            { xmlSerializer.Serialize(fs, uints); }

            //Random random = new Random();

            //RoundBuffer<int?> ints;
            //RoundBuffer<char> rbuff = new(25);
            //char ch = 'a';
            //for (int i = 0; i < 30; i++)
            //{
            //    rbuff.Write(ch);
            //    ch++;
            //}
            //foreach (var item in rbuff)
            //    Console.WriteLine(item);

            //BigInteger

            Console.ReadKey();
        }
    }
}
