using ERP_Service.Domain.Abstractions.Repository.Products;
using ERP_Service.Domain.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Products;

public class ProductRateRepository : RepositoryBase<ProductRate, int>, IProductRateRepository
{
	public ProductRateRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}
	public async Task<bool> Create(ProductRate model)
	{
		await CreateAsync(model);
		return true;
	}

	public async Task<bool> Update(ProductRate model)
	{
		await UpdateAsync(model); 
		return true;
	}

	public async Task<bool> Delete(int id)
	{
		await DeleteAsync(id);
		return true;
	}
}
