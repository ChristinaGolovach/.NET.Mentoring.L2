using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataLayer.Repositories
{
	public interface IRepository<T>
	{
		IQueryable<T> GetAll();
		Task<T> GetAsync(int id);
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}
