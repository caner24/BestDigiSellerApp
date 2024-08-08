using AutoMapper;
using BestDigiSellerApp.Basket.Entity.Dto;
using BestDigiSellerApp.Basket.Entity;

namespace BestDigiSellerApp.Basket.Api.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketForCreateDto, ShoppingCart>()
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
          .ForMember(dest => dest.Items, opt => opt.MapFrom(src => new List<ShoppingCartItem>
          {
                new ShoppingCartItem
                {
                    Quantity = src.Quantity,
                    Price = src.Price,
                    ProductId = src.ProductId,
                    ImageFile = src.ImageFile,
                    MaxPoint = src.MaxPoint,
                    PointPercentage = src.PointPercentage,
                    ProductName = src.ProductName
                }
          }));
        }
    }
}
