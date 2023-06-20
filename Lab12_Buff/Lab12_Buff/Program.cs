using System;
using System.Numerics;

namespace Lab12_Buff
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Start!");

            //Generator
            uint[] uints = new uint[100];
            Generator gen = new(78946);
            for (int i = 0; i < 100; i++)
                uints[i] = gen.Next(6);
            //Array.Sort(uints);
            foreach (uint val in uints)
                Console.WriteLine(val);
            //Random random = new Random();

            //RoundBuffer<int?> ints;
            RoundBuffer<char> rbuff = new(25);
            char ch = 'a';
            for (int i = 0; i < 30; i++)
            {
                rbuff.Write(ch);
                ch++;
            }
            foreach (var item in rbuff)
                Console.WriteLine(item);

            //BigInteger

            Console.ReadKey();
        }
    }
}
