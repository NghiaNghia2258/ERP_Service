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
		model.StoreId = _payloadToken.StoreId;
		model.ProductVariants = new List<ProductVariant>();
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
			.Include(p => p.Category)
			.Include(p => p.ProductRates)
			.Include(p => p.ProductVariants)
			.Where(x => x.StoreId == _payloadToken.StoreId)
            .Select(q => new Product
			{
				Id = q.Id,
				Name = q.Name,
				MainImageUrl = q.MainImageUrl,
				TotalInventory = q.TotalInventory,
				CategoryName = q.Category.Name,
				PropertyName1 = q.PropertyName1,
				PropertyName2 = q.PropertyName2,
				ProductRates = q.ProductRates,
				ProductVariants = q.ProductVariants.Where(x => x.IsActivate).ToList()
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
			.Select(x => 
				new Product()
				{
                    Id = x.Id,
                    Name = x.Name,
                    NameEn = x.NameEn,
					Price = x.Price,
					OriginalPrice = x.OriginalPrice,
                    Description = x.Description,
                    MainImageUrl = x.MainImageUrl,
                    TotalInventory = x.TotalInventory,
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    BrandId = x.BrandId,
                    BrandName = x.BrandName,
                    IsPhysicalProduct = x.IsPhysicalProduct,
                    Weight = x.Weight,
                    UnitWeight = x.UnitWeight,
                    PropertyName1 = x.PropertyName1,
                    PropertyName2 = x.PropertyName2,
                    Version = x.Version,
					PropertyValue1 = x.PropertyValue1,
					PropertyValue2 = x.PropertyValue2,
					Specifications = x.Specifications,
					StoreId = x.StoreId,
					CreatedAt = x.CreatedAt,
					CreatedBy = x.CreatedBy,
					CreatedName = x.CreatedName,
					ProductVariants = x.ProductVariants.Select(y => new ProductVariant()
					{
						Id = y.Id,
						PropertyValue1 = y.PropertyValue1,
						PropertyValue2 = y.PropertyValue2,
						ImageUrl = y.ImageUrl,
						Price = y.Price,
						Inventory = y.Inventory,
						Version = y.Version,
						IsActivate = y.IsActivate,
					}).ToList(),
					ImageUrls = x.ImageUrls,
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
