using AutoMapper;
using CrudWebApi.Dto;
using CrudWebApi.Model;

namespace CrudWebApi.Profiles
{
    public class EmployeeReadDtoProfile : Profile
    {
        public EmployeeReadDtoProfile()
        {
            // Source -> Destination
            CreateMap<TblEmployee, EmployeeReadDto>();
        }
    }
}
