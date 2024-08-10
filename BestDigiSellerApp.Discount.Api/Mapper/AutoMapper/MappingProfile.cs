using AutoMapper;
using BestDigiSellerApp.Discount.Entity.Dto;

namespace BestDigiSellerApp.Discount.Api.Mapper.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCouponCodeDto, Discount.Entity.Discount>();
        }
    }
}
