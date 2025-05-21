using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERP_Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(AppDbContext _dbContext, IAuthoziService _authoziService, ICacheService cacheService) : ControllerBase
{
    [HttpGet("get-best-seller")]
    public async Task<IActionResult> GetBestSeller()
    {
        var averageSellCount = await _dbContext.Products
            .AverageAsync(p => p.SellCount);

        var products = await _dbContext.Products
        .Include(p => p.ProductRates)
        .Include(p => p.Category)
        .Include(p => p.Brand)
        .Include(p => p.ProductVariants)
        .Select(p => new
        {
            id = p.Id,
            name = p.Name,
            images = JsonConvert.DeserializeObject<List<string>>(p.ImageUrls),
            price = p.ProductVariants.OrderBy(v => v.Price).FirstOrDefault()!.Price,
            rating = p.Rate,
            reviewCount = p.RateCount,
            inStock = p.TotalInventory > 0,
            isNew = p.CreatedAt >= DateTime.Now.AddDays(-30),
            isBestSeller = p.SellCount >= 1000,
            category = p.Category.Name,
            brand = p.Brand.Name,
            shortDescription = p.Description
        })
        .ToListAsync();

        return Ok("");
    }
    [HttpGet("get-recommend")]
    public async Task<IActionResult> GetRecommendedProductIds()
    {
        PayloadToken token = _authoziService.PayloadToken;
        IEnumerable<int> productIds = await GetProductIds(token.CustomerId);
        return Ok();
    }
    private async Task<IEnumerable<int>> GetProductIds(Guid customerId)
    {
        IEnumerable<int> productIds = await cacheService.GetAsync<List<int>>($"recomend_{customerId}") ?? new List<int>();
        if(productIds.Count() <= 0)
        {
            productIds = await GetProductIdsFromDB(customerId);
            await cacheService.SetAsync($"recomend_{customerId}", productIds, TimeSpan.FromHours(1));
        }
        return productIds;
    }

    private async Task<IEnumerable<int>> GetProductIdsFromDB(Guid customerId)
    {
        IEnumerable<int> productIds = Enumerable.Empty<int>();
        var userVectors = await _dbContext.UserProductScores
        .GroupBy(x => x.UserId)
        .ToDictionaryAsync(
            g => g.Key,
            g => g.ToDictionary(x => x.ProductId, x => x.Score)
        );

        if (!userVectors.ContainsKey(customerId))
            return new List<int>();

        var targetVector = userVectors[customerId];

        var similarities = new List<(Guid UserId, double Similarity)>();

        foreach (var kvp in userVectors)
        {
            var otherUserId = kvp.Key;
            if (otherUserId == customerId) continue;

            var otherVector = kvp.Value;
            double similarity = ComputeCosineSimilarity(targetVector, otherVector);

            if (similarity > 0)
                similarities.Add((otherUserId, similarity));
        }
        var topSimilarUsers = similarities
            .OrderByDescending(s => s.Similarity)
            .Take(3)
            .ToList();

        var recommendedScores = new Dictionary<int, double>();

        foreach (var (otherUserId, similarity) in topSimilarUsers)
        {
            var otherVector = userVectors[otherUserId];

            foreach (var kvp in otherVector)
            {
                int productId = kvp.Key;
                int score = kvp.Value;

                if (targetVector.ContainsKey(productId)) continue;

                if (!recommendedScores.ContainsKey(productId))
                    recommendedScores[productId] = 0;

                recommendedScores[productId] += similarity * score;
            }
        }

        productIds = recommendedScores
            .OrderByDescending(kvp => kvp.Value)
            .Take(10)
            .Select(kvp => kvp.Key)
            .ToList();

        return productIds;
    }
    private double ComputeCosineSimilarity(Dictionary<int, int> vec1, Dictionary<int, int> vec2)
    {
        var commonKeys = vec1.Keys.Intersect(vec2.Keys);

        double dot = commonKeys.Sum(k => vec1[k] * vec2[k]);
        double norm1 = Math.Sqrt(vec1.Values.Sum(v => v * v));
        double norm2 = Math.Sqrt(vec2.Values.Sum(v => v * v));

        if (norm1 == 0 || norm2 == 0) return 0;

        return dot / (norm1 * norm2);
    }
}
