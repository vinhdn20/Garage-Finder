using AutoMapper;
using DataAccess.DTO;
using GFData.Models.Entity;

namespace Garage_Finder_Backend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsersDTO, Users>();
        }
    }
}
