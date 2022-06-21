/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

			Task arrayManipulation = Task.Run(() =>
			{
				var array = GetArray(10);
				PrintArray(array);
				return array;
			}).ContinueWith(arrayResult =>
			{
				var array = Multiply(arrayResult.Result);
				PrintArray(array);
				return array;
			}).ContinueWith(arrayResult =>
			{
				var array = arrayResult.Result;
				Array.Sort(array);
				PrintArray(array);
				return array;
			}).ContinueWith(arrayResult => Console.WriteLine(GetAvarage(arrayResult.Result)));

			arrayManipulation.Wait();

            Console.ReadLine();
        }

        private static int[] GetArray(int arraySize)
		{
            int[] array = new int[arraySize];
            int minItemValue = 1;
            int maxItemValue = 20;
            var random = new Random();
			for (int i = 0; i < array.Length; i++)
			{
                array[i] = random.Next(minItemValue, maxItemValue);
			}

            return array;
		}

		private static int[] Multiply(int[] array)
		{
            int minItemValue = 2;
            int maxItemValue = 5;
			var randomMultiplier = new Random().Next(minItemValue, maxItemValue);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i] * randomMultiplier;
			}

			return array;
		}

        private static double GetAvarage(int[] array)
		{
            return array.Average();
		}

		private static void PrintArray(int[] array)
		{
            var output = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
                output.Append($"{array[i]}  ");
			}

            Console.WriteLine(output);
		}
    }
}
