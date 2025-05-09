using ERP_Service.Domain.Abstractions.Repository.Orders;
using ERP_Service.Domain.Models.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Orders;

public class OrderItemRepository : RepositoryBase<OrderItem, int>, IOrderItemRepository
{
	public OrderItemRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}
	public async Task<bool> Create(OrderItem model)
	{
		await CreateAsync(model);
		return true;
	}
	public async Task<bool> Update(OrderItem model)
	{
		await UpdateAsync(model);
		return true;
	}
	public async Task<bool> Delete(int id)
	{
		await DeleteAsync(id);
		return true;
	}
	public async Task<OrderItem> GetById(int id)
	{
		//return await _dbContext.OrderItems
		//	.FirstOrDefaultAsync(x => x.Id == id) ?? new OrderItem();
		return new();
	}
}
