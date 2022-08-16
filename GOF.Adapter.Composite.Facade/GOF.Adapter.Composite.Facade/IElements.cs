using System.Collections.Generic;

namespace GOF.Adapter
{
	public interface IElements<T>
	{
		IEnumerable<T> GetElements();
	}
}
