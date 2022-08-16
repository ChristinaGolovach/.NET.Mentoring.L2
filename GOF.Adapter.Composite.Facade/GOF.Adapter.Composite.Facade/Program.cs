using System;
using System.Collections.Generic;

namespace GOF.Adapter
{
	class Program
	{
		static void Main(string[] args)
		{
			IList<int> elements = new List<int> {1, 2,3 ,4,5};

			IElements<int> adaptee = new Elements<int>(elements);
			IContainer<int> container = new Adapter<int>(adaptee);
			Printer printer = new Printer();

			printer.Print(container);

			Console.ReadKey();
		}
	}
}
