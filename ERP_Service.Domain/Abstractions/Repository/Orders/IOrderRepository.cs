using ERP_Service.Domain.Models.Orders;

namespace ERP_Service.Domain.Abstractions.Repository.Orders;

public interface IOrderRepository
{
	Task<bool> Create(Order model);
	Task<bool> Delete(Guid id);
	Task<Order> GetById(Guid id);
	Task<bool> Update(Order model);
}
