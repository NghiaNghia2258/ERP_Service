using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products.Variants;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Queries.Products;

public class GetProductVariantByProductIdQuery: IRequest<ApiResult>
{
	public int ProductId { get; set; }
	public GetProductVariantByProductIdQuery(int productId)
	{
		ProductId = productId;
	}
}
public class GetProductVariantByProductIdQueryHandler : QueryHandlerBase, IRequestHandler<GetProductVariantByProductIdQuery, ApiResult>
{
	public GetProductVariantByProductIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}
	public async Task<ApiResult> Handle(GetProductVariantByProductIdQuery request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();

		var productVariants = await _unitOfWork.ProductVariant.GetProductVariantsByProductId(request.ProductId);

		if (productVariants == null)
		{
			res = new ApiNotFoundResult("product variant not found.");
			return res;
		}

		var productVariantsDto = _mapper.Map<IEnumerable<ProductVariantDto>>(productVariants);

		res = new ApiSuccessResult<IEnumerable<ProductVariantDto>>(productVariantsDto);

		return res;
	}
}
