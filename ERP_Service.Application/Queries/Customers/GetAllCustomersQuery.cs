using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.PagingRequest;
using MediatR;

namespace ERP_Service.Application.Queries.Customers;

public class GetAllCustomersQuery : IRequest<ApiResult>
{
	public GetAllCustomersQuery(OptionFilterCustomer option)
	{
		Option = option;
	}

	public OptionFilterCustomer Option { get; set; }
}

public class GetAllCustomersQueryHandler : QueryHandlerBase, IRequestHandler<GetAllCustomersQuery, ApiResult>
{
	public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
		var customers = await _unitOfWork.CustomerRepository.GetAll(request.Option);
		if (customers == null)
		{
			res = new ApiNotFoundResult("Customers not found.");
		}
		else
		{
			var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
			res = new ApiSuccessResult<IEnumerable<CustomerDto>>(customerDtos)
			{
				FetchedRecordsCount = customerDtos.Count(),
				TotalRecordsCount = TotalRecords.CUSTOMER
			};
		}
		return res;
	}
}