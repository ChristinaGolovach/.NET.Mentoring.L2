namespace Catalog.BussinesLayer.ApplicationModels
{
	public class PaginationConfig
	{
		private const int maxCountPerPage = 50;

		public int PageNumber { get; set; } = 1;

		private int _countPerPage;
		public int CountPerPage
		{
			get => _countPerPage;
			set
			{
				_countPerPage = (value > maxCountPerPage) ? maxCountPerPage : value;
			}
		}
	}
}
