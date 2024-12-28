using ERP_Service.Domain.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERP_Service.Domain.Abstractions;

public interface IUnitOfWork: IDisposable
{
	public ICustomerRepository CustomerRepository { get; }
	Task<IDbContextTransaction> BeginTransactionAsync();
	Task CommitAsync();
	Task EndTransactionAsync();
	DbContext GetDbContext();
	Task RollbackTransactionAsync();
}
