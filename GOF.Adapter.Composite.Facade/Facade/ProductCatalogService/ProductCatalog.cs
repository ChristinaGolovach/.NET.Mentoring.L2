using System.Collections.Generic;
using System.Linq;

namespace Facade.ProductCatalogService
{
	public class ProductCatalog : IProductCatalog
	{
		private IList<Product> _catalog;

		public ProductCatalog()
		{
			InitializeCatalog();
		}

		public Product GetProductDetails(string productId)
		{
			return _catalog.FirstOrDefault(product => product.Id == productId);
		}

		private void InitializeCatalog()
		{
			_catalog = new List<Product> 
			{ 
				new Product { Id = "1" , Price = 11 },
				new Product { Id = "2" , Price = 12 },
				new Product { Id = "3" , Price = 13 },
			};
		}
	}
}
