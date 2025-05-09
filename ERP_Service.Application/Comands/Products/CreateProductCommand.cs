using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Products;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ERP_Service.Application.Comands.Products;

public class CreateProductCommand : IRequest<ApiResult>
{
	public CreateProductDto Model { get; set; }
	public CreateProductCommand(CreateProductDto model)
	{
		Model = model;
	}
}
public class CreateProductCommandHandler : CommandHandlerBase, IRequestHandler<CreateProductCommand, ApiResult>
{
    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
    {
    }

    public async Task<ApiResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
		using (var transaction = await _unitOfWork.BeginTransactionAsync())
		{
			try
			{
                Product newProduct = _mapper.Map<Product>(request.Model);
				newProduct.ImageUrls = ERP_Service.Shared.Utilities.JsonHelper.ConvertToJsonString(request.Model.ExistingUrls);
                int productId = await _unitOfWork.Product.Create(newProduct);
				foreach (var variant in request.Model.ProductVariants)
				{
					var newProductVariant = _mapper.Map<ProductVariant>(variant);
					newProductVariant.ProductId = productId;
					await _unitOfWork.ProductVariant.Create(newProductVariant);
				}
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				res = new ApiErrorResult(500,ex.InnerException == null ? ex.Message : ex.InnerException.Message);
			}
		}
		
		return res;
	}
}
