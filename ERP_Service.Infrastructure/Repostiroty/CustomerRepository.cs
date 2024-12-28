using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.Models;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.Infrastructure.Repostiroty;

public class CustomerRepository : RepositoryBase<Customer, Guid>, ICustomerRepository
{
	public CustomerRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public async Task<bool> Create(Customer model, PayloadToken payload)
	{
		await CreateAsync(model,payload);
		return true;
	}

	public async Task<bool> Delete(Guid id, PayloadToken payload)
	{
		await DeleteAsync(id, payload);
		return true;
	}

	public async Task<IEnumerable<Customer>> GetAll(OptionFilterCustomer option)
	{
		var query = _dbContext.Customers
			   .Select(q => new Customer
			   {
				   Id = q.Id,
				   Name = q.Name,
				   Phone = q.Phone,
				   CreatedAt = q.CreatedAt,
				   CreatedBy = q.CreatedBy
			   });
		TotalRecords.CUSTOMER = await query.CountAsync();
		return await query
			.Skip((option.PageIndex - 1) * option.PageSize)
			.Take(option.PageSize)
			.ToListAsync();
	}

	public async Task<Customer> GetById(Guid id)
	{
		return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id) ?? new Customer();
	}

	public async Task<bool> Update(Customer model, PayloadToken payload)
	{
		await UpdateAsync(model, payload);
		return true;
	}
}
