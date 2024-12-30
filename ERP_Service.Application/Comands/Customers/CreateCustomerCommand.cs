using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models;
using ERP_Service.Shared.Models;
using MediatR;

namespace ERP_Service.Application.Comands.Customers;


public class CreateCustomerCommand : IRequest<ApiResult>
{
	public CreateCustomerDto CreateCustomerDto { get; set; }
	public CreateCustomerCommand(CreateCustomerDto createCustomerDto)
	{
		CreateCustomerDto = createCustomerDto;
	}
}

public class CreateCustomerCommandHandle : CommandHandlerBase, IRequestHandler<CreateCustomerCommand, ApiResult>
{
	public CreateCustomerCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
		Customer newCustomer = _mapper.Map<Customer>(request.CreateCustomerDto);
		bool isSuccess = await _unitOfWork.Customer.Create(newCustomer);
		if(!isSuccess)
		{
			res = new ApiErrorResult();
		}
		return res;
	}
}
