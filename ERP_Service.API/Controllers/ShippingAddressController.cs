using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models.Customers;
using ERP_Service.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController(
        AppDbContext _dbContext,
        IAuthoziService _authoziService
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShippingAddressDto dto)
        {
            var token = _authoziService.PayloadToken;

            var entity = new ShippingAddress
            {
                Id = dto.Id,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                Ward = dto.Ward,
                District = dto.District,
                Province = dto.Province,
                IsDefault = false,
                CustomerId = token.CustomerId,
            };

            _dbContext.ShippingAddresses.Add(entity);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiSuccessResult<string>("Thêm địa chỉ thành công"));
        }
        [HttpGet("my-addresses")]
        public async Task<IActionResult> GetMyAddresses()
        {
            var token = _authoziService.PayloadToken;

            var addresses = await _dbContext.ShippingAddresses
                .Where(x => x.CustomerId == token.CustomerId)
                .OrderByDescending(x => x.IsDefault)
                .ToListAsync();

            return Ok(new ApiSuccessResult<List<ShippingAddress>>(addresses));
        }
    }
}
public class CreateShippingAddressDto
{
    public string Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
}