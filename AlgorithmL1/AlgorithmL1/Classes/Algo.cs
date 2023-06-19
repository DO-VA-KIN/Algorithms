using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmL1
{
    //public class Algo<T> where T : IEnumerable<T>
    public class Algo
    {
        public double[] Gen(int count)
        {
            double[] arr = new double[count];
            for (int i = 0; i < count; i++)
            {
                Random rand = new Random((int)DateTime.Now.Ticks + i);
                arr[i] = (double)rand.Next(-10000, 10000) / 10000.0;                
            }
            return arr;
        }


        /// <summary>
        /// сортировка пузырьком
        /// </summary>
        /// <param name="arr"></param>
        public void Shafle(ref double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length-i-1; j++)
                {

                    if(arr[j] > arr[j+1])
                    {
                        double t = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = t;
                    }
                }
            }
        }

        /// <summary>
        /// сортировка перемешиванием
        /// </summary>
        /// <param name="arr"></param>
        public void Shaker(ref double[] arr)
        {
            for (int i = 0; i < arr.Length / 2; i++)
            {
                bool flag = false;//флаг о том что элементы менялись местами
                //слева направо
                for (int j = 0; j < arr.Length - i - 1; j++)
                {

                    if (arr[j] > arr[j + 1])
                    {
                        double t = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = t;
                        flag = true;
                    }
                }
                //справа налево
                for (int j = arr.Length - i - 1; j > i; j--)
                {
                    if (arr[j - 1] > arr[j])
                    {
                        double t = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = t;
                        flag = true;
                    }
                }

                if (!flag) break;
            }
        }

        /// <summary>
        /// сортировка вставками
        /// </summary>
        /// <param name="arr"></param>
        public void Insert(ref double[] arr)
        {
            double k;
            int j;
            for (int i = 1; i < arr.Length; i++)
            {
                k = arr[i];
                j = i;
                while (j > 0 && arr[j - 1] > k)
                {
                    double t = arr[j-1];
                    arr[j - 1] = arr[j];
                    arr[j] = t;
                    j -= 1;
                }
                arr[j] = k;
            }
        }
        /// <summary>
        /// сортировка выбором
        /// </summary>
        /// <param name="arr"></param>
        public void Chose(ref double[] arr)
        {
            int k;//индекс мин числа
            for (int i = 0; i < arr.Length; i++)
            {
                k = i;//берем стартовый индекс
                for (int j = i; j < arr.Length; j++)//поиск мин в несорт. части
                {
                    if (arr[j] < arr[k])
                        k = j;
                }
                if (k == i) continue;//если одинаковы - нет смысла менять

                double d = arr[i];
                arr[i] = arr[k];
                arr[k] = d;
            }
        }

        /// <summary>
        /// быстрая сортировка - слиянием
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="l"></param>
        /// <param name="r"></param>
        public void QSort(ref double[] arr, int l, int r)
        {
            if (l >= r) return;

            double d = arr[(l + r + 1) / 2];
            int i = l;
            int j = r;

            while (i <= j)
            {
                while (arr[i] < d) i++;
                while (arr[j] > d) j--;

                if(i <= j)
                {
                    d = arr[i];
                    arr[i] = arr[j];
                    arr[j] = d;
                    i++;
                    j--;

                    QSort(ref arr, l, j);
                    QSort(ref arr, i, r);
                }
            }
        }


        public void Merge(ref List<double> list)
        {
            double d = list[list.Count/2];
            List<double> left = new List<double>(list.Count / 2);
            List<double> right = new List<double>(list.Count / 2);

            foreach (double i in list)
            {
                if (i < d) left.Add(i);
                else right.Add(i);
            }

            Merge(ref left);
            Merge(ref right);

            left.AddRange(right);
            list = left;
        }

    }
}
