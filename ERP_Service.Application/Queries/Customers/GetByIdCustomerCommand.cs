using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Queries.Customers;

public class GetByIdCustomerCommand: IRequest<ApiResult>
{
	public Guid Id;

	public GetByIdCustomerCommand(Guid id)
	{
		Id = id;
	}
}

public class GetByIdCustomerCommandHandler : QueryHandlerBase, IRequestHandler<GetByIdCustomerCommand, ApiResult>
{
	public GetByIdCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(GetByIdCustomerCommand request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();

		var customer = await _unitOfWork.Customer.GetById(request.Id);

		if (customer == null || customer.Id == Guid.Empty)
		{
			res = new ApiNotFoundResult("Customer not found.");
			return res;
		}

		var customerDto = _mapper.Map<CustomerDto>(customer);

		res = new ApiSuccessResult<CustomerDto>(customerDto);

		return res;
	}
}
