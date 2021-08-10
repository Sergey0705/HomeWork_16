using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace HomeWork_16_3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] firstArray = new int[1000, 1000];
            int[,] secondArray = new int[1000, 1000];

            var sw = new Stopwatch();
            sw.Start();

            Task task1 = Task.Factory.StartNew(() => FillMatrixAndWriteItToFile(ref firstArray, "matrix1.txt"));
            Task task2 = Task.Factory.StartNew(() => FillMatrixAndWriteItToFile(ref secondArray, "matrix2.txt"));

            Task.WaitAll(task1, task2);

            sw.Stop();
            Console.WriteLine($"Время на заполнение и запись умножаемых матриц размером [1000, 1000] в файлы matrix1.txt и matrix2.txt затрачено: {sw.Elapsed}");

            int[,] resultArray = new int[1000, 1000];

            var sw2 = new Stopwatch();
            sw2.Start();

            Task task3 = Task.Factory.StartNew(() => FillResultMatrix(firstArray, secondArray, ref resultArray, 0, 200));
            Task task4 = Task.Factory.StartNew(() => FillResultMatrix(firstArray, secondArray, ref resultArray, 200, 400));
            Task task5 = Task.Factory.StartNew(() => FillResultMatrix(firstArray, secondArray, ref resultArray, 400, 600));
            Task task6 = Task.Factory.StartNew(() => FillResultMatrix(firstArray, secondArray, ref resultArray, 600, 800));
            Task task7 = Task.Factory.StartNew(() => FillResultMatrix(firstArray, secondArray, ref resultArray, 800, 1000));

            Task.WaitAll(task3, task4, task5, task6, task7);

            sw2.Stop();

            Console.WriteLine($"На заполнение итоговой матрицы размером [1000, 1000] затрачено: {sw2.Elapsed}");

            var sw3 = new Stopwatch();
            sw3.Start();

            Task task8 = Task.Factory.StartNew(() => WriteResulrMatrixToFile(resultArray, "resultMatrix.txt"));

            Task.WaitAll(task8);

            Console.WriteLine($"На запись итоговой матрицы в файл resultMatrix.txt затрачено: {sw3.Elapsed}");

            Console.ReadKey();
        }

      

        static void FillMatrixAndWriteItToFile(ref int[,] firstArray, string pathToFile)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(pathToFile, false, UTF32Encoding.UTF32))
            {
                for (int i = 0; i < firstArray.GetLength(0); i++)
                {
                    for (int j = 0; j < firstArray.GetLength(1); j++)
                    {
                        firstArray[i, j] = rand.Next(1, 10);


                        if (j == 0)
                        {
                            sw.Write($"| {firstArray[i, j]} ");
                        }
                        else if (j > 0 && j != firstArray.GetLength(1) - 1)
                        {
                            sw.Write($"{firstArray[i, j]} ");
                        }
                        else
                        {
                            sw.Write($"{firstArray[i, j]} |\n");
                        }
                    }
                }
            }
        }

        static void FillResultMatrix(int[,] firstArray, int[,] secondArray, ref int[,] resultArray, int startX, int limitX)
        {
            for (int i = startX; i < limitX; i++)
            {
                for (int j = 0; j < resultArray.GetLength(1); j++)
                {
                    int tmpI = 0;
                    int tmpJ = 0;
                    while (tmpI != secondArray.GetLength(0) && tmpJ != firstArray.GetLength(1))
                    {
                        resultArray[i, j] += firstArray[i, tmpJ] * secondArray[tmpI, j];
                        tmpI += 1;
                        tmpJ += 1;
                    }
                }
            }
        }

        static void WriteResulrMatrixToFile(int[,] resultArray, string pathToFile)
        {
            using (StreamWriter sw = new StreamWriter(pathToFile, false, UTF32Encoding.UTF32))
            {
                for (int i = 0; i < resultArray.GetLength(0); i++)
                {
                    for (int j = 0; j < resultArray.GetLength(1); j++)
                    {
                        if (j == 0)
                        {
                            sw.Write($"| {resultArray[i, j]} ");
                        }
                        else if (j > 0 && j != resultArray.GetLength(1) - 1)
                        {
                            sw.Write($"{resultArray[i, j]} ");
                        }
                        else
                        {
                            sw.Write($"{resultArray[i, j]} |\n");
                        }
                    }
                }
            }
        }
    }
}
