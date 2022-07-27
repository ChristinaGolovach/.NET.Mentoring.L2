using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Catalog.DataLayer.Entities;

namespace Catalog.DataLayer.Repositories
{
	public class Repository<T> : IRepository<T> where T : Entity
	{
		private readonly CatalogDbContext _dbContext;
		public Repository(CatalogDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException($"Data base context {nameof(dbContext)} is null.");
		}

		public IQueryable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking();
		}

		public async Task<T> GetAsync(int id)
		{
			return await _dbContext.Set<T>().AsNoTracking()
				.FirstOrDefaultAsync(entity => entity.Id == id);
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(int id)
		{
			T entity = await GetAsync(id);
			//TODO Exception
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}
	}
}
