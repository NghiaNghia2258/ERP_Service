using ERP_Service.Domain.Models;
using ERP_Service.Domain.PagingRequest;

namespace ERP_Service.Domain.Abstractions.Repository;

public interface ICustomerRepository
{
	Task<bool> Create(Customer model);
	Task<bool> Delete(Guid id);
	Task<IEnumerable<Customer>> GetAll(OptionFilterCustomer option);
	Task<Customer> GetById(Guid id);
	Task<bool> Update(Customer model);
}
