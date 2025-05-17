using ERP_Service.Infrastructure;
using ERP_Service.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

            return Ok(new );
        }
    }
}
