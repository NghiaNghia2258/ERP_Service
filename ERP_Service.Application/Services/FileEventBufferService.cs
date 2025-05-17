using ERP_Service.Application.Services.Interfaces;

namespace ERP_Service.Application.Services;

public class FileEventBufferService : IEventBufferService
{
    private readonly string _folderPath;
    private readonly string _filePath;

    public FileEventBufferService()
    {
        _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "EventBuffers");
        Directory.CreateDirectory(_folderPath); 

        var fileName = $"events_buffer_{DateTime.UtcNow:yyyyMMdd}.log";
        _filePath = Path.Combine(_folderPath, fileName);
    }

    public async Task AppendEventAsync(string eventJson)
    {
        await File.AppendAllTextAsync(_filePath, eventJson + Environment.NewLine);
    }
}
