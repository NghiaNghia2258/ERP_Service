using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BundleDiscountController(AppDbContext _dbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BundleDiscount bundle)
        {
            _dbContext.BundleDiscounts.Add(bundle);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiSuccessResult<bool>(true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bundle = await _dbContext.BundleDiscounts
                .Include(x => x.BundleDiscountItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bundle == null)
                return NotFound(new ApiResult(false, "Không tìm thấy gói khuyến mãi", 404));

            return Ok(new ApiSuccessResult<BundleDiscount>(bundle));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BundleDiscount updated)
        {
            var existing = await _dbContext.BundleDiscounts
                .Include(x => x.BundleDiscountItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
                return NotFound(new ApiResult(false, "Không tìm thấy gói khuyến mãi", 404));

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.DiscountValue = updated.DiscountValue;
            existing.IsPercentage = updated.IsPercentage;
            existing.MaxUsageCount = updated.MaxUsageCount;
            existing.UsageCount = updated.UsageCount;
            existing.IsActive = updated.IsActive;
            existing.BundleDiscountItems = updated.BundleDiscountItems;

            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<string>("Cập nhật thành công"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bundle = await _dbContext.BundleDiscounts.FindAsync(id);
            if (bundle == null)
                return NotFound(new ApiResult(false, "Không tìm thấy gói khuyến mãi", 404));

            _dbContext.BundleDiscounts.Remove(bundle);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiSuccessResult<string>("Xóa thành công"));
        }
    }
}
