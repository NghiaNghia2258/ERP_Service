using AutoMapper;
using ERP_Service.Application.Mapper.Model.Orders;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using MediatR;

namespace ERP_Service.Application.Queries.Orders;

public class GetByIdOrderQuery: IRequest<ApiResult>
{
	public Guid Id { get; set; }

	public GetByIdOrderQuery(Guid id)
	{
		Id = id;
	}
}
public class GetByIdOrderQueryHandle :QueryHandlerBase, IRequestHandler<GetByIdOrderQuery, ApiResult>
{
	public GetByIdOrderQueryHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
	{
		Order order = await _unitOfWork.Order.GetById(request.Id);
		if (order == null)
		{
			return new ApiNotFoundResult("Order not found");
		}
		GetByIdOrderDto orderDto = _mapper.Map<GetByIdOrderDto>(order);
		return new ApiSuccessResult<GetByIdOrderDto>(orderDto);
	}
}
