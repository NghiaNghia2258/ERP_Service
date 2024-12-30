using ERP_Service.Domain.Abstractions.Repository.Products;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.PagingRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Products;

public class ProductCategoryRepository : RepositoryBase<ProductCategory, int>, IProductCategoryRepository
{
	public ProductCategoryRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}

	public async Task<bool> Create(ProductCategory model)
	{
		await CreateAsync(model);
		return true;
	}

	public async Task<bool> Delete(int id)
	{
		await DeleteAsync(id); 
		return true;
	}
	public async Task<IEnumerable<ProductCategory>> GetAll(OptionFilterProductCategory option)
	{
		var query = _dbContext.ProductCategories
			.Select(q => new ProductCategory
			{
				Id = q.Id,
				Name = q.Name,
			});

		return await query
			.Skip((option.PageIndex - 1) * option.PageSize)
			.Take(option.PageSize)
			.ToListAsync();
	}
	public async Task<ProductCategory> GetById(int id)
	{
		return await _dbContext.ProductCategories
			.FirstOrDefaultAsync(x => x.Id == id) ?? new ProductCategory(); 
	}

	public async Task<bool> Update(ProductCategory model)
	{
		await UpdateAsync(model); 
		return true;
	}
}
