using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class AddToCartCommand: IRequest<ApiResult>
{
}
public class AddToCartCommandHandle : CommandHandlerBase, IRequestHandler<AddToCartCommand, ApiResult>
{
	public AddToCartCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(AddToCartCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
