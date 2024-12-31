using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class UpdateOrderCommand : IRequest<ApiResult>
{
}
public class UpdateOrderCommandHandle : CommandHandlerBase, IRequestHandler<UpdateOrderCommand, ApiResult>
{
	public UpdateOrderCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}