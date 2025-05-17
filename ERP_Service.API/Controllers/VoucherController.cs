using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController(
        AppDbContext _dbContext
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Voucher voucher)
        {
            voucher.Id = Guid.NewGuid();
            voucher.CreatedAt = DateTime.Now;

            _dbContext.Vouchers.Add(voucher);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiSuccessResult<bool>(true));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
            if (voucher == null)
                return NotFound(new ApiResult(false, "Voucher không tồn tại", 404));

            return Ok(new ApiSuccessResult<Voucher>(voucher));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Voucher updatedVoucher)
        {
            var existing = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (existing == null)
                return NotFound(new ApiResult(false, "Voucher không tồn tại", 404));

            existing.VoucherCode = updatedVoucher.VoucherCode;
            existing.Title = updatedVoucher.Title;
            existing.Description = updatedVoucher.Description;
            existing.DiscountPercent = updatedVoucher.DiscountPercent;
            existing.DiscountValue = updatedVoucher.DiscountValue;
            existing.MaxDiscountValue = updatedVoucher.MaxDiscountValue;
            existing.MinOrderValue = updatedVoucher.MinOrderValue;
            existing.UsageLimit = updatedVoucher.UsageLimit;
            existing.StartDate = updatedVoucher.StartDate;
            existing.ExpirationDate = updatedVoucher.ExpirationDate;
            existing.UpdatedAt = DateTime.Now;
            existing.UpdatedBy = updatedVoucher.UpdatedBy;
            existing.UpdatedName = updatedVoucher.UpdatedName;

            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<string>("Cập nhật thành công"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (voucher == null)
                return NotFound(new ApiResult(false, "Voucher không tồn tại", 404));

            voucher.IsDeleted = true;
            voucher.DeletedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<string>("Xóa thành công"));
        }

    }
}
