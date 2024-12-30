using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Application.Mapper.Model.Products.Categories;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.PagingRequest;
using MediatR;

namespace ERP_Service.Application.Queries.Products;

public class GetAllProductCategoryQuery : IRequest<ApiResult>
{
	public GetAllProductCategoryQuery(OptionFilterProductCategory option)
	{
		Option = option;
	}

	public OptionFilterProductCategory Option { get; set; }
}

public class GetAllProductCategoryQueryHandler : QueryHandlerBase, IRequestHandler<GetAllProductCategoryQuery, ApiResult>
{
	public GetAllProductCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();

		var categories = await _unitOfWork.ProductCategory.GetAll(request.Option);

		if (categories == null)
		{
			res = new ApiNotFoundResult("product not found.");
			return res;
		}

		var categoriesDto = _mapper.Map<IEnumerable<ProductCategoryDto>>(categories);

		res = new ApiSuccessResult<IEnumerable<ProductCategoryDto>>(categoriesDto);

		return res;
	}
}
