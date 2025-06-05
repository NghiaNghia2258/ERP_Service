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
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

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

        if (!Directory.Exists(logFolder))
        {
            Console.WriteLine("No log folder found.");
            return;
        }

        var logFiles = Directory.GetFiles(logFolder, "*.log");
        if (logFiles.Length == 0)
        {
            Console.WriteLine("No log files to process.");
            return;
        }

        var events = new List<UserEvent>();

        foreach (var file in logFiles)
        {
            try
            {
                var lines = await File.ReadAllLinesAsync(file);
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
                        Console.WriteLine($"Error parsing line in file {file}: {ex.Message}");
                    }
                }

                // Delete file after processing
                File.Delete(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {file}: {ex.Message}");
            }
        }

        if (events.Count > 0)
        {
            using var scope = _serviceProvider.CreateScope();

            var userEventRepository = scope.ServiceProvider.GetRequiredService<IUserEventRepository>();
            await userEventRepository.AddRange(events);
            await userEventRepository.UpdateUserProductScoresAsync();
        }
    }
}
