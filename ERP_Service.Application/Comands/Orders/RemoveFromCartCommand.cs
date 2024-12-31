using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class RemoveFromCartCommand: IRequest<ApiResult>
{
	public RemoveFromCartCommand(int orderItemId)
	{
		OrderItemId = orderItemId;
	}

	public int OrderItemId { get; set; }
}
public class RemoveFromCartCommandHandle : CommandHandlerBase, IRequestHandler<RemoveFromCartCommand, ApiResult>
{
	public RemoveFromCartCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.OrderItem.Delete(request.OrderItemId);
		return new ApiSuccessResult();
	}
}
