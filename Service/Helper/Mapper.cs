using AutoMapper;
using Entities;
using Service.Dto.Response;

namespace Service.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Users, UserResponse>();
        }
    }
}