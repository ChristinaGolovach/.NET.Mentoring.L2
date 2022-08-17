using System;

namespace Facade.InvoiceSystemService
{
	public class InvoiceSystem : IInvoiceSystem
	{
		public void SendInvoice(Invoice invoice)
		{
			Console.WriteLine($"the Invoice with number {invoice.Number} has been send to {invoice.Email}");
		}
	}
}
