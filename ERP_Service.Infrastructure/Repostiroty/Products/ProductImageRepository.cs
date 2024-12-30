using ERP_Service.Domain.Abstractions.Repository.Products;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Products;

internal class ProductImageRepository : RepositoryBase<ProductImage, int>, IProductImageRepository
{
	public ProductImageRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}
	public async Task<bool> Create(ProductImage model)
	{
		await CreateAsync(model); 
		return true;
	}

	public async Task<bool> Delete(int id)
	{
		await DeleteAsync(id);
		return true;
	}
}
