using System;
using System.Threading.Tasks;
using Catalog.BussinesLayer.Models;
using Catalog.BussinesLayer.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
	public class CategoriesController : ApiController
	{
		private readonly ICategoryService _categoryService;
		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException($"Service {nameof(categoryService)} is null.");
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllAsync()
		{
			var categories = await _categoryService.GetAllAsync();
			return Ok(categories);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAsync(int id)
		{
			var category = await _categoryService.GetAsync(id);
			return ReturnResultOrNotFound(category);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateAsync(CategoryModel category)
		{
			var newCategory = await _categoryService.AddAsync(category);

			return CreatedAtAction(nameof(CategoriesController.GetAsync), new { id = newCategory.Id }, newCategory); ;
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await _categoryService.DeleteAsync(id);
			return NoContent();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateAsync(CategoryModel category)
		{
			var updatedCategory = await _categoryService.UpdateAsync(category);
			return Ok(updatedCategory);
		}
	}
}
