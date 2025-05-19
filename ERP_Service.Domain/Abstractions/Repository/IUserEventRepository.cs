using ERP_Service.Domain.Models;

namespace ERP_Service.Domain.Abstractions.Repository;

public interface IUserEventRepository
{
    Task AddRange(IEnumerable<UserEvent> events);
    Task UpdateUserProductScoresAsync();
}
