using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Products;
using MediatR;

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
	public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
		using (var transaction = await _unitOfWork.BeginTransactionAsync())
		{
			try
			{
				var newProduct = _mapper.Map<Product>(request.Model);
				int productId = await _unitOfWork.Product.Create(newProduct);
				foreach (var variant in request.Model.CreateProductVariants)
				{
					var newProductVariant = _mapper.Map<ProductVariant>(variant);
					newProductVariant.ProductId = productId;
					await _unitOfWork.ProductVariant.Create(newProductVariant);
				}
				foreach (var image in request.Model.CreateProductImages)
				{
					var newProductImage = _mapper.Map<ProductImage>(image);
					newProductImage.ProductId = productId;
					await _unitOfWork.ProductImage.Create(newProductImage);
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
