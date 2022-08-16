using System.Collections.Generic;
using System.Linq;

namespace GOF.Adapter
{
	public class Adapter<T> : IContainer<T>
	{
		private readonly IElements<T> _adaptee;

		public Adapter(IElements<T> elements)
		{
			_adaptee = elements;
		}

		public IEnumerable<T> Items => _adaptee.GetElements();

		public int Count => Items.Count();
	}
}
