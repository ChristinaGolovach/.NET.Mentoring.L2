using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.BussinesLayer.ApplicationModels;
using Catalog.BussinesLayer.Models;
using Catalog.DataLayer.Entities;
using Catalog.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.BussinesLayer.Services.ItemService
{
	public class ItemService : IItemService
	{
		private readonly IMapper _mapper;
		private readonly IRepository<Item> _itemRepository;

		public ItemService(IRepository<Item> itemRepository,
			IMapper mapper)
		{
			_itemRepository = itemRepository;
			_mapper = mapper;
		}

		public async Task<ItemModel> AddAsync(ItemModel item)
		{
			var itemEntity = _mapper.Map<ItemModel, Item>(item);
			var createdItem = await _itemRepository.AddAsync(itemEntity);

			return _mapper.Map<Item, ItemModel>(createdItem);
		}

		public async Task DeleteAsync(int id)
		{
			await _itemRepository.DeleteAsync(id);
		}

		public async Task<IList<ItemModel>> GetAllAsync()
		{
			var itemEntities = await _itemRepository.GetAll().ToListAsync();
			return _mapper.Map<IList<Item>, IList<ItemModel>>(itemEntities);
		}

		public async Task<IList<ItemModel>> GetAllAsync(ItemFilter itemFilter)
		{
			if (itemFilter == null)
			{
				return await GetAllAsync();
			}

			var itemQuery = _itemRepository.GetAll();
			if (itemFilter.CategoryId.HasValue)
			{
				itemQuery = itemQuery.Where(item => item.CategoryId == itemFilter.CategoryId);
			}

			var itemEntities = await itemQuery
				.Skip((itemFilter.PageNumber - 1) * itemFilter.CountPerPage)
				.Take(itemFilter.CountPerPage)
				.ToListAsync();

			return _mapper.Map<IList<Item>, IList<ItemModel>>(itemEntities);
		}

		public async Task<ItemModel> GetAsync(int id)
		{
			var itemEntity = await _itemRepository.GetAsync(id);
			return _mapper.Map<Item, ItemModel>(itemEntity);
		}

		public async Task<ItemModel> UpdateAsync(ItemModel item)
		{
			var itemEntity = _mapper.Map<ItemModel, Item>(item);
			var updatedItem = await _itemRepository.UpdateAsync(itemEntity);
			return _mapper.Map<Item, ItemModel>(updatedItem);
		}
	}
}
