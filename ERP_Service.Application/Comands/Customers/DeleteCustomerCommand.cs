using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Shared.Models;
using MediatR;

namespace ERP_Service.Application.Comands.Customers;

public class DeleteCustomerCommand : IRequest<ApiResult>
{
	public Guid Id { get; set; }
	public PayloadToken PayloadToken { get; set; }
	public DeleteCustomerCommand(Guid id, PayloadToken payloadToken)
	{
		Id = id;
		PayloadToken = payloadToken;
	}
}
public class DeleteCustomerCommandHandler : CommandHandlerBase, IRequestHandler<DeleteCustomerCommand, ApiResult>
{
	public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}
	public async Task<ApiResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
	{
		var customer = await _unitOfWork.CustomerRepository.GetById(request.Id);
		if (customer == null)
		{
			return new ApiNotFoundResult("Customer not found");
		}

		var isSuccess = await _unitOfWork.CustomerRepository.Delete(request.Id, request.PayloadToken);
		if (!isSuccess)
		{
			return new ApiErrorResult();
		}

		return new ApiSuccessResult<bool>(isSuccess);
	}
}