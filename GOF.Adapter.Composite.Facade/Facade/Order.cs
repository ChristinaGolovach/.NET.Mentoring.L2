using System;

using Facade.InvoiceSystemService;
using Facade.PaymentSystemService;
using Facade.ProductCatalogService;

namespace Facade
{
	public class Order
	{
		private IProductCatalog _productCatalog;
		private IPaymentSystem _paymentSystem;
		private IInvoiceSystem _invoiceSystem;

		public Order(IProductCatalog productCatalog, IPaymentSystem paymentSystem, IInvoiceSystem invoiceSystem)
		{
			_productCatalog = productCatalog;
			_paymentSystem = paymentSystem;
			_invoiceSystem = invoiceSystem;
		}

		public void PlaceOrder(string productId, int quantity, string email)
		{
			var product = _productCatalog.GetProductDetails(productId);

			if (product == null)
			{
				throw new ArgumentException($"The product with id {productId}is not found");
			}

			Random rnd = new Random();
			var payment = new Payment { Number= rnd.Next(1, int.MaxValue), ProductSum = product.Price * quantity, PaymentAmount = product.Price * quantity };

			if (_paymentSystem.MakePayment(payment))
			{
				var invoice = new Invoice { Number= rnd.Next(1, int.MaxValue), Email = email, Sum = payment.ProductSum };
				_invoiceSystem.SendInvoice(invoice);
			}
		}
	}
}
