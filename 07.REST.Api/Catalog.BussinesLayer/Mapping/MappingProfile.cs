using AutoMapper;
using Catalog.BussinesLayer.Models;
using Catalog.DataLayer.Entities;

namespace Catalog.BussinesLayer.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CategoryModel, Category>().ReverseMap();
			CreateMap<ItemModel, Item>().ReverseMap();
		}
	}
}
