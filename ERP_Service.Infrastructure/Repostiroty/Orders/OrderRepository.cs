using ERP_Service.Domain.Abstractions.Repository.Orders;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.PagingRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Orders;

public class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
{
	public OrderRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}
	public async Task<Guid> Create(Order model)
	{
		Guid id = await CreateAsync(model);
		return id;
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
	public async Task<int> CountOrderForCurrentMonth()
	{
		var startOfMonth = new DateTime(TimeConst.Now.Year, TimeConst.Now.Month, 1); // Ngày đầu tiên của tháng hiện tại
		var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Ngày cuối cùng của tháng hiện tại

		return await _dbContext.Orders
		.Where(x => x.CreatedAt.Date >= startOfMonth && x.CreatedAt.Date <= endOfMonth)
		.CountAsync();
	}
	public async Task<IEnumerable<Order>> GetAll(OptionFilterOrder option)
	{

		var query = _dbContext.Orders
			.Select(o => new Order
			{
				Id = o.Id,
				Code = o.Code,
				Name = o.Name,
				PaymentStatus = o.PaymentStatus,
			});
		TotalRecords.ORDER = await query.CountAsync();
		return await query
			.Skip((option.PageIndex - 1) * option.PageSize)
			.Take(option.PageSize)
			.ToListAsync();
	}
	public async Task<IEnumerable<Order>> GetOrderNotCompleted(OptionFilterOrder option)
	{
		var query = _dbContext.Orders
		.Where(x => x.PaymentStatus != StatusOrder.Completed)
			.Select(o => new Order
			{
				Id = o.Id,
				Code = o.Code,
				Name = o.Name,
				PaymentStatus = o.PaymentStatus,
			});
		TotalRecords.ORDER = await query.CountAsync();
		return await query
			.Skip((option.PageIndex - 1) * option.PageSize)
			.Take(option.PageSize)
			.ToListAsync();
	}
}
