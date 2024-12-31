using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class CreateOrderCommand: IRequest<ApiResult>
{
}
public class CreateOrderCommandHandle : CommandHandlerBase, IRequestHandler<CreateOrderCommand, ApiResult>
{
	public CreateOrderCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		// Tạo mã code
		// INV: Hóa đơn bán
		// POS: bán tại quầy - WEB: khách hàng đặt trên web
		// currentDate: Ngày tạo 
		// invoiceNumber: Số thứ tự hóa đơn trong tháng 
		int _monthlyInvoiceCounter = await _unitOfWork.Order.CountOrderForCurrentMonth();
		string currentDate = DateTime.Now.ToString("yyyyMMdd");
		string invoiceNumber = _monthlyInvoiceCounter.ToString("D6");
		string code = $"INV-POS-{currentDate}-{invoiceNumber}";

		// Tạo hóa đơn
		Guid id = await _unitOfWork.Order.Create(new Order()
		{
			Code = code
		});
		return new ApiSuccessResult<Guid>(id);
	}
}
