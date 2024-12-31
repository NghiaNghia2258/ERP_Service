using ERP_Service.Domain.Models.Orders;

namespace ERP_Service.Domain.Abstractions.Repository.Orders;

public interface IOrderItemRepository
{
	Task<bool> Create(OrderItem model);
	Task<bool> Delete(int id);
	Task<OrderItem> GetById(int id);
	Task<bool> Update(OrderItem model);
}
