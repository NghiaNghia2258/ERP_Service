using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Shared.Models;
using MediatR;

namespace ERP_Service.Application.Comands.Customers;

public class UpdateCustomerCommand : IRequest<ApiResult>
{
	public CustomerDto Model { get; set; }
	public PayloadToken PayloadToken { get; set; }

	public UpdateCustomerCommand(CustomerDto model, PayloadToken payloadToken)
	{
		Model = model;
		PayloadToken = payloadToken;
	}
}
public class UpdateCustomerCommandHandler : CommandHandlerBase, IRequestHandler<UpdateCustomerCommand, ApiResult>
{
	public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
	{
		var customer = await _unitOfWork.CustomerRepository.GetById(request.Model.Id);
		if (customer == null)
		{
			return new ApiNotFoundResult("Customer not found");
		}

		_mapper.Map(request.Model, customer);
		var isSuccess = await _unitOfWork.CustomerRepository.Update(customer, request.PayloadToken);

		if (!isSuccess)
		{
			return new ApiErrorResult();
		}

		return new ApiSuccessResult<bool>(isSuccess);
	}
}
