using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Domain.Models;

namespace ERP_Service.Application.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Customer, CustomerDto>().ReverseMap();
		CreateMap<CreateCustomerDto, Customer>();
	}
}
