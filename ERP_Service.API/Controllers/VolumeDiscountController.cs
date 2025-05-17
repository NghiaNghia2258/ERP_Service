using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumeDiscountController(AppDbContext _dbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VolumeDiscount discount)
        {
            _dbContext.VolumeDiscounts.Add(discount);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiSuccessResult<bool>(true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _dbContext.VolumeDiscounts
                .Include(x => x.VolumeDiscountItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (discount == null)
                return NotFound(new ApiResult(false, "Không tìm thấy giảm giá theo số lượng", 404));

            return Ok(new ApiSuccessResult<VolumeDiscount>(discount));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VolumeDiscount updated)
        {
            var existing = await _dbContext.VolumeDiscounts
                .Include(x => x.VolumeDiscountItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
                return NotFound(new ApiResult(false, "Không tìm thấy giảm giá theo số lượng", 404));

            existing.MinQuantity = updated.MinQuantity;
            existing.DiscountValue = updated.DiscountValue;
            existing.IsPercentage = updated.IsPercentage;
            _dbContext.VolumeDiscountItems.RemoveRange(existing.VolumeDiscountItems);
            existing.VolumeDiscountItems = updated.VolumeDiscountItems;

            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<string>("Cập nhật thành công"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var discount = await _dbContext.VolumeDiscounts.FindAsync(id);
            if (discount == null)
                return NotFound(new ApiResult(false, "Không tìm thấy giảm giá theo số lượng", 404));

            _dbContext.VolumeDiscounts.Remove(discount);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiSuccessResult<string>("Xóa thành công"));
        }
    }
}
