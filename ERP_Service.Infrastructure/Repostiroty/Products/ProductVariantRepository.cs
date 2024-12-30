using ERP_Service.Domain.Abstractions.Repository.Products;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Products;

public class ProductVariantRepository : RepositoryBase<ProductVariant, int>, IProductVariantRepository
{
	public ProductVariantRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}

	public async Task<bool> Create(ProductVariant model)
	{
		await CreateAsync(model);
		return true;
	}

	public async Task<bool> Delete(int id)
	{
		await DeleteAsync(id); 
		return true;
	}

	public async Task<ProductVariant> GetById(int id)
	{
		return await _dbContext.ProductVariants
			.FirstOrDefaultAsync(x => x.Id == id) ?? new ProductVariant(); 
	}

	public async Task<bool> Update(ProductVariant model)
	{
		await UpdateAsync(model);
		return true;
	}
}
