using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.Models.Orders;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class PaymentCommand : IRequest<ApiResult>
{
	public Guid OrderId { get; set; }
	public PaymentCommand(Guid orderId)
	{
		OrderId = orderId;
	}
}
public class PaymentCommandHandle : CommandHandlerBase, IRequestHandler<PaymentCommand, ApiResult>
{
	public PaymentCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(PaymentCommand request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
		using (var transaction = await _unitOfWork.BeginTransactionAsync())
		{
			try
			{
				Order order = await _unitOfWork.Order.GetById(request.OrderId);
				if (order == null)
				{
					return new ApiNotFoundResult("Not found Order");
				}
				order.PaymentStatus = StatusOrder.Completed;
				await _unitOfWork.Order.Update(order);
				await transaction.CommitAsync();
				return res;
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackTransactionAsync();
				return new ApiErrorResult(500,ex.Message);
			}
		}
	}
}