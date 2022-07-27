using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.BussinesLayer.ApplicationModels;
using Catalog.BussinesLayer.Models;

namespace Catalog.BussinesLayer.Services.ItemService
{
	public interface IItemService
	{
		Task<IList<ItemModel>> GetAllAsync();
		Task<IList<ItemModel>> GetAllAsync(ItemFilter paginationConfig);
		Task<ItemModel> GetAsync(int id);
		Task<ItemModel> AddAsync(ItemModel category);
		Task<ItemModel> UpdateAsync(ItemModel category);
		Task DeleteAsync(int id);
	}
}
