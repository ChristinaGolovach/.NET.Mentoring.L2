using Microsoft.Extensions.DependencyInjection;
using Catalog.DataLayer.Entities;
using Catalog.DataLayer.Repositories;
using Catalog.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Setup
{
	public static class DataSetup
	{
		public static IServiceCollection AddDataLayer(this IServiceCollection services)
		{
			services.AddScoped<IRepository<Category>, Repository<Category>>();
			services.AddScoped<IRepository<Item>, Repository<Item>>();

			services.AddDbContext<CatalogDbContext>();

			return services;
		}
	}
}
