using AutoMapper;
using ERP_Service.Domain.Abstractions;

namespace ERP_Service.Application.Comands;

public class CommandHandlerBase
{
	protected IUnitOfWork _unitOfWork;
	protected IMapper _mapper;

	public CommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
}
