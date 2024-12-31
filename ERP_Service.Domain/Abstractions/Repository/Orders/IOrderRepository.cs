using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.PagingRequest;

namespace ERP_Service.Domain.Abstractions.Repository.Orders;

public interface IOrderRepository
{
	Task<int> CountOrderForCurrentMonth();
	Task<Guid> Create(Order model);
	Task<bool> Delete(Guid id);
	Task<IEnumerable<Order>> GetAll(OptionFilterOrder option);
	Task<Order> GetById(Guid id);
	Task<IEnumerable<Order>> GetOrderNotCompleted(OptionFilterOrder option);
	Task<bool> Update(Order model);
}
