using AutoMapper;
using ERP_Service.Application.Mapper.Model.Orders;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class UpdateOrderCommand : IRequest<ApiResult>
{
	public UpdateOrderCommand(UpdateOrderDto model)
	{
		Model = model;
	}

	public UpdateOrderDto Model { get; set; }
}
public class UpdateOrderCommandHandle : CommandHandlerBase, IRequestHandler<UpdateOrderCommand, ApiResult>
{
	public UpdateOrderCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		Order order = _mapper.Map<Order>(request.Model);
		await _unitOfWork.Order.Update(order);
		return new ApiSuccessResult();
	}
}