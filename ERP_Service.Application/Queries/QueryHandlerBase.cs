using AutoMapper;
using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Application.Queries;

public class QueryHandlerBase
{
	protected IUnitOfWork _unitOfWork;
	protected IMapper _mapper;

	public QueryHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
}

