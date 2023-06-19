using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab12_Buff
{
    public class Generator
    {
        private uint Seed;

        public Generator() 
        {
            Seed = (uint)DateTime.Now.TimeOfDay.Ticks;
        }
        public Generator(uint seed)
        {
            Seed = seed;
        }


        //prbs31 = X31+X28+1
        public uint Next(uint mod)
        {
            //Seed >>= 1;
            BitArray bits = new BitArray(BitConverter.GetBytes(Seed));//1
            bits.LeftShift(1);//1
            bits[0] = bits[31] ^ bits[28];
            uint[] uints = new uint[1];
            bits.CopyTo(uints, 0);
            Seed = uints[0];
            return Seed % mod;
        }

    }
}
