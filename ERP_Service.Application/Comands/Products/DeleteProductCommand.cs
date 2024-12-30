using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using MediatR;

namespace ERP_Service.Application.Comands.Products;

public class DeleteProductCommand : IRequest<ApiResult>
{
	public int Id { get; set; }
	public DeleteProductCommand(int id)
	{
		Id = id;
	}
}
public class DeleteProductCommandHandler : CommandHandlerBase, IRequestHandler<DeleteProductCommand, ApiResult>
{
	public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}
	public async Task<ApiResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		var isSuccess = await _unitOfWork.Product.Delete(request.Id);
		if (!isSuccess)
		{
			return new ApiErrorResult();
		}

		return new ApiSuccessResult<bool>(isSuccess);
	}
}
