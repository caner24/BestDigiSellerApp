using AutoMapper;
using BestDigiSellerApp.Product.Entity.Dto;
using BestDigiSellerApp.Product.Entity;

namespace BestDigiSellerApp.Product.Api.Mapper.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryForCreateDto, Category>();
            CreateMap<PhotosForCreateDto, Photo>();
            CreateMap<ProductForCreateDto, Product.Entity.Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
         .ForMember(dest => dest.ProductDetail, opt => opt.MapFrom(src => new ProductDetail
         {
             PointPercentage = src.PointPercentage,
             MaxPoint = src.MaxPoint,
             Stock = src.Stock,
             Price = src.Price
         }));
        }
    }
}
