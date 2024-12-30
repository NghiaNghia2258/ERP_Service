using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Shared.Models;

namespace ERP_Service.Domain.Abstractions.Repository.Products;

public interface IProductRepository
{
	Task<bool> Create(Product model);
	Task<bool> Delete(int id);
	Task<IEnumerable<Product>> GetAll(OptionFilterProduct option);
	Task<Product> GetById(int id);
	Task<bool> Update(Product model);
}
