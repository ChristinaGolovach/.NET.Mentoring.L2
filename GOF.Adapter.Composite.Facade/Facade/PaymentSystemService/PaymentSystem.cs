
namespace Facade.PaymentSystemService
{
	public class PaymentSystem : IPaymentSystem
	{
		public bool MakePayment(Payment payment)
		{
			return payment.PaymentAmount >= payment.ProductSum ? true : false;
		}
	}
}
