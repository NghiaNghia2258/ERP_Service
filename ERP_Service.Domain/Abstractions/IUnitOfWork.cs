using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Domain.Abstractions.Repository.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERP_Service.Domain.Abstractions;

public interface IUnitOfWork: IDisposable
{
	public ICustomerRepository Customer { get; }
	public IProductRepository Product { get; }
	public IProductVariantRepository ProductVariant { get; }
	public IProductCategoryRepository ProductCategory { get; }
	public IProductImageRepository ProductImage { get; }
	public IProductRateRepository ProductRate { get; }

	Task<IDbContextTransaction> BeginTransactionAsync();
	Task CommitAsync();
	Task EndTransactionAsync();
	DbContext GetDbContext();
	Task RollbackTransactionAsync();
}
