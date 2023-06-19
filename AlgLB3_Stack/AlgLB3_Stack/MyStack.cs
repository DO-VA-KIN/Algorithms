using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLB3_Stack
{
    public class MyStack<T>
    {
        //public MyStack()
        //{ }

        private class Node
        {
            public T Value;
            public Node Next;
        }

        private Node Root = null;


        public bool IsEmpty()
        { return Root == null; }

        public void Push(T item)
        {
            Node newNode = new Node
            {
                Value = item,
                Next = Root
            };
            Root = newNode;
        }

        public T Pop()
        {
            Node delNode = Root;
            Root = Root.Next;
            return delNode.Value;
        }


        public IEnumerator<T> GetEnumerator()
        {
            Node it = new Node();
            it = Root;

            while (it != null)
            {
                yield return it.Value;
                it = it.Next;
            }
        }

    }
}
