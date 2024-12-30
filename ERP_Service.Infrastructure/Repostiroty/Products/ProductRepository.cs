using ERP_Service.Domain.Abstractions.Repository.Products;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.PagingRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty.Products;

public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
	public ProductRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
	{
	}

	public async Task<bool> Create(Product model)
	{
		await CreateAsync(model); 
		return true;
	}

	public async Task<bool> Delete(int id)
	{
		await DeleteAsync(id); 
		return true;
	}

	public async Task<IEnumerable<Product>> GetAll(OptionFilterProduct option)
	{
		var query = _dbContext.Products
			.Select(q => new Product
			{
				Id = q.Id,
				Name = q.Name,
				CreatedAt = q.CreatedAt,
				CreatedBy = q.CreatedBy
			});

		TotalRecords.PRODUCT = await query.CountAsync();
		return await query
			.Skip((option.PageIndex - 1) * option.PageSize)
			.Take(option.PageSize)
			.ToListAsync();
	}

	public async Task<Product> GetById(int id)
	{
		return await _dbContext.Products
			.FirstOrDefaultAsync(x => x.Id == id) ?? new Product();
	}

	public async Task<bool> Update(Product model)
	{
		await UpdateAsync(model); 
		return true;
	}
}
