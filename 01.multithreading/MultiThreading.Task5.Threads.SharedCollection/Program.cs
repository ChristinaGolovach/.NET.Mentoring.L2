/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
	class Program
	{
		private static int elementCount = 10;
		private static List<int> sharedCollection = new List<int>();
		private static AutoResetEvent readEvent = new AutoResetEvent(false);
		private static AutoResetEvent writeEvent = new AutoResetEvent(true);

		static void Main(string[] args)
		{
			Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
			Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
			Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
			Console.WriteLine();

			//feel free to add your code
			Task addItemsTask = Task.Run(() => AddItems());
			Task.Run(() => PrintItems(addItemsTask));

			Console.ReadLine();
		}

		private static void AddItems()
		{
			for (int i = 0; i < elementCount; i++)
			{
				readEvent.WaitOne();
				sharedCollection.Add(i);
				Console.WriteLine($"item {i} has been added.");
				writeEvent.Set();
			}
		}

		private static void PrintItems(Task addItemsTask)
		{
			while (!addItemsTask.IsCompleted)
			{
				writeEvent.WaitOne();
				for (int i = 0; i < sharedCollection.Count; i++)
				{
					Console.Write($"{sharedCollection[i]} ");
				}
				Console.WriteLine();
				readEvent.Set();
			}
		}
	}
}
