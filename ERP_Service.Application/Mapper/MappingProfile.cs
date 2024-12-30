using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Application.Mapper.Model.Products.Images;
using ERP_Service.Application.Mapper.Model.Products.Variants;
using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Application.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Customer, CustomerDto>().ReverseMap();
		CreateMap<CreateCustomerDto, Customer>();

		CreateMap<CreateProductDto, Product>();
		CreateMap<Product, GetByIdProductDto>().ReverseMap();

		CreateMap<CreateProductVariantDto, ProductVariant>();
		CreateMap<ProductVariant, ProductVariantDto>().ReverseMap();

		CreateMap<ProductImageDto, ProductImage>().ReverseMap();

	}
}
