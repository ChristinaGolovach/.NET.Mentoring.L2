/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
			Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
			Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
			Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
			Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
			Console.WriteLine("Demonstrate the work of the each case with console utility.");
			Console.WriteLine();

			// feel free to add your code
			Task taskA = RunA();
			Task taskB = RunB();
			Task taskC = RunC();
			Task taskD = RunD();

			Task.WaitAll(taskA, taskB, taskC, taskD);

			Console.ReadLine();
		}

		private static Task RunA()
		{
			Task mainTask = Task.Run(() => Console.WriteLine($"Parent Task A is runned;"));
			return mainTask.ContinueWith(result => Console.WriteLine($"Child Task A is runned;"));
		}

		private static Task RunB()
		{
			var mainTask = GetTaskFromException();
			return mainTask.ContinueWith(result => Console.WriteLine($"Child Task B is runned. Parent task is faulted - {result.IsFaulted};"),
				TaskContinuationOptions.OnlyOnFaulted);
		}

		private static Task RunC()
		{
			return Task.Factory.StartNew(CompositeTaskSynchronously);
		}

		private static Task RunD()
		{
			using (CancellationTokenSource src = new CancellationTokenSource())
			{
				var token = src.Token;
				src.Cancel();

				return ParentTask(token).ContinueWith((result) => Console.WriteLine(
						$"Inner {nameof(ChildTask)} D is in thread pool: {Thread.CurrentThread.IsThreadPoolThread}."), TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);
			}
		}

		private static Task GetTaskFromException()
		{
			return Task.FromException(new ArgumentException("Invalid ***."));
		}

		private static Task GetTaskFromExceptionWithSameThread()
		{
			Console.WriteLine($"Parent task thread Id - {Thread.CurrentThread.ManagedThreadId}.");
			return Task.FromException(new ArgumentException("Invalid ***."));
		}

		private static void CompositeTaskSynchronously()
		{
			Console.WriteLine();
			var parentTask = GetTaskFromExceptionWithSameThread();
			if (parentTask.IsFaulted)
			{
				parentTask.ContinueWith(result => Console.WriteLine($"Child Task C is runned. Parent task is faulted - {result.IsFaulted}; Current thread id - {Thread.CurrentThread.ManagedThreadId}"),
				TaskContinuationOptions.ExecuteSynchronously);
			}
		}

		private static Task ParentTask(CancellationToken cancellationToken)
		{
			return Task.FromCanceled(cancellationToken);
		}

		private static Task ChildTask(Task parentTask)
		{
			return parentTask.ContinueWith(task =>
					Console.WriteLine(
						$"Inner {nameof(ChildTask)} D is in thread pool: {Thread.CurrentThread.IsThreadPoolThread}."),
				TaskContinuationOptions.LongRunning);
		}
	}
}
