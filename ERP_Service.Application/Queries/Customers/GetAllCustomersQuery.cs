using AutoMapper;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Application.Services.Interfaces;
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
	public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : base(unitOfWork, mapper, cacheService)
	{
	}
	public async Task<ApiResult> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
		var cacheKey = $"GetAllCustomers_{request.Option.PageIndex}_{request.Option.PageSize}";
		var cachedData = await _cacheService.GetAsync<IEnumerable<CustomerDto>>(cacheKey);
		if (cachedData != null)
		{
			await _cacheService.SetAsync(cacheKey, cachedData, TimeSpan.FromSeconds(10));
			return new ApiSuccessResult<IEnumerable<CustomerDto>>(cachedData)
			{
				FetchedRecordsCount = cachedData.Count(),
				TotalRecordsCount = TotalRecords.CUSTOMER,
				Message = "Get data from cache."
			};
		}
		var customers = await _unitOfWork.CustomerRepository.GetAll(request.Option);
		if (customers == null)
		{
			return new ApiNotFoundResult("Customers not found.");
		}
		var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
		await _cacheService.SetAsync(cacheKey, customerDtos, TimeSpan.FromSeconds(10));
		res = new ApiSuccessResult<IEnumerable<CustomerDto>>(customerDtos)
		{
			FetchedRecordsCount = customerDtos.Count(),
			TotalRecordsCount = TotalRecords.CUSTOMER
		};
		return res;
	}
}