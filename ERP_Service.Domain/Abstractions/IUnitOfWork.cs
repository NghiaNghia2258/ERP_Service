using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERP_Service.Domain.Abstractions;

public interface IUnitOfWork: IDisposable
{
	//public IQuizRepository QuizRepository { get; }
	Task<IDbContextTransaction> BeginTransactionAsync();
	Task CommitAsync();
	Task EndTransactionAsync();
	DbContext GetDbContext();
	Task RollbackTransactionAsync();
}
