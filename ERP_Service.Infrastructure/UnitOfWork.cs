using ERP_Service.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Infrastructure.Repostiroty;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ERP_Service.Domain.Abstractions.Repository.Products;
using ERP_Service.Infrastructure.Repostiroty.Products;
using ERP_Service.Domain.Abstractions.Repository.Orders;
using ERP_Service.Infrastructure.Repostiroty.Orders;

namespace ERP_Service.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IConfiguration _config;

	#region Repository module Products
	private readonly ICustomerRepository _customerRepository;
	private readonly IProductRepository _productRepository;
	private readonly IProductVariantRepository _productVariantRepository;
	private readonly IProductCategoryRepository _productCategoryRepository;
	private readonly IProductImageRepository _productImageRepository;
	private readonly IProductRateRepository _productRateRepository;

	public ICustomerRepository Customer => _customerRepository ?? new CustomerRepository(_dbContext, _httpContextAccessor, _config);
	public IProductRepository Product => _productRepository ?? new ProductRepository(_dbContext, _httpContextAccessor, _config);
	public IProductVariantRepository ProductVariant => _productVariantRepository ?? new ProductVariantRepository(_dbContext, _httpContextAccessor, _config);
	public IProductCategoryRepository ProductCategory => _productCategoryRepository ?? new ProductCategoryRepository(_dbContext, _httpContextAccessor, _config);
	public IProductImageRepository ProductImage => _productImageRepository ?? new ProductImageRepository(_dbContext, _httpContextAccessor, _config);
	public IProductRateRepository ProductRate => _productRateRepository ?? new ProductRateRepository(_dbContext, _httpContextAccessor, _config);
	#endregion
	#region Repository module Orders
	private readonly IOrderRepository _orderRepository;
	private readonly IOrderItemRepository _orderItemRepository;
	private readonly IVoucherRepository _voucherRepository;

	public IOrderRepository Order => _orderRepository ?? new OrderRepository(_dbContext, _httpContextAccessor, _config);
	public IOrderItemRepository OrderItem => _orderItemRepository ?? new OrderItemRepository(_dbContext, _httpContextAccessor, _config);
	public IVoucherRepository Voucher => _voucherRepository ?? new VoucherRepository(_dbContext, _httpContextAccessor, _config);
	#endregion
#pragma warning disable CS8618
	public UnitOfWork(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}
	#pragma warning restore CS8618 
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
