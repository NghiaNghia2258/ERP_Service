
namespace ERP_Service.Application.Services.Interfaces;

public interface IEventBufferService
{
    Task AppendEventAsync(string eventJson);
}
