using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class DeleteOrderCommand : IRequest<ApiResult>
{
	public DeleteOrderCommand(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; set; }
}
public class DeleteOrderCommandHandle : CommandHandlerBase, IRequestHandler<DeleteOrderCommand, ApiResult>
{
	public DeleteOrderCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Order.Delete(request.Id);

		return new ApiSuccessResult();
	}
}