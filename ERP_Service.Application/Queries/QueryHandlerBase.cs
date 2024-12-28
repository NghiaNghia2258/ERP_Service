using AutoMapper;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Application.Queries;

public class QueryHandlerBase
{
	protected readonly IUnitOfWork _unitOfWork;
	protected readonly IMapper _mapper;
	protected readonly ICacheService? _cacheService;

	public QueryHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public QueryHandlerBase(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_cacheService = cacheService;
	}
}

