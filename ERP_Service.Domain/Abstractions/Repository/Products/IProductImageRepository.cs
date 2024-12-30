using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Abstractions.Repository.Products;

public interface IProductImageRepository
{
	Task<bool> Create(ProductImage model);
	Task<bool> Delete(int id);
}
