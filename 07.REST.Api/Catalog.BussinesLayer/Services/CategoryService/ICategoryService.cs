
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.BussinesLayer.Models;

namespace Catalog.BussinesLayer.Services.CategoryService
{
	public interface ICategoryService
	{
		Task<IList<CategoryModel>> GetAllAsync();
		Task<CategoryModel> GetAsync(int id);
		Task<CategoryModel> AddAsync(CategoryModel category);
		Task<CategoryModel> UpdateAsync(CategoryModel category);
		Task DeleteAsync(int id);
	}
}
