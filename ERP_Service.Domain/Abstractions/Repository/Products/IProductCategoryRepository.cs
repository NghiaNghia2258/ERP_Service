using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Shared.Models;

namespace ERP_Service.Domain.Abstractions.Repository.Products;

public interface IProductCategoryRepository
{
	Task<bool> Create(ProductCategory model);
	Task<bool> Delete(int id);
	Task<IEnumerable<ProductCategory>> GetAll(OptionFilterProductCategory option);
	Task<ProductCategory> GetById(int id);
	Task<bool> Update(ProductCategory model);
}
