using AutoMapper;
using ERP_Service.Application.Comands;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.PagingRequest;
using MediatR;

namespace ERP_Service.Application.Queries.Orders;

public class GetAllOrderQuery: IRequest<ApiResult>
{
	public GetAllOrderQuery(OptionFilterOrder option)
	{
		Option = option;
	}

	public OptionFilterOrder Option { get; set; }
}
public class GetAllOrderQueryHandle :CommandHandlerBase, IRequestHandler<GetAllOrderQuery, ApiResult>
{
	public GetAllOrderQueryHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}
	public async Task<ApiResult> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
