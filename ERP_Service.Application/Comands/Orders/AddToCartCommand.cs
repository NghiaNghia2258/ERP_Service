using AutoMapper;
using ERP_Service.Application.Mapper.Model.Orders.OrderItems;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.Models.Products;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class AddToCartCommand: IRequest<ApiResult>
{
	public AddToCartCommand(CreateOrderItemDto model)
	{
		Model = model;
	}

	public CreateOrderItemDto Model { get; set; }
}
public class AddToCartCommandHandle : CommandHandlerBase, IRequestHandler<AddToCartCommand, ApiResult>
{
	public AddToCartCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(AddToCartCommand request, CancellationToken cancellationToken)
	{
		ApiResult res = new ApiSuccessResult();
		using (var transaction = await _unitOfWork.BeginTransactionAsync())
		{
			try
			{
				var order = await _unitOfWork.Order.GetById(request.Model.OrderId);
				if (order == null)
				{
					return new ApiNotFoundResult("Not found Order");
				}

				#region Thêm sản phẩm vào giỏ hàng
				OrderItem? isExist = order.OrderItems.FirstOrDefault(x => x.ProductVariantId == request.Model.ProductVariantId);
				if (isExist != null)
				{
					isExist.Quantity += request.Model.Quantity;
				}
				else
				{
					var orderItem = _mapper.Map<OrderItem>(request.Model);
					await _unitOfWork.OrderItem.Create(orderItem);
				}
				#endregion

				#region Trừ số lượng tồn kho của sản phẩm 
				ProductVariant productVariant = await _unitOfWork.ProductVariant.GetByIdWithProduct(request.Model.ProductVariantId);
				productVariant.DeductInventory(request.Model.Quantity);
				await _unitOfWork.ProductVariant.Update(productVariant);

				productVariant?.Product?.DeductInventory(request.Model.Quantity);
				await _unitOfWork.Product.Update(productVariant.Product);
				#endregion

				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackTransactionAsync();
				return new ApiErrorResult(500, ex.Message);
			}
		}
		return res;
	}
}
