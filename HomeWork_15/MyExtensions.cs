using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_15
{
   static class MyExtensions
   {
        /// <summary>
        /// Метод преобразуест строковое значение числа в число, удваивает его и возвращает его строковое значение
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static string DoubleUpSumValue(this string sum)
        {
            return (Convert.ToInt32(sum) * 2).ToString();
        }
   }
}
