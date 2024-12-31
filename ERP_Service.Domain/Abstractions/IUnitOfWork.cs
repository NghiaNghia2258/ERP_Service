using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Domain.Abstractions.Repository.Orders;
using ERP_Service.Domain.Abstractions.Repository.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERP_Service.Domain.Abstractions;

public interface IUnitOfWork: IDisposable
{
	public ICustomerRepository Customer { get; }

	#region Repository module Products
	public IProductRepository Product { get; }
	public IProductVariantRepository ProductVariant { get; }
	public IProductCategoryRepository ProductCategory { get; }
	public IProductImageRepository ProductImage { get; }
	public IProductRateRepository ProductRate { get; }
	#endregion
	#region Repository module Orders
	public IOrderRepository Order { get; }
	public IOrderItemRepository OrderItem { get; }
	public IVoucherRepository Voucher { get; }
	#endregion

	Task<IDbContextTransaction> BeginTransactionAsync();
	Task CommitAsync();
	Task EndTransactionAsync();
	DbContext GetDbContext();
	Task RollbackTransactionAsync();
}
