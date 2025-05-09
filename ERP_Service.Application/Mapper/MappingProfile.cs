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
using ERP_Service.Shared.Utilities;
using System.Text.Json;
using Newtonsoft.Json;

namespace ERP_Service.Application.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Customer, CustomerDto>().ReverseMap();
		CreateMap<CreateCustomerDto, Customer>();

		CreateMap<CreateProductDto, Product>()
           .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(src => JsonHelper.ConvertToJsonString(src.ExistingUrls)))
            .ForMember(dest => dest.PropertyValue1,
                opt => opt.MapFrom(src => string.Join(",", src.PropertyValue1)))
            .ForMember(dest => dest.PropertyValue2,
                opt => opt.MapFrom(src => string.Join(",", src.PropertyValue2)))
            .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(src => src.ExistingUrls))
            .ForMember(dest => dest.MainImageUrl,
                opt => opt.MapFrom(src => src.ExistingUrls.FirstOrDefault()))
            .ForMember(dest => dest.TotalInventory,
                opt => opt.MapFrom(src => src.ProductVariants.Sum(x => x.Stock)))
            .ForMember(dest => dest.Specifications,
                opt => opt.MapFrom(src => JsonHelper.ConvertToJsonString(src.Specifications)))
            ;
        CreateMap<Product, CreateProductDto>()
             .ForMember(dest => dest.ExistingUrls,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.ImageUrls)
                        ? new List<string>()
                        : JsonConvert.DeserializeObject<List<string>>(src.ImageUrls)))
            .ForMember(dest => dest.PropertyValue1,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.PropertyValue1)
                        ? new List<string>()
                        : src.PropertyValue1.Split(',').ToList()))
            .ForMember(dest => dest.PropertyValue2,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.PropertyValue2)
                        ? new List<string>()
                        : src.PropertyValue2.Split(',').ToList()))
            .ForMember(dest => dest.Specifications,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.Specifications)
                        ? new List<ProductSpecificationAttribute>()
                        : JsonConvert.DeserializeObject<List<ProductSpecificationAttribute>>(src.Specifications)))
            ;


        CreateMap<VariantCreate, ProductVariant>()
             .ForMember(dest => dest.Inventory,
                opt => opt.MapFrom(src => src.Stock))
             .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.Image)).ReverseMap();

        CreateMap<Product, GetByIdProductDto>()
             .ForMember(dest => dest.ExistingUrls,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.ImageUrls)
                        ? new List<string>()
                        : JsonConvert.DeserializeObject<List<string>>(src.ImageUrls)))
            .ForMember(dest => dest.PropertyValue1,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.PropertyValue1)
                        ? new List<string>()
                        : src.PropertyValue1.Split(',').ToList()))
            .ForMember(dest => dest.PropertyValue2,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.PropertyValue2)
                        ? new List<string>()
                        : src.PropertyValue2.Split(',').ToList()))
            .ForMember(dest => dest.Specifications,
                opt => opt.MapFrom((src, dest) =>
                    string.IsNullOrEmpty(src.Specifications)
                        ? new List<ProductSpecificationAttribute>()
                        : JsonConvert.DeserializeObject<List<ProductSpecificationAttribute>>(src.Specifications)))
            ;
        CreateMap<Product, GetAllProductDto>();
		CreateMap<ProductVariant, ResponseGetAllProductVariant>().ReverseMap();
        CreateMap<GetByIdProductDto, Product>()
            .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(src => JsonHelper.ConvertToJsonString(src.ExistingUrls)))
            .ForMember(dest => dest.PropertyValue1,
                opt => opt.MapFrom(src => string.Join(",", src.PropertyValue1)))
            .ForMember(dest => dest.PropertyValue2,
                opt => opt.MapFrom(src => string.Join(",", src.PropertyValue2)))
            .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(src => src.ExistingUrls))
            .ForMember(dest => dest.MainImageUrl,
                opt => opt.MapFrom(src => src.ExistingUrls.FirstOrDefault()))
            .ForMember(dest => dest.TotalInventory,
                opt => opt.MapFrom(src => src.ProductVariants.Sum(x => x.Stock)))
            .ForMember(dest => dest.Specifications,
                opt => opt.MapFrom(src => JsonHelper.ConvertToJsonString(src.Specifications)));

		CreateMap<CreateProductVariantDto, ProductVariant>();
		CreateMap<ProductVariant, ProductVariantDto>().ReverseMap();

		CreateMap<ProductCategory, ProductCategoryDto>();

		CreateMap<Order, GetByIdOrderDto>().ReverseMap();
		CreateMap<UpdateOrderDto, Order>();
		CreateMap<OrderItem, OrderItemDto>().ReverseMap();
		CreateMap<CreateOrderItemDto, OrderItem>();
		CreateMap<Voucher, VoucherDto>().ReverseMap();
		CreateMap<Order, GetAllOrderDto>();
	}
}
