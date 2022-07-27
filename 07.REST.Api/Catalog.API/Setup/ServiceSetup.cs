using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.BussinesLayer.Services.CategoryService;
using Catalog.BussinesLayer.Services.ItemService;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Setup
{
	public static class ServiceSetup
	{
		public static IServiceCollection AddServiceLayer(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IItemService, ItemService>();

			return services;
		}
	}
}
