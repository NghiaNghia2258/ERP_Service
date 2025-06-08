using ERP_Service.Application.Mapper.Model.Carts;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using ERP_Service.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController(
        AppDbContext _dbContext,
        IAuthoziService _authoziService
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Voucher voucher)
        {
            PayloadToken token = _authoziService.PayloadToken;
            voucher.StoreId = token.StoreId;

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
        [HttpGet("get-by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.VoucherCode == code);
            if (voucher == null)
                return NotFound(new ApiResult(false, "Voucher không tồn tại", 404));

            return Ok(new ApiSuccessResult<Voucher>(voucher));
        }
        [HttpGet("use-voucher")]
        public async Task<IActionResult> UseVoucher(string code, double discountAmount)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.VoucherCode == code);
            if (voucher == null)
                return NotFound(new ApiResult(false, "Voucher không tồn tại", 404));
            voucher.Use();
            PayloadToken token = _authoziService.PayloadToken;
            var cart = await _dbContext.Carts
                .FirstOrDefaultAsync(x => !x.HasOrder && x.CustomerId == token.CustomerId);

            List<VoucherCartDto> vouchers = JsonConvert.DeserializeObject<List<VoucherCartDto>>(cart.Vouchers ?? "[]");
            vouchers.Add(new VoucherCartDto
            {
                Code = code,
                Id = voucher.Id,
                Title = voucher.Title,
                ShopId = voucher.StoreId,
                DiscountPercent = voucher.DiscountPercent,
                DiscountValue = voucher.DiscountValue,
                MaxDiscountValue = voucher.MaxDiscountValue,
                DiscountAmount = discountAmount
            });
            cart.Vouchers = JsonHelper.ConvertToJsonString(vouchers);
            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<Voucher>(voucher));
        }
        [HttpGet("remove-voucher/{code}")]
        public async Task<IActionResult> RemoveVoucher(string code)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.VoucherCode == code);
            if (voucher == null)
                return NotFound(new ApiResult(false, "Voucher không tồn tại", 404));
            PayloadToken token = _authoziService.PayloadToken;
            var cart = await _dbContext.Carts
                .FirstOrDefaultAsync(x => !x.HasOrder && x.CustomerId == token.CustomerId);

            List<VoucherCartDto> vouchers = JsonConvert.DeserializeObject<List<VoucherCartDto>>(cart.Vouchers);
            VoucherCartDto voucherRemove = vouchers.FirstOrDefault(x => x.Code == code);
            vouchers.Remove(voucherRemove);

            cart.Vouchers = JsonHelper.ConvertToJsonString(vouchers);
            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<bool>(true));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Voucher updatedVoucher)
        {
            var existing = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
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

            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<string>("Cập nhật thành công"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
            if (voucher == null)
                return NotFound(new ApiResult(false, "Voucher không tồn tại", 404));

            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<string>("Xóa thành công"));
        }

    }
}
