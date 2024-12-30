using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Shared.Utilities;
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
				Product product = _mapper.Map<Product>(request.Model);
				await _unitOfWork.Product.Update(product);
				foreach (var image in request.Model.productImages)
				{
					if(!image.Id.HasValue)
					{
						var productImage = _mapper.Map<ProductImage>(image);
						productImage.ProductId = product.Id;
						await _unitOfWork.ProductImage.Create(productImage);
					}
					else if (image.IsDeleted ?? false)
					{
						UploadHelper uploadHelper = new UploadHelper();
						uploadHelper.DeleteFile(image.ImageUrl);
						await _unitOfWork.ProductImage.Delete(image.Id.Value);
					}
				}

				foreach (var variant in request.Model.ProductVariants)
				{
					if (!variant.Id.HasValue)
					{
						var productVariant = _mapper.Map<ProductVariant>(variant);
						productVariant.ProductId = product.Id;
						await _unitOfWork.ProductVariant.Create(productVariant);
					}
					else if (variant.IsDeleted ?? false)
					{
						await _unitOfWork.ProductVariant.Delete(variant.Id.Value);
					}
					else if (variant.IsEdited ?? false)
					{
						await _unitOfWork.ProductVariant.Update(_mapper.Map<ProductVariant>(variant));
					}
				}
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
