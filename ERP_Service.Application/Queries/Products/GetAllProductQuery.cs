using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.PagingRequest;
using MediatR;

namespace ERP_Service.Application.Queries.Products;

public class GetAllProductQuery : IRequest<ApiResult>
{
	public GetAllProductQuery(OptionFilterProduct option)
	{
		Option = option;
	}

	public OptionFilterProduct Option { get; set; }
}

public class GetAllProductQueryHandler : QueryHandlerBase, IRequestHandler<GetAllProductQuery, ApiResult>
{
	public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();

		var products = await _unitOfWork.Product.GetAll(request.Option);

		if (products == null)
		{
			res = new ApiNotFoundResult("product not found.");
			return res;
		}

		var productsDto = _mapper.Map<IEnumerable<GetAllProductDto>>(products);

		res = new ApiSuccessResult<IEnumerable<GetAllProductDto>>(productsDto)
		{
			TotalRecordsCount = TotalRecords.PRODUCT,
			FetchedRecordsCount = productsDto.LongCount()
		};

		return res;
	}
}


