using AutoMapper;
using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using MediatR;

namespace ERP_Service.Application.Comands.Orders;

public class CreateOrderUsePOSCommand: IRequest<ApiResult>
{
}
public class CreateOrderUsePOSCommandHandle : CommandHandlerBase, IRequestHandler<CreateOrderUsePOSCommand, ApiResult>
{
	public CreateOrderUsePOSCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
	}

	public async Task<ApiResult> Handle(CreateOrderUsePOSCommand request, CancellationToken cancellationToken)
	{
		// Tạo mã code theo định dạng: INV-POS-YYYYMMDD-XXXXXX
		// - "INV": Tiền tố chỉ hóa đơn.
		// - "POS": Chỉ hóa đơn được bán tại quầy (hoặc "WEB" cho đơn hàng đặt online).
		// - "YYYYMMDD": Ngày hiện tại khi tạo hóa đơn.
		// - "XXXXXX": Số thứ tự hóa đơn trong tháng, được định dạng 6 chữ số (thêm số 0 ở đầu nếu cần).
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
