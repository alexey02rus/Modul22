using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumArray);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxArray);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();
            Console.Write("Массив: ");
            foreach (int num in task1.Result)
            {
                Console.Write($"{num}  ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Сумма чисел массива: {task2.Result}");
            Console.WriteLine();
            Console.WriteLine($"Максимальное число массива: {task3.Result}"); ;
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n - 1; i++)
            {
                array[i] = random.Next(0, 50);
            }
            return array;
        }
        static int SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            return array.Sum();
        }
        static int MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            return array.Max();
        }
    }
}
