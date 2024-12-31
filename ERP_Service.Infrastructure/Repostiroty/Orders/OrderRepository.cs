using ERP_Service.Domain.Abstractions.Repository.Orders;
using ERP_Service.Domain.Models.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Orders;

public class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
{
	public OrderRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}
	public async Task<bool> Create(Order model)
	{
		await CreateAsync(model);
		return true;
	}
	public async Task<bool> Update(Order model)
	{
		await UpdateAsync(model);
		return true;
	}

	public async Task<bool> Delete(Guid id)
	{
		await DeleteAsync(id);
		return true;
	}

	public async Task<Order> GetById(Guid id)
	{
		return await _dbContext.Orders
			.Include(o => o.OrderItems)
			.Include(o => o.Customer)
			.FirstOrDefaultAsync(x => x.Id == id) ?? new Order();
	}
}
