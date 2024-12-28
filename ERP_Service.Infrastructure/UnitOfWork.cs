using ERP_Service.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Infrastructure.Repostiroty;

namespace ERP_Service.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _dbContext;

	public ICustomerRepository CustomerRepository => _customerRepository ?? new CustomerRepository(_dbContext);

	private readonly ICustomerRepository _customerRepository;

	public UnitOfWork(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public DbContext GetDbContext()
	{
		return _dbContext;
	}
	public async Task CommitAsync()
	{
		await _dbContext.SaveChangesAsync();
	}
	public Task<IDbContextTransaction> BeginTransactionAsync()
	{
		return _dbContext.Database.BeginTransactionAsync();
	}
	public Task RollbackTransactionAsync()
	{
		return _dbContext.Database.RollbackTransactionAsync();
	}
	public async Task EndTransactionAsync()
	{
		await CommitAsync();
		await _dbContext.Database.CommitTransactionAsync();
	}

	public void Dispose()
	{
		_dbContext.Dispose();
	}
}
