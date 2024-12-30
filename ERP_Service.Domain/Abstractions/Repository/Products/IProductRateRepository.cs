using ERP_Service.Domain.Models.Products;

namespace ERP_Service.Domain.Abstractions.Repository.Products;

public interface IProductRateRepository
{
	Task<bool> Create(ProductRate model);
	Task<bool> Delete(int id);
	Task<bool> Update(ProductRate model);
}
