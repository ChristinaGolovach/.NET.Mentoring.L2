using System.Collections.Generic;

namespace GOF.Adapter
{
	public class Elements<T> : IElements<T>
	{
		private readonly IEnumerable<T> _elements;
		public Elements(IEnumerable<T> elements)
		{
			_elements = elements;
		}

		public IEnumerable<T> GetElements() => _elements;
	}
}
