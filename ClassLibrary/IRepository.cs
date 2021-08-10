using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IRepository<T>
    {
        void Push(T Value);
        T Pop();
        T this[int index] { get; set; }
    }
}
