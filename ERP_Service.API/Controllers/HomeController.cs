using ERP_Service.Infrastructure;
using ERP_Service.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(AppDbContext _dbContext) : ControllerBase
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
                images = JsonSerializer.Deserialize<List<string>>(p.ImageUrls),
                price = p.ProductVariants.OrderBy(v => v.Price).FirstOrDefault()!.Price,
                rating = p.ProductRates.Count > 0 ? p.ProductRates.Average(r => r.Rating) : 0,
                reviewCount = p.ProductRates.Count,
                inStock = p.TotalInventory > 0,
                isNew = p.CreatedAt >= DateTime.Now.AddDays(-30),
                isBestSeller = p.SellCount >= 1000,
                category = p.Category.Name,
                brand = p.Brand.Name,
                shortDescription = p.Description
            })
            .ToListAsync();
        }
    }
}
