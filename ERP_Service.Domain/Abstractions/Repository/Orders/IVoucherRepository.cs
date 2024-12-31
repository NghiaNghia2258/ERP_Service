using ERP_Service.Domain.Models.Orders;

namespace ERP_Service.Domain.Abstractions.Repository.Orders;

public interface IVoucherRepository
{
	Task<bool> Create(Voucher model);
	Task<bool> Delete(Guid id);
	Task<Voucher> GetById(Guid id);
	Task<bool> Update(Voucher model);
}
