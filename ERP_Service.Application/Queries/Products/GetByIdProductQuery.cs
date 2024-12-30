using AutoMapper;
using ERP_Service.Application.Mapper.Model.Products;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Queries.Products;

public class GetByIdProductQuery : IRequest<ApiResult>
{
	public GetByIdProductQuery(int id)
	{
		Id = id;
	}
	public int Id { get; set; }

}

public class GetByIdProductQueryHandler : QueryHandlerBase, IRequestHandler<GetByIdProductQuery, ApiResult>
{
	public GetByIdProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();

		var product = await _unitOfWork.Product.GetById(request.Id);

		if (product == null)
		{
			res = new ApiNotFoundResult("product not found.");
			return res;
		}

		var productDto = _mapper.Map<GetByIdProductDto>(product);

		res = new ApiSuccessResult<GetByIdProductDto>(productDto);

		return res;
	}
}
