﻿/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    public class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;

        public static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();
            
            HundredTasks();

            Console.ReadLine();
        }

        private static void HundredTasks()
        {
            Task[] tasks = new Task[TaskAmount];
			for (int i = 0; i < tasks.Length; i++)
			{
                int taskNumber = i;
                tasks[taskNumber] = Task.Run(() => GoIteration(taskNumber));
			}

            Task.WaitAll(tasks);
        }

        private static void GoIteration(int taskNumber)
		{
            for (int iterationStep = 1; iterationStep <= MaxIterationsCount; iterationStep++)
			{
                Output(taskNumber, iterationStep);
			}
		}

        private static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }
    }
}
