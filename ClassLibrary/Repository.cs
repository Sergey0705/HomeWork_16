using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Класс обобщения
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T>
    {
        T[] items;
        int size;

        public Repository()
        {
            this.items = new T[0];
            this.size = 0;
        }

        public Repository(params T[] Args)
        {
            this.items = Args;
            this.size = Args.Length - 1;
        }

        public void Push(T Value)
        {
            if (this.size == this.items.Length)
            {
                Array.Resize(ref this.items, this.size == 0 ? 4 : this.items.Length * 2);
            }
            this.items[this.size++] = Value;
        }

        public T Pop()
        {
            T temp = this.items[--this.size];
            this.items[this.size] = default(T);
            return temp;
        }

        public T this[int index]
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }

        public int Count { get { return this.size; } }
    }
}
