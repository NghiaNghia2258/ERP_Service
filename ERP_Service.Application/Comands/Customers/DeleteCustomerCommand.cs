using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Comands.Customers;

public class DeleteCustomerCommand : IRequest<ApiResult>
{
	public Guid Id { get; set; }
	public DeleteCustomerCommand(Guid id)
	{
		Id = id;
	}
}
public class DeleteCustomerCommandHandler : CommandHandlerBase, IRequestHandler<DeleteCustomerCommand, ApiResult>
{
	public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}
	public async Task<ApiResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
	{
		var customer = await _unitOfWork.Customer.GetById(request.Id);
		if (customer == null)
		{
			return new ApiNotFoundResult("Customer not found");
		}

		var isSuccess = await _unitOfWork.Customer.Delete(request.Id);
		if (!isSuccess)
		{
			return new ApiErrorResult();
		}

		return new ApiSuccessResult<bool>(isSuccess);
	}
}