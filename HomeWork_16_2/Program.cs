using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HomeWork_16_2
{
    class Program
    {
      
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            uint result = default;
            Task<uint> task1 = Task<uint>.Factory.StartNew(() => CalculateNumberOfMultipleNumbers(1_200_000_000));

            Task<uint> task2 = Task<uint>.Factory.StartNew(() => CalculateNumberOfMultipleNumbers(1_400_000_000));

            Task<uint> task3 = Task<uint>.Factory.StartNew(() => CalculateNumberOfMultipleNumbers(1_600_000_000));

            Task<uint> task4 = Task<uint>.Factory.StartNew(() => CalculateNumberOfMultipleNumbers(1_800_000_000));

            Task<uint> task5 = Task<uint>.Factory.StartNew(() => CalculateNumberOfMultipleNumbers(2_000_000_000));

            Console.WriteLine("Задачи выполняются");

            Task.WaitAll(task1, task2, task3, task4, task5);

            Console.WriteLine("Задачи выполнены");

            result += task1.Result;
            result += task2.Result;
            result += task3.Result;
            result += task4.Result;
            result += task5.Result;
            //result += task6.Result;
            //result += task7.Result;
            //result += task8.Result;
            //result += task9.Result;
            //result += task10.Result;


            //Parallel.For(1_000_000_000, 1_200_000_000, Foo);
            //Parallel.For(1_200_000_000, 1_400_000_000, Foo);
            //Parallel.For(1_400_000_000, 1_600_000_000, Foo);
            //Parallel.For(1_600_000_000, 1_800_000_000, Foo);
            //Parallel.For(1_800_000_000, 2_000_000_000, Foo);

            Console.WriteLine($"Результат: {result}");
            sw.Stop();
            Console.WriteLine($"Время затрачено: {sw.Elapsed}");
            Console.ReadKey();
        }

        static uint Foo(uint i)
        {
            uint sum = 0;
            uint lastNumber = 0;
            for (uint j = i, k = 0; j > 0;)
            {
                sum += j % 10;
                if (sum == 0 || sum == 1)
                {
                    return 1;
                }
                j /= 10;
                if (k == 0)
                {
                    lastNumber = sum;
                    k++;
                }         
            }

            if (sum % lastNumber == 0)
            {
                return 1;
            }
            return 0;
        }

        static uint CalculateNumberOfMultipleNumbers(uint number)
        {
            uint result = default;
            uint start = number;
            uint limit = start + 200_000_000;

            while (start < limit)
            {
                result += Foo(start);
                start++;
            }

            return result;
        }
    }
}
