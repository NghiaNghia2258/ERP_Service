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

	public async Task<int> Create(Product model)
	{
		int id = await CreateAsync(model); 
		return id;
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
				Description = q.Description,
				MainImageUrl = q.MainImageUrl,
				TotalInventory = q.TotalInventory,
				CategoryId = q.CategoryId,
				CategoryName = q.Category.Name,

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
			.Include(x => x.ProductVariants)
			.Include(x => x.ProductImages)
			.Select(x => 
				new Product()
				{
					Id = x.Id,
					Name = x.Name,
					NameEn = x.NameEn,
					Description = x.Description,
					MainImageUrl = x.MainImageUrl,
					TotalInventory = x.TotalInventory,
					CategoryId = x.CategoryId,
					CategoryName = x.CategoryName,
					Version = x.Version,
					ProductVariants = x.ProductVariants.Select(y => new ProductVariant()
					{
						Id = y.Id,
						ImageUrl = y.ImageUrl,
						Inventory = y.Inventory,
						Version = y.Version,
					}).ToList(),
					ProductImages = x.ProductImages.Select(y => new ProductImage
					{
						Id = y.Id,
						ImageUrl = y.ImageUrl
					}).ToList()
				}
			)
			.FirstOrDefaultAsync(x => x.Id == id) ?? new Product();
	}

	public async Task<bool> Update(Product model)
	{
		await UpdateAsync(model); 
		return true;
	}
}
