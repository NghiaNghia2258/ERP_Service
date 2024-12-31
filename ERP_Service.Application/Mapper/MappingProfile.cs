using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Application.Mapper.Model.Orders;
using ERP_Service.Application.Mapper.Model.Orders.OrderItems;
using ERP_Service.Application.Mapper.Model.Orders.Voucher;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Application.Mapper.Model.Products.Categories;
using ERP_Service.Application.Mapper.Model.Products.Images;
using ERP_Service.Application.Mapper.Model.Products.Variants;
using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Application.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Customer, CustomerDto>().ReverseMap();
		CreateMap<CreateCustomerDto, Customer>();

		CreateMap<CreateProductDto, Product>();
		CreateMap<Product, GetByIdProductDto>();
		CreateMap<Product, GetAllProductDto>();
		CreateMap<GetByIdProductDto, Product>()
			.ForMember(dest => dest.ProductVariants, opt => opt.Ignore())
			.ForMember(dest => dest.ProductImages, opt => opt.Ignore());

		CreateMap<CreateProductVariantDto, ProductVariant>();
		CreateMap<ProductVariant, ProductVariantDto>().ReverseMap();

		CreateMap<ProductImageDto, ProductImage>().ReverseMap();

		CreateMap<ProductCategory, ProductCategoryDto>();

		CreateMap<Order, GetByIdOrderDto>().ReverseMap();
		CreateMap<OrderItem, OrderItemDto>().ReverseMap();
		CreateMap<Voucher, VoucherDto>().ReverseMap();
		CreateMap<Order, GetAllOrderDto>();
	}
}
