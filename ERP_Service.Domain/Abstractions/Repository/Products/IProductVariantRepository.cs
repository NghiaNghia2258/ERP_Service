using ERP_Service.Domain.Models.Products;
using ERP_Service.Shared.Models;

namespace ERP_Service.Domain.Abstractions.Repository.Products;

public interface IProductVariantRepository
{
	Task<bool> Create(ProductVariant model);
	Task<bool> Delete(int id);
	Task<ProductVariant> GetById(int id);
	Task<ProductVariant> GetByIdWithProduct(int id);
	Task<IEnumerable<ProductVariant>> GetProductVariantsByProductId(int productId);
	Task<bool> Update(ProductVariant model);
}

