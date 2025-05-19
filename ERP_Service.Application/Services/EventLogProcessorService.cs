using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace ERP_Service.Application.Services;


public class EventLogProcessorService(
    IServiceProvider _serviceProvider
    ) : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessEventLogs();

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task ProcessEventLogs()
    {
        string logFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "EventBuffers");
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
            using var scope = _serviceProvider.CreateScope();

            var userEventRepository = scope.ServiceProvider.GetRequiredService<IUserEventRepository>();
            await userEventRepository.AddRange(events);
            await userEventRepository.UpdateUserProductScoresAsync();
        }

        File.Delete(todayLogFile);
    }
}
