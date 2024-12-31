using AutoMapper;
using ERP_Service.Application.Mapper.Model.Orders;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.PagingRequest;
using MediatR;

namespace ERP_Service.Application.Queries.Orders;

public class GetOrderNotCompletedQuery: IRequest<ApiResult>
{
	public GetOrderNotCompletedQuery(OptionFilterOrder option)
	{
		Option = option;
	}

	public OptionFilterOrder Option { get; set; }
}
public class GetOrderNotCompletedQueryHandle :QueryHandlerBase, IRequestHandler<GetOrderNotCompletedQuery, ApiResult>
{
	public GetOrderNotCompletedQueryHandle(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : base(unitOfWork, mapper, cacheService)
	{
	}

	public async Task<ApiResult> Handle(GetOrderNotCompletedQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<Order> orders = await _unitOfWork.Order.GetOrderNotCompleted(request.Option);
		IEnumerable<GetAllOrderDto> orderDto = _mapper.Map<IEnumerable<GetAllOrderDto>>(orders);
		return new ApiSuccessResult<IEnumerable<GetAllOrderDto>>(orderDto);
	}
}
