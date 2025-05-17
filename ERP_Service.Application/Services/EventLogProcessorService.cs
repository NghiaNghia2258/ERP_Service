using ERP_Service.Domain.Models;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace ERP_Service.Application.Services;


public class EventLogProcessorService : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromHours(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessEventLogs();

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private void ProcessEventLogs()
    {
        string logFolder = Path.Combine(AppContext.BaseDirectory, "Logs", "EventBuffers");
        string todayLogFile = Path.Combine(logFolder, $"events_buffer_{DateTime.UtcNow:yyyyMMdd}.log");

        if (!File.Exists(todayLogFile))
        {
            Console.WriteLine("No log file to process.");
            return;
        }

        var lines = File.ReadAllLines(todayLogFile);
        var events = new List<UserEvent>();

        foreach (var line in lines)
        {
            try
            {
                var userEvent = JsonSerializer.Deserialize<UserEvent>(line);
                if (userEvent != null)
                    events.Add(userEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing line: {ex.Message}");
            }
        }

        if (events.Count > 0)
        {
            SaveEventsToDatabase(events);
        }

        File.Delete(todayLogFile);
    }

    private void SaveEventsToDatabase(List<UserEvent> events)
    {
        // TODO: Viết logic lưu dữ liệu vào DB có sẵn của bạn
    }
}
