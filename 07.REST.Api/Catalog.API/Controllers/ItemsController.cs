using System;
using System.Threading.Tasks;
using Catalog.BussinesLayer.ApplicationModels;
using Catalog.BussinesLayer.Models;
using Catalog.BussinesLayer.Services.ItemService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
	public class ItemsController : ApiController
	{
		private readonly IItemService _itemService;
		public ItemsController(IItemService itemService)
		{
			_itemService = itemService ?? throw new ArgumentNullException($"Service {nameof(itemService)} is null.");
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllAsync([FromQuery] ItemFilter itemFilter)
		{
			var items = await _itemService.GetAllAsync(itemFilter);
			return Ok(items);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAsync(int id)
		{
			var item = await _itemService.GetAsync(id);
			return ReturnResultOrNotFound(item);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateAsync(ItemModel item)
		{
			var newItem = await _itemService.AddAsync(item);

			return CreatedAtAction(nameof(ItemsController.GetAsync), new { id = newItem.Id }, newItem); ;
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await _itemService.DeleteAsync(id);
			return NoContent();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateAsync(ItemModel item)
		{
			var updatedItem = await _itemService.UpdateAsync(item);
			return Ok(updatedItem);
		}
	}
}
