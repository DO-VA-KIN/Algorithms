using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLB3_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            Examples example = new Examples();
            example.IntExample(-1000, 1000, 1000);
            example.StringExample(10);
            example.PersonExample(100, 1980, 2020);
            MyStack<int> st = example.SortExample(1000);
            example.ReverseStack(st, 1000);
            Console.ReadKey();
        }
    }

    public class Examples
    {

        public MyStack<int> IntExample(int beg, int end, int count)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            MyStack<int> stack = new MyStack<int>();
            Random rand = new Random();

            for (int i = 0; i < count; i++)
                stack.Push(rand.Next(beg, end));


            long sum = 0;
            int max = int.MinValue;
            int min = int.MaxValue;

            foreach (var item in stack)
            {
                sum += item;
                max = Math.Max(max, item);
                min = Math.Min(min, item);
            }
            time = DateTime.Now.TimeOfDay - time;

            Console.WriteLine("- Заполнение Int -");
            Console.WriteLine("сумма: " + sum);
            Console.WriteLine("среднее: " + sum/count);
            Console.WriteLine("максимальное: " + max);
            Console.WriteLine("минимальное: " + min);
            Console.WriteLine("время вычислений: " + time + "\n");

            return stack;
        }


        public MyStack<string> StringExample(int count)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            MyStack<string> stack = new MyStack<string>();
            Random rand = new Random();

            for (int i = 0; i < count; i++)
                stack.Push(RandString());

            while (!stack.IsEmpty())
                stack.Pop();

            time = DateTime.Now.TimeOfDay - time;

            Console.WriteLine("- Заполнение и извлечение String -");
            Console.WriteLine("количество элементов: " + count);
            Console.WriteLine("время вычислений: " + time + "\n");

            return stack;
        }


        
        public MyStack<Person> PersonExample(int count, int startYear, int endYear)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            MyStack<Person> persons = new MyStack<Person>();
            for (int i = 0; i < count; i++)
                persons.Push(new Person().GetRandom(startYear, endYear));

            MyStack<Person> younger20 = new MyStack<Person>();
            MyStack<Person> older30 = new MyStack<Person>();
            //List<Person> check = new List<Person>(count/4);
            foreach (var p in persons)
            {
                if (AgeSelect(p.DOB, 0, 19))//моложе 20
                    younger20.Push(p);
                else if (AgeSelect(p.DOB, 31, 199))//старше 30
                    older30.Push(p);
                //else check.Add(p);
            }

            time = DateTime.Now.TimeOfDay - time;


            Console.WriteLine("- Заполнение Person -");
            Console.WriteLine("количество элементов: " + count);
            //Console.WriteLine("количество людей от 20 до 30: " + check.Count);
            Console.WriteLine("время вычислений: " + time + "\n");

            return persons;
        }
        public struct Person
        {
            /// <summary>
            /// Имя
            /// </summary>
            public string Name;
            /// <summary>
            /// Фамилия
            /// </summary>
            public string Surname;
            /// <summary>
            /// Отчество
            /// </summary>
            public string Patronymic;
            /// <summary>
            /// Дата рождения (DOB - date of Birth) 
            /// </summary>
            public DateTime DOB;

            public Person GetRandom(int startYear, int endYear)
            {
                Person person = new Person()
                {
                    Name = RandString(),
                    Surname = RandString(),
                    Patronymic = RandString(),
                    DOB = RandData(startYear, endYear)
                };


                return person;
            }
        }


        public MyStack<int> SortExample(int count)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            MyStack<int> my = new MyStack<int>();
            Stack<int> stack = new Stack<int>();

            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int n = rand.Next(-100, 100);
                stack.Push(n);
                my.Push(n);
            }

            int count1 = 0;
            foreach (int item in my)// узнал размерность
                count1++;

            int[] arr1 = new int[count1];
            for (int i = 0; i < count1; i++)
                arr1[i] = my.Pop();

            //сама сортировка
            for (int i = 0; i < arr1.Length; i++)
            {
                for (int j = 0; j < arr1.Length-1; j++)
                {
                    if(arr1[j] > arr1[j+1])
                    {
                        int a = arr1[j];
                        arr1[j] = arr1[j + 1];
                        arr1[j + 1] = a;
                    }
                }
            }

            foreach (var item in arr1)
                my.Push(item);


            //библиотеные ф-ии
            int count2 = 0;
            foreach (int item in stack)// узнал размерность
                count2++;

            int[] arr2 = new int[count2];
            for (int i = 0; i < count2; i++)
                arr2[i] = stack.Pop();

            arr2 = arr2.OrderBy(x => x).ToArray();
            foreach (var item in arr2)
                stack.Push(item);

            bool flag = true;
            while(stack.Count !=0 && my != null)
            {
                int s = stack.Pop();
                int m = my.Pop();

                if (s != m)
                {
                    flag = false;
                    break;
                }
            }

            time = DateTime.Now.TimeOfDay - time;

            Console.WriteLine("- Сортировка int -");
            Console.WriteLine("количество элементов: " + count);
            Console.WriteLine("проверка библиотечными функциями: " + flag);
            Console.WriteLine("время вычислений: " + time + "\n");

            return my;
        }

        public MyStack<int> ReverseStack(MyStack<int> input, int count)
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            MyStack<int> my = new MyStack<int>();
            foreach (var item in input)
                my.Push(item);

            time = DateTime.Now.TimeOfDay - time;
            Console.WriteLine("- Разворот -");
            Console.WriteLine("количество элементов: " + count);
            Console.WriteLine("время вычислений: " + time + "\n");

            return my;
        }







        public static string RandString()
        {
            string rStr = "";

            Random rand = new Random();
            int length = rand.Next(3, 20);
            char[] chars = "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ,".ToCharArray();


            for (int i = 0; i < length; i++)
                rStr += chars[rand.Next(0, length - 1)];

            return rStr;
        }
        public static DateTime RandData(int startYear, int endYear)
        {
            Random rand = new Random();
            int year = rand.Next(startYear, endYear);
            int month = rand.Next(1, 12);
            int day = rand.Next(1, 7);

            return new DateTime(year, month, day);
        }
        public static bool AgeSelect(DateTime DOB, int older, int yuonger)
        {
            DateTime now = DateTime.Now;
            bool flag = false;

            if (now.Year - DOB.Year > older && now.Year - DOB.Year < yuonger)
                flag = true;

            else if (now.Year == DOB.Year)
            {
                if (now.Month > DOB.Month)
                    flag = true;
                else if (now.Month == DOB.Month)
                {
                    if (now.Day >= DOB.Day)
                        flag = true;
                }
            }

            return flag;
        }

    }
}
