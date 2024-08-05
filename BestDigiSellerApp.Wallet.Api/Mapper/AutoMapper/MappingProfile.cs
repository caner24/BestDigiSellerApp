using AutoMapper;
using BestDigiSellerApp.Wallet.Entity.Dto;

namespace BestDigiSellerApp.Wallet.Api.Mapper.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateWalletDto, Wallet.Entity.Wallet>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserEmail))
              .ForMember(dest => dest.WalletDetails, opt => opt.Ignore())
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.CreateDate, opt => opt.Ignore());
        }
    }
}
