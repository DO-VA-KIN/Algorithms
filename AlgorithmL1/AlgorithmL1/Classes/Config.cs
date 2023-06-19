using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AlgorithmL1.Classes
{
    public static class Config
    {
        public static DispatcherTimer Timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 15) };
        public static readonly int[] Range = { 1000, 2000, 4000, 8000, 16000, 32000, 64000, 128000 };
        public static readonly string[] Methods = { "встроенная", "пузырьком", "перемешиванием", "вставками", "выбором", "быстрая", "слияние" };
        public static short CurrentSortMethod = 0;
        public static Label[] Labels;
        public static ListBox[] ListBoxes;
        public static BackgroundWorker[] Backs = new BackgroundWorker[8];

        public static double[][] Doubles = new double[][]
            {
            new double[0],
            new double[0],
            new double[0],
            new double[0],
            new double[0],
            new double[0],
            new double[0],
            new double[0]
            };

        public static Dictionary<string, Method> Results = new Dictionary<string, Method>(5);
    };

    public struct Method
    {
        public TimeSpan[] LeftTime;
        public long[] UsedMemory;
    }
}
