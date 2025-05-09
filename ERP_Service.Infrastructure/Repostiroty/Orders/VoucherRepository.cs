using ERP_Service.Domain.Abstractions.Repository.Orders;
using ERP_Service.Domain.Models.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Orders;

public class VoucherRepository : RepositoryBase<Voucher, Guid>, IVoucherRepository
{
	public VoucherRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}
	public async Task<bool> Create(Voucher model)
	{
		await CreateAsync(model);
		return true;
	}
	public async Task<bool> Update(Voucher model)
	{
		await UpdateAsync(model);
		return true;
	}
	public async Task<bool> Delete(Guid id)
	{
		await DeleteAsync(id);
		return true;
	}
	public async Task<Voucher> GetById(Guid id)
	{
		//return await _dbContext.Vouchers
		//	.FirstOrDefaultAsync(x => x.Id == id) ?? new Voucher();
		return new();
	}
}
