using AutoMapper;
using CrudWebApi.Dto;
using CrudWebApi.Model;

namespace CrudWebApi.Profiles
{
    public class EmployeeCreateDtoProfile : Profile
    {
        public EmployeeCreateDtoProfile()
        {
            //Source => Destination
            CreateMap<EmployeeCreateDto, TblEmployee>();
        }
    }
}
