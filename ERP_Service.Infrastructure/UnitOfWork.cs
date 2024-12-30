﻿using ERP_Service.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Infrastructure.Repostiroty;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ERP_Service.Domain.Abstractions.Repository.Products;
using ERP_Service.Infrastructure.Repostiroty.Products;

namespace ERP_Service.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IConfiguration _config;


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



	public UnitOfWork(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config)
	{
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
		_config = config;
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
