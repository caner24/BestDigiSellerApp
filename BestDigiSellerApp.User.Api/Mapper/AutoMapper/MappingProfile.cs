using AutoMapper;
using BestDigiSellerApp.User.Entity.Dto;

namespace BestDigiSellerApp.User.Api.Mapper.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User.Entity.User>();


        }
    }
}
