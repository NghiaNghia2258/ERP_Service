using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Products;
using MediatR;

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
		using (var transaction = await _unitOfWork.BeginTransactionAsync())
		{
			try
			{
				Product getById = await _unitOfWork.Product.GetById(request.Model.Id);
				if (getById is null) throw new Exception("Not found to update");
                for (int i = 0; i < request.Model.ProductVariants.Count; i++)
                {
					request.Model.ProductVariants[i].CreatedAt = getById.CreatedAt;
					request.Model.ProductVariants[i].CreatedBy = getById.CreatedBy;
					request.Model.ProductVariants[i].CreatedName = getById.CreatedName;
                }
                Product product = _mapper.Map<Product>(request.Model);
				product.StoreId = getById.StoreId;
				product.SellCount = getById.SellCount;
				product.CreatedAt = getById.CreatedAt;
				product.CreatedBy = getById.CreatedBy;
				product.CreatedName = getById.CreatedName;
                product.ImageUrls = ERP_Service.Shared.Utilities.JsonHelper.ConvertToJsonString(request.Model.ExistingUrls);

				await _unitOfWork.ProductVariant.DeleteVariantsByProductId(product.Id);
                await _unitOfWork.Product.Update(product);

                await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				res = new ApiErrorResult(500, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
			}
		}

		return res;
	}
}
