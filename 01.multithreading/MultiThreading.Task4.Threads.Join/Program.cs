/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
	class Program
	{
		private const int ThreadAmount = 10;
		private static Semaphore semaphore = new Semaphore(1, 1);

		static void Main(string[] args)
		{
			Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
			Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
			Console.WriteLine("Implement all of the following options:");
			Console.WriteLine();
			Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
			Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

			Console.WriteLine();

			PrintNumbersThroughThreads(ThreadAmount, 0);
			PrintNumbersThroughThreadPool(ThreadAmount, 0);

			Console.ReadLine();
		}

		private static void PrintNumbersThroughThreads(int threadAmount, int currentNumber)
		{
			if (threadAmount == 0)
			{
				return;
			}

			int result = 0;
			var thread = new Thread(() => result = PrintNextNumber(currentNumber));
			thread.Start();
			thread.Join();

			PrintNumbersThroughThreads(--threadAmount, result);
		}

		private static void PrintNumbersThroughThreadPool(int threadAmount, int currentNumber)
		{
			if (threadAmount == 0)
			{
				return;
			}

			ThreadPool.QueueUserWorkItem((i) =>
			{
				semaphore.WaitOne();

				currentNumber += 1;
				Console.WriteLine(currentNumber);

				PrintNumbersThroughThreadPool(--threadAmount, currentNumber);

				semaphore.Release();
			});

		}

		private static int PrintNextNumber(int currentNumber)
		{
			int nextNumber = ++currentNumber;
			Console.WriteLine(nextNumber);
			return nextNumber;
		}
	}
}
