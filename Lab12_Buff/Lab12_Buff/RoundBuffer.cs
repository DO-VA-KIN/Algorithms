using System;
using System.Collections.Generic;
using System.Text;

namespace Lab12_Buff
{
    /// <summary>
    /// Класс реализующий работу кольцевого буфера. 
    /// Требует указание кол-ва элементов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RoundBuffer<T>
    {
        /// <summary>
        /// Позиция чтения
        /// </summary>
        private int Head;
        /// <summary>
        /// Позиция записи
        /// </summary>
        private int Tail;
        /// <summary>
        /// Значения
        /// </summary>
        private readonly T[] Values;
        /// <summary>
        /// Флаг перезаписи
        /// </summary>
        public bool Recycle;

        /// <summary>
        /// Конструктор. Принимает размер кольцевого буфера.
        /// Может принимать флаг на перезапись данных.
        /// </summary>
        /// <param name="size"></param>
        public RoundBuffer(int size)
        {
            Head = 0;
            Tail = 0;
            Values = new T[size];
            Recycle = true;
        }
        /// <summary>
        /// Конструктор. Принимает размер кольцевого буфера.
        /// Может принимать флаг на перезапись данных.
        /// </summary>
        /// <param name="size"></param>
        public RoundBuffer(int size, bool recycle)
        {
            Head = 0;
            Tail = 0;
            Values = new T[size];
            Recycle = recycle;
        }


        /// <summary>
        /// Извлечение значения.
        /// </summary>
        /// <returns></returns>
        public T Read()
        {
            //в случае "пустоты" - топчимся на месте
            if (Values[Head].Equals(default(T)))
                return default;

            T t = Values[Head];
            Values[Head] = default;

            if (Head == Values.Length - 1)
                Head = 0;
            else Head++;

            return t;
        }


        /// <summary>
        /// Запись значения.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Write(T t)
        {
            //если данные уже записаны
            if (!Values[Tail].Equals(default(T)) || Tail + 1 == Head || (Tail == Values.Length - 1 && Head == 0))
            {
                if (!Recycle)
                    return false;
                else
                {
                    if (Head == Values.Length - 1)
                        Head = 0;
                    else Head++;
                }
            }

            Values[Tail] = t;

            if (Tail == Values.Length - 1)
                Tail = 0;
            else Tail++;

            return true;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Values.Length; i++)
                yield return Read();

            //int h = Head;
            //for (int i = Head; i < Values.Length; i++)
            //    yield return Values[i];
            //for (int i = 0; i < h; i++)
            //    yield return Values[i];
        }
    }
}
