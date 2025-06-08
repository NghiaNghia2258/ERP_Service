using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.Application.Comands.Products;

public class UpdateProductCommand : IRequest<ApiResult>
{
	public UpdateProductCommand(GetByIdProductDto model)
	{
		Model = model;
	}

	public GetByIdProductDto Model { get; set; }
}

public class UpdateProductCommandHandler : CommandHandlerBase, IRequestHandler<UpdateProductCommand, ApiResult>
{
	public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
        AppDbContext context = _unitOfWork.GetDbContext() as AppDbContext;
        Product getById = context.Products.Include(x => x.ProductVariants).FirstOrDefault(x => x.Id == request.Model.Id);

        if (getById is null) throw new Exception("Not found to update");
        for (int i = 0; i < request.Model.ProductVariants.Count; i++)
        {
            request.Model.ProductVariants[i].CreatedAt = getById.CreatedAt;
            request.Model.ProductVariants[i].CreatedBy = getById.CreatedBy;
            request.Model.ProductVariants[i].CreatedName = getById.CreatedName;
        }
        getById.Name = request.Model.Name;
        getById.NameEn = request.Model.NameEn;
        getById.Description = request.Model.Description;
        getById.MainImageUrl = request.Model.ExistingUrls.FirstOrDefault();
        getById.ImageUrls = ERP_Service.Shared.Utilities.JsonHelper.ConvertToJsonString(request.Model.ExistingUrls);
        getById.TotalInventory = request.Model.TotalInventory ?? 0;
        getById.CategoryId = request.Model.CategoryId;
        getById.BrandId = request.Model.BrandId;
        getById.IsPhysicalProduct = request.Model.IsPhysicalProduct;
        getById.Weight = request.Model.Weight;
        getById.UnitWeight = request.Model.UnitWeight;
        getById.PropertyName1 = request.Model.PropertyName1;
        getById.PropertyName2 = request.Model.PropertyName2;
        getById.OriginalPrice = request.Model.OriginalPrice;
        getById.Price = request.Model.Price;

        getById.PropertyValue1 = string.Join(",", request.Model.PropertyValue1);
        getById.PropertyValue2 = string.Join(",", request.Model.PropertyValue2);
        getById.Specifications = ERP_Service.Shared.Utilities.JsonHelper.ConvertToJsonString(request.Model.Specifications);

        foreach (var variantDto in request.Model.ProductVariants)
        {
            ProductVariant? variant = await context.ProductVariants.Where(x => x.ProductId == getById.Id
            && variantDto.PropertyValue1 == x.PropertyValue1
            && variantDto.PropertyValue2 == x.PropertyValue2
            ).FirstOrDefaultAsync();

            if(variant == null)
            {
                getById.ProductVariants.Add(new ProductVariant
                {
                    Price = variantDto.Price,
                    IsActivate = variantDto.IsActivate,
                    Inventory = variantDto.Stock,
                    PropertyValue1 = variantDto.PropertyValue1,
                    PropertyValue2 = variantDto.PropertyValue2,
                    CreatedName = "admin",
                    CreatedBy = "admin",
                });      
            }
            else
            {
                variant.Price = variantDto.Price;
                variant.ImageUrl = variantDto.Image;
                variant.IsActivate = variantDto.IsActivate;
                variant.Inventory = variantDto.Stock;
            }
        }
        await context.SaveChangesAsync();

		return res;
	}
}
