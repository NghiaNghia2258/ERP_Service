using ERP_Service.Domain.Abstractions.Repository;
using ERP_Service.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Infrastructure.Repostiroty;

public class UserEventRepository : RepositoryBase<UserEvent, Guid>, IUserEventRepository
{
    public UserEventRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(dbContext, httpContextAccessor, config)
    {
    }

    public async Task AddRange(IEnumerable<UserEvent> events)
    {
        await _dbContext.AddRangeAsync(events);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateUserProductScoresAsync()
    {
        var scores = await _dbContext.UserEvents
            .Where(e => e.EventTime >= DateTime.UtcNow.AddDays(-30))
            .GroupBy(e => new { e.UserId, e.ProductId })
            .Select(g => new UserProductScore
            {
                UserId = g.Key.UserId,
                ProductId = g.Key.ProductId,
                Score = g.Sum(e => e.Weight)
            })
            .ToListAsync();

        _dbContext.UserProductScores.RemoveRange(_dbContext.UserProductScores);

        await _dbContext.UserProductScores.AddRangeAsync(scores);
        await _dbContext.SaveChangesAsync();
    }
}
