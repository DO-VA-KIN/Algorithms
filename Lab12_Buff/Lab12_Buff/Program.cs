using System;

namespace Lab12_Buff
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            //Generator
            uint[] uints = new uint[100];
            Generator gen = new Generator(78946);
            for (int i = 0; i < 100; i++)
                uints[i] = gen.Next(6);
            //Array.Sort(uints);
            foreach (uint val in uints)
                Console.WriteLine(val);


            //RoundBuffer
            //RoundBuffer<char> rbuff = new RoundBuffer<char>(25);
            //char ch = 'a';
            //for (int i = 0; i < 30; i++)
            //{
            //    rbuff.Write(ch);
            //    ch++;
            //}
            //foreach (var item in rbuff)
            //    Console.WriteLine(item);

            Console.ReadKey();
        }
    }
}
