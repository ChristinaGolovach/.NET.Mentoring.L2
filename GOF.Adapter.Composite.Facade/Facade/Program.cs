using System;

using Facade.InvoiceSystemService;
using Facade.PaymentSystemService;
using Facade.ProductCatalogService;

namespace Facade
{
	class Program
	{
		static void Main(string[] args)
		{

			var order = new Order(new ProductCatalog(), new PaymentSystem(), new InvoiceSystem());
			order.PlaceOrder("1", 2, "test@gmail.com");

			Console.ReadKey();
		}
	}
}
