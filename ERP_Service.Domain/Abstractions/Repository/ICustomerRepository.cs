using ERP_Service.Domain.Models;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Shared.Models;

namespace ERP_Service.Domain.Abstractions.Repository;

public interface ICustomerRepository
{
	Task<bool> Create(Customer model, PayloadToken payload);
	Task<bool> Delete(Guid id, PayloadToken payload);
	Task<IEnumerable<Customer>> GetAll(OptionFilterCustomer option);
	Task<Customer> GetById(Guid id);
	Task<bool> Update(Customer model, PayloadToken payload);
}
