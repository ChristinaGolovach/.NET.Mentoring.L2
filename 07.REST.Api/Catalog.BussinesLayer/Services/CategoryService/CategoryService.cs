using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Catalog.BussinesLayer.Models;
using Catalog.DataLayer.Entities;
using Catalog.DataLayer.Repositories;

namespace Catalog.BussinesLayer.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		private readonly IMapper _mapper;
		private readonly IRepository<Category> _catalogRepository;

		public CategoryService(IRepository<Category> catalogRepository,
			IMapper mapper)
		{
			_catalogRepository = catalogRepository;
			_mapper = mapper;
		}

		public async Task<CategoryModel> AddAsync(CategoryModel category)
		{
			var categoryEntity = _mapper.Map<CategoryModel, Category>(category);
			var createdCategory = await _catalogRepository.AddAsync(categoryEntity);

			return _mapper.Map<Category, CategoryModel>(createdCategory);
		}

		public async Task DeleteAsync(int id)
		{
			await _catalogRepository.DeleteAsync(id);
		}

		public async Task<IList<CategoryModel>> GetAllAsync()
		{
			var categoryEntities = await _catalogRepository.GetAll().ToListAsync();
			return _mapper.Map<IList<Category>, IList<CategoryModel>>(categoryEntities);
		}

		public async Task<CategoryModel> GetAsync(int id)
		{
			var categoryEntity = await _catalogRepository.GetAsync(id);
			return _mapper.Map<Category, CategoryModel>(categoryEntity);
		}

		public async Task<CategoryModel> UpdateAsync(CategoryModel category)
		{
			var categoryEntity = _mapper.Map<CategoryModel, Category>(category);
			//TODO GEtById first??
			var updatedCategory = await _catalogRepository.UpdateAsync(categoryEntity);
			return _mapper.Map<Category, CategoryModel>(updatedCategory);
		}
	}
}
