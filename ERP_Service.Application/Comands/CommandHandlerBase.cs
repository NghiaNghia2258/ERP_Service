using AutoMapper;
using ERP_Service.Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace ERP_Service.Application.Comands;

public class CommandHandlerBase
{
	protected readonly IUnitOfWork _unitOfWork;
	protected readonly IMapper _mapper;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public CommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    public CommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
        _httpContextAccessor = httpContextAccessor;

    }
}
