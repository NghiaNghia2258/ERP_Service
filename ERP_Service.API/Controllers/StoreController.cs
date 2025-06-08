using ERP_Service.Application.Mapper.Model.Stores;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Stores;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using ERP_Service.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERP_Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController(
     AppDbContext _dbContext,
     IAuthoziService _authoziService,
     IMailService _mailService
    ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        UserLogin newUser = new UserLogin()
        {
            Username = model.ContactEmail,
            Password = "",
            RoleGroupId = 3,
            Stores = new List<Store>() { new Store() { 
                Name = model.Name,
                Description = model.Description,
                Logo = model.Logo,
                Location = model.Location,
                ContactEmail = model.ContactEmail,
                ContactPhone = model.ContactPhone,
                Facebook = model.Facebook,
                Instagram = model.Instagram,
                Twitter = model.Twitter,
            } }
        };

        _dbContext.UserLogins.Add(newUser);
        await _dbContext.SaveChangesAsync();

        return Ok(new ApiSuccessResult());
    }
    [HttpGet("store-info")]
    public async Task<IActionResult> GetStoreById()
    {
        PayloadToken token = _authoziService.PayloadToken;
        var store = await _dbContext.Stores
            .Include(s => s.UserLogin) 
            .FirstOrDefaultAsync(s => s.Id == token.StoreId);

        if (store == null)
        {
            return NotFound(new ApiErrorResult());
        }

        return Ok(new ApiSuccessResult<StoreDto>(new StoreDto
        {
            Id = store.Id.ToString(),
            Name = store.Name,
            Description = store.Description,
            Logo = store.Logo,
            CoverImage = store.CoverImage,
            Location = store.Location,
            ContactPhone = store.ContactPhone,
            ContactEmail = store.ContactEmail,
            Facebook = store.Facebook,
            Instagram = store.Instagram,
            Twitter = store.Twitter,
            Policies = JsonConvert.DeserializeObject<List<StorePolicyDto>>(store.Policies),
            UserName = store.UserLogin.Username,
            Password = store.UserLogin.Password
        }));
    }
    [HttpGet("store-info-customer/{id}")]
    public async Task<IActionResult> GetStoreInfo(Guid id)
    {
        var store = await _dbContext.Stores
            .FirstOrDefaultAsync(s => s.Id == id);

        if (store == null)
        {
            return NotFound(new ApiErrorResult());
        }

        return Ok(new ApiSuccessResult<StoreDto>(new StoreDto
        {
            Id = store.Id.ToString(),
            Name = store.Name,
            Description = store.Description,
            Logo = store.Logo,
            CoverImage = store.CoverImage,
            Location = store.Location,
            Rating = 4.7,
            Followers = 23,
            ReviewCount = 12,
            IsFollow = true,
            Verified = store.Verified,
            ContactPhone = store.ContactPhone,
            ContactEmail = store.ContactEmail,
            Facebook = store.Facebook,
            Instagram = store.Instagram,
            Twitter = store.Twitter,
            Policies = JsonConvert.DeserializeObject<List<StorePolicyDto>>(store.Policies ??"[]"),
        }));
    }
    [HttpGet("store-info-product/{id}")]
    public async Task<IActionResult> GetStoreInfoByProductId(int id)
    {
        var data = await _dbContext.Products
            .Include(s => s.Store)
            .Where(s => s.Id == id)
            .Select(x => new StoreDto
            {
                Id = x.Store.Id.ToString(),
                Name = x.Store.Name,
                Description = x.Store.Description,
                Logo = x.Store.Logo,
                CoverImage = x.Store.CoverImage,
                Location = x.Store.Location,
                Rating = 4.7,
                Followers = 23,
                ReviewCount = 12,
                Verified = x.Store.Verified,
            }).FirstOrDefaultAsync();
            ;


        return Ok(new ApiSuccessResult<StoreDto>(data));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateStoreDto model)
    {
        if (!Guid.TryParse(model.Id, out Guid storeId))
        {
            return BadRequest(new ApiErrorResult());
        }

        var store = await _dbContext.Stores
            .Include(s => s.UserLogin)
            .FirstOrDefaultAsync(s => s.Id == storeId);

        if (store == null)
        {
            return NotFound(new ApiErrorResult());
        }

        store.Name = model.Name;
        store.Description = model.Description;
        store.Logo = model.Logo;
        store.CoverImage = model.CoverImage;
        store.Location = model.Location;
        store.ContactPhone = model.ContactPhone;
        store.ContactEmail = model.ContactEmail;
        store.Facebook = model.Facebook;
        store.Instagram = model.Instagram;
        store.Twitter = model.Twitter;
        store.Policies = JsonHelper.ConvertToJsonString(model.Policies);
        store.UserLogin.Password = model.Password;

        await _dbContext.SaveChangesAsync();

        return Ok(new ApiSuccessResult());
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll([FromQuery] OptionFilterStore request)
    {
        var query = _dbContext.Stores.AsQueryable();

        if (!string.IsNullOrEmpty(request.KeyWord))
        {
            query = query.Where(x =>
                x.Name.Contains(request.KeyWord) ||
                x.Location.Contains(request.KeyWord) ||
                x.ContactEmail.Contains(request.KeyWord) ||
                x.ContactPhone.Contains(request.KeyWord));
        }

        int totalRow = await query.CountAsync();

        var data = await query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new StoreDto   
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Location = x.Location,
                ContactPhone = x.ContactPhone,
                ContactEmail = x.ContactEmail,
                Verified = x.Verified ?? false,
            })
            .ToListAsync();

        return Ok(new ApiSuccessResult<List<StoreDto>>(data)
        {
            TotalRecordsCount = totalRow,
        });
    }
    [HttpGet("active-store/{id}")]
    public async Task<IActionResult> ActiveStore(Guid id)
    {
        var store = await _dbContext.Stores.Include(x => x.UserLogin).FirstOrDefaultAsync(x => x.Id == id);
        store.Verified = true;
        store.UserLogin.Password = GenerateRandomString(6);
        await _dbContext.SaveChangesAsync();

        _ = Task.Run(() =>
        {
            string body = $@"
                <h2>Xin chào {store.Name},</h2>
                <p>Cửa hàng của bạn đã được đăng ký thành công trên hệ thống.</p>
                <p><b>Thông tin đăng nhập:</b></p>
                <ul>
                    <li><b>Email:</b> {store.ContactEmail}</li>
                    <li><b>Mật khẩu:</b> {store.UserLogin.Password}</li>
                </ul>
                <p>Hãy đăng nhập và cập nhật thông tin cửa hàng của bạn nhé!</p>
                <hr />
                <p>Trân trọng,<br/>Liên hệ hỗ trợ: 0342534443</p>";
            _mailService.SendEmailAsync(store.ContactEmail, "Đăng ký thành công!", body);
        });

        return Ok(true);
    }
    private string GenerateRandomString(int length)
    {
        Random _random = new();
        string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        return new string(Enumerable.Repeat(_chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
public class OptionFilterStore
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 30;
    public string? KeyWord { get; set; }
}
public class RegisterDto
{
    public string? Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public string? Logo { get; set; } = default!;
    public string? Location { get; set; } = default!;
    public string? ContactPhone { get; set; } = default!;
    public string? ContactEmail { get; set; } = default!;
    public string? Facebook { get; set; } = default!;
    public string? Instagram { get; set; } = default!;
    public string? Twitter { get; set; } = default!;

}
