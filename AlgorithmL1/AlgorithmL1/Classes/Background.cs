using System;
using System.Collections.Generic;
using System.Management;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace AlgorithmL1
{
    public class Background
    {
        public delegate void Complete(Background B);
        public event Complete CompleteSort;

        private double[] SortArray;

        public TimeSpan LeftTime;
        public BackgroundWorker Back;
        public short NumAlgo;
        public short NumBack;

        public Background(BackgroundWorker back, short numBack, ref double[] sortArray, short numAlgo)
        {
            LeftTime = new TimeSpan(0, 0, 0);
            SortArray = sortArray;
            NumAlgo = numAlgo;
            NumBack = numBack;
            Back = back;           
            Back.DoWork += Back_DoWork;
            Back.RunWorkerCompleted += Back_RunWorkerCompleted;
        }

        public void Start()
        {
            Back.RunWorkerAsync();
        }

        private void Back_DoWork(object sender, DoWorkEventArgs e)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;
            Algo algo = new Algo();
            switch (NumAlgo)
            {
                case 1:
                    algo.Shafle(ref SortArray);
                    break;
                case 2:
                    algo.Shaker(ref SortArray);
                    break;
                case 3:
                    algo.Insert(ref SortArray);
                    break;
                case 4:
                    algo.Chose(ref SortArray);
                    break;
                case 5:
                    if(SortArray.Length < 8001)
                        algo.QSort(ref SortArray, 0, SortArray.Length - 1);
                    break;
                case 6:
                    List<double> d = SortArray.ToList();
                    algo.Merge(ref d);
                    SortArray = d.ToArray();
                    break;
                default:
                    Array.Sort(SortArray);
                    break;
            }
            LeftTime = DateTime.Now.TimeOfDay - time;
        }

        private void Back_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (CompleteSort != null)
                CompleteSort.Invoke(this);
            Back.DoWork -= Back_DoWork;
            Back.RunWorkerCompleted -= Back_RunWorkerCompleted;
        }



    }
}
